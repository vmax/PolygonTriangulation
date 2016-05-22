using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PolygonTriangulation
{
    class PolygonVertexManager
    {
        List<PolygonVertex> vertices;
        List<PolygonEdge> edges;
        List<PolygonEdge> addedDiagonals;
        PictureBox polygonBox;
        Graphics graphics;
        HashSet<PolygonEdge> T;

        Dictionary<PolygonEdge, PolygonVertex> helpers;

        public PolygonVertexManager(PictureBox polygonPictureBox)
        {
            vertices = new List<PolygonVertex>();
            edges = new List<PolygonEdge>();
            addedDiagonals = new List<PolygonEdge>();
            T = new HashSet<PolygonEdge>();
            polygonBox = polygonPictureBox;
            graphics = polygonBox.CreateGraphics();
        }

        public void addVertex(PolygonVertex pv)
        {
            if (vertices.Count > 2 && checkCrossing(pv))
            {
                MessageBox.Show("Добавленная вершина создаст в прямоугольнике самопересечение!", "Ошибка");
                return;
            };

            pv.index = vertices.Count;
            vertices.Add(pv);
            graphics.FillEllipse(Brushes.Black, pv.X -5 , (polygonBox.Height - pv.Y - 5), 10, 10);
            if (vertices.Count > 1)
            {
                PolygonVertex prevVertex = vertices[vertices.Count - 2];
                pv.neighboor1 = prevVertex;
                prevVertex.neighboor2 = pv;
                edges.Add(new PolygonEdge(prevVertex, pv));
                graphics.DrawLine(Pens.Black, pv.X, polygonBox.Height - pv.Y, prevVertex.X, polygonBox.Height - prevVertex.Y);
            }
        }

        #region checking for intersection
        // SRC: http://e-maxx.ru/algo/segments_intersection_checking
        private static void Swap (ref float a, ref float b)
        {
            float c = a;
            a = b;
            b = c;
        }
        private static float area(PolygonVertex a, PolygonVertex b, PolygonVertex c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }
        private static bool intersect_1(float a, float b, float c, float d)
        {
            if (a > b) Swap(ref a,ref b);
            if (c > d) Swap(ref c, ref d);
            return Math.Max(a, c) <= Math.Min(b, d);
        }
        private static bool intersect(PolygonVertex a, PolygonVertex b, PolygonVertex c, PolygonVertex d)
        {
            return intersect_1(a.X, b.X, c.X, d.X)
                && intersect_1(a.Y, b.Y, c.Y, d.Y)
                && area(a, b, c) * area(a, b, d) <= 0
                && area(c, d, a) * area(c, d, b) <= 0;
        }
        #endregion

        public void insertDiagonal(PolygonVertex a, PolygonVertex b, Pen pen = null)
        {
            if (pen == null)
            {
                pen = Pens.Red;
            }
            graphics.DrawLine(pen, a.X, polygonBox.Height - a.Y, b.X, polygonBox.Height - b.Y);
            addedDiagonals.Add(new PolygonEdge(a, b));
        }
        private void performVerticesClassification()          
        {
            foreach (PolygonVertex v in vertices)
            {
                v.classifySelf();
            }
            foreach (PolygonVertex v in vertices)
            {
                Brush b = Brushes.Black;
                switch(v.type)
                {
                    case PolygonVertexType.start:
                        b = Brushes.Yellow;
                        break;
                    case PolygonVertexType.split:
                        b = Brushes.Red;
                        break;
                    case PolygonVertexType.end:
                        b = Brushes.Green;
                        break;
                    case PolygonVertexType.merge:
                        b = Brushes.Blue;
                        break;
                    case PolygonVertexType.regular:
                        b = Brushes.Black;
                        break;
                }
                graphics.FillEllipse(b, v.X - 5 , polygonBox.Height - v.Y - 5, 10, 10);

            }
            // TODO: самопересечение которое образуется при звершении построения

        }

        public bool checkCrossing(PolygonVertex newVertex)
        {
            PolygonVertex lastPoint = vertices[vertices.Count - 1];
            for (int i = 0; i < vertices.Count - 2; i++)
            {
                if (intersect(vertices[i], vertices[i+1], newVertex, lastPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public void performTriangulation ()
        {
            performVerticesClassification();
            monotonePolygonPartition();
            var monotonePolygons = splitIntoMonotonous();

            var origDiag = addedDiagonals;

            foreach (var polygon in monotonePolygons.Where(polygon => polygon.Count > 3))
            {
                reorderPolygon(polygon);
                triangulateMonotonePolygon(polygon);
            }
        }

        private void fillPolygon(List<PolygonVertex> polygon, Brush b)
        {
            graphics.FillPolygon(b, polygon.Select(v => new Point(v.X, polygonBox.Height - v.Y)).ToArray(), System.Drawing.Drawing2D.FillMode.Alternate);
        }

        public void monotonePolygonPartition()
        {  
            // needed data structures
            //
        
            ConcurrentPriorityQueue.ConcurrentPriorityQueue<PolygonVertex, double> queue = constructVertexQueue();

            // helper for edge
            helpers = new Dictionary<PolygonEdge, PolygonVertex>();

            while(queue.Count > 0)
            {
                var vMax = queue.Dequeue();
                 switch(vMax.type)
                 {
                     case PolygonVertexType.start:
                         HandleStartVertex(vMax);
                         break;
                     case PolygonVertexType.end:
                         HandleEndVertex(vMax);
                         break;
                     case PolygonVertexType.split:
                         HandleSplitVertex(vMax);
                         break;
                     case PolygonVertexType.merge:
                         HandleMergeVertex(vMax);
                         break;
                     case PolygonVertexType.regular:
                         HandleRegularVertex(vMax);
                         break;
                 }

            }
        }

        /*
         * e_i => ребро которое начинается (edge1) в v_i и заканчивается (edge2) в v_{i+1}
         * e_{i-1} -> ребро которое начинается (edge1) в v_{i-1} и заканчивается(edge2) в v_i
         * e_i <==> e
         * e_{i-1} <==> d
         * helper(e_i) <==> eh
         * helper(e_{i-1} <==> dh
         */

        private PointF intersectionPoint(Tuple<int, int, int> line, int horizontalLine)
        {
            int a1, b1, c1, c2 = -horizontalLine;
            a1 = line.Item1;
            b1 = line.Item2;
            c1 = line.Item3;
            float x = -(c1 + horizontalLine * b1) / (a1);
            float y = horizontalLine;
            return new PointF(x, y);
        }
        private Dictionary<PolygonEdge, PointF> getIntersections (HashSet<PolygonEdge> where, PolygonVertex v)
        {
            var ints = new Dictionary<PolygonEdge, PointF>();
            foreach (PolygonEdge e in where)
            {
                var line = e.toLine();
                ints[e] = intersectionPoint(line, v.Y);
            }
            return ints;
        }

        #region Vertex Handlers
        private void HandleStartVertex(PolygonVertex v)
        {
            PolygonEdge e = edges.findEdgeStartingIn(v);
            T.Add(e);
            helpers[e] = v;
        }
        private void HandleEndVertex(PolygonVertex v)
        {
            PolygonEdge d = edges.findEdgeEndingIn(v);
            PolygonVertex dh = helpers[d];
            if (dh.type == PolygonVertexType.merge)
            {
                insertDiagonal(v, dh);
            }
            T.Remove(d);
        }
        private void HandleSplitVertex(PolygonVertex v)
        {
            // search in T to find the edge ej directly left of v
            Dictionary<PolygonEdge, PointF> interSections = getIntersections(T, v) ;
            // take intersections to the left of vertex & select the leftmost _edge_ from them
            PolygonEdge ej = interSections.Where(kvp => kvp.Value.X < v.X).OrderBy(kvp => kvp.Value.X).Last().Key;
            PolygonEdge e = edges.findEdgeStartingIn(v);
            insertDiagonal(v, helpers[ej]);
            helpers[ej] = v;
            T.Add(e);
            helpers[e] = v;

        }
        private void HandleMergeVertex(PolygonVertex v)
        {
            PolygonEdge d = edges.findEdgeEndingIn(v);
            PolygonVertex dh = helpers[d];
            if (dh.type == PolygonVertexType.merge)
            {
                insertDiagonal(v, dh);
            }
            T.Remove(d);
            // search in T to find the edge ej directly left of v
            Dictionary<PolygonEdge, PointF> interSections = getIntersections(T, v);
            // take intersections to the left of vertex & select the leftmost _edge_ from them
            PolygonEdge ej = interSections.Where(kvp => kvp.Value.X < v.X).OrderBy(kvp => kvp.Value.X).Last().Key;

            if (helpers[ej].type == PolygonVertexType.merge)
            {
                insertDiagonal(v, helpers[ej]);
            }
            helpers[ej] = v;
        }
        private void HandleRegularVertex(PolygonVertex v)
        {
            if (v.neighboor2.Y < v.Y)
            {
                // interior of P lies to the right of v (counterclockwise ordering)
                PolygonEdge d = edges.findEdgeEndingIn(v);
                PolygonEdge e = edges.findEdgeStartingIn(v);
                PolygonVertex dh = helpers[d];
                if (dh.type == PolygonVertexType.merge)
                {
                    insertDiagonal(dh, v);
                }
                T.Remove(d);

                T.Add(e);
                helpers[e] = v;
            }
            else
            {
                // interior of P lies to the left of v
                // search in T to find the edge ej directly left of v
                Dictionary<PolygonEdge, PointF> interSections = getIntersections(T, v);
                // take intersections to the left of vertex & select the leftmost _edge_ from them
                PolygonEdge ej = interSections.Where(kvp => kvp.Value.X < v.X).OrderBy(kvp => kvp.Value.X).Last().Key;
                if (helpers[ej].type == PolygonVertexType.merge)
                {
                    insertDiagonal(helpers[ej], v);
                }
                helpers[ej] = v;
            }
        }
        #endregion
    
        private ConcurrentPriorityQueue.ConcurrentPriorityQueue<PolygonVertex, double> constructVertexQueue ()
        {
            // FIXME: possibly equal Y coord then should add vertex with min X first
            ConcurrentPriorityQueue.ConcurrentPriorityQueue<PolygonVertex, double> queue = new ConcurrentPriorityQueue.ConcurrentPriorityQueue<PolygonVertex, double>();
            foreach (var v in vertices)
            {
                queue.Enqueue(v, v.Y);
            }
            return queue;
        }

        public void finishBuilding()
        {
            PolygonVertex first, last;
            first = vertices[0];
            last = vertices[vertices.Count - 1];
            graphics.DrawLine(Pens.Black, first.X, polygonBox.Height -  first.Y, last.X, polygonBox.Height - last.Y);
            last.neighboor2 = first;
            first.neighboor1 = last;

            edges.Add(new PolygonEdge(last, first));
            // FIXME: disable but don't clear drawed things
        }

        private int LeftTurnPredicate(PolygonVertex a, PolygonVertex b, PolygonVertex c)
        {
            return Math.Sign((c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X));
        }

        private bool orientation(PolygonVertex a, PolygonVertex b, PolygonVertex c)
        {
            int v = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
            return v > 0;
        }

        private void reorderPolygon(List<PolygonVertex> polygon)
        {
            for (int i = 0; i < polygon.Count - 1; i++)
            {
                polygon[i].index = i + 1;
                polygon[i+1].index = i + 2;
                polygon[i].neighboor2 = polygon[i + 1];
                polygon[i + 1].neighboor1 = polygon[i];
            }
            polygon[polygon.Count - 1].index = polygon.Count;
            polygon[polygon.Count - 1].neighboor2 = polygon[0];
            polygon[0].neighboor1 = polygon[polygon.Count - 1];
        }

        private bool sameChain(Dictionary<PolygonVertex, int> chains, PolygonVertex a, PolygonVertex b)
        {
            if (a.neighboor1 == b || a.neighboor2 == b)
            {
                return true;

            }
            return chains[a] == chains[b];
        }

        private void triangulateMonotonePolygon(List<PolygonVertex> polygon)
        {
            List<PolygonVertex> V = polygon.OrderByDescending(v => v.Y).ToList();
            Dictionary<PolygonVertex, int> chains = new Dictionary<PolygonVertex, int>();

            PolygonVertex chainStart = V[0], chainEnd = V.Last(), lc = chainStart.neighboor2, rc = chainStart.neighboor1;
            while(lc != chainEnd)
            {
                chains[lc] = -1; // left chain
                lc = lc.neighboor2;
                 
            }

            while(rc != chainEnd)
            {
                chains[rc] = 1; // right chain
                rc = rc.neighboor1;
            }

            Stack<PolygonVertex> S = new Stack<PolygonVertex>();

            S.Push(V[0]);
            S.Push(V[1]);

            for (int j = 2; j < V.Count - 1; j++)
            {   
                if (!sameChain(chains, V[j], S.Peek()))
                {
                    while (S.Count > 0)
                    {
                        if (S.Count != 1)
                        {
                            insertDiagonal(V[j], S.Peek(), new Pen(Color.SeaGreen, 1f));
                        }
                        S.Pop();
                        
                    }
                    S.Push(V[j - 1]);
                    S.Push(V[j]);
                }
                else
                {
                    PolygonVertex last = S.Pop();
                    while (S.Count > 0 && (
                        (orientation(S.Peek(), last, V[j]) && chains[last] == -1) || (!orientation(S.Peek(), last, V[j]) && chains[last] == 1)))
                    {
                        last = S.Pop();
                        insertDiagonal(V[j], last, new Pen(Color.Yellow, 1f));
                    }
                    S.Push(last);
                    S.Push(V[j]);
                }
            }
            S.Pop(); // except the first
            while (S.Count > 0)
            {
                if (S.Count != 1)
                {
                    insertDiagonal(V[V.Count - 1], S.Peek(), Pens.Violet); // diag from u_j to all vert on the stack
                }
                S.Pop(); // and the last one
            }
        }

        private List<List<PolygonVertex>> splitIntoMonotonous()
        {
            List<List<PolygonVertex>> monotonePolys = new List<List<PolygonVertex>>();

            
            addedDiagonals = addedDiagonals.OrderBy(e =>
            {
                var i = e.edge1.index;
                var j = e.edge2.index;
                return Math.Abs(i-j);
            }).ToList();

            foreach (PolygonEdge diag in addedDiagonals)
            {
                monotonePolys.Add(findMonotones(diag));
            }

            monotonePolys.Add(vertices);
            return monotonePolys;
        }
        private List<PolygonVertex> findMonotones(PolygonEdge diagonal)
        {
            PolygonVertex start, end;
            List<PolygonVertex> vert = new List<PolygonVertex>();
            if (diagonal.edge1.index < diagonal.edge2.index)
            {
                start = diagonal.edge1;
                end = diagonal.edge2;
            }
            else
            {
                start = diagonal.edge2;
                end = diagonal.edge1;
            }

            PolygonVertex fixedStart = vertices.Find(v => v == start);
            PolygonVertex fixedEnd = vertices.Find(v => v == end);
            while (start != end)
            {
                vert.Add(start);
                start = start.neighboor2;
            }
            vert.Add(end);

            fixedStart.neighboor2 = fixedEnd;
            fixedEnd.neighboor1 = fixedStart;

            vertices = vertices.Except(vert.Except(new[] { diagonal.edge1, diagonal.edge2 })).ToList();
            return vert;

        }



        /*
            private void triangulateMonotonePolygon()
            {
                List<PolygonVertex> V = vertices.OrderByDescending(v => v.Y).ToList();
                Stack<PolygonVertex> S = new Stack<PolygonVertex>();

                S.Push(V[0]);
                S.Push(V[1]);

                for (int i = 3; i < V.Count; i++)
                {
                    PolygonVertex v = V[i];
                    while (S.Count > 0)
                    {
                        PolygonVertex vv = S.Peek();
                        insertDiagonal(vv, v, Pens.Red);
                        S.Pop();
                    }
                    S.Push(v);
                }
            }*/
    }
}
using System;
using System.Collections.Generic;

namespace AStar
{
    public class Point : IEquatable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator+ (Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public bool Equals(Point other)
        {
            return (other != null) && (x == other.x) && (y == other.y);
        }

        public override String ToString()
        {
            return x + "," + y;
        }
    }

    class PointPair : IEquatable<PointPair>
    {
        public Point a;
        public Point b;

        public PointPair(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }

        public PointPair(int ax, int ay, int bx, int by)
        {
            a = new Point(ax, ay);
            b = new Point(bx, by);
        }

        public bool Equals(PointPair other)
        {
            return (other != null) && (a.Equals(other.a)) && (b.Equals(other.b));
        }

        public override String ToString()
        {
            return a + " -> " + b;
        }
    }

    interface IMap
    {
        List<Point> GetNeighboors(int x, int y);
    }

    class Node
    {
        public int x;
        public int y;
        public int cost;
        public Node parent;

        public Node(Point p)
        {
            x = p.x;
            y = p.y;
            cost = 0;
            parent = null;
        }

        public bool Equals(Node other)
        {
            return (x == other.x) && (y == other.y);
        }

        public override String ToString()
        {
            return x + "; " + y;
        }
    }

    class Heap
    {
        private List<Node> storage;

        public Heap()
        {
            storage = new List<Node>();
        }

        public void Push(Node p)
        {
            storage.Add(p);
            storage.Sort((a, b) => a.cost.CompareTo(b.cost));
        }

        public Node Pop()
        {
            Node p = storage[0];
            storage.Remove(p);
            return p;
        }

        public bool NotEmpty()
        {
            return (storage.Count > 0);
        }

        public Node At(int x, int y)
        {
            return storage.Find(n => n.x == x && n.y == y);
        }

        public void Remove(Node n)
        {
            storage.Remove(n);
        }

        public void Clear()
        {
            storage.Clear();
        }
    }

    class Solver
    {
        private Heap openList;
        private Heap closedList;

        public List<Point> Path;

        public Solver()
        {
            openList = new Heap();
            closedList = new Heap();
            Path = new List<Point>();
        }

        public bool PathExists(IMap map, Point a, Point b)
        {
            openList.Clear();
            closedList.Clear();
            Path.Clear();
            Node startNode = new Node(a);
            Node endNode = new Node(b);

            openList.Push(startNode);

            while (openList.NotEmpty())
            {
                Node currentNode = openList.Pop();

                if (currentNode.Equals(endNode))
                {
                    MakePath(currentNode);
                    return true;
                }
                    
                List<Point> neighboors = map.GetNeighboors(currentNode.x, currentNode.y);
                ProcessList(currentNode, neighboors);

                closedList.Push(currentNode);
            }

            return false;
        }

        public bool PathExists(IMap map, Point a, int targetY)
        {
            openList.Clear();
            closedList.Clear();
            Path.Clear();
            Node startNode = new Node(a);

            openList.Push(startNode);

            while (openList.NotEmpty())
            {
                Node currentNode = openList.Pop();

                if (currentNode.y == targetY)
                {
                    MakePath(currentNode);
                    return true;
                }

                List<Point> neighboors = map.GetNeighboors(currentNode.x, currentNode.y);
                ProcessList(currentNode, neighboors);

                closedList.Push(currentNode);
            }

            return false;
        }

        private void ProcessList(Node current, List<Point> neighboors)
        {
            foreach (Point neighboor in neighboors)
            {
                Node neighboorNode = new Node(neighboor)
                {
                    cost = current.cost + 1,
                    parent = current
                };

                Node openNode = openList.At(neighboor.x, neighboor.y);
                if (openNode != null && openNode.cost < neighboorNode.cost)
                    continue;

                Node closedNode = closedList.At(neighboor.x, neighboor.y);
                if (closedNode != null && closedNode.cost < neighboorNode.cost)
                    continue;

                openList.Remove(openNode);
                openList.Push(neighboorNode);

                closedList.Remove(closedNode);               
            }
        }

        public void MakePath(Node lastNode)
        {
            Point end = new Point(lastNode.x, lastNode.y);
            Path.Add(end);

            while (lastNode.parent != null)
            {
                lastNode = lastNode.parent;
                Point p = new Point(lastNode.x, lastNode.y);
                Path.Add(p);
            }

            Path.Reverse();
        }
    }
}

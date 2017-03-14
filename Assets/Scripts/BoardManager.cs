using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using AStar;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public GameObject horizontalWall;
    public GameObject verticalWall;
    public GameObject[] FloorTiles;
    public GameObject[] BorderTiles;
    public int XSize = 9;
    public int YSize = 9;
    private Map map;
    private Solver solver;
    private Point player1;
    private Point player2;

    public void SetupScene()
    {
        map = new Map(XSize, YSize);
        solver = new Solver();
        player1 = new Point(0, 0);
        player2 = new Point(0, 0);

        for (int x = -1; x <= XSize; x++)
        {
            for (int y = -1; y <= YSize; y++)
            {
                GameObject tileChoice;

                if (x == -1 || x == XSize || y == -1 || y == YSize)
                    tileChoice = BorderTiles[Random.Range(0, BorderTiles.Length)];
                else
                    tileChoice = FloorTiles[Random.Range(0, FloorTiles.Length)];
                    
                Instantiate(tileChoice, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

        SetupWalls();
    }

    private void SetupWalls()
    {
        for (var x = 0; x <= XSize - 1; x++)
        {
            for (var y = 0.5; y <= YSize - 1; y++)
            {
                var wall = Instantiate(horizontalWall, new Vector3(x, (float)y, 0), Quaternion.identity);
                wall.GetComponent<WallController>().WallType = WallType.Horizontal;               
            }
        }

        for (var x = 0.5; x <= XSize - 1; x++)
        {
            for (var y = 0; y <= YSize - 1; y++)
            {
                var wall = Instantiate(verticalWall, new Vector3((float)x, y, 0), Quaternion.identity);
                wall.GetComponent<WallController>().WallType = WallType.Vertical;
            }
        }
    }

    public void AddWall(bool vertical, float x, float y)
    {
        if (vertical)
        {
            int x1 = (int)Math.Round(x - 0.5);
            int x2 = (int)Math.Round(x + 0.5);
            map.AddWall(x1, (int)y, x2, (int)y);
            map.AddWall(x2, (int)y, x1, (int)y);
        }
        else
        {
            int y1 = (int)Math.Round(y - 0.5);
            int y2 = (int)Math.Round(y + 0.5);
            map.AddWall((int)x, y1, (int)x, y2);
            map.AddWall((int)x, y2, (int)x, y1);
        }
    }

    public void UpdatePlayer(int id, float x, float y)
    {
        switch (id)
        {
            case 1: player1.x = (int)x;
                player1.y = (int)y;
                break;
            case 2:
                player2.x = (int)x;
                player2.y = (int)y;
                break;
        }
    }

    public bool CheckNewWall(bool vertical, float x1, float y1, float x2, float y2)
    {
        map.Backup();
        AddWall(vertical, x1, y1);
        AddWall(vertical, x2, y2);

        bool ok = solver.PathExists(map, player1, 0);
        ok &= solver.PathExists(map, player1, YSize - 1);
        ok &= solver.PathExists(map, player2, 0);
        ok &= solver.PathExists(map, player2, YSize - 1);

        map.Restore();
        return ok;
    }
}

public class Map : IMap
{
    Point min;
    Point max;
    List<PointPair> walls;
    List<PointPair> wallsCopy;
    List<Point> directions;

    public Map(int xSize, int ySize)
    {
        min = new Point(0, 0);
        max = new Point(xSize, ySize);
        walls = new List<PointPair>();
        directions = new List<Point> { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };
    }

    public void AddWall(int fromX, int fromY, int toX, int toY)
    {
        walls.Add(new PointPair(fromX, fromY, toX, toY));
    }

    public void Backup()
    {
        wallsCopy = walls.ToList();
    }

    public void Restore()
    {
        walls = wallsCopy.ToList();
    }

    public List<Point> GetNeighboors(int x, int y)
    {
        Point current = new Point(x, y);

        var list = new List<Point>();

        foreach (var dir in directions)
        {
            var next = current + dir;
            var movement = new PointPair(current, next);

            bool blocked = walls.Contains(movement);
            blocked |= next.x < min.x;
            blocked |= next.x >= max.x;
            blocked |= next.y < min.y;
            blocked |= next.y >= max.y;

            if (!blocked)
                list.Add(current + dir);
        }

        return list;
    }
}
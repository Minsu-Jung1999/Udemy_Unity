using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }

    [SerializeField] Vector2Int destinateCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinateCoordinates; } }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridmanager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridmanager = FindObjectOfType<GridManager>();
        if(gridmanager!= null)
        {
            grid = gridmanager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinateCoordinates];
        }

    }

    private void Start()
    {

        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(StartCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridmanager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    private void ExploerNeighbors()
    {
        List<Node> neightbors = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neightborCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neightborCoords))
            {
                neightbors.Add(grid[neightborCoords]);
            }
        }

        foreach(Node neighbor in neightbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isrunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isrunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploerNeighbors();
            if(currentSearchNode.coordinates == destinateCoordinates)
            {
                isrunning = false; 
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo !=null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count<=1)
            {
                GetNewPath();
                return true;
            }

        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("Recalculatepath",false,SendMessageOptions.DontRequireReceiver);
    }

}

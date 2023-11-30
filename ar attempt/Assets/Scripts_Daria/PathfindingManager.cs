using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    //things to test: canceling path, normal path, path recalculation when wrong place reached, path recalculation when end changed, creating a ne wpath

    Node[] nodes;
    [SerializeField] private Node nodeStart;
    [SerializeField] private Node nodeEnd;

    public Node currentNode;
    private List<Node> currentPath = new List<Node>();
    enum PathState {idle, pathFound, pathEndReached };
    PathState state = PathState.idle;
    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        if (nodeStart != null && nodeEnd != null)
        {
            Generate(nodeStart, nodeEnd);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // after path selected
        if (Input.GetKeyDown(KeyCode.N))
        {
            GoThroughQueue();
        }

        if(state == PathState.pathEndReached)
        {
            //
            currentPath = null;
            state = PathState.idle;
        }
    }

    //button click - > new pTo - > create path - > go trhourgh the path
    public void NewPathRequested(Node pTo) // from ui when loation pressed
    {
        nodeEnd = pTo;
        if (currentNode!= null)
        {
            nodeStart = currentNode;
        }
        if (nodeStart != null && nodeEnd != null)
        { 
            currentPath = Generate(nodeStart, nodeEnd);
            nodeIndex = 0;
        }
        else
        {
            Debug.Log("currentNode: " + currentNode);
            Debug.Log("nodestart: "+ nodeStart);
            Debug.Log("nodeEnd: " + nodeEnd);

            Debug.Log("node start or node end missing ");
        }

    }



    Node nextNode;
    int nodeIndex = 0;

    public void NodeReached(Node activeNode) //called in image tracking when a node
    {
        currentNode = activeNode;
        if (activeNode == nextNode)
        {
            
            //go next node
            GoThroughQueue(); 
        }
        else
        {
            //recalculate path
            nodeStart = activeNode;
            Generate(nodeStart, nodeEnd);
        }
    }

    public void PathCanceled()
    {
        currentPath = null;

        state = PathState.idle;
    }

    void GoThroughQueue()
    {
        if (nodeIndex < path.Count)
        {
            nodeIndex++;
            nextNode = path[nodeIndex];
        }
        else
        {
            //path finisehd
            state = PathState.pathEndReached;
            currentPath = null;
            Debug.Log("path end reached");
            // something happen if pathe end here
            state = PathState.idle;
       
        }
        Debug.Log("nextNode: " + nextNode.name);
    }

    List<Node> visited = new List<Node>();
    List<Node> queue = new List<Node>();
    List<Node> path = new List<Node>();



     List<Node> Generate(Node pFrom, Node pTo)
    {
        visited = new List<Node>();
        queue = new List<Node>();
        path = new List<Node>();

        FindPath(pFrom, pTo);
        path.Reverse();
        PrintList(path);
        return path;

    }

    private void FindPath(Node pFrom, Node pTo)
    {
        queue.Add(pFrom);
       // Console.WriteLine("q add: " + pFrom);
        visited.Add(pFrom);

        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i] == pTo)
                break;

            FindNextNode(queue[i], pTo);

        }
        // Reconstruct the path

        Node currentNode = pTo;
        while (currentNode != pFrom)
        {
            path.Add(currentNode);
            currentNode = currentNode.Previous;

        }
        path.Add(pFrom);

    }
    private void FindNextNode(Node pFrom, Node pTo)
    {
        for (int i = 0; i < pFrom.GetConnections().Count; i++)
        {
                if (CheckTakenNodes(pFrom.GetConnections()[i]))
                {
                    queue.Add(pFrom.GetConnections()[i]);
                    visited.Add(pFrom);
                    pFrom.GetConnections()[i].Previous = pFrom;

                }



        }
    }

    private bool CheckTakenNodes(Node nodeToCheck)
    {
        for (int i = 0; i < visited.Count; i++)
        {
            if (nodeToCheck == visited[i])
            {
                return false;
            }
        }
        return true;
    }


    private void PrintList(List<Node> list)
    {
        foreach (Node node in list) { Debug.Log(node + ", "); }
        Debug.Log("/");
    }
}

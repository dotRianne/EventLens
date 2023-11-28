using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    Node[] nodes;
    public Node nodeStart;
    public Node nodeEnd;

    public Node currentNode;
    private List<Node> currentPath = new List<Node>();
    enum pathState {idle, pathFound, pathEndReached };
    pathState state = pathState.idle;
    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        generate(nodeStart, nodeEnd);
    }

    // Update is called once per frame
    void Update()
    {
        // after path selected
    }

    //button click - > new pTo - > create path - > go trhourgh the path
    public void newPathRequested(Node pTo) // from ui when 
    {
        nodeEnd = pTo;
        nodeStart = currentNode;
        currentPath= generate(nodeStart, nodeEnd);
        
        //queue
    }

    List<Node> visited = new List<Node>();
    List<Node> queue = new List<Node>();
    List<Node> path = new List<Node>();



     List<Node> generate(Node pFrom, Node pTo)
    {
        visited = new List<Node>();
        queue = new List<Node>();
        path = new List<Node>();

        findPath(pFrom, pTo);
        path.Reverse();
        printList(path);
        return path;

    }

    private void findPath(Node pFrom, Node pTo)
    {
        queue.Add(pFrom);
       // Console.WriteLine("q add: " + pFrom);
        visited.Add(pFrom);

        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i] == pTo)
                break;

            findNextNode(queue[i], pTo);

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
    private void findNextNode(Node pFrom, Node pTo)
    {
        for (int i = 0; i < pFrom.GetConnections().Count; i++)
        {
                if (checkTakenNodes(pFrom.GetConnections()[i]))
                {
                    queue.Add(pFrom.GetConnections()[i]);
                    visited.Add(pFrom);
                    pFrom.GetConnections()[i].Previous = pFrom;

                }



        }
    }

    private bool checkTakenNodes(Node nodeToCheck)
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


    private void printList(List<Node> list)
    {
        foreach (Node node in list) { Debug.Log(node + ", "); }
        Debug.Log("/");
    }
}

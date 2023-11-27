using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    Node[] nodes;
    public Node nodeStart;
    public Node nodeEnd;

    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        generate(nodeStart, nodeEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
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


            /* 
             printList(visited);
             Console.WriteLine("queue: ");
             printList(queue);
             Console.WriteLine("path: ");
             printList(path);*/
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
    private bool pathFound = false;
    private void findNextNode(Node pFrom, Node pTo)
    {
        for (int i = 0; i < pFrom.GetConnections().Length; i++)
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

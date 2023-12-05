using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PathfindingManager : MonoBehaviour
{
    //things to test: canceling path, path recalculation when wrong place reached, path recalculation when end changed

    public DrawMapConnections mapConnections;

    Node[] nodes;
    [SerializeField]
    Node nodeStart;
    [SerializeField]
    Node nodeEnd;
 
    [SerializeField] 
    Node currentNode;

    [SerializeField]
    GameObject digitalPlayer;
    private List<Node> currentPath = new List<Node>();
    enum pathState {idle, pathEndReached, goingThroughPath };
    pathState state = pathState.idle;

    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        if (nodeStart != null && nodeEnd != null)
        {
            //generate(nodeStart, nodeEnd);
        }
    }

    float coolDownNewNode = 3;
    float timerStart = 0;
    bool cooldown = false;
    bool doOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (state == pathState.goingThroughPath && currentPath.Count-1>nodeIndex)
        {
            Debug.Log(currentPath[nodeIndex+1]);
            digitalPlayer.transform.LookAt(currentPath[nodeIndex+1].gameObject.transform.position);
           // digitalPlayer.transform.LookAt(guideLookAt.gameObject.transform.position);
        }
        // after path selected
        /* if (Input.GetMouseButtonDown(0))
         {
             Debug.Log(" press");
             goThroughQueue();
         }*/
        if ( cooldown && Time.time - timerStart > coolDownNewNode)
        {
           // timerStart = Time.time;
            cooldown = false;
        }

        if (nodeStart == nodeEnd && !doOnce)
        {
            //path finisehd
            state = pathState.pathEndReached;
            currentPath = null;
            nextNode = null;
            Debug.Log("path end reached");
            // something happen if pathe end here
            state = pathState.idle;
            doOnce = true;

        }
    }

    //button click - > new pTo - > create path - > go throurgh the path
    public void newPathRequested(Node pTo) // from ui when location is pressed
    {
        nodeStart = currentNode;
        nodeEnd = pTo;
        

        if (nodeStart != null && nodeEnd != null)
        { 
            currentPath = generate(nodeStart, nodeEnd);
            state = pathState.goingThroughPath;
            nodeIndex = 0;
            mapConnections.AllNodes();
        }
        else
        {
            Debug.Log("nodestart: "+ nodeStart);
            Debug.Log("nodeEnd: " + nodeEnd);

            Debug.Log("node start or node end missing ");
        }

    }



    Node nextNode;
    int nodeIndex = 0;

    public void nodeReached(Node activeNode) //called in image tracking when a node 
    {
       if (currentNode != activeNode && !cooldown)
       {
            doOnce = false;
            cooldown = true;
            timerStart = Time.time;
            currentNode = activeNode;
            digitalPlayer.transform.position = currentNode.transform.position;
            if (state == pathState.goingThroughPath)
            {
                if (activeNode == nextNode)
                {

                    //go next node
                    goThroughQueue();
                }
                else
                {
                    //recalculate path
                    nodeStart = activeNode;
                   currentPath= generate(nodeStart, nodeEnd);
                }
            }
        }
    }

    public void PathCanceled()
    {
        currentPath = null;

        state = pathState.idle;
    }

    void goThroughQueue()
    {
        if (nodeIndex < path.Count-1)
        {
            nodeIndex++;
            nextNode = path[nodeIndex];
            //digitalPlayer.transform.LookAt(nextNode.transform.position);
        }
        if (nodeStart.name == nodeEnd.name)
        {
            //path finisehd
            state = pathState.pathEndReached;
            currentPath = null;
            nextNode = null;
            Debug.Log("path end reached");
            // something happen if pathe end here
            state = pathState.idle;

        }
        Debug.Log("nextNode: " + nextNode.name);
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
        visited.Add(pFrom);

        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i] == pTo)
                break;

            findNextNode(queue[i], pTo);

        }
        // Reconstruct the path

        Node currentNodePathFinding = pTo;
        while (currentNodePathFinding != pFrom)
        {
            path.Add(currentNodePathFinding);
            currentNodePathFinding = currentNodePathFinding.GetPrevious();

        }
        path.Add(pFrom);

    }
    private void findNextNode(Node pFrom, Node pTo)
    {
        for (int i = 0; i < pFrom.GetConnections().Count; i++)
        {
               // if (checkTakenNodes(pFrom.GetConnections()[i]))
               // {
                    queue.Add(pFrom.GetConnections()[i]);
                    visited.Add(pFrom);
                    pFrom.GetConnections()[i].SetPrevious(pFrom);

               // }



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
        foreach (Node node in list) { Debug.Log("thePath: "+ node + ", "); }
        Debug.Log("/");
    }

    public List<Node> GetPath()
    {
        return currentPath;
    }
}

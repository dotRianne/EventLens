using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    //things to test: canceling path, path recalculation when wrong place reached, path recalculation when end changed

    Node[] nodes;
    //[SerializeField]
    public Node nodeStart;
    public Node nodeEnd;

  //  public Node currentNode;
   // private List<Node> currentPath = new List<Node>();
   // enum pathState {idle, pathFound, pathEndReached };
   /// pathState state = pathState.idle;
    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
      //  if (nodeStart != null && nodeEnd != null)
       // {
           generate(nodeStart, nodeEnd);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // after path selected
       /* if (Input.GetKeyDown(KeyCode.N))
        {
            goThroughQueue();
        }

        if(state == pathState.pathEndReached)
        {
            //
            currentPath = null;
            state = pathState.idle;
        }*/
    }

    //button click - > new pTo - > create path - > go trhourgh the path
   /* public void newPathRequested(Node pTo) // from ui when loation pressed
    {
        Debug.Log("new nodeEnd: " + pTo);
        nodeEnd = pTo;
      //  if (currentNode!= null)
     //   {
           // nodeStart = currentNode;
       // }
        if (nodeStart != null && nodeEnd != null)
        { 
            currentPath = generate(nodeStart, nodeEnd);
            nodeIndex = 0;
        }
        else
        {
          // Debug.Log("currentNode: " + currentNode);
            Debug.Log("nodestart: "+ nodeStart);
            Debug.Log("nodeEnd: " + nodeEnd);

            Debug.Log("node start or node end missing ");
        }

    }*/



    Node nextNode;
    int nodeIndex = 0;

 /*   public void nodeReached(Node activeNode) //called in image tracking when a node
    {
        //currentNode = activeNode;
        if (activeNode == nextNode)
        {
            
            //go next node
            goThroughQueue(); 
        }
        else
        {
            //recalculate path
            nodeStart = activeNode;
            generate(nodeStart, nodeEnd);
        }
    }

  /*  public void PathCanceled()
    {
        currentPath = null;

        state = pathState.idle;
    }*/

 /*   void goThroughQueue()
    {
        if (nodeIndex < path.Count-1)
        {
            nodeIndex++;
            nextNode = path[nodeIndex];
        }
        else
        {
            //path finisehd
            state = pathState.pathEndReached;
            currentPath = null;
            Debug.Log("path end reached");
            // something happen if pathe end here
            state = pathState.idle;
       
        }
        Debug.Log("nextNode: " + nextNode.name);
    }
 */
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

        Node currentNode = pTo;
        while (currentNode != pFrom)
        {
            path.Add(currentNode);
           // currentNode = currentNode.Previous;
            currentNode = currentNode.GetPrevious();

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
                // pFrom.GetConnections()[i].Previous = pFrom;
                pFrom.GetConnections()[i].SetPrevious(pFrom);

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
        foreach (Node node in list) { Debug.Log("thePath: "+ node + ", "); }
        Debug.Log("/");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPathFinding : MonoBehaviour
{
    Node[] nodes;
    public Node nodeStart;
    public Node nodeEnd;

    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<Node>();
        List<Node> startPath = 
        generate(nodeStart, nodeEnd);

        printList(startPath);
    }

    // Update is called once per frame
    void Update()
    {

    }

   public List<Node> visited = new List<Node>();
   public List<Node> queue = new List<Node>();
    List<Node> path = new List<Node>();



    List<Node> generate(Node pFrom, Node pTo)
    {
        visited = new List<Node>();
        queue = new List<Node>();
        path = new List<Node>();

        findPath(pFrom, pTo);
        path.Reverse();
       // printList(path);
        return path;

    }

    private void findPath(Node pFrom, Node pTo)
    {
        queue.Add(pFrom);
        // Console.WriteLine("q add: " + pFrom);
       // visited.Add(pFrom);
       
        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i] == pTo||pathFound)
                break;

            findNextNode(queue[i], pTo);


        }
        // Reconstruct the path

        Node currentNode = pTo;
        while (currentNode != pFrom)
        {
            path.Add(currentNode);
            ///currentNode = currentNode.Previous;
            currentNode = currentNode.GetPrevious();

        }
        path.Add(pFrom);

    }
    bool pathFound=false;
    private void findNextNode(Node pFrom, Node pTo)
    {
        List<Node> connectionpFrom = pFrom.GetConnections();
        /* for (int i = 0; i < pFrom.GetConnections().Count; i++)
         {
             if (checkTakenNodes(pFrom.GetConnections()[i]))
             {
                 queue.Add(pFrom.GetConnections()[i]);
                 visited.Add(pFrom);
                 pFrom.GetConnections()[i].Previous = pFrom;

             }



         }*/
        Debug.Log("connections not taken of: " + pFrom);
        for (int i = 0; i < connectionpFrom.Count; i++)
        {
            if (checkTakenNodes(connectionpFrom[i]))
            {
                queue.Add(connectionpFrom[i]);
                //visited.Add(pFrom);
                ///Debug.Log("new visit: "+pFrom);
              //  connectionpFrom[i].Previous = pFrom;
                connectionpFrom[i].SetPrevious(pFrom);
                Debug.Log("con: "+connectionpFrom[i]);
                if (connectionpFrom[i] == pTo)
                {
                    pathFound = true;
                }
            }



        }

        
        visited.Add(pFrom);
    }

    private bool checkTakenNodes(Node nodeToCheck)
    {
        for (int i = 0; i < visited.Count; i++)
        {
            if (nodeToCheck.name == visited[i].name)
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

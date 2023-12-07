using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [SerializeField] private GameObject spaceInfoBoard;
    [SerializeField]
    GameObject pathEndUI;
    [SerializeField]
    TextMeshProUGUI debugtext;
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
        if (currentNode!=null)
        {
           // debugtext.text = currentNode.name;
        }
        //Debug.Log("nodeStart is: " + nodeStart);
        if (state == pathState.goingThroughPath && currentPath.Count-1>nodeIndex)
        {
           // Debug.Log(currentPath[nodeIndex+1]);
            digitalPlayer.transform.LookAt(currentPath[nodeIndex+1].gameObject.transform.position);
           // digitalPlayer.transform.LookAt(guideLookAt.gameObject.transform.position);
        }

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
            pathEndUI.SetActive(true);
           // mapConnections.Des
            // something happen if pathe end here
            state = pathState.idle;
            doOnce = true;

        }
    }

    //button click - > new pTo - > create path - > go throurgh the path
    public void newPathRequested(Node pTo) // from ui when location is pressed
    {
        // nodeStart = currentNode;
        spaceInfoBoard.SetActive(false);
        nodeEnd = pTo;
        pathEndUI.SetActive(false);

        if (nodeStart != null && nodeEnd != null)
        {
            Debug.Log("GENERATING PATH WITH STARTNODE: " + nodeStart);
           // Debug.Log(nodeEnd);

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
            debugtext.text = activeNode.name;
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
        bool found = false;
        queue.Add(pFrom);
        while (queue.Count>0)
        {
            Node current = queue[0];
            queue.RemoveAt(0);
            //  Debug.Log("BFS: considering node " + current.name+" visited: "+visited.Count+" queue: "+queue.Count);
            visited.Add(current);
            if (current==pTo)
            {
                Debug.Log("Done");
                found = true;
                break;
            }
            foreach (Node nb in current.GetConnections())
            {
                if (!visited.Contains(nb) && !queue.Contains(nb))
                {
                    queue.Add(nb);
                    nb.SetPrevious(current); 
                }
            }
        }
        if (found)
        {
            List<Node> path=new List<Node>();
            Node curr = pTo;
            path.Add(curr);
            Debug.Log("Creating path");
            while (curr!=pFrom)
            {
                curr=curr.GetPrevious();
                path.Add(curr);
            }
            Debug.Log("Path creation done");
            path.Reverse();
            printList(path);
            return path;

        }
        Debug.Log("No path found. Disconnected map?");
        return null;


    }
  

    private void printList(List<Node> list)
    {
        foreach (Node node in list) { Debug.Log("thePath: "+ node + ", "); }
        Debug.Log("/");
    }

    public List<Node> GetPath()
    {
        //Debug.Log("GETPATH: " + currentPath.Count);
        return currentPath;
    }
}

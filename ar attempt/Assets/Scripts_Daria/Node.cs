using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    
    [SerializeField]
    List<Node> connections = new List<Node>();
    //Node[] connections; //addconnections to smth
    [SerializeField]
    Color color = Color.white;

    [SerializeField]
    LayerMask nodeMask;

    [SerializeField]
    bool showAllPrevious;

    public List<Node> nodes = new List<Node>();

   //[HideInInspector]
    [SerializeField] Node Previous = null;
    Node oldPrev = null;

    int connectionsAtStart =2;
    int distanceRaycast = 5;
    int j = 0;
    void Start()
    {
        nodes.Add(Previous);
        connectionsAtStart = connections.Count;
        for (int i =0; i<connectionsAtStart; i++)
        {
         //   connections[i].connections.Add(this);
        }

        for (int i = 0; i <= 360; i += 15)
        {
            if (Physics.Raycast(transform.position,  Quaternion.AngleAxis(i, Vector3.up) * Vector3.right , out RaycastHit hitInfo, distanceRaycast, nodeMask))
            {
                
              //  connections.Add(hitInfo.transform.GetComponent<Node>());

            }
        }
    }
    private void OnDrawGizmos()
    {
        
        /*for (; j <= 360; j += 15)
        {
            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(j, Vector3.up) * Vector3.right, out RaycastHit hitInfo, distanceRaycast, nodeMask))
            {

                connections.Add(hitInfo.transform.GetComponent<Node>());

            }
        }*/
        /*for (int i = 0; i <= 360; i += 15)
        {
          // Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(i, Vector3.up) * Vector3.right );

            //  Debug.Log(i);
            // Gizmos.DrawLine(transform.position, Quaternion.AngleAxis(i, Vector3.up)  * transform.position );
        }*/
        foreach (Node connection in connections)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, (connection.transform.position+transform.position)/2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPrev != Previous)
        {
            nodes.Add(Previous);
            oldPrev = Previous;
        }
    }

    public List<Node> GetConnections()
    {
        return connections;
    }
    public Node GetPrevious()
    {
        return Previous;
    }
    public void SetPrevious(Node node)
    {
        if (Previous == null)
        {
            Previous = node;
        }
    }
}

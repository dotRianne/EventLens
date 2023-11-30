using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connections = new List<Node>();
    //Node[] connections; //addconnections to smth
    [SerializeField]
    Color color = Color.white;

    [SerializeField]
    LayerMask nodeMask;

    [HideInInspector]
    public Node Previous = null;

    int connectionsAtStart =2;
    int distanceRaycast = 5;
    int j = 0;
    void Start()
    {
        connectionsAtStart = connections.Count;
        for (int i =0; i<connectionsAtStart; i++)
        {
         //   connections[i].connections.Add(this);
        }

        for (int i = 0; i <= 360; i += 15)
        {
            if (Physics.Raycast(transform.position,  Quaternion.AngleAxis(i, Vector3.up) * Vector3.right , out RaycastHit hitInfo, distanceRaycast, nodeMask))
            {
                
                connections.Add(hitInfo.transform.GetComponent<Node>());

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

    public List<Node> GetConnections()
    {
        return connections;
    }
}

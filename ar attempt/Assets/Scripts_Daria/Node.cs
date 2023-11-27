using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    Node[] connections; //addconnections to smth
    [SerializeField]
    Color color = Color.white;

    [HideInInspector]
    public Node Previous = null;
    void Start()
    {

    }
    private void OnDrawGizmos()
    {
        foreach (Node connection in connections)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, connection.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node[] GetConnections()
    {
        return connections;
    }
}

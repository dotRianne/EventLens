using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPathManager : MonoBehaviour
{
    NodeConnections[] nodeConnections;
 //   List<Node> connections = new List<Node>();
    List<Node> path = new List<Node>();

    private void Awake()
    {
        nodeConnections = FindObjectsOfType<NodeConnections>();
    }

    public void CreatePath()
    {
        for (int i=0; i<path.Count-1;i++) 
        {
            foreach(NodeConnections nodeConnection in nodeConnections)
            {
                //check if nodeA is equal to path[i]
                //when somthing is equal
                //check if nodeB is equal to path[i+1]
                //break this for of connection

                //do te smae for nodeB

                
            }
            //activate gameobject for connection

        }
    }

    public void VisualisePath()
    {
        foreach(NodeConnections nodeConnection in nodeConnections)
        {
            if (nodeConnection.activePath) nodeConnection.gameObject.SetActive(true);
        }
    }
}

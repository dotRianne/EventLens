using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class DrawMapConnections : MonoBehaviour
{
    [SerializeField] private Transform connectionParent;
    [SerializeField] private Image imagePrefab;
    [SerializeField] private Image imgA;
    [SerializeField] private Image imgB;
    [SerializeField] private Node nodeA;
    [SerializeField] private Node nodeB;
    public Node[] nodeList;
    public Button[] imgList;
    public List<Node> connections;

    private float lineWidth = 20f;

    private void Awake()
    {
        AllNodes();
    }

    public void AllNodes()
    {
        for (int i = 0; i < nodeList.Length; i++)
        {
            ConnectedNodes(nodeList[i]);
        }
    }


    public void ConnectedNodes(Node node)
    {
        connections = node.GetConnections();

        nodeA = node;
        for(int i = 0; i < node.GetConnections().Count; i++)
        {
            imgA = imgList[i].GetComponent<Image>();

            for(int j = 0; j < imgList.Length;j++)
            {
                imgB = imgList[j].GetComponent<Image>();
                nodeB = connections[i];

                if (connections.Contains(nodeB) && imgB.name == nodeB.name)
                {
                    DrawConnection(imagePrefab, imgA, imgB);
                }
            }
        }
    }

    public void DrawConnection(Image image_Prefab, Image img_A, Image img_B)
    {
        //Get node_A and node_B map-position
        Vector3 posA = img_A.gameObject.transform.position;
        Vector3 posB = img_B.gameObject.transform.position;

        //Get rotation from nodeA to nodeB
        Vector3 vectorToTarget = img_B.gameObject.transform.position - img_A.gameObject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        //Get distance from nodeA to nodeB
        float dX = posB.x - posA.x; 
        float dY = posB.y - posA.y;
        float distance = Mathf.Sqrt(Mathf.Pow(dX, 2) + Mathf.Pow(dY, 2));
        
        //Create an image with height(dist(nodeB - nodeA)), width(lineWidth)
        image_Prefab.rectTransform.sizeDelta = new Vector2(lineWidth, distance);

        //Instantiate image with position from node_A, distance from nodeA to nodeB, rotation towards nodeB
        Instantiate(image_Prefab, posA, targetRotation, connectionParent);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR.ARFoundation;

public class PlaceTrackedImage : MonoBehaviour
{
    Node[] nodes;
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    [SerializeField]
    GameObject[] objectPrefabs;
    bool mapRotationSet = false;


    [SerializeField]
    Node nodeStart;

    PathfindingManager pathfindingManager;

    Camera cam;

    Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        nodes = FindObjectsOfType<Node>();
        cam = FindObjectOfType<Camera>();
        pathfindingManager = FindObjectOfType<PathfindingManager>();
       // m_TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in objectPrefabs)
        {
           GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
           // Debug.Log("image added");
            // Instantiate(prefab);
            // Destroy(prefabToDestroy);
            UpdateImage(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            //Debug.Log("image updated");
            UpdateImage(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
            Debug.Log("image removed");
            spawnedPrefabs[removedImage.name].SetActive(false);
        }
    }

    private void Update()
    {
        //ListAllImages();
        // Debug.Log(m_TrackedImageManager.referenceLibrary.count);
        if (m_TrackedImageManager.referenceLibrary.count > 0)
        {
          //  Destroy(prefabToDestroy);
        }


        foreach(GameObject obj in spawnedPrefabs.Values)
        {
            if (cam)
            {
                Vector3 viewPos = cam.WorldToViewportPoint(obj.transform.position);
                if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
                {
                  //  Debug.Log(obj.name);

                }
                else
                {
                    obj.SetActive(false);

                }
            }
        }
    }

    /*    void ListAllImages()
        {
            Debug.Log(
                $"There are {m_TrackedImageManager.trackables.count} images being tracked.");

            foreach (var trackedImage in m_TrackedImageManager.trackables)
            {
                Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                          $"{trackedImage.transform.position}");
            }
        }*/

    bool currentlyVisibleNode = false;
    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 pos = trackedImage.transform.position;
        //check if npc
        if (name.StartsWith("npc"))
        {
            pos.y += 0.5f;
        }
        //check if Node
        //scan node and send nodereached
        if (name.StartsWith("node"))
        {
            if (!mapRotationSet)
            {
                if (name == nodeStart.name && cam && pathfindingManager)
                {
                    Debug.Log("map rotation set");
                    Vector3 camRotation = cam.transform.eulerAngles;
                    camRotation.x = 0;
                    camRotation.z = 0;
                    camRotation.y -= 90;
                    pathfindingManager.transform.eulerAngles = camRotation;  
                    mapRotationSet = true;
                }
            }
            else
            {
                //Debug.Log(trackedImage.name.ToString());
                foreach (Node node in nodes)
                {
                    if (name == node.name)
                    {
                        pathfindingManager.nodeReached(node);
                        // currentlyVisibleNode = true;
                    }


                }
            }
        }
        else
        {
          //  currentlyVisibleNode = false;
            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = pos;
            prefab.SetActive(true);
        }

       /* foreach(GameObject obj in spawnedPrefabs.Values)
        {
            if (obj.name !=name)
            {
               // obj.SetActive(false);
            }
        }*/



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class Guide : MonoBehaviour
{
    [SerializeField]
    GameObject digitalPlayer;

    
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation= digitalPlayer.transform.rotation;
        //transform.position = cam.transform.position+ new Vector3(0,0,3);
    }
}

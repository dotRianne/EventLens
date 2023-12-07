using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class Guide : MonoBehaviour
{
    [SerializeField]
    GameObject digitalPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation= digitalPlayer.transform.rotation;
    }
}

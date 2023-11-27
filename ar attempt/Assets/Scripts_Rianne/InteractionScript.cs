using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;
public class InteractionScript : MonoBehaviour
{ 
    public Buttons buttons;
    internal bool canSendRaycasts = true;

    [SerializeField] LayerMask layerMask;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        buttons = GetComponent<Buttons>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }
    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerUp -= FingerDown;
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        Debug.Log(canSendRaycasts);
//        Debug.Log("Finger down! " + finger.index + " at " + finger.currentTouch.screenPosition);
        if (finger.index != 0) return;
//        Debug.Log("Raycast sent! sent at: " + finger.currentTouch.screenPosition);
        if (canSendRaycasts)
        {
        Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Debug.Log("Hit: " + hit.collider);
            // Perform raycasting
            if (Physics.Raycast(ray, out hit))
            {
//                Debug.Log("tag: " + hit.collider.tag + ", position: " + hit.transform.position + ", ray: " + ray);
                // Check if the object has a specific tag (you can customize this)
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "one" || hit.collider.name == "two")
                {
                    Debug.Log("THIS IS ONE OR TWO: " + hit.collider.name);
                    buttons.OpenInteractions("Joeri 1 2 3");
                }
            }
        }
    }
}

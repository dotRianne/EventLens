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


    [SerializeField] private string joeriText = "";
    [SerializeField] private string markText = "";
    //[SerializeField] LayerMask layerMask;
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
        if (finger.index != 0) return;
        if (canSendRaycasts)
        {
            Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
            RaycastHit hit;
            // Perform raycasting
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object has a specific tag (you can customize this)
                switch(hit.collider.name)
                {
                    case "one":
                        buttons.OpenInteractions(joeriText);
                        break;
                    case "two":
                        buttons.OpenInteractions(markText);
                        break;
                }
            }
        }
    }
}

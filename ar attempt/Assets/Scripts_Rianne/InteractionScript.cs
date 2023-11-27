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
    public GameObject interactionCanvas;


    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        interactionCanvas = GameObject.Find("InteractCanvas");
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
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.AllTypes))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Perform raycasting
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("tag: " + hit.collider.tag + ", position: " + hit.transform.position + ", ray: " + ray);
                // Check if the object has a specific tag (you can customize this)
                if (hit.collider.tag == "Untagged")
                {
                    // Show interaction canvas
                    ShowInteractionCanvas();

                    // Perform actions based on the hit object
                    HandleObjectInteraction(hit.collider.gameObject);
                }
            }
        }
    }

    public void ShowInteractionCanvas()
    {
        // Enable the canvas and set its position
        interactionCanvas.SetActive(true);
    }

    public void HandleObjectInteraction(GameObject interactableObject)
    {
        // Customize this method based on the specific interactions you want
        // For example, update UI text with information about the object
        TMP_Text uiText = interactionCanvas.GetComponentInChildren<TMP_Text>();
        uiText.text = "Hello I am " + interactableObject.name + ", How nice to meet you!";

        // You can add more interactions here, like playing animations, spawning objects, etc.
    }

    public void CloseInteractionMenu()
    {
        interactionCanvas.SetActive(false);
    }
}

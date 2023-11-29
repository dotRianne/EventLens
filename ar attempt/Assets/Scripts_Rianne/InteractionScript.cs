using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;

public class InteractionScript : MonoBehaviour
{ 
    public Buttons buttons;
    internal bool canSendRaycasts = true;

    [SerializeField] private string[] npcNames;
    [SerializeField] private string[] npcTexts;
    private Dictionary<string, string> npcDict = new Dictionary<string, string>();
    [SerializeField] private string[] infoNames;
    [SerializeField] private string[] infoTexts;
    private Dictionary<string, string> infoDict = new Dictionary<string, string>();

    //[SerializeField] LayerMask layerMask;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        buttons = GetComponent<Buttons>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();

        for(int i = 0; i < npcNames.Length; i++) npcDict.Add(npcNames[i], npcTexts[i]);
        for(int i = 0; i < infoNames.Length; i++) infoDict.Add(infoNames[i], infoTexts[i]);
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
        if (finger.index != 0) return;
        Debug.Log("Can send raycasts: " + canSendRaycasts);
        if (canSendRaycasts)
        {
            Ray ray = Camera.main.ScreenPointToRay(finger.currentTouch.screenPosition);
            RaycastHit hit;
            // Perform raycasting
            if (Physics.Raycast(ray, out hit))
            {
                if(npcDict.ContainsKey(hit.collider.name))
                {
                    buttons.OpenInteractions(npcDict[hit.collider.name]);
                }
                if (infoDict.ContainsKey(hit.collider.name))
                {
                    buttons.OpenInformations(infoDict[hit.collider.name]);
                }
            }
        }
    }
}

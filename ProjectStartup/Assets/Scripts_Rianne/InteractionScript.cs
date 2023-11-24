using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public Canvas interactionCanvas;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Perform raycasting
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("tag: " + hit.collider.tag +", position: " + hit.transform.position + ", ray: " + ray);
                // Check if the object has a specific tag (you can customize this)
                if (hit.collider.tag == "NPC")
                {
                    // Show interaction canvas
                    ShowInteractionCanvas(hit.point);

                    // Perform actions based on the hit object
                    HandleObjectInteraction(hit.collider.gameObject);
                }
            }
        }
    }

    void ShowInteractionCanvas(Vector3 position)
    {
        // Enable the canvas and set its position
        interactionCanvas.gameObject.SetActive(true);
        interactionCanvas.transform.position = position;
        interactionCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    void HandleObjectInteraction(GameObject interactableObject)
    {
        // Customize this method based on the specific interactions you want
        // For example, update UI text with information about the object
        Text uiText = interactionCanvas.GetComponentInChildren<Text>();
        uiText.text = "Interacting with: " + interactableObject.name;

        // You can add more interactions here, like playing animations, spawning objects, etc.
    }
}

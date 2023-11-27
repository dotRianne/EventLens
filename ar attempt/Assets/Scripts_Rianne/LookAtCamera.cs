using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RotateToCamera();
    }

    void RotateToCamera()
    {
        // Find the main camera
        Camera mainCamera = Camera.main;

        // Check if the main camera exists
        if (mainCamera != null)
        {
            // Calculate the direction from the prefab to the camera
            Vector3 lookAtPosition = mainCamera.transform.position;
            Vector3 targetDir = lookAtPosition - transform.position;
            targetDir.y = 0f;

            // Rotate the prefab to face the camera
            transform.rotation = Quaternion.LookRotation(targetDir);
        }
        else
        {
            Debug.LogWarning("Main camera not found.");
        }
    }
}
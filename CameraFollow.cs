using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The capsule to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Default offset (adjust as needed)
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void LateUpdate()
    {
        // Calculate the desired position based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Optionally, make the camera look at the target
        transform.LookAt(target);
    }
}

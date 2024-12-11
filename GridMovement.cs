using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveDistance = 1f; // Distance to move in one step
    public float moveSpeed = 10f;  // Speed of movement between grid points
    public float spinSpeed = 360f; // Speed of the 360-degree spin

    private Vector3 targetPosition; // The next position to move to
    private bool isMoving = false;  // Whether the object is currently moving
    private bool isSpinning = false; // Whether the object is currently spinning

    void Start()
    {
        // Initialize target position to the starting position
        targetPosition = transform.position;
    }

    void Update()
    {
        // If not moving or spinning, process input
        if (!isMoving && !isSpinning)
        {
            HandleInput();
        }

        // Move or spin based on current state
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Forward
        {
            targetPosition += Vector3.forward * moveDistance;
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Backward
        {
            targetPosition += Vector3.back * moveDistance;
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Left
        {
            targetPosition += Vector3.left * moveDistance;
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            targetPosition += Vector3.right * moveDistance;
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q)) // Spin
        {
            StartCoroutine(PerformSpin());
        }
    }

    void MoveToTarget()
    {
        // Smoothly move the object toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the target position is reached
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    System.Collections.IEnumerator PerformSpin()
    {
        isSpinning = true; // Block other actions

        float totalRotation = 0f;
        while (totalRotation < 360f)
        {
            float rotationStep = spinSpeed * Time.deltaTime; // Rotation for this frame
            transform.Rotate(0, rotationStep, 0); // Rotate around the Y-axis
            totalRotation += rotationStep;
            yield return null; // Wait for the next frame
        }

        // Ensure the rotation is exactly 360 degrees
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Round(transform.eulerAngles.y), transform.eulerAngles.z);

        isSpinning = false; // Allow other actions
    }
}

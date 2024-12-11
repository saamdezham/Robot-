using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float moveSpeed = 1.0f;  
    
    // Method for interpreting move commands
    public void InterpretMoveCommand(string direction, int steps)
{
    Vector3 moveDirection = Vector3.zero;

    switch (direction.ToLower())
    {
        case "up":
            moveDirection = Vector3.forward;  
            break;
        case "down":
            moveDirection = Vector3.back; 
            break;
        case "left":
            moveDirection = Vector3.left;  
            break;
        case "right":
            moveDirection = Vector3.right; 
            break;
        default:
            Debug.LogError($"Unknown direction: {direction}");
            return;
    }

    float moveDistance = steps * moveSpeed; // Calculate total distance to move
    transform.Translate(moveDirection * moveDistance, Space.World);

    Debug.Log($"Moved {direction} by {steps} step(s). New position: {transform.position}");
}


    // Method for interpreting rotate commands
    public void InterpretRotateCommand(int angle)
    {
        transform.Rotate(Vector3.up, angle);  // Rotate around the Y-axis (adjust if needed)
        Debug.Log($"Rotated by {angle} degrees. New rotation: {transform.rotation.eulerAngles}");
    }
    public void InterpretInitialCommand(string position)
{
    Debug.Log($"Initial position set to: {position}");
    // Optional: Parse position and move robot to that position
}

public void InterpretPowerDownCommand()
{
    Debug.Log("Robot powering down...");
}

// Method for interpreting speech commands
public void InterpretSpeechCommand(string speechCommand)
{
    switch (speechCommand.ToLower())
    {
        case "how was your day?":
            Debug.Log("I'm just a robot; every day is the same!");
            break;
        case "what is your purpose?":
            Debug.Log("To serve you!");
            break;
        case "flip a coin":
            string result = (Random.Range(0, 2) == 0) ? "Heads" : "Tails";
            Debug.Log($"The coin landed on: {result}");
            break;
        default:
            Debug.Log($"Unknown command: {speechCommand}");
            break;
    }
}


}

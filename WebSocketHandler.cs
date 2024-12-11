using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
public class WebSocketHandler : WebSocketBehavior
{
    public static RobotController UnityRobotController; // Static reference to RobotController

    protected override void OnMessage(MessageEventArgs e)
    {
        string message = e.Data;
        Debug.Log($"Received message: {message}");

        if (UnityRobotController == null)
        {
            Debug.LogWarning("RobotController is not set.");
            return;
        }
        // Queue Unity operations on the main thread
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            try
            {
                // Parse and forward the message to Unity's RobotController
                var command = JsonUtility.FromJson<RobotCommand>(message);
                switch (command.command_type)
                {
                    case "Move":
                        UnityRobotController.InterpretMoveCommand(command.direction, command.steps ?? 1);
                        break;
                    case "Spin":
                        UnityRobotController.InterpretRotateCommand(360); 
                        break;
                    default:
                        Debug.LogError($"Unknown command type: {command.command_type}");
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error parsing message: {ex.Message}");
            }
        });
    }
}


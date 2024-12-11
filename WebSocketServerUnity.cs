using UnityEngine;
using WebSocketSharp.Server;

public class WebSocketServerUnity : MonoBehaviour
{
    private WebSocketServer wss;

    void Start()
{
    wss = new WebSocketServer("ws://localhost:8080");
    wss.AddWebSocketService<WebSocketHandler>("/Robot");

    // Link the RobotController
    WebSocketHandler.UnityRobotController = FindObjectOfType<RobotController>();

    wss.Start();
    Debug.Log("WebSocket server started at ws://localhost:8080/Robot");
}



    void OnDestroy()
    {
        if (wss != null)
        {
            wss.Stop();
            Debug.Log("WebSocket server stopped.");
        }
    }
}

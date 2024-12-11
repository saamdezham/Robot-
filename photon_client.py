import asyncio
import websockets
import json


APP_ID = "4a58a37c-4358-473f-a39c-08f0d98aad77" 
PHOTON_SERVER_URL = f"wss://{APP_ID}.photonengine.com:5055" 

async def connect_to_photon():
    # Connect to the Photon WebSocket server
    async with websockets.connect(PHOTON_SERVER_URL) as websocket:
        print("Connected to Photon WebSocket!")

        join_message = {
            "msg": "join",  # Message type
            "appId": APP_ID,  # Your Photon App ID
            "userId": "python_user_1"  # A unique ID for this user
        }

        # Send the join message to Photon
        await websocket.send(json.dumps(join_message))

        # Wait for response from Photon
        response = await websocket.recv()
        print(f"Received from Photon: {response}")

        # Example: Send a move command
        move_command = {
            "msg": "rpc",  # Indicate that this is an RPC call
            "method": "InterpretMoveCommand",  # The name of the method in Unity 
            "args": ["up", 3]  # Arguments for the method (direction and steps)
        }
        await websocket.send(json.dumps(move_command))

        # Wait for the move result from Unity (Photon server will send it back)
        move_result = await websocket.recv()
        print(f"Move result: {move_result}")

# Run the asynchronous function
asyncio.get_event_loop().run_until_complete(connect_to_photon())

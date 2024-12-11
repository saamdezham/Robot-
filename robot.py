import websocket
import json
import random
from textx import metamodel_from_file

def move_command_processor(move_cmd):
    # Object processor for MoveCommand
    if move_cmd.steps == 0:
        move_cmd.steps = 1

class Robot:
    def __init__(self):
        self.x = 0
        self.y = 0
        self.initial_set = False

    def __str__(self):
        return f"Robot position is {self.x}, {self.y}."

    def send_command_to_unity(self, command_type, direction=None, steps=None):
        try:
            # Connect to WebSocket server running in Unity
            ws = websocket.create_connection("ws://localhost:8080/Robot")  # Unity WebSocket is running on this port (8080)
            
            
            if steps is None:
                steps = 1  
            
            message = {"command_type": command_type, "direction": direction, "steps": steps}
            
            # Send the message as JSON
            ws.send(json.dumps(message))
        except ConnectionRefusedError:
            print("Error: Could not connect to Unity WebSocket server")
        except Exception as e:
            print(f"An unexpected error occurred: {e}")
        finally:
            if 'ws' in locals():
                ws.close()


    def interpret(self, model):
        if not model.commands or model.commands[0].__class__.__name__ != "InitialCommand":
            raise ValueError("The first command must be 'InitialCommand'.")
        
        for c in model.commands:
            if c.__class__.__name__ == "InitialCommand":
                if self.initial_set:
                    raise ValueError("The 'InitialCommand' can only be set once.")
                print(f"Setting position to: {c.x}, {c.y}")
                self.x = c.x
                self.y = c.y
                self.initial_set = True

            elif c.__class__.__name__ == "MoveCommand":
                steps = c.steps if c.steps is not None else 1
                if c.direction == "spin":
                    print("Performing 360 spin!")
                    self.send_command_to_unity("Spin", direction="spin", steps=None) 
                else:
                    print(f"Moving {c.direction} for {steps} step(s).")
                    self.send_command_to_unity("Move", direction=c.direction, steps=steps)  # Send move command to Unity

            elif c.__class__.__name__ == "SpeechCommand":
                if c.command_text == "how was your day?":
                    print("I'm just a robot; every day is the same!")
                elif c.command_text == "what is your purpose?":
                    print("To serve you!")
                elif c.command_text == "flip a coin":
                    result = random.choice(["Heads", "Tails"])
                    print(f"The coin landed on: {result}")

def main(debug=False):
    robot_mm = metamodel_from_file('robot.tx', debug=debug)  # Load the grammar
    robot_model = robot_mm.model_from_file('program.rbt')    # Parse the program

    robot = Robot()
    robot.interpret(robot_model)

if __name__ == "__main__":
    main()

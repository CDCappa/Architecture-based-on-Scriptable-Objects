# About
This repository demonstrates the benefits of an architecture that integrates Scriptable Objects. Specifically, it emphasizes modularity and decoupling, ensuring robustness, separation of concerns, scalability, flexibility, maintainability, and reduced risk when distributing tasks within a team.

# Feature/Event System PR #1

## Description
This pull request introduces an Event System using Scriptable Objects in Unity. It consists of three main scripts: EventEditor.cs, GameEvent.cs, and GameEventListener.cs. 

### EventEditor.cs
In the EventEditor class, a custom editor is created for the GameEvent scriptable object to allow events to be raised during play mode. A green "Raise" button is displayed in the inspector when the game is running.

### GameEvent.cs
The GameEvent scriptable object contains a list of listeners that will be notified when the event is raised. It provides methods to raise the event and manage listener registration and unregistration.

### GameEventListener.cs
The GameEventListener MonoBehaviour listens to a specific GameEvent and invokes a UnityEvent response when that event is raised. It handles registering and unregistering itself as a listener to the specified event.

## Changes Made
- Added EventEditor.cs for custom editor functionality.
- Created GameEvent.cs for managing events using Scriptable Objects.
- Developed GameEventListener.cs to handle event listening and response invocation.

## How to Test
1. Attach the GameEvent scriptable object to a GameObject.
2. Create a new GameEventListener component on another GameObject.
3. Assign the desired GameEvent and UnityEvent response in the GameEventListener component.
4. Run the game and observe the response invoked when the event is raised.

## Additional Notes
- Ensure all necessary scripts are properly placed in their respective folders.
- Double-check references between GameEvent and GameEventListener components for proper event handling.

# Feature/brain behaviors PR #2

## Description
This pull request introduces a new Brain abstract class in the Brains.SO namespace. The Brain class is designed to manage coroutines and behaviors for Unity entities through Scriptable Objects. It provides functionality for starting, stopping, and handling entity behaviors within coroutines.

### Changes Made
- Added the Brain abstract class to manage coroutines and behaviors.
- Implemented methods for starting and stopping behaviors with coroutines.
- Included functionality to initialize, start, and stop behaviors for Unity entities.

## Testing Instructions
1. Create a new class that inherits from the Brain abstract class.
2. Implement the required Behavior method to define the behavior of the entity.
3. Instantiate the new class as a Scriptable Object in Unity.
4. Attach the Scriptable Object to a GameObject in the scene.
5. Call the StartBehaviour method on the Brain instance to initiate the behavior.
6. Test stopping the behavior by calling the StopBehaviour method.

## Additional Notes
- Ensure proper initialization and cleanup of behaviors within the Brain class.
- Utilize the provided methods to manage entity behaviors effectively.

# Demo/Brain Behaviors PR #3

## Description
This pull request introduces a flexible and modular Continuous Rotator system in Unity, designed for rotating game objects around a central point with customizable parameters. The system leverages ScriptableObject-based brains to define different behaviors, enhancing reusability and maintainability. Additionally, an event-driven mechanism using ScriptableObjects is implemented for controlling the rotator's behavior.

## Components
ContinuousRotator MonoBehaviour
The ContinuousRotator component is responsible for rotating a game object around a specified axis and radius. It includes methods for setting up rotation parameters, initiating behaviors, and handling visual transitions.

## Key Methods

- SetUp: Configures rotation parameters (radius, speed, axis, direction).
- CO_ShowUpByScaleInterpolation: Smoothly scales the object up over a specified duration.
- StartBehaviour: Starts the defined behavior using a brain ScriptableObject.
- StopBehaviour: Stops the current behavior.
- CO_MoveToRadiusInitialPosition: Moves the object to its initial position based on the radius.
- CO_TurningAround: Continuously rotates the object around the specified axis.
- CalculatePosition: Calculates the current position based on the rotation parameters.

## Brain ScriptableObjects
The Brain ScriptableObjects define specific behaviors for the ContinuousRotator. Two variations are provided:

### ContinuousRotatorBrain1
Defines a behavior that includes scaling up the object, moving to the initial position, and then rotating indefinitely.

### ContinuousRotatorBrain2
Defines a behavior with dynamic direction and radius changes over time while the object rotates.

## FlowController
The FlowController MonoBehaviour raises events based on user input to start or stop behaviors. It uses the event system to decouple the input handling from the behavior execution.

## Event System
The event system uses ScriptableObjects to define events (GameEvent) and listeners (GameEventListener). This system allows for flexible event-driven interactions between game components.

## Usage
### Setting Up a ContinuousRotator

1. Attach the ContinuousRotator component to a GameObject.
2. Configure the rotation parameters via the inspector or through the SetUp method in code.

## Creating and Assigning a Brain
1. Create a new Brain ScriptableObject:

- Right-click in the Project window.
- Navigate to Create -> SO -> Brains.
- Select the appropriate brain type (e.g., MovingCubeBrain1 or MovingCubeBrain2).

2. Assign the Brain to the ContinuousRotator component via the inspector.

## Using the Event System
1. Create GameEvent ScriptableObjects:
- Right-click in the Project window.
- Navigate to Create -> SO -> GameEvent.
- Name the events appropriately (e.g., StartBehavioursEvent, StopBehavioursEvent).
2. Add GameEventListener components to GameObjects that should respond to these events.
- Assign the corresponding GameEvent and define the responses in the inspector.
3. Raise events from the FlowController or other scripts to trigger the behaviors.
## Example Flow
The FlowController script raises events based on user input:

- Pressing the spacebar raises the ChangeTextEvent and StartBehavioursEvent.
- Releasing the escape key raises the StopBehavioursEvent.
## Benefits
- Modularity: The system separates behavior definitions from the rotator logic, allowing for easy creation of new behaviors without modifying existing code.
- Reusability: Brains can be reused across different rotators, and events can be used by various game components.
- Decoupling: The event-driven approach reduces direct dependencies between game objects, enhancing flexibility and maintainability.
- Ease of Testing: Custom editors and event systems facilitate testing and debugging directly within the Unity Editor.
## Conclusion
This Continuous Rotator system provides a robust and flexible framework for implementing complex rotation behaviors in Unity. By leveraging ScriptableObjects and an event-driven architecture, the system ensures high modularity, reusability, and maintainability, making it a valuable addition to any Unity project.

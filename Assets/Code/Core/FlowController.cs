using EventSystems.SO;

using UnityEngine;

public class FlowController : MonoBehaviour
{
    [SerializeField] private GameEvent _changeTextEvent;
    [SerializeField] private GameEvent _startBehavioursEvent;
    [SerializeField] private GameEvent _stopBehavioursEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _changeTextEvent.Raise();
            _startBehavioursEvent.Raise();
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            _stopBehavioursEvent.Raise();
        }
    }
}
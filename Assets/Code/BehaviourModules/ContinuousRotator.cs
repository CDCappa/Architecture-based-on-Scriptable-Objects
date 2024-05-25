using System.Collections;

using Brains.SO;

using UnityEngine;

public class ContinuousRotator : MonoBehaviour
{
    [SerializeField] private Brain _brain;
    private float _radius = 2f;
    private float _speed = 1f;
    private Axis _axis = Axis.Y;
    private RotationDirection _rotationDirection = RotationDirection.Clockwise;

    private Vector3 _centerPosition;
    private float _directionMultiplier = 1f;
    private float _currentAngle = 0f;
    private Quaternion _rotationQuaternion;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        _rotationQuaternion = Quaternion.identity;
    }

    public void SetUp(float radius, float speed, Axis axis, RotationDirection rotationDirection)
    {
        ChangeRadius(radius);
        _speed = speed;
        _axis = axis;
        ChangeDirection(rotationDirection);

        _centerPosition = transform.position;
    }

    public IEnumerator CO_ShowUpByScaleInterpolation(float duration = 1f, float scale = 1f)
    {
        gameObject.SetActive(true);

        float timer = 0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * scale;

        while (timer < duration)
        {
            float scaleFactor = timer / duration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, scaleFactor);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    public void StartBehaviour() => _brain.StartBehaviour(this);
    public void StopBehaviour() => _brain.StopBehaviour(this);
    public bool BehaviourIsActive() => _brain.BehaviourIsRunning;

    public IEnumerator CO_MoveToRadiusInitialPosition(float duration)
    {
        Vector3 startPos = _centerPosition;
        float timer = 0f;

        while (timer < duration)
        {
            float progress = timer / duration;
            Vector3 newPos = Vector3.Lerp(startPos, CalculatePosition(), progress);
            timer += Time.deltaTime;

            transform.position = newPos;
            yield return null;
        }

        transform.position = CalculatePosition();
    }

    public IEnumerator CO_TurningAround()
    {
        while (true)
        {
            _currentAngle += Time.deltaTime * _speed * _directionMultiplier;

            transform.position = CalculatePosition();
            yield return null;
        }
    }

    private Vector3 CalculatePosition()
    {
        Vector3 offset = Vector3.zero;

        switch (_axis)
        {
            case Axis.X:
                _rotationQuaternion = Quaternion.Euler(_currentAngle * Mathf.Rad2Deg, 0, 0);
                offset = _rotationQuaternion * new Vector3(0, _radius, 0);
                break;
            case Axis.Y:
                _rotationQuaternion = Quaternion.Euler(0, _currentAngle * Mathf.Rad2Deg, 0);
                offset = _rotationQuaternion * new Vector3(_radius, 0, 0);
                break;
            case Axis.Z:
                _rotationQuaternion = Quaternion.Euler(0, 0, _currentAngle * Mathf.Rad2Deg);
                offset = _rotationQuaternion * new Vector3(_radius, 0, 0);
                break;
        }

        return _centerPosition + offset;
    }

    public void ChangeDirection(RotationDirection newDirection)
    {
        if (_rotationDirection != newDirection)
        {
            _directionMultiplier *= -1;
        }
        _rotationDirection = newDirection;
    }

    public void ChangeRadius(float newRadius)
    {
        _radius = newRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_centerPosition, _radius);
    }

    private void OnDestroy()
    {
        _brain.StopBehaviour(this);
    }
}

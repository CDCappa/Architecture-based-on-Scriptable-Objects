using System.Collections;
using UnityEngine;

namespace Brains.SO
{
    [CreateAssetMenu(fileName = "Brain_EntityName-ActivityName", menuName = "SO/Brains/MovingCubeBrain1 Brain", order = 0)]
    public class ContinuousRotatorBrain1 : Brain
    {
        [SerializeField] private float _scale = 1f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Axis _axis = Axis.Y;
        [SerializeField] private RotationDirection _rotationDirection = RotationDirection.Clockwise;
        [SerializeField] private float _showUpTime = 3;
        [SerializeField] private float _startBehaviorDelay = 1;
        [SerializeField] private float _intervalToDisplacementToInitialPosition = 3;

        protected override IEnumerator Behaviour(MonoBehaviour entity)
        {
            ContinuousRotator continuousRotator = entity as ContinuousRotator;

            continuousRotator.SetUp(_radius, _speed, _axis, _rotationDirection);

            yield return continuousRotator.CO_ShowUpByScaleInterpolation(_showUpTime, _scale);

            yield return new WaitForSeconds(_startBehaviorDelay);

            yield return continuousRotator.CO_MoveToRadiusInitialPosition(_intervalToDisplacementToInitialPosition);

            yield return continuousRotator.CO_TurningAround();
        }
    }
}
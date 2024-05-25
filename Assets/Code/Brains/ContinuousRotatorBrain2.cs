using System.Collections;
using UnityEngine;

namespace Brains.SO
{
    [CreateAssetMenu(fileName = "Brain_EntityName-ActivityName", menuName = "SO/Brains/MovingCubeBrain2 Brain", order = 0)]
    public class ContinuousRotatorBrain2 : Brain
    {
        [SerializeField] private float _intervalToChangeDirection = 2f;
        [SerializeField] private float _intervalToChangeRadius = 2f;
        [SerializeField] private float[] _radius = new float[1]{2};
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Axis _axis = Axis.Y;
        [SerializeField] private RotationDirection _initialRotationDirection = RotationDirection.Clockwise;
        private float _showUpTime = 0;
        private float _scale = 0f;

        protected override IEnumerator Behaviour(MonoBehaviour entity)
        {
            ContinuousRotator continuousRotator = entity as ContinuousRotator;

            continuousRotator.SetUp(_radius[0], _speed, _axis, _initialRotationDirection);

            yield return continuousRotator.CO_ShowUpByScaleInterpolation(_showUpTime, _scale);

            StartIndependentCoroutine(continuousRotator, continuousRotator.CO_TurningAround());
            StartIndependentCoroutine(continuousRotator, CO_ChangeDirection(continuousRotator, _intervalToChangeDirection));

            yield return CO_ChangeRadius(continuousRotator, _intervalToChangeRadius);
        }

        public IEnumerator CO_ChangeDirection(ContinuousRotator continuousRotator, float interval)
        {
            RotationDirection newDirection = _initialRotationDirection;
            while (true)
            {
                yield return new WaitForSeconds(interval);

                newDirection = newDirection != RotationDirection.Clockwise ? RotationDirection.Clockwise : RotationDirection.CounterClockwise;
                continuousRotator.ChangeDirection(newDirection);
            }
        }

        public IEnumerator CO_ChangeRadius(ContinuousRotator continuousRotator, float interval)
        {
            int index = 1;
            while (true)
            { 
                yield return new WaitForSeconds(interval);

                continuousRotator.ChangeRadius(_radius[index]);

                index++;
                if (index >= _radius.Length)
                {
                    index = 0;
                }
            }
        }
    }
}
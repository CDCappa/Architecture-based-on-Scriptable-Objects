using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Brains.SO
{
    public abstract class Brain : ScriptableObject
    {
        private List<IEnumerator> _coroutines = new();
        protected bool _running = false;
        public virtual bool BehaviourIsRunning => _running;

        public void StartBehaviour(MonoBehaviour entity)
        {
            StartBehaviour(entity, Behaviour(entity));
        }

        public virtual void StopBehaviour(MonoBehaviour entity)
        {
            foreach (var coroutine in _coroutines)
            {
                entity.StopCoroutine(coroutine);
            }
            _coroutines = new();

            _running = false;
        }

        protected virtual void Initialize(MonoBehaviour entity)
        {
            StopBehaviour(entity);
        }

        protected abstract IEnumerator Behaviour(MonoBehaviour entity);

        protected IEnumerator CO_StartBehaviour(MonoBehaviour entity, IEnumerator behaviour)
        {
            if (_running)
            {
                yield break;
            }

            _running = true;

            yield return CO_StartIndependentCoroutine(entity, behaviour);

            _running = false;
        }

        protected void StartIndependentCoroutine(MonoBehaviour entity, IEnumerator behaviour)
        {
            _coroutines.Add(behaviour);
            entity.StartCoroutine(_coroutines[_coroutines.Count - 1]);
        }

        private void StartBehaviour(MonoBehaviour entity, IEnumerator behaviour)
        {
            Initialize(entity);
            StartIndependentCoroutine(entity, CO_StartBehaviour(entity, behaviour));
        }

        private IEnumerator CO_StartIndependentCoroutine(MonoBehaviour entity, IEnumerator behaviour)
        {
            _coroutines.Add(behaviour);
            yield return entity.StartCoroutine(_coroutines[_coroutines.Count - 1]);
        }
    }
}
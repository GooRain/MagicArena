using System;
using Service;
using Service.Updating;
using UnityEngine;

namespace FantasyGame.GamePlay
{
    public class FollowObject : MonoBehaviour, IUpdateable
    {
        [SerializeField]
        private Vector3 offset;

        [SerializeField]
        private Transform target;

        private Transform cachedTransform;

        private void Awake()
        {
            ComponentLocator.Resolve<Updater>().Register(UpdateType.Late, this);
            cachedTransform = transform;

            IsEnabled = target != null;
        }

        public bool IsEnabled { get; private set; }

        public void DoUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void DoFixedUpdate(float fixedDeltaTime)
        {
            throw new NotImplementedException();
        }

        public void DoLateUpdate(float deltaTime)
        {
            cachedTransform.position = target.position + offset;
        }

        private void OnDestroy()
        {
            ComponentLocator.Resolve<Updater>().Delete(UpdateType.Late, this);
        }
    }
}
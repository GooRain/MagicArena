using System;
using Service;
using Service.Updating;
using UnityEngine;

namespace FantasyGame.GamePlay
{
    public class MouseLook : MonoBehaviour, IUpdateable
    {
        [SerializeField]
        private Transform pivot;

        private Transform cachedTransform;

        public bool IsEnabled { get; }

        private void Awake()
        {
            cachedTransform = transform;

            ComponentLocator.Resolve<Updater>().Register(UpdateType.Default, this);
        }

        public void DoUpdate(float deltaTime)
        {
            var xAxis = Input.GetAxis("Mouse X");
            var yAxis = Input.GetAxis("Mouse Y");

            cachedTransform.Rotate(xAxis, yAxis, 0f);
        }

        public void DoFixedUpdate(float fixedDeltaTime)
        {
            throw new NotImplementedException();
        }

        public void DoLateUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            ComponentLocator.Resolve<Updater>().Delete(UpdateType.Default, this);
        }
    }
}
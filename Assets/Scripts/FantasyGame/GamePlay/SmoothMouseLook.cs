using System;
using Service;
using Service.Updating;

namespace FantasyGame.GamePlay
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    [AddComponentMenu("Camera-Control/Smooth Mouse Look")]
    public class SmoothMouseLook : MonoBehaviour, IUpdateable
    {
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        private float rotationX = 0F;
        private float rotationY = 0F;

        public float frameCounter = 20;

        private Quaternion originalRotation;

        private Transform cachedTransform;

        public bool IsEnabled { get; }

        private void Awake()
        {
            cachedTransform = transform;
            
            ComponentLocator.Resolve<Updater>().Register(UpdateType.Default, this);
        }

        private void Start()
        {
            originalRotation = cachedTransform.localRotation;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            angle = angle % 360;
            if ((angle >= -360F) && (angle <= 360F))
            {
                if (angle < -360F)
                {
                    angle += 360F;
                }

                if (angle > 360F)
                {
                    angle -= 360F;
                }
            }

            return Mathf.Clamp(angle, min, max);
        }

        public void DoUpdate(float deltaTime)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            
            var yQuaternion = Quaternion.AngleAxis(ClampAngle(rotationY, minimumY, maximumY), Vector3.left);
            var xQuaternion = Quaternion.AngleAxis(ClampAngle(rotationX, minimumX, maximumX), Vector3.up);

            cachedTransform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }

        public void DoFixedUpdate(float fixedDeltaTime)
        {
            throw new NotImplementedException();
        }

        public void DoLateUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}
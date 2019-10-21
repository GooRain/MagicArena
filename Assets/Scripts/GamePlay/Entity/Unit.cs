using System;
using GamePlay.Control;
using Service;
using UnityEngine;
using Utility;

namespace GamePlay.Entity
{
    public class Unit : UpdateableMonoBehaviour, IControllable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float moveSpeed;

        private Vector3 currentPosition;
        private Vector3 nextPosition;
        private Transform cachedTransform;
        private float deltaTime;

        private float currentMoveSpeed;

        protected override void Awake()
        {
            base.Awake();
            cachedTransform = transform;
            
            Register(UpdateType.Default);
        }

        public override void DoUpdate(float deltaTime)
        {
            animator.SetFloat(AnimatorHash.Move, currentMoveSpeed);
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            throw new NotImplementedException();
        }

        public override void DoLateUpdate(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void Move(Vector2 direction)
        {
            currentMoveSpeed = direction.magnitude;

            if (!(Math.Abs(direction.x) > float.Epsilon) && !(Math.Abs(direction.y) > float.Epsilon)) return;
            
            deltaTime = Time.deltaTime;
            currentPosition = cachedTransform.position;
            nextPosition = currentPosition + moveSpeed * deltaTime * direction.ToXYZ();
            cachedTransform.position = nextPosition;
        }

        public void Jump()
        {
            
        }
    }
}

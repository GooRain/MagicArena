using System;
using FantasyGame.GamePlay.Control;
using Service.Updating;
using UnityEngine;
using Utility;

namespace FantasyGame.GamePlay.Entity
{
    public class Unit : UpdateableMonoBehaviour, IControllable
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float dampingSpeed = 1f;

        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private float gravity = 9.8f;

        private Vector3 currentPosition;
        private Vector3 nextPosition;
        private Transform cachedTransform;
        private float deltaTime;

        private float currentMoveSpeed;

        private Vector3 currentMoveDirection;
        private Vector3 motion;

        protected override void Awake()
        {
            base.Awake();
            cachedTransform = transform;

            Register(UpdateType.Default);
        }

        public override void DoUpdate(float deltaTime)
        {
            animator.SetFloat(AnimatorHash.Move, currentMoveSpeed / moveSpeed);
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
            var relativeDirection = GetRelativeVector(direction);
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, relativeDirection.magnitude * moveSpeed, dampingSpeed * deltaTime);

            motion = currentMoveDirection * (deltaTime * currentMoveSpeed);

            deltaTime = Time.deltaTime;
            currentPosition = cachedTransform.position;
            nextPosition = currentPosition + motion;
            // cachedTransform.position = nextPosition;

            ApplyGravity();
            characterController.Move(motion);

            if (!IsDirectionZero(relativeDirection))
            {
                cachedTransform.rotation = Quaternion.LookRotation(nextPosition - currentPosition, Vector3.up);
                currentMoveDirection = relativeDirection;
            }
        }

        private void ApplyGravity()
        {
            if (!characterController.isGrounded)
            {
                motion.y = -gravity * deltaTime;
            }
        }

        protected virtual Vector3 GetRelativeVector(Vector2 direction)
        {
            return direction.ToXYZ();
        }

        private bool IsDirectionZero(Vector3 direction)
        {
            return (!IsDirectionAboveZero(direction.x) && !IsDirectionAboveZero(direction.y));
        }

        private bool IsDirectionAboveZero(float direction)
        {
            return Mathf.Abs(direction) > float.Epsilon;
        }

        public void Jump()
        {
        }
    }
}
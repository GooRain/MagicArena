using System;
using FantasyGame.GamePlay.Control;
using Service.Updating;
using UnityEngine;
using Utility;

namespace FantasyGame.GamePlay.Entity
{
    public class Unit : UpdateableMonoBehaviour, IControllable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float moveSpeed;

        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private float gravity = 9.8f;

        private Vector3 currentPosition;
        private Vector3 nextPosition;
        private Transform cachedTransform;
        private float deltaTime;

        private float currentMoveSpeed;

        private Vector3 motion;

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
            
            if (IsDirectionZero(direction))
            {
                motion.x = 0f;
                motion.y = -gravity * deltaTime;
                motion.z = 0f;
                characterController.Move(motion);
                return;
            }
            
            motion = moveSpeed * deltaTime * direction.ToXYZ();
            
            deltaTime = Time.deltaTime;
            currentPosition = cachedTransform.position;
            nextPosition = currentPosition + motion;
            // cachedTransform.position = nextPosition;

            motion.y = -gravity * deltaTime;
            characterController.Move(motion);

            cachedTransform.rotation = Quaternion.LookRotation(nextPosition - currentPosition, Vector3.up);
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

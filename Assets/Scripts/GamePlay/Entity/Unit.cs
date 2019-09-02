using System;
using GamePlay.Control;
using UnityEngine;
using Utility;

namespace GamePlay.Entity
{
    public class Unit : MonoBehaviour, IControllable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float moveSpeed;

        private Vector3 currentPosition;
        private Vector3 nextPosition;
        private Transform cachedTransform;
        private float deltaTime;

        private float currentMoveSpeed;

        private void Awake()
        {
            cachedTransform = transform;
        }

        private void Update()
        {
            animator.SetFloat(AnimatorHash.Move, currentMoveSpeed);
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

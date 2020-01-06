using FantasyGame.GamePlay.Control;
using Service;
using UnityEngine;

namespace FantasyGame.GamePlay.Entity
{
    public class Player : Unit
    {
        [SerializeField]
        private Transform relativeTransform;

        protected override void Awake()
        {
            base.Awake();
            Locator.Register<IControllable>(this);
        }

        protected override Vector3 GetRelativeVector(Vector2 direction)
        {
            var forward = relativeTransform.forward;
            var right = relativeTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            var relative = forward * direction.y + right * direction.x;

            return relative;
        }
    }
}
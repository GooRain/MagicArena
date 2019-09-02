using System;
using UnityEngine;

namespace GamePlay.Control
{
    internal class InputReceiver : MonoBehaviour
    {
        private IControllable controllable;

        private void Awake()
        {
            controllable = GetComponent<IControllable>();
        }

        public void Move(Vector2 direction)
        {
            controllable.Move(direction.normalized);
        }

        public void Jump()
        {
            controllable.Jump();
        }
    }
}
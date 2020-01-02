using UnityEngine;

namespace FantasyGame.GamePlay.Control
{
    internal class InputReceiver
    {
        private readonly IControllable controllable;

        public InputReceiver(IControllable controllable)
        {
            this.controllable = controllable;
        }
        
        public void Move(Vector2 direction)
        {
            controllable.Move(direction.normalized);
        }

        public void Jump()
        {
            controllable.Jump();
        }

        public bool IsCorrect()
        {
            return controllable != null;
        }
    }
}
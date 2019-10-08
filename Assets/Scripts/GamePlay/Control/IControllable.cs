using UnityEngine;

namespace GamePlay.Control
{
    public interface IControllable
    {
        void Move(Vector2 direction);
        void Jump();
    }
}
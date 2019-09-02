using UnityEngine;

namespace GamePlay.Control
{
    internal interface IControllable
    {
        void Move(Vector2 direction);
        void Jump();
    }
}
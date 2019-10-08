using Service;
using UnityEngine;
using Utility;

namespace GamePlay.Control
{
    public class InputSender : UpdateableMonoBehaviour
    {
        private IControllable controllable;
        private InputReceiver inputReceiver;

        protected override void Awake()
        {
            base.Awake();
            inputReceiver = new InputReceiver(controllable);
        }

        public override void DoUpdate(float deltaTime)
        {
            base.DoUpdate(deltaTime);
            HandleMoveInput();
        }

        private void HandleMoveInput()
        {
            var axis = new Vector2(Input.GetAxis(StringExt.Horizontal), Input.GetAxis(StringExt.Vertical));
            inputReceiver.Move(axis);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                {
                    inputReceiver.Jump();
                }
            }
        }
    }
}
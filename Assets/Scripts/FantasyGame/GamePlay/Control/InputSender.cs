using Service;
using Service.Updating;
using UnityEngine;
using Utility;

namespace FantasyGame.GamePlay.Control
{
    public class InputSender : UpdateableMonoBehaviour
    {
        private InputReceiver inputReceiver;

        private void Start()
        {
            inputReceiver = new InputReceiver(Locator.Resolve<IControllable>());

            if (inputReceiver.IsCorrect())
            {
                Register(UpdateType.Default);
            }
        }

        public override void DoUpdate(float deltaTime)
        {
            HandleMoveInput();
        }

        public override void DoFixedUpdate(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void DoLateUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        private void HandleMoveInput()
        {
            var axis = new Vector2(Input.GetAxis(StringExt.Horizontal), Input.GetAxis(StringExt.Vertical));
            inputReceiver.Move(axis);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputReceiver.Jump();
            }
        }
    }
}
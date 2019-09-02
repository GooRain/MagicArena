using System;
using UnityEngine;
using Utility;

namespace GamePlay.Control
{
    public class InputController : MonoBehaviour
    {
        private InputReceiver receiver;

        private Vector2 moveDirection;
        private float deltaTime;
        
        private void Awake()
        {
            receiver = FindObjectOfType<InputReceiver>();
        }

        private void Update()
        {
            moveDirection = new Vector2(Input.GetAxis(StringExt.Horizontal), Input.GetAxis(StringExt.Vertical));
            receiver.Move(moveDirection);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                receiver.Jump();
            }
        }
    }
}
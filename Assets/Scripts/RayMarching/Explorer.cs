using System;
using Service.Updating;
using UnityEngine;

namespace RayMarching
{
    [ExecuteInEditMode]
    public class Explorer : MonoBehaviour
    {
        [SerializeField]
        private Material mat;

        [Header("Movement")]
        [SerializeField]
        private Vector2 pos;

        [SerializeField]
        private float moveSpeed = 4f;

        [Header("Zoom")]
        [SerializeField]
        private float scale;

        [SerializeField]
        private float scaleSpeed = 0.01f;

        [Header("Angle")]
        [SerializeField]
        private float angle;

        [SerializeField]
        private float angleSpeed = 5f;

        [SerializeField]
        private float smoothDamp = 0.0333f;

        private static readonly int Area = Shader.PropertyToID("_Area");

        private float aspect;

        private Vector2 moveInput;

        private bool doShaderUpdate;

        private Vector2 smoothPos;
        private float smoothScale;
        private float smoothAngle;
        private static readonly int Angle = Shader.PropertyToID("_Angle");

        private void Awake()
        {
            aspect = (float) Screen.width / Screen.height;

            smoothPos = pos;
            smoothScale = scale;
        }

        private void Start()
        {
            UpdateShader(1f);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            HandleInputs(deltaTime);
            UpdateShader(deltaTime);
        }

        private void HandleInputs(float deltaTime)
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            var dir = new Vector2(moveSpeed * scale * deltaTime, 0);

            float s = Mathf.Sin(angle);
            float c = Mathf.Cos(angle);

            dir = new Vector2(dir.x * c, dir.x * s);

            if (Input.GetKey(KeyCode.A))
            {
                pos -= dir;
                doShaderUpdate = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                pos += dir;
                doShaderUpdate = true;
            }

            dir = new Vector2(-dir.y, dir.x);

            if (Input.GetKey(KeyCode.W))
            {
                pos += dir;
                doShaderUpdate = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= dir;
                doShaderUpdate = true;
            }

            if (Input.GetKey(KeyCode.Equals))
            {
                scale -= scale * scaleSpeed * deltaTime;
                doShaderUpdate = true;
            }
            else if (Input.GetKey(KeyCode.Minus))
            {
                scale += scale * scaleSpeed * deltaTime;
                doShaderUpdate = true;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                angle -= angleSpeed * deltaTime;
                doShaderUpdate = true;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                angle += angleSpeed * deltaTime;
                doShaderUpdate = true;
            }
        }

        private void UpdateShader(float deltaTime)
        {
            smoothPos = Vector2.Lerp(smoothPos, pos, smoothDamp * deltaTime);
            smoothScale = Mathf.Lerp(smoothScale, scale, smoothDamp * deltaTime);
            smoothAngle = Mathf.Lerp(smoothAngle, angle, smoothDamp * deltaTime);

            float scaleX = smoothScale, scaleY = smoothScale;

            if (aspect > 1f)
            {
                scaleY /= aspect;
            }
            else
            {
                scaleX *= aspect;
            }

            mat.SetVector(Area, new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
            mat.SetFloat(Angle, smoothAngle);
        }
    }
}
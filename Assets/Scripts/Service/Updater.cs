using System;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class Updater : MonoBehaviour
    {
        private const int MaxCount = 1000;
        
        private IUpdateable[] updateables;
        
        private int currentCount;
        private float deltaTime;
        private float fixedDeltaTime;
        
        private void InitializeInstance()
        {
            updateables = new IUpdateable[MaxCount];
        }

        private void Update()
        {
            deltaTime = Time.deltaTime;
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            fixedDeltaTime = Time.fixedDeltaTime;
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoFixedUpdate(fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            deltaTime = Time.deltaTime;
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoLateUpdate(deltaTime);
            }
        }

        public void Delete(UpdateableMonoBehaviour updateableMonoBehaviour)
        {
            throw new NotImplementedException();
        }

        public void Registrate(UpdateableMonoBehaviour updateableMonoBehaviour)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Service
{
    public class Updater : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            new GameObject("[Instance] Updater").AddComponent<Updater>();
        }
        
        private Dictionary<UpdateType, Updateables> UpdateablesDictionary;

        private float deltaTime;
        private float fixedDeltaTime;

        private void Awake()
        {
            UpdateablesDictionary = new Dictionary<UpdateType, Updateables>();
            Enum<UpdateType>.ForEach(type => UpdateablesDictionary.Add(type, new Updateables(1000)));
        }

        private void Update()
        {
            deltaTime = Time.deltaTime;
            UpdateablesDictionary[UpdateType.Default].Update(deltaTime);
        }

        private void FixedUpdate()
        {
            fixedDeltaTime = Time.fixedDeltaTime;
            UpdateablesDictionary[UpdateType.Fixed].Update(fixedDeltaTime);
        }

        private void LateUpdate()
        {
            UpdateablesDictionary[UpdateType.Late].Update(deltaTime);
        }

        public void Delete(UpdateType type, IUpdateable updateableMonoBehaviour)
        {
            UpdateablesDictionary[type].Delete(updateableMonoBehaviour);
        }

        public void Register(UpdateType type, IUpdateable updateableMonoBehaviour)
        {
            UpdateablesDictionary[type].Register(updateableMonoBehaviour);
        }
    }
}
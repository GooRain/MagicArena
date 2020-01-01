using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Service.Updating
{
    public class Updater : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            new GameObject("[Instance] Updater").AddComponent<Updater>();
        }
        
        private Dictionary<UpdateType, Updateables> updateablesDictionary;

        private float deltaTime;
        private float fixedDeltaTime;

        private void Awake()
        {
            updateablesDictionary = new Dictionary<UpdateType, Updateables>();
            Enum<UpdateType>.ForEach(type => updateablesDictionary.Add(type, new Updateables(1000)));
        }

        private void Update()
        {
            deltaTime = Time.deltaTime;
            updateablesDictionary[UpdateType.Default].Update(deltaTime);
        }

        private void FixedUpdate()
        {
            fixedDeltaTime = Time.fixedDeltaTime;
            updateablesDictionary[UpdateType.Fixed].FixedUpdate(fixedDeltaTime);
        }

        private void LateUpdate()
        {
            updateablesDictionary[UpdateType.Late].LateUpdate(deltaTime);
        }

        public void Delete(UpdateType type, IUpdateable updateableMonoBehaviour)
        {
            updateablesDictionary[type].Delete(updateableMonoBehaviour);
        }

        public void Register(UpdateType type, IUpdateable updateableMonoBehaviour)
        {
            updateablesDictionary[type].Register(updateableMonoBehaviour);
        }
    }
}
using UnityEngine;

namespace Service.Updating
{
    public class Updateables
    {
        public IUpdateable this[int i] => updateables[i];

        private readonly IUpdateable[] updateables;
        private int currentCount;

        public Updateables(int maxCount)
        {
            updateables = new IUpdateable[maxCount];
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoUpdate(deltaTime);
            }
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoFixedUpdate(fixedDeltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            for (var i = 0; i < currentCount; i++)
            {
                updateables[i].DoLateUpdate(deltaTime);
            }
        }

        public bool Delete(IUpdateable updateableMonoBehaviour)
        {
            for (var i = 0; i < currentCount; i++)
            {
                if (updateables[i] != updateableMonoBehaviour) continue;

                var tmp = updateables[i];
                updateables[i] = updateables[currentCount];
                updateables[currentCount] = tmp;

                currentCount--;
                return true;
            }

            return false;
        }

        public void Register(IUpdateable updateableMonoBehaviour)
        {
            updateables[currentCount++] = updateableMonoBehaviour;
        }
    }
}
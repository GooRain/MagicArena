using UnityEngine;

namespace Service
{
    public abstract class UpdateableMonoBehaviour : MonoBehaviour, IUpdateable
    {
        // ReSharper disable once InconsistentNaming
        public new bool enabled
        {
            get => base.enabled;
            set
            {
                base.enabled = value;
                IsEnabled = value;
            }
        }

        public bool IsEnabled { get; private set; }

        protected virtual void Awake()
        {
            ComponentLocator.Resolve<Updater>().Registrate(this);
        }

        public virtual void DoUpdate(float deltaTime)
        {
            
        }

        public virtual void DoFixedUpdate(float fixedDeltaTime)
        {
        }

        public virtual void DoLateUpdate(float deltaTime)
        {
        }

        protected virtual void OnDestroy()
        {
            ComponentLocator.Resolve<Updater>().Delete(this);
        }
    }
}
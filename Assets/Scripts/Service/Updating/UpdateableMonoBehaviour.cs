using UnityEngine;

namespace Service.Updating
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

        private Updater updater;

        protected virtual void Awake()
        {
            updater = ComponentLocator.Resolve<Updater>();
        }

        public abstract void DoUpdate(float deltaTime);
        public abstract void DoFixedUpdate(float fixedDeltaTime);
        public abstract void DoLateUpdate(float deltaTime);

        protected void Register(UpdateType type)
        {
            updater.Register(type, this);
        }
    }
}
namespace Service
{
    public interface IUpdateable
    {
        bool IsEnabled { get; }
        
        void DoUpdate(float deltaTime);
        void DoFixedUpdate(float fixedDeltaTime);
        void DoLateUpdate(float deltaTime);
    }
}
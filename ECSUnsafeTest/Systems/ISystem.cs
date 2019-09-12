namespace ECSUnsafeTest.Systems
{
    public interface ISystem
    {
        void OnRegisterEntity(int Id);
        void Update();
    }
}

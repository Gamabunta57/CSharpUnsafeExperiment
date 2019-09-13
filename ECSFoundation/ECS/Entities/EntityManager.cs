using ECSFoundation.MemoryManagement.MemoryAllocators;

namespace ECSFoundation.ECS.Entities
{
    public static class EntityManager
    {
    
        public static event NewEntityCreated OnNewEntityCreated;
        public delegate void NewEntityCreated(IEntity entity);


        public static void Init(MemoryAllocator memoryManager) => memory = memoryManager;

        public static T NewEntity<T>() where T : IEntity, new()
        {
            var entity = memory.NewEntity<T>();

            OnNewEntityCreated?.Invoke(entity);

            return entity;
        }

        static MemoryAllocator memory;
    }
}

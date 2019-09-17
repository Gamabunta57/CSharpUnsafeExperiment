using ECSFoundation.MemoryManagement.Attributes;

namespace ECSFoundation.ECS.Component
{

    [AllocateMemory(65535)]
    public struct BaseEntity
    {
        public int Id;
    }
}

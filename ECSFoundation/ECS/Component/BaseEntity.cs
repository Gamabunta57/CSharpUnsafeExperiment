using ECSFoundation.MemoryManagement.Attributes;

namespace ECSFoundation.ECS.Component
{

    [AllocateMemory(0x04)]
    public struct BaseEntity
    {
        public int Id;
    }
}

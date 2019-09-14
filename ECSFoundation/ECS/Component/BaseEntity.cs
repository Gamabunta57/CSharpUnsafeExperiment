using ECSFoundation.MemoryManagement.Attributes;

namespace ECSFoundation.ECS.Component
{

    [AllocateMemory(0x07)]
    public struct BaseEntity
    {
        public int Id;
    }
}

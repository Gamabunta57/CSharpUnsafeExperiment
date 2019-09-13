using ECSUnsafeTest.MemoryManagement.Attributes;

namespace ECSUnsafeTest.ECS.Component
{

    [AllocateMemory(0x03)]
    public struct BaseEntity
    {
        public int Id;
    }
}

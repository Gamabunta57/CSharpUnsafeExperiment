using ECSUnsafeTest.Attributes;

namespace ECSUnsafeTest.Component
{

    [AllocateMemory(0x02)]
    public struct BaseEntity
    {
        public int Id;
    }
}

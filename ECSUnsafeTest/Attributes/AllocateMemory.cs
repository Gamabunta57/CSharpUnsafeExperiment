using System;

namespace ECSUnsafeTest.Attributes
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public unsafe class AllocateMemory : Attribute
    {
        public ushort EntityCount { get;}
        public void* StartAddress { get; private set; }
        public uint Offset;
        public uint Alignement;
        public uint StackPointer = 0;
        public AllocateMemory(ushort entityCount) => EntityCount = entityCount;

        public void SetAddress(void* address) => StartAddress = address;

    }
}

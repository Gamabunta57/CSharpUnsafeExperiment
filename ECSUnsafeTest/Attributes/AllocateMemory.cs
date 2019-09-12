using System;

namespace ECSUnsafeTest.Attributes
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public class AllocateMemory : Attribute
    {
        public ushort EntityCount { get;}
        public AllocateMemory(ushort entityCount) => EntityCount = entityCount;
    }
}

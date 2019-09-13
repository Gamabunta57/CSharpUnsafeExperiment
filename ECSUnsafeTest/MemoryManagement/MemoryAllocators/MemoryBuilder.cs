using ECSUnsafeTest.MemoryManagement.Attributes;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ECSUnsafeTest.MemoryManagement.MemoryAllocators
{
    public static class MemoryBuilder
    {
        public static MemoryAllocator AutoSetData()
        {
            var asm = Assembly.GetExecutingAssembly();
            var entityCount = 0u;
            var types = asm.GetTypes();
            var maxAlignement = 0u;

            for(var i = 0; i < types.Length; i++)
            {
                var aam = types[i].GetCustomAttribute<AllocateMemory>(false);
                if (null == aam)
                    continue;

                var align = (uint)Marshal.SizeOf(types[i]);
                if (align > maxAlignement)
                    maxAlignement = align;

                entityCount += aam.EntityCount;
            }

            return new MemoryAllocator(entityCount * maxAlignement, maxAlignement);
        }
    }
}

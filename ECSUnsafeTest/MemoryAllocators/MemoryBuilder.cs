using ECSUnsafeTest.Attributes;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ECSUnsafeTest.MemoryAllocators
{
    class MemoryBuilder
    {
        public MasterMemoryAllocator AutoSetData()
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

            return new MasterMemoryAllocator(entityCount * maxAlignement, maxAlignement);
        }
    }
}

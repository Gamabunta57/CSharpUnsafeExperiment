using ECSFoundation.MemoryManagement.Attributes;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ECSFoundation.MemoryManagement.MemoryAllocators
{
    public static class MemoryBuilder
    {
        public static MemoryAllocator BuildMemoryAllocator()
        {
            var asm = Assembly.GetCallingAssembly(); //TODO : get all assemblies, it miss BaseEntity in the computation
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
                Console.WriteLine($"Size of type {types[i].Name}: {align} | Count: {entityCount}");
            }

            return new MemoryAllocator(entityCount * maxAlignement, maxAlignement);
        }
    }
}

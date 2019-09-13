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
            var asmList = AppDomain.CurrentDomain.GetAssemblies();
            var entityCount = 0u;
            var maxAlignement = 0u;

            foreach(var asm in asmList) {
                var types = asm.GetTypes();
                for (var i = 0u; i < types.Length; i++)
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
            }
            return new MemoryAllocator(entityCount * maxAlignement, maxAlignement);
        }
    }
}

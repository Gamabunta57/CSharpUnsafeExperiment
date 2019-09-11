using ECSUnsafeTest.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ECSUnsafeTest.MemoryAllocators
{
    unsafe class MemoryBuilder
    {
        public MasterMemoryAllocator AutoSetData()
        {
            var asm = Assembly.GetExecutingAssembly();
            var offset = 0u;
            var types = asm.GetTypes();
            for(var i = 0; i < types.Length; i++)
            {
                var aam = types[i].GetCustomAttribute<AllocateMemory>(false);
                if (null == aam)
                    continue;

                aam.Offset = offset;
                aam.StackPointer = offset;
                aam.Alignement = (uint)Marshal.SizeOf(types[i]);
                autoAllocator.Add(types[i], aam);
                offset += aam.Alignement * aam.EntityCount;
            }

            var master = new MasterMemoryAllocator(offset);
            master.SetMemoryTypes(autoAllocator);
            return master;
        }


        readonly Dictionary<Type, AllocateMemory> autoAllocator = new Dictionary<Type, AllocateMemory>();
    }
}

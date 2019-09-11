using ECSUnsafeTest.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ECSUnsafeTest.MemoryAllocators
{
    public unsafe class MasterMemoryAllocator : IDisposable
    {
        public byte* OriginAddress { get; }

        /**
         * Allocating "a lot" of memory to see if the release from a raw pointer is efficient or not.
         * The test concludes that <yes> even from a simple pointer all the needed memory is freed :D
         */
        public MasterMemoryAllocator(uint size) => OriginAddress = (byte*)Marshal.AllocHGlobal((int)size).ToPointer();
        public void Dispose() => Marshal.FreeHGlobal((IntPtr)OriginAddress);

        public void SetMemoryTypes(Dictionary<Type, AllocateMemory> types)
        {
            memorySlots = types;
            foreach (var kvPair in memorySlots)
                kvPair.Value.SetAddress(OriginAddress + kvPair.Value.Offset);
        }

        public ref T Get<T>(ushort index) where T : unmanaged
        {
            memorySlots.TryGetValue(typeof(T), out var value);
            return ref *(T*)(OriginAddress + value.Offset + index * value.Alignement);
        }

        public ref T New<T>() where T : unmanaged
        {
            memorySlots.TryGetValue(typeof(T), out var value);
            var currentPointer = value.StackPointer;
            value.StackPointer += value.Alignement;
            return ref *(T*)(OriginAddress + currentPointer);
        }

        private Dictionary<Type, AllocateMemory> memorySlots;
    }
}

using System;
using System.Runtime.InteropServices;
using ECSUnsafeTest.Component;
using ECSUnsafeTest.Entities;

namespace ECSUnsafeTest.MemoryAllocators
{
    public unsafe class MasterMemoryAllocator : IDisposable
    {
        public byte* OriginAddress { get; }

        public MasterMemoryAllocator(uint size, uint alignment)
        {
            OriginAddress = (byte*)Marshal.AllocHGlobal((int)size).ToPointer();
            Alignment = alignment;
            StackPointer = 0;
            currentIndex = 0;
        }

        public void Dispose() => Marshal.FreeHGlobal((IntPtr)OriginAddress);

        public ref T Get<T>(int index) where T : unmanaged => ref *(T*)(OriginAddress + index * Alignment);

        public ref T New<T>() where T : unmanaged
        {
            var currentPointer = StackPointer;
            StackPointer += Alignment;
            *(int*)(OriginAddress + currentPointer) = currentIndex++; //Set the ID property of the entity
            return ref *(T*)(OriginAddress + currentPointer);
        }

        public T* NewPointer<T>() where T : unmanaged
        {
            var currentPointer = StackPointer;
            StackPointer += Alignment;
            *(int*)(OriginAddress + currentPointer) = currentIndex++; //Set the ID property of the entity
            return (T*)(OriginAddress + currentPointer);
        }

        public PlayerEntity New()
        {
            var player = new PlayerEntity
            {
                baseEntity = NewPointer<BaseEntity>(),
                heading = NewPointer<Heading>(),
                position = NewPointer<Position>(),
                collider = NewPointer<RectCollider>()
            };

            return player;
        }

        readonly uint Alignment;
        uint StackPointer;
        int currentIndex;
    }
}

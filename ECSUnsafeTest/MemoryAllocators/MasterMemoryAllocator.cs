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

        public byte* NewPointer()
        {
            var currentPointer = StackPointer;
            StackPointer += Alignment;
            *(int*)(OriginAddress + currentPointer) = currentIndex++; //Set the ID property of the entity
            return OriginAddress + currentPointer;
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

        public T NewEntity<T>() where T : IEntity, new()
        {
            var entity = new T();

            var members = typeof(T).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach(var member in members)
            {
                Console.WriteLine($"Member name: {member.Name}");
                member.SetValue(entity, (IntPtr)NewPointer());
            }

            return entity;
        }

        readonly uint Alignment;
        uint StackPointer;
        int currentIndex;
    }
}

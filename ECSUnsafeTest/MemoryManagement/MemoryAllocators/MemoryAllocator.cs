using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ECSUnsafeTest.Entities;

namespace ECSUnsafeTest.MemoryManagement.MemoryAllocators
{
    public unsafe class MemoryAllocator : IDisposable
    {

        public MemoryAllocator(uint size, uint alignment)
        {
            OriginAddress = (byte*)Marshal.AllocHGlobal((int)size).ToPointer();
            Alignment = alignment;
            StackPointer = 0;
            currentIndex = 0;
        }

        public void Dispose() => Marshal.FreeHGlobal((IntPtr)OriginAddress);

        public T NewEntity<T>() where T : IEntity, new()
        {
            var entity = new T();
            var boxed = (IEntity)entity;

            var members = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var member in members)
                member.SetValue(boxed, (IntPtr)NewPointer());

            boxed.BaseEntity.Id = currentIndex++;
            return (T)boxed;
        }

        public ref T Get<T>(int index) where T : unmanaged => ref *(T*)(OriginAddress + index * Alignment);

        void* NewPointer()
        {
            var currentPointer = StackPointer;
            StackPointer += Alignment;
            return OriginAddress + currentPointer;
        }

        readonly uint Alignment;
        readonly byte* OriginAddress;
        uint StackPointer;
        int currentIndex;
    }
}

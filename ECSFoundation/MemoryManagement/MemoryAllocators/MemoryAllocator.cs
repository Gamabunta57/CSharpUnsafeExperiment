using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ECSFoundation.ECS.Entities;

namespace ECSFoundation.MemoryManagement.MemoryAllocators
{
    public unsafe class MemoryAllocator : IDisposable
    {
    
        public MemoryAllocator(uint size, uint alignment)
        {
            OriginAddress =(byte*)Marshal.AllocHGlobal((int)size);
            Alignment = alignment;
            StackPointer = 0;
            currentIndex = 0;
        } 

        public void Dispose() => Marshal.FreeHGlobal((IntPtr)OriginAddress);

        public T NewEntity<T>() where T : IEntity, new()
        {
            var entity = new T();
            var boxed = (IEntity)entity;

            var members = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic); // Get all the raw pointers
            foreach (var member in members)
            {
                member.SetValue(boxed, (IntPtr)(OriginAddress + StackPointer));
                StackPointer += Alignment;
            }

            boxed.BaseEntity.Id = currentIndex++;
            return (T)boxed;
        }

        readonly uint Alignment;
        readonly byte* OriginAddress;

        uint StackPointer;
        int currentIndex;
    }
}

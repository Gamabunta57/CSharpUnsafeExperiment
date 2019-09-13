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
            Console.WriteLine((IntPtr)OriginAddress);
            Alignment = alignment;
            StackPointer = 0;
            currentIndex = 0;
        } 

        public void Dispose()
        {
            if (!isMemoryFreed) {
                isMemoryFreed = true;
                Marshal.FreeHGlobal((IntPtr)OriginAddress);
            }
            else
            {
                Console.WriteLine("Ask a second time");
            }
            Console.WriteLine("Ask for dispose");
        }

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

        byte* NewPointer()
        {
            var currentPointer = StackPointer;
            StackPointer += Alignment;
            return OriginAddress + currentPointer;
        }

        readonly uint Alignment;
        byte* OriginAddress;
        uint StackPointer;
        int currentIndex;
        bool isMemoryFreed;
    }
}

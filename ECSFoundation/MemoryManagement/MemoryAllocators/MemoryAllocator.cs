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
            _originAddress =(byte*)Marshal.AllocHGlobal((int)size);
            _alignment = alignment;
            _stackPointer = 0;
            _currentIndex = 0;
        } 

        public void Dispose() => Marshal.FreeHGlobal((IntPtr)_originAddress);

        public T NewEntity<T>() where T : IEntity, new()
        {
            var entity = new T();
            var boxed = (IEntity)entity;

            var members = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic); // Get all the raw pointers
            foreach (var member in members)
            {
                member.SetValue(boxed, (IntPtr)(_originAddress + _stackPointer));
                _stackPointer += _alignment;
            }

            boxed.BaseEntity.Id = _currentIndex++;
            return (T)boxed;
        }

        public void ResetStack()
        {
            _currentIndex = 0;
            _stackPointer = 0;
        }

        readonly uint _alignment;
        readonly byte* _originAddress;

        uint _stackPointer;
        int _currentIndex;
    }
}

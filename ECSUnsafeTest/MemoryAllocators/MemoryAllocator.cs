using System.Runtime.InteropServices;
using ECSUnsafeTest.Component;

namespace ECSUnsafeTest.MemoryAllocators
{
    public unsafe class MemoryAllocator
    {
        readonly byte* memory;
        int nextAvailableMemoryPosition = 0;

        public MemoryAllocator()
        {
            memory =(byte*)Marshal.AllocHGlobal(
                sizeof(Heading) * 2 +
                sizeof(Position) * 2
            ).ToPointer();
        }

        public ref T New<T>() where T : unmanaged
        {
            var address = memory + nextAvailableMemoryPosition;
            nextAvailableMemoryPosition += sizeof(T);


            return ref *(T*)address;
        }

        public ref T Get<T>(int position) where T : unmanaged
        {
            var address = memory + (position * sizeof(T));

            return ref *(T*)address;
        }
    }
}

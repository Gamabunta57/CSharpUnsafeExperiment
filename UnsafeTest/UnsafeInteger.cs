using System.Runtime.InteropServices;

namespace UnsafeTest
{
    public unsafe struct UnsafeInteger
    {
        public UnsafeInteger(int size) => array = (int*)Marshal.AllocHGlobal(size * sizeof(int)).ToPointer();

        public ComponentInt getValue(int index) => new ComponentInt(array + index);

        private int* array;
    }
}

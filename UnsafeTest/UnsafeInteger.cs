using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace UnsafeTest
{
    public struct UnsafeInteger
    {
        unsafe private int* array;

        unsafe public UnsafeInteger(int size) => array = (int*)Marshal.AllocHGlobal(size * sizeof(int)).ToPointer();

        public ComponentInt getComponent() => new ComponentInt();
        unsafe public void updateComponent(ref ComponentInt component, int index) => component.val = (array + index);

        unsafe public ComponentInt getValue(int index) => new ComponentInt(array + index);

        unsafe public void setValue(int index, int val) => new ComponentInt(array + index);
    }
}

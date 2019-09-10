using System;
using System.Collections.Generic;
using System.Text;

namespace UnsafeTest
{
    public unsafe struct ComponentInt
    {
        public int Value{
            get => *val;
            set => *val = value;
        }

        public ComponentInt(int* val) => this.val = val;

        public int* val;
    }
}

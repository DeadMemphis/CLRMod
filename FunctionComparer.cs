using System;
using System.Collections.Generic;

namespace CLEARSKIES
{
    // Token: 0x02000026 RID: 38
    internal class FunctionComparer<T> : System.Collections.Generic.IComparer<T>
    {
        // Token: 0x06000149 RID: 329 RVA: 0x00003017 File Offset: 0x00001217
        public FunctionComparer(System.Comparison<T> comparison)
        {
            this.c = System.Collections.Generic.Comparer<T>.Default;
            this.comparison = comparison;
        }

        // Token: 0x0600014A RID: 330 RVA: 0x00003031 File Offset: 0x00001231
        public int Compare(T x, T y)
        {
            return this.comparison(x, y);
        }

        // Token: 0x04000111 RID: 273
        private System.Collections.Generic.Comparer<T> c;

        // Token: 0x04000112 RID: 274
        private System.Comparison<T> comparison;
    }
}
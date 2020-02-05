using System;
using System.Collections.Generic;
using System.Text;

namespace 周易
{
    public sealed class 爻
    {
        internal 爻(int 位置, 阴阳 阴阳性质, string 爻辞)
        {
            this.爻题 = new 爻题(位置, 阴阳性质);
            this.爻辞 = 爻辞;
        }
        public string 爻辞 { get; }
        public 爻题 爻题 { get; }
    }
}

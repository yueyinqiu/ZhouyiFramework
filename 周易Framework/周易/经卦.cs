using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 周易
{
    public sealed partial class 经卦
    {
        internal 经卦(byte index, char 卦名, 卦画 卦画)
        {
            this.Index = index;
            this.卦名 = 卦名;
            阴阳[] 各爻阴阳 = 卦画.ToArray();
            this.初爻阴阳 = 各爻阴阳[0];
            this.中爻阴阳 = 各爻阴阳[1];
            this.上爻阴阳 = 各爻阴阳[2];
        }
        internal byte Index { get; }
        public char 卦名 { get; }
        public 卦画 卦画 => new 卦画(this.初爻阴阳, this.中爻阴阳, this.上爻阴阳);
        public 阴阳 上爻阴阳 { get; }
        public 阴阳 中爻阴阳 { get; }
        public 阴阳 初爻阴阳 { get; }
    }
}

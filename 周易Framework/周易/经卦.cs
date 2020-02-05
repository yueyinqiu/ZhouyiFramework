using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace 周易
{
    public sealed partial class 经卦
    {
        internal 经卦(byte index, char 卦名, 卦画 卦画)
        {
            this.index = index;
            this.卦名 = 卦名;
            阴阳[] 各爻阴阳 = 卦画.各爻阴阳.ToArray();
            初爻阴阳 = 各爻阴阳[0];
            中爻阴阳 = 各爻阴阳[1];
            上爻阴阳 = 各爻阴阳[2];
        }
        internal byte index { get; }
        public char 卦名 { get; }
        public 卦画 卦画 => new 卦画(初爻阴阳, 中爻阴阳, 上爻阴阳);
        public 阴阳 上爻阴阳 { get; }
        public 阴阳 中爻阴阳 { get; }
        public 阴阳 初爻阴阳 { get; }
    }
}

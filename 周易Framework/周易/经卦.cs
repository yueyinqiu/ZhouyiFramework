using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 周易
{
    /// <summary>
    /// Represents a trigram (a trigram is made up of any combination of three yin and yang lines) .
    /// </summary>
    public sealed partial class 经卦
    {
        internal 经卦(byte index, char 卦名, char 自然现象, 卦画 卦画)
        {
            this.Index = index;
            this.卦名 = 卦名;
            this.自然现象 = 自然现象;
            阴阳[] 各爻阴阳 = 卦画.ToArray();
            this.初爻阴阳 = 各爻阴阳[0];
            this.中爻阴阳 = 各爻阴阳[1];
            this.上爻阴阳 = 各爻阴阳[2];
        }
        internal byte Index { get; }
        public char 卦名 { get; }
        public char 自然现象 { get; }
        public 卦画 卦画 => new 卦画(this.初爻阴阳, this.中爻阴阳, this.上爻阴阳);
        public 阴阳 上爻阴阳 { get; }
        public 阴阳 中爻阴阳 { get; }
        public 阴阳 初爻阴阳 { get; }
    }
}

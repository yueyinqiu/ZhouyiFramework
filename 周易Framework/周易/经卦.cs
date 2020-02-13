using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 周易
{
    /// <summary>
    /// Represents a trigram.
    /// </summary>
    public sealed partial class 经卦
    {
        internal 经卦(int index, char 卦名, char 自然现象, 卦画 卦画)
        {
            this.Index = index;
            this.卦名 = 卦名;
            this.自然属性 = 自然现象;
            阴阳[] 各爻阴阳 = 卦画.ToArray();
            this.初爻阴阳 = 各爻阴阳[0];
            this.中爻阴阳 = 各爻阴阳[1];
            this.上爻阴阳 = 各爻阴阳[2];
        }
        internal int Index { get; }
        /// <summary>
        /// Get the trigram's name.
        /// </summary>
        public char 卦名 { get; }
        /// <summary>
        /// Get the nature attribute the trigram represents.
        /// </summary>
        public char 自然属性 { get; }
        /// <summary>
        /// Get the painting of the trigram.
        /// </summary>
        public 卦画 卦画 => new 卦画(false, this.初爻阴阳, this.中爻阴阳, this.上爻阴阳);
        /// <summary>
        /// Get the attribute (yin or yang) of the top (third) line.
        /// </summary>
        public 阴阳 上爻阴阳 { get; }
        /// <summary>
        /// Get the attribute (yin or yang) of the middle (second) line.
        /// </summary>
        public 阴阳 中爻阴阳 { get; }
        /// <summary>
        /// Get the attribute (yin or yang) of the bottom (first) line.
        /// </summary>
        public 阴阳 初爻阴阳 { get; }
    }
}

using System.Text;

namespace 周易
{
    /// <summary>
    /// Represents the title of a line.
    /// </summary>
    public sealed class 爻题
    {
        internal 爻题(int 位置, 阴阳 阴阳性质)
        {
            this.爻位置 = 位置;
            this.爻阴阳 = 阴阳性质;
        }
        /// <summary>
        /// The index of the line. This value is equal to <c> <see cref="爻位置"/> - 1 </c> .
        /// </summary>
        public int Index => this.爻位置 - 1;
        /// <summary>
        /// The position of the line.
        /// </summary>
        public int 爻位置 { get; }
        /// <summary>
        /// The attribute (yin or yang) of the line.
        /// </summary>
        public 阴阳 爻阴阳 { get; }
        /// <summary>
        /// Returns a string that represents the title.
        /// </summary>
        /// <returns>A string that represents the title.</returns>
        public override string ToString()
        {
            var nineOrSix = this.爻阴阳 == 阴阳.阳 ? "九" : "六";
            return this.爻位置 switch {
                1 => "初" + nineOrSix,
                2 => nineOrSix + "二",
                3 => nineOrSix + "三",
                4 => nineOrSix + "四",
                5 => nineOrSix + "五",
                _ => "上" + nineOrSix
            };
        }
    }
}

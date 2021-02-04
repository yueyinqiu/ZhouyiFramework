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
        /// The index of the line. This value is equal to (<see cref="爻位置"/> - 1).
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
            StringBuilder stringBuilder = new StringBuilder(2);
            stringBuilder.Append(this.爻阴阳 == 阴阳.阳 ? '九' : '六');
            switch (this.爻位置)
            {
                case 1:
                    stringBuilder.Insert(0, '初');
                    break;
                case 2:
                    stringBuilder.Append('二');
                    break;
                case 3:
                    stringBuilder.Append('三');
                    break;
                case 4:
                    stringBuilder.Append('四');
                    break;
                case 5:
                    stringBuilder.Append('五');
                    break;
                case 6:
                    stringBuilder.Insert(0, '上');
                    break;
            }
            return stringBuilder.ToString();
        }
    }
}

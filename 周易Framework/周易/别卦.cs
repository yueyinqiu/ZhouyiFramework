using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace 周易
{
    /// <summary>
    /// Represents a hexagram (the divinatory symbol) .
    /// </summary>
    public sealed partial class 别卦
    {
        internal 别卦(string 卦名, string 卦辞, string 用辞 = null, params 爻[] 各爻)
        {
            this.卦名 = 卦名;
            this.卦辞 = 卦辞;
            this.用辞 = 用辞;
            this.初爻 = 各爻[0];
            this.二爻 = 各爻[1];
            this.三爻 = 各爻[2];
            this.四爻 = 各爻[3];
            this.五爻 = 各爻[4];
            this.上爻 = 各爻[5];
        }
        /// <summary>
        /// Get the painting of the hexagram.
        /// </summary>
        public 卦画 卦画 =>
            new 卦画(this.各爻.ToArray());
        /// <summary>
        /// Get the name of the hexagram.
        /// </summary>
        public string 卦名 { get; }
        /// <summary>
        /// Get the the meaning of the hexagram.
        /// </summary>
        public string 卦辞 { get; }
        /// <summary>
        /// Get the the words after the word "yong" which only exists in "qian" (heaven) and "kun" (earth).
        /// It will returns null if the hexagram isn't "qian" or "kun".
        /// </summary>
        public string 用辞 { get; }

        public IEnumerable<爻> 各爻
        {
            get
            {
                yield return 初爻;
                yield return 二爻;
                yield return 三爻;
                yield return 四爻;
                yield return 五爻;
                yield return 上爻;
            }
        }

        #region 客卦
        public 经卦 客卦 => 经卦.获取经卦(new 卦画(
            this.四爻.爻题.爻阴阳,
            this.五爻.爻题.爻阴阳,
            this.上爻.爻题.爻阴阳));
        public 爻 上爻 { get; }
        public 爻 五爻 { get; }
        public 爻 四爻 { get; }
        #endregion

        #region 主卦
        public 经卦 主卦 => 经卦.获取经卦(new 卦画(
            this.初爻.爻题.爻阴阳,
            this.二爻.爻题.爻阴阳,
            this.三爻.爻题.爻阴阳));
        public 爻 三爻 { get; }
        public 爻 二爻 { get; }
        public 爻 初爻 { get; }
        #endregion
    }
}

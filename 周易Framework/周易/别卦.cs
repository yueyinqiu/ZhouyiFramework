using System;
using System.Resources;

namespace 周易
{
    public sealed partial class 别卦
    {
        internal 别卦(string 卦名, string 卦辞, string 用辞 = null, params 爻[] 各爻)
        {
            this.卦名 = 卦名;
            this.卦辞 = 卦辞;
            this.用辞 = 用辞;
            this.各爻 = 各爻;
        }

        public 卦画 卦画 =>
            new 卦画(this.各爻);
        public string 卦名 { get; }
        public string 卦辞 { get; }
        public string 用辞 { get; }

        private readonly 爻[] 各爻;

        public 爻 获取爻(int index)
            => this.各爻[index];
        public 爻 获取爻(爻题 爻题)
            => this.各爻[爻题.爻位置 - 1];

        #region 客卦
        public 经卦 客卦 => 经卦.获取经卦(new 卦画(
            this.四爻.爻题.爻阴阳,
            this.五爻.爻题.爻阴阳,
            this.上爻.爻题.爻阴阳));
        public 爻 上爻 => this.各爻[5];
        public 爻 五爻 => this.各爻[4];
        public 爻 四爻 => this.各爻[3];
        #endregion

        #region 主卦
        public 经卦 主卦 => 经卦.获取经卦(new 卦画(
            this.初爻.爻题.爻阴阳,
            this.二爻.爻题.爻阴阳,
            this.三爻.爻题.爻阴阳));
        public 爻 三爻 => this.各爻[2];
        public 爻 二爻 => this.各爻[1];
        public 爻 初爻 => this.各爻[0];
        #endregion
    }
}

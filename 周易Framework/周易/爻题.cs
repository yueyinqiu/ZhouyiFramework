using System;
using System.Collections.Generic;
using System.Text;

namespace 周易
{
    public sealed class 爻题
    {
        internal 爻题(int 位置, 阴阳 阴阳性质)
        {
            this.爻位置 = 位置;
            this.爻阴阳 = 阴阳性质;
        }
        public int Index => this.爻位置 - 1;
        public int 爻位置 { get; }
        public 阴阳 爻阴阳 { get; }

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

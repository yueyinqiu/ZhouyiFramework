﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 周易
{
    public sealed class 卦画
    {
        public IEnumerable<阴阳> 各爻阴阳 { get; private set; }
        private 卦画() { }
        internal 卦画(params 阴阳[] 各爻阴阳)
        {
            this.各爻阴阳 = 各爻阴阳;
        }
        internal 卦画(params 爻[] 各爻)
        {
            this.各爻阴阳 =
                from 爻 in 各爻
                select 爻.爻题.爻阴阳;
        }
        public byte ToByte()
        {
            byte result = 0;
            byte n = (byte)Math.Pow(2, this.各爻阴阳.Count());
            result += n;
            n /= 2;
            foreach (var 阴阳 in this.各爻阴阳)
            {
                if (阴阳 == 阴阳.阳)
                {
                    result += n;
                }
                n /= 2;
            }
            return result;
        }
        public static 卦画 FromByte(byte b)
        {
            BitArray bitArray = new BitArray(new byte[] { b });
            List<阴阳> r = new List<阴阳>(6);
            bool notStarted = true;
            for (int i = 0; i < 8; i++)
            {
                var bit = bitArray[i + 2];
                if (notStarted)
                {
                    if (bit)
                        notStarted = false;
                    continue;
                }
                r.Add(bit ? 阴阳.阳 : 阴阳.阴);
            }
            return new 卦画() {
                各爻阴阳 = r
            };
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(6);
            foreach (var 阴阳 in this.各爻阴阳)
            {
                stringBuilder.Append(阴阳 == 阴阳.阳 ? 1 : 0);
            }
            return stringBuilder.ToString();
        }
        public static 卦画 Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            List<阴阳> r = new List<阴阳>(6);
            foreach (var c in s)
            {
                if (c < '0' || c > '9')
                {
                    throw new ArgumentException($"{nameof(s)}的内容不正确。只允许出现数字字符。", nameof(s));
                }
                r.Add(c / 2 == 0 ? 阴阳.阴 : 阴阳.阳);
            }
            return new 卦画() {
                各爻阴阳 = r
            };
        }
    }
}

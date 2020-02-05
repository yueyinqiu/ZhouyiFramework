using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace 周易
{
    public sealed partial class 经卦
    {
        public static 经卦 获取经卦(char 卦名)
        {
            byte? index = null;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.经卦卦名对照)))
            using(StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            {
                for (byte i = 0; i < 8; i++)
                {
                    if ((char)sr.Read() == 卦名)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (!index.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(卦名));
            }
            return new 经卦(
                index.Value, 卦名, 获取卦画(index.Value));
        }
        public static 经卦 获取经卦(卦画 卦画)
        {
            byte? index = null;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.经卦卦画对照)))
            {
                var b = 卦画.ToByte();
                for (byte i = 0; i < 8; i++)
                {
                    if(ms.ReadByte() == b)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if(!index.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(卦画));
            }
            return new 经卦(
                index.Value, 获取卦名(index.Value), 卦画);
        }

        internal static char 获取卦名(byte index)
        {
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.经卦卦名对照)))
            {
                ms.Position = index * 2;
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    return (char)sr.Read();
                }
            }
        }
        internal static 卦画 获取卦画(byte index)
        {
            byte b;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.经卦卦画对照)))
            {
                ms.Position = index;
                b = (byte)ms.ReadByte();
            }
            return 卦画.FromByte(b);
        }
    }
}

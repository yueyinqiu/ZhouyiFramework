using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 周易
{
    public sealed partial class 别卦
    {
        public static 别卦 获取别卦(string 卦名)
        {
            byte? index = null;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.别卦卦名对照)))
            {
                for (byte r = 0; r < 64; r++)
                {
                    List<byte> bytes = new List<byte>(8);
                    for (; ; )
                    {
                        var br = ms.ReadByte();
                        if (br == 30)
                        {
                            break;
                        }
                        bytes.Add((byte)br);
                    }
                    var str = Encoding.UTF8.GetString(bytes.ToArray());
                    if (str == 卦名)
                    {
                        index = r;
                    }
                }
            }
            if (!index.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(卦名));
            }
            return 获取别卦(index.Value, 卦名);
        }
        public static 别卦 获取别卦(经卦 主卦, 经卦 客卦)
        {
            byte index;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.别卦经卦对照)))
            {
                ms.Position = 客卦.Index * 8 + 主卦.Index;
                index = (byte)ms.ReadByte();
            }
            return 获取别卦(index, 主卦, 客卦);
        }
        public static 别卦 获取别卦(卦画 卦画)
        {
            if (卦画.爻数 != 6)
            {
                throw new ArgumentException($"{nameof(卦画)}不正确。应该为六爻。", nameof(卦画));
            }
            经卦 主卦 = 经卦.获取经卦(new 卦画(卦画[0], 卦画[1], 卦画[2]));
            经卦 客卦 = 经卦.获取经卦(new 卦画(卦画[3], 卦画[4], 卦画[5]));
            return 获取别卦(主卦, 客卦);
        }

        private static 别卦 获取别卦(byte index, 经卦 主卦, 经卦 客卦)
        {
            string 卦名;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.别卦卦名对照)))
            {
                for (byte r = 0; r < index; r++)
                {
                    for (; ; )
                    {
                        var br = ms.ReadByte();
                        if (br == 30)
                        {
                            break;
                        }
                    }
                }
                List<byte> bytes = new List<byte>(8);
                for (; ; )
                {
                    var br = ms.ReadByte();
                    if (br == 30)
                    {
                        break;
                    }
                    bytes.Add((byte)br);
                }
                卦名 = Encoding.UTF8.GetString(bytes.ToArray());
            }

            string 卦辞;
            爻[] 各爻 = new 爻[6];
            using (var str = Resource.ResourceManager.GetStream($"_{index}"))
            {
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 31)
                        {
                            break;
                        }
                        bytes.Add((byte)br);
                    }
                    卦辞 = Encoding.UTF8.GetString(bytes.ToArray());
                }
                for (byte i = 0; i < 6; i++)
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 30)
                        {
                            break;
                        }
                        bytes.Add((byte)br);
                    }
                    阴阳 阴阳;
                    if (i < 3)
                    {
                        阴阳 = 主卦.卦画[i];
                    }
                    else
                    {
                        阴阳 = 客卦.卦画[i - 3];
                    }
                    各爻[i] = new 爻(i, 阴阳, Encoding.UTF8.GetString(bytes.ToArray()));
                }
            }

            string 用辞 = null;
            if (index == 0)
            {
                用辞 = "见群龙无首，吉。";
            }
            if (index == 1)
            {
                用辞 = "利永贞。";
            }
            return new 别卦(卦名, 卦辞, 用辞, 各爻);
        }
        private static 别卦 获取别卦(byte index, string 卦名)
        {
            经卦 主卦 = null;
            经卦 客卦 = null;
            using (var ms = Resource.ResourceManager.GetStream(nameof(Resource.别卦经卦对照)))
            {
                for (byte i = 0; i < 64; i++)
                {
                    if (ms.ReadByte() == index)
                    {
                        主卦 = 经卦.获取经卦((byte)(i / 6));
                        客卦 = 经卦.获取经卦((byte)(i % 6));
                    }
                }
            }

            string 卦辞;
            爻[] 各爻 = new 爻[6];
            using (var str = Resource.ResourceManager.GetStream($"_{index}"))
            {
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 31)
                        {
                            break;
                        }
                        bytes.Add((byte)br);
                    }
                    卦辞 = Encoding.UTF8.GetString(bytes.ToArray());
                }
                for (byte i = 0; i < 6; i++)
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 30)
                        {
                            break;
                        }
                        bytes.Add((byte)br);
                    }
                    阴阳 阴阳;
                    if (i < 3)
                    {
                        阴阳 = 主卦.卦画[i];
                    }
                    else
                    {
                        阴阳 = 客卦.卦画[i - 3];
                    }
                    各爻[i] = new 爻(i, 阴阳, Encoding.UTF8.GetString(bytes.ToArray()));
                }
            }

            string 用辞 = null;
            if (index == 0)
            {
                用辞 = "见群龙无首，吉。";
            }
            if (index == 1)
            {
                用辞 = "利永贞。";
            }
            return new 别卦(卦名, 卦辞, 用辞, 各爻);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace 周易
{
    public sealed partial class 别卦
    {
        /// <summary>
        /// Get the names of all hexagrams.
        /// This is arranged according to I Ching (I don't want to list it, buy the book and you will know it).
        /// </summary>
        public static IEnumerable<string> 全部别卦卦名
        {
            get
            {
                using var ms = new MemoryStream(Properties.Resources.别卦卦名对照);
                for (int r = 0; r < 64; r++)
                {
                    List<byte> bytes = new List<byte>(8);
                    for (; ; )
                    {
                        var br = ms.ReadByte();
                        if (br == 30)
                            break;
                        bytes.Add((byte)br);
                    }
                    var str = Encoding.UTF8.GetString(bytes.ToArray());
                    yield return str;
                }
                yield break;
            }
        }
        /// <summary>
        /// Get a hexagram from its name.
        /// </summary>
        /// <param name="卦名">
        /// The name.
        /// The value shouldn't end with '卦'.
        /// </param>
        /// <param name="result">The hexagram.</param>
        /// <returns>A value indicates whether the hexagram has been found or not.</returns>
        /// <exception cref="ArgumentOutOfRangeException">No such hexagram was found.</exception>
        /// <exception cref="ArgumentNullException"> <paramref name="卦名"/> is null.</exception>
        public static bool 获取别卦(string 卦名, [NotNullWhen(true)] out 别卦? result)
        {
            int index = -1;
            using (var ms = new MemoryStream(Properties.Resources.别卦卦名对照))
            {
                for (int r = 0; r < 64; r++)
                {
                    List<byte> bytes = new List<byte>(8);
                    for (; ; )
                    {
                        var br = ms.ReadByte();
                        if (br == 30)
                            break;
                        bytes.Add((byte)br);
                    }
                    var str = Encoding.UTF8.GetString(bytes.ToArray());
                    if (str == 卦名)
                    {
                        index = r;
                        break;
                    }
                }
            }
            if (index == -1)
            {
                result = null;
                return false;
            }
            result = 获取别卦(index, 卦名);
            return true;
        }
        /// <summary>
        /// Get a hexagram from its two trigrams.
        /// </summary>
        /// <param name="主卦">The lower trigram.</param>
        /// <param name="客卦">The upper trigram.</param>
        /// <param name="result">The hexagram.</param>
        /// <returns>Always <c>true</c> .</returns>
        /// <exception cref="ArgumentNullException">At least one argument is null.</exception>
        public static bool 获取别卦(经卦 主卦, 经卦 客卦, [NotNullWhen(true)] out 别卦? result)
        {
            if (主卦 is null)
                throw new ArgumentNullException(nameof(主卦));
            if (客卦 is null)
                throw new ArgumentNullException(nameof(客卦));
            int index;
            using (var ms = new MemoryStream(Properties.Resources.别卦经卦对照))
            {
                ms.Position = 客卦.Index * 8 + 主卦.Index;
                index = ms.ReadByte();
            }
            result = 获取别卦(index, 主卦, 客卦);
            return true;
        }
        /// <summary>
        /// Get a hexagram from its painting.
        /// </summary>
        /// <param name="卦画">
        /// The painting.
        /// Its property <see cref="卦画.爻数"/> should be 6.
        /// </param>
        /// <param name="result">The hexagram.</param>
        /// <returns>A value indicates whether the hexagram has been found or not.</returns>
        /// <exception cref="ArgumentException"> <see cref="卦画.爻数"/> of <paramref name="卦画"/> isn't 6.</exception>
        /// <exception cref="ArgumentNullException"> <paramref name="卦画"/> is null.</exception>
        public static bool 获取别卦(卦画 卦画, [NotNullWhen(true)] out 别卦? result)
        {
            if (卦画 is null)
                throw new ArgumentNullException(nameof(卦画));
            if (卦画.爻数 != 6)
            {
                result = null;
                return false;
            }

            经卦.获取经卦(new 卦画(false, 卦画[0], 卦画[1], 卦画[2]), out 经卦? 主卦);
            Debug.Assert(主卦 is not null);

            经卦.获取经卦(new 卦画(false, 卦画[3], 卦画[4], 卦画[5]), out 经卦? 客卦);
            Debug.Assert(客卦 is not null);

            return 获取别卦(主卦, 客卦, out result);
        }

        private static 别卦 获取别卦(int index, 经卦 主卦, 经卦 客卦)
        {
            string 卦名;
            using (var ms = new MemoryStream(Properties.Resources.别卦卦名对照))
            {
                for (byte r = 0; r < index; r++)
                {
                    for (; ; )
                    {
                        var br = ms.ReadByte();
                        if (br == 30)
                            break;
                    }
                }
                List<byte> bytes = new List<byte>(8);
                for (; ; )
                {
                    var br = ms.ReadByte();
                    if (br == 30)
                        break;
                    bytes.Add((byte)br);
                }
                卦名 = Encoding.UTF8.GetString(bytes.ToArray());
            }

            string 卦辞;
            爻[] 各爻 = new 爻[6];

            var gindex = Properties.Resources.ResourceManager.GetObject($"G{index}") as byte[];
            Debug.Assert(gindex is not null);
            using (var str = new MemoryStream(gindex))
            {
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 31)
                            break;
                        bytes.Add((byte)br);
                    }
                    卦辞 = Encoding.UTF8.GetString(bytes.ToArray());
                }
                for (int i = 0; i < 6; i++)
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 30)
                            break;
                        bytes.Add((byte)br);
                    }
                    阴阳 阴阳;
                    if (i < 3)
                        阴阳 = 主卦.卦画[i];
                    else
                        阴阳 = 客卦.卦画[i - 3];
                    各爻[i] = new 爻(i + 1, 阴阳, Encoding.UTF8.GetString(bytes.ToArray()));
                }
            }

            string? 用辞 = index switch {
                0 => Properties.Resources.乾卦用辞,
                1 => Properties.Resources.坤卦用辞,
                _ => null
            };

            return new 别卦(index, 卦名, 卦辞, 用辞, 各爻);
        }
        private static 别卦 获取别卦(int index, string 卦名)
        {
            经卦? 主卦 = null;
            经卦? 客卦 = null;
            using (var ms = new MemoryStream(Properties.Resources.别卦经卦对照))
            {
                for (int i = 0; i < 64; i++)
                {
                    if (ms.ReadByte() == index)
                    {
                        主卦 = 经卦.获取经卦(i % 8);
                        客卦 = 经卦.获取经卦(i / 8);
                    }
                }
            }

            Debug.Assert(主卦 is not null);
            Debug.Assert(客卦 is not null);

            string 卦辞;
            爻[] 各爻 = new 爻[6];

            var gindex = Properties.Resources.ResourceManager.GetObject($"G{index}") as byte[];
            Debug.Assert(gindex is not null);
            using (var str = new MemoryStream(gindex))
            {
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 31)
                            break;
                        bytes.Add((byte)br);
                    }
                    卦辞 = Encoding.UTF8.GetString(bytes.ToArray());
                }
                for (int i = 0; i < 6; i++)
                {
                    List<byte> bytes = new List<byte>(100);
                    for (; ; )
                    {
                        var br = str.ReadByte();
                        if (br == 30)
                            break;
                        bytes.Add((byte)br);
                    }
                    阴阳 阴阳;
                    if (i < 3)
                        阴阳 = 主卦.卦画[i];
                    else
                        阴阳 = 客卦.卦画[i - 3];
                    各爻[i] = new 爻(i + 1, 阴阳, Encoding.UTF8.GetString(bytes.ToArray()));
                }
            }

            string? 用辞 = index switch {
                0 => Properties.Resources.乾卦用辞,
                1 => Properties.Resources.坤卦用辞,
                _ => null
            };

            return new 别卦(index, 卦名, 卦辞, 用辞, 各爻);
        }
    }
}

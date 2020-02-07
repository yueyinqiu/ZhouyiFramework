using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    internal class 别卦生成器
    {
        public static void Start()
        {
            for (byte i = 0; i < 64; i++)
            {
                using (var fs = new FileStream($"qaq/G{i}", FileMode.Create))
                using (var sr = new StreamWriter(fs))
                {
                    GetInfoOfOne(fs, sr);
                }
            }
        }
        private static void GetInfoOfOne(FileStream fs, StreamWriter sr)
        {
            卦辞(sr);
            fs.Write(new byte[] { 31 }, 0, 1);
            fs.Flush();
            for (int i = 0; i < 6; i++)
            {
                爻辞(sr);
                fs.Write(new byte[] { 30 }, 0, 1);
                fs.Flush();
            }
            到底();
        }
        private static void 卦辞(StreamWriter sr)
        {
            for (; ; )
            {
                var inp = Console.ReadLine();
                inp = Regex.Replace(inp, @"\s", "");
                if ((!string.IsNullOrWhiteSpace(inp)) && inp[0] == '第')
                {
                    inp = Console.ReadLine();
                    inp = Regex.Replace(inp, @"\s", "");
                    for (; string.IsNullOrWhiteSpace(inp);)
                    {
                        inp = Console.ReadLine();
                        inp = Regex.Replace(inp, @"\s", "");
                    }
                    inp = inp.Remove(0, inp.IndexOf('：') + 1);
                    sr.Write(inp);
                    sr.Flush();
                    break;
                }
            }
        }
        private static void 爻辞(StreamWriter sr)
        {
            for (; ; )
            {
                var inp = Console.ReadLine();
                inp = Regex.Replace(inp, @"\s", "");
                if ((!string.IsNullOrWhiteSpace(inp)) && (inp[0] == '初' || inp[0] == '上' ||
                    inp[1] == '二' || inp[1] == '三' ||
                    inp[1] == '四' || inp[1] == '五'))
                {
                    inp = inp.Remove(0, 3);
                    sr.Write(inp);
                    sr.Flush();
                    break;
                }
            }
        }
        private static void 到底()
        {
            for (; ; )
            {
                var inp = Console.ReadLine();
                inp = Regex.Replace(inp, @"\s", "");
                if ((!string.IsNullOrWhiteSpace(inp)) && inp[0] == '【')
                {
                    break;
                }
            }
        }
    }
}

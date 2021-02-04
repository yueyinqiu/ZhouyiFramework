using System;
using System.Diagnostics;
using System.Linq;
using 周易;

namespace Tester
{
    internal class Program
    {
        private static readonly Random random = new Random();
        private static void Main()
        {
            Console.WriteLine("Press Enter.");
            {
                Console.ReadLine();
                Console.WriteLine(
                    "This example shows how to get a hexagram by its name.");

                _ = 别卦.获取别卦("乾", out 别卦 乾卦);
                Console.WriteLine("乾卦");
                Console.WriteLine($"用九：{乾卦.用辞}");
                Console.WriteLine(乾卦.上爻.爻辞);

                Console.WriteLine();
            }
            {
                Console.ReadLine();
                Console.WriteLine(
                    "This example shows how to get a hexagram by two trigrams.");

                _ = 经卦.获取经卦('兑', out 经卦 兑卦);
                _ = 经卦.获取经卦('艮', out 经卦 艮卦);
                _ = 别卦.获取别卦(兑卦, 艮卦, out 别卦 兑下艮上);
                Console.WriteLine($"兑下艮上 {兑下艮上.卦名}卦");
                Console.WriteLine(兑下艮上.卦辞);
                Console.WriteLine($"兑卦卦画：{兑下艮上.主卦.卦画}");

                Console.WriteLine();
            }
            {
                Console.ReadLine();
                Console.WriteLine(
                    "This example shows the high degree of freedom when using the paintings.");

                _ = 经卦.获取经卦('兑', out 经卦 经卦兑卦);
                卦画 经卦兑卦卦画 = 经卦兑卦.卦画;
                卦画 别卦兑卦卦画 = new 卦画(经卦兑卦卦画.Concat(经卦兑卦卦画).ToArray());
                _ = 别卦.获取别卦(别卦兑卦卦画, out 别卦 别卦兑卦);
                Console.WriteLine($"兑卦：{别卦兑卦.卦辞}");

                Console.WriteLine();
            }
            {
                Console.ReadLine();
                Console.WriteLine(
                    "This example shows the usage of the method '周易.别卦.ToString()'.");
                
                int rand = random.Next(0, 63);
                string 卦名 = 别卦.全部别卦卦名.ToArray()[rand];
                _ = 别卦.获取别卦(卦名, out 别卦 卦);
                Console.WriteLine(卦.ToString());

                Console.WriteLine();
            }
        }
    }
}

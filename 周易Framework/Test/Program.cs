using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using 周易;

namespace Test
{
    internal class Program
    {
        static Random random = new Random();
        private static void Main()
        {
            {
                Console.WriteLine("This example shows how to get a hexagram by its name.");
                别卦 乾卦 = 别卦.获取别卦("乾");

                Console.WriteLine($"乾卦");
                Console.WriteLine($"用九：{乾卦.用辞}");
                Console.WriteLine(乾卦.上爻.爻辞);
                Console.WriteLine();
            }
            {
                Console.WriteLine("This example shows how to get a hexagram by two trigrams.");
                经卦 兑卦 = 经卦.获取经卦('兑');
                经卦 艮卦 = 经卦.获取经卦('艮');
                别卦 兑下艮上 = 别卦.获取别卦(兑卦, 艮卦);

                Console.WriteLine($"兑下艮上 {兑下艮上.卦名}卦");
                Console.WriteLine(兑下艮上.卦辞);
                Console.WriteLine($"兑卦卦画：{兑下艮上.主卦.卦画}");
                Console.WriteLine();
            }
            {
                Console.WriteLine("This example shows the high degree of freedom when using the paintings.");
                经卦 经卦兑卦 = 经卦.获取经卦('兑');
                卦画 经卦兑卦卦画 = 经卦兑卦.卦画;
                卦画 别卦兑卦卦画 = 卦画.Parse(经卦兑卦卦画.ToString() + 经卦兑卦卦画.ToString());
                // The return of the method "经卦兑卦卦画.ToString()" can be known 
                // through the output of the previous example.
                别卦 别卦兑卦 = 别卦.获取别卦(别卦兑卦卦画);
                Console.WriteLine($"兑卦：{别卦兑卦.卦辞}");
                Console.WriteLine();
            }
            {
                Console.WriteLine("This example shows the usage of the method \"周易.别卦.ToString()\".");
                int rand = random.Next(0, 63);
                string 卦名 = 别卦.全部别卦卦名.ToArray()[rand];
                别卦 卦 = 别卦.获取别卦(卦名);
                Console.WriteLine(卦.ToString());
                Console.ReadLine();
            }
        }
    }
}

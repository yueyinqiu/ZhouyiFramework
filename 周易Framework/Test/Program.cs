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
        private static void Main()
        {
            Random r = new Random();
            {
                别卦 乾卦 = 别卦.获取别卦("乾");
                Console.WriteLine($"乾卦");
                Console.WriteLine($"用九：{乾卦.用辞}");
                Console.WriteLine(乾卦.上爻.爻辞);
            }
            Console.WriteLine();
            {
                经卦 兑卦 = 经卦.获取经卦('兑');
                经卦 艮卦 = 经卦.获取经卦('艮');
                别卦 兑下艮上 = 别卦.获取别卦(兑卦, 艮卦);
                Console.WriteLine($"兑下艮上 {兑下艮上.卦名}卦");
                Console.WriteLine(兑下艮上.卦辞);
                Console.WriteLine($"兑卦卦画：{兑下艮上.主卦.卦画}");
            }
            Console.WriteLine();
            {
                经卦 经卦兑卦 = 经卦.获取经卦('兑');
                卦画 经卦兑卦卦画 = 经卦兑卦.卦画;
                卦画 别卦兑卦卦画 = 卦画.Parse(经卦兑卦卦画.ToString() + 经卦兑卦卦画.ToString());
                别卦 别卦兑卦 = 别卦.获取别卦(别卦兑卦卦画);
                Console.WriteLine($"兑卦：{别卦兑卦.卦辞}");
            }
            Console.WriteLine();
            {
                int rand = r.Next(0, 63);
                Console.WriteLine(rand);
                string 卦名 = 别卦.全部别卦卦名.ToArray()[rand];
                别卦 卦 = 别卦.获取别卦(卦名);
                Console.WriteLine($"{卦.卦名}卦 {卦.客卦.卦名}上{卦.主卦.卦名}下");
                Console.WriteLine(卦.卦辞);
                foreach(var 爻 in 卦.各爻)
                {
                    Console.WriteLine($"{爻.爻题}：{爻.爻辞}");
                }
            }
        }
    }
}

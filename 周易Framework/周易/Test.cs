using System;
using System.Collections.Generic;
using System.Text;

namespace 周易
{
    class Test
    {
        private static void Main()
        {
            经卦 兑卦 = 经卦.获取经卦('兑');
            经卦 艮卦 = 经卦.获取经卦('艮');
            别卦 兑下艮上 = 别卦.获取别卦(兑卦, 艮卦);
            Console.WriteLine($"{兑下艮上.卦名}：兑下艮上。{兑下艮上.卦辞}");
        }
    }
}

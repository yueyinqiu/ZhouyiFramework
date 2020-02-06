﻿using System;
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
            经卦 兑卦 = 经卦.获取经卦('兑');
            经卦 艮卦 = 经卦.获取经卦('艮');
            别卦 兑下艮上 = 别卦.获取别卦(兑卦, 艮卦);
            Console.WriteLine($"{兑下艮上.卦名}：兑下艮上。{兑下艮上.卦辞}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace baithaynoi
{
    public class baitap
    {
 public void bai1(int x , int n)
    {
        int kq = 1;
        int sovonglap = 0;
        for(int i = 0; i < n; i++)
        {
                kq = kq * x;
                if(i == 0)
                {
                    continue;
                }else if(i == 1)
                {
                    sovonglap = 1;
                    continue;
                }
                else if(i == 2) 
                {
                    sovonglap = 2;
                    continue;
                }
                else
                {
                    continue;
                }
           
        }
          
            Console.WriteLine("ket qua la " + kq);
            Console.WriteLine("so vong lap " + sovonglap);
    }
      
    }
   
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            baitap bt = new baitap();
            bt.bai1(x, n);
            Console.ReadLine();
        }
    }
}

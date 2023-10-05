using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapxep
{
    public class hihi {
        public void nhapmang(int n , int[] arr)
        {
         for(int i = 0; i < n; i++)
            {
                Console.WriteLine("Nhap arr["+i+"] = ");
               arr[i] = int.Parse(Console.ReadLine());
            }         
        }
        public int TimkiemNhiPhan(int n , int[] arr , int x)
        {
            int left = 0;
            int right = n - 1;
           
           
            while (left <= right )
            { int mid = (left + right) / 2;
                if (arr[mid] == x)
                {
                   return mid;
       
                }
                 else if (arr[mid] > x){
                    right = mid - 1 ;
                }
                else
                {
                    left = mid + 1 ;
                }
            }
            return -1;
        }
        public void sapxepchon(int n , int[] arr )
        {
            for(int i = 0; i < n-1; i++)
            {
                int imin = i;
                for(int j = i+1 ; j < n; j++)
                {
                    if (arr[imin] > arr[j])
                    {
                        imin = j;
            
                    }  
                }
                int tam = arr[imin];
                arr[imin] = arr[i];
                arr[i] = tam;
            }

        }
        public void sapxepnoibotmax(int n , int[] arr)
        {
            for(int i = n-1; i > 0 ; i--)
            {
                for(int j = 0; j < i ; j++)
                {
                    if (arr[j] > arr[j+1])
                    {
                        int tam = arr[j];
                        arr[j] = arr[j+1];
                        arr[j+1] = tam;
                    }
                }
            }
        }
        public void xuatmang(int n , int[] arr)
        {
            for(int i = 0; i < n ; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }

   
    internal class Program
    {
        static void Main(string[] args)
        {
            hihi h = new hihi();
            Console.WriteLine("Nhap so Phan Tu cua mang ");
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n]; 
            h.nhapmang(n, arr);
            h.sapxepchon(n, arr);
            //h.sapxepnoibotmax(n,arr);
            //Console.WriteLine(h.TimkiemNhiPhan(n, arr, 3));
            h.xuatmang(n,arr);
            Console.ReadLine();
        }
    }
}

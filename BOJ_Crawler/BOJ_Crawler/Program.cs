using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOJ_Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            BOJCrawler boj = new BOJCrawler();
            Console.Write("BOJ id : ");
            string id = Console.ReadLine();
            BOJCrawler.Info info = boj.get_user_info(id);
            if(info == null)
            {
                Console.WriteLine("존재하지 않는 BOJ id 입니다.");
            }
            else
            {
                Console.WriteLine("rank : " + info.rank);
                Console.WriteLine("solved : " + info.solved);
                Console.WriteLine("submitted : " + info.submitted);
                Console.WriteLine("AC : " + info.AC);
                Console.WriteLine("PE : " + info.PE);
                Console.WriteLine("WA : " + info.WA);
                Console.WriteLine("TLE : " + info.TLE);
                Console.WriteLine("MLE : " + info.MLE);
                Console.WriteLine("OLE : " + info.OLE);
                Console.WriteLine("RE : " + info.RE);
                Console.WriteLine("CE : " + info.CE);
                Console.Write("푼 문제\n");
                for (int j = 0; j < info.solved_list.Length; j++)
                    Console.Write(info.solved_list[j] + ", ");
                Console.Write("\n시도했지만 풀지 못한 문제\n");
                for (int j = 0; j < info.tried_list.Length; j++)
                    Console.Write(info.tried_list[j] + ", ");
            }
            Console.ReadLine();
        }
    }
}

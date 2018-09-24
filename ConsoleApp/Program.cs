using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositoryTweets repo = new RepositoryTweets();

            Thread thread = new Thread(RepositoryTweets.GetTweets);
            thread.Start();
            //Console.WriteLine("The no. of tweets :" + RepositoryTweets.GetCount());

            Console.ReadKey();
        }
    }
}

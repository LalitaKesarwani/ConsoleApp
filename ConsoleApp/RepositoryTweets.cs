using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class RepositoryTweets
    {
        static Dictionary<string, ModelClass> dict;
        static int Countval = 0;
        public static void GetTweets()
        {
            DateTime startDate = new DateTime(2016, 1, 1, 00, 00, 00,
                                    DateTimeKind.Utc);

            DateTime endDate = new DateTime(2017, 12, 31, 23, 59, 59,
                                    DateTimeKind.Utc);
            int year = 2016;
            while (startDate <= endDate)

            {
                //-------------------------- Making call to API---------------------------------------
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                client.QueryString.Add("startDate", startDate
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
                client.QueryString.Add("endDate", endDate
                    .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
                string result = client.DownloadString("https://badapi.iqvia.io/api/v1/Tweets");
                List<ModelClass> md = JsonConvert.DeserializeObject<List<ModelClass>>(result);
                int count = md.Count();
                DateTime date1 = Convert.ToDateTime(md[count - 1].Stamp);
                int day = date1.Day;
                int month = date1.Month;
                year = date1.Year;

                foreach (ModelClass m in md)
                {
                    //Thread thread = new Thread();
                    AddToDictionary(m);
                }

                //Console.WriteLine("Another Set starts");
                //string date1 = (Convert.ToDateTime(md[99].stamp)).ToShortDateString();

                startDate = new DateTime(year, month, day, 00, 00, 00,
                                        DateTimeKind.Utc);
                endDate = new DateTime(2017, 12, 31, 23, 59, 59,
                                    DateTimeKind.Utc);

            }
        }

        public static async void AddToDictionary(ModelClass md)
        {
            // ---------------------Storing the Tweet object into dictionary to remove dupicates---------------------------------
            dict = new Dictionary<string, ModelClass>();
            List<string> lst = new List<string>();
            lst = dict.Keys.ToList();
            if (lst.Contains(md.Id))
            {

            }

            else
            {
                //-------------------If any tweet with key is already present it will not get added into dictionary else it will get added-----------
                dict.Add(md.Id, md);
                await PrintTweet(md);
            }
        }

        public static async Task PrintTweet(ModelClass tweet)
        {
            Console.WriteLine("Tweet Text is : " + tweet.Text);
            Console.WriteLine("");
            Console.WriteLine("Tweeted at : " + Convert.ToDateTime(tweet.Stamp));
            Console.WriteLine("________________________________________________________________________________________________");
            Console.WriteLine("");
        }
    }
}

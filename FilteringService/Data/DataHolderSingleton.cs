using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Business;

namespace Data
{
    public class DataHolderSingleton
    {
        private static DataHolderSingleton instance;
        private static Dictionary<string, string> abbr = new Dictionary<string, string>();
        private static List<Sms> smsList = new List<Sms>();
        private static List<string> quarantineUrl = new List<string>();
        private static List<Email> emailList = new List<Email>();
        private static List<Tweet> tweetList = new List<Tweet>();


        private DataHolderSingleton() { }

        public static DataHolderSingleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataHolderSingleton();
                return instance;
            }
        }

        public void readAbbr()
        {
            using (var reader = new StreamReader(@"../../../Data/abbr.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    abbr.Add(values[0], values[1]);
                }
            }
        }

        public void ConvertAbbr()
        {
            foreach(Sms s in smsList)
            {
                string[] elements = s.Message.Split(' ');
                for(int i=0; i< elements.Length; i++)
                {
                    if (abbr.ContainsKey(elements[i])){
                        elements[i] += (" <"+ abbr[elements[i]]+">");
                    }
                }
                s.Message = string.Join(" ", elements);
            }

            foreach (Tweet t in tweetList)
            {
                string[] elements = t.Message.Split(' ');
                for (int i = 0; i < elements.Length; i++)
                {
                    if (abbr.ContainsKey(elements[i]))
                    {
                        elements[i] += (" \u003E" + abbr[elements[i]] + "\u003C");
                    }
                }
                t.Message = string.Join(" ", elements);
            }
        }

        public List<Sms> SmsList
        {
            get { return smsList; }
        }
        public List<Email> EmailList
        {
            get { return emailList; }
        }
        public List<Tweet> TweetList
        {
            get { return tweetList; }
        }
        public List<string> QuarantineUrl
        {
            get { return quarantineUrl; }
        }
        public Dictionary<string,string> Abbreviations
        {
            get { return abbr; }
        }

        public void AddSms(Sms s)
        {
            smsList.Add(s);
        }
        public void AddEmail(Email e)
        {
            emailList.Add(e);
        }
        public void AddTweet(Tweet t)
        {
            tweetList.Add(t);
        }

        public void SaveData()
        {
            var serializer = new JavaScriptSerializer();
            var serializedSms = serializer.Serialize(smsList);
            File.WriteAllText(@"../../../Data/sms.json", serializedSms);
            var serializedMail = serializer.Serialize(emailList);
            File.WriteAllText(@"../../../Data/email.json", serializedMail);
            var serializedTweet = serializer.Serialize(tweetList);
            File.WriteAllText(@"../../../Data/tweet.json", serializedTweet);

        }
    }
}

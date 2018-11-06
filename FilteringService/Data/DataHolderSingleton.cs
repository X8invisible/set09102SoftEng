using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Business;

namespace Data
{
    public class DataHolderSingleton
    {
        private static DataHolderSingleton instance;
        private static Dictionary<string, string> abbr;
        private static List<Sms> smsList = new List<Sms>();
        private static List<string> quarantineUrl = new List<string>();
        private static List<Email> emailList = new List<Email>();
        private static List<Tweet> tweetList = new List<Tweet>();
        private static List<RawMessage> fullList = new List<RawMessage>();
        private static Dictionary<string, string> sirList;
        private static Dictionary<string, int> trendingList;


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
            if (abbr == null)
            {
                abbr = new Dictionary<string, string>();
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
                        elements[i] += (" \u003C" + abbr[elements[i]] + "\u003E");
                    }
                }
                t.Message = string.Join(" ", elements);
            }
        }

		public void ConvertUrl()
		{
			foreach(Email e in emailList)
			{
				string[] elements = e.Message.Split(' ');
				for(int i=0; i < elements.Length; i++)
				{
                    Regex rule = new Regex(@"(http|https)://.*$");
					if (rule.IsMatch(elements[i]))
					{
						quarantineUrl.Add(elements[i]);
						elements[i] = "\u003C URL Quarantined \u003E";
					}
				}
				e.Message = string.Join(" ", elements);
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
        public Dictionary<string,int> TrendList
        {
            get
            {
                UpdateTrendList();
                return trendingList;
            }
        }
        public List<RawMessage> FullList
        {
            get
            {
                UpdateFullList();
                return fullList;
            }
        }
        public Dictionary<string,string> Abbreviations
        {
            get { return abbr; }
        }
        public Dictionary<string, string> SirList
        {
            get
            {
                UpdateSirList();
                return sirList;
            }
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

        public void UpdateSirList()
        {
            sirList = new Dictionary<string, string>();
            foreach(Email e in emailList)
            {
                if(e.Type == "Significant Incident Report")
                {
                    string actualValue;
                    if(sirList.TryGetValue(e.SortCode, out actualValue) && actualValue.Equals(e.Incident))
                        sirList.Add(e.SortCode, e.Incident);
                }
            }
        }
        public void UpdateTrendList()
        {
            trendingList = new Dictionary<string, int>();
            foreach(Tweet t in tweetList)
            {
                string[] elements = t.Message.Split(new Char[] { ',', ' ','.','?','!',';' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < elements.Length; i++)
                {
                    if(elements[i][0] == '#')
                    {
                        
                        if (trendingList.ContainsKey(elements[i]))
                        {
                            trendingList[elements[i]]++;
                        }
                        else
                        {
                            trendingList.Add(elements[i], 1);
                        }
                    }
                }
            }
        }
        public void UpdateFullList()
        {
            fullList = new List<RawMessage>(smsList.Count + emailList.Count + tweetList.Count);
            smsList.ForEach(obj => fullList.Add(obj));
            emailList.ForEach(obj => fullList.Add(obj));
            tweetList.ForEach(obj => fullList.Add(obj));
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
            var serializedUrl = serializer.Serialize(quarantineUrl);
            File.WriteAllText(@"../../../Data/quarantine.json", serializedUrl);

        }
        public void ReadData()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (File.Exists(@"../../../Data/sms.json"))
            {
                string sms = File.ReadAllText(@"../../../Data/sms.json");
                smsList = serializer.Deserialize<List<Sms>>(sms);
            }
            if (File.Exists(@"../../../Data/email.json"))
            {
                string email = File.ReadAllText(@"../../../Data/email.json");
                emailList = serializer.Deserialize<List<Email>>(email);
            }
            if (File.Exists(@"../../../Data/tweet.json"))
            {
                string tweet = File.ReadAllText(@"../../../Data/tweet.json");
                tweetList = serializer.Deserialize<List<Tweet>>(tweet);
            }
            if (File.Exists(@"../../../Data/quarantine.json"))
            {
                string url = File.ReadAllText(@"../../../Data/quarantine.json");
                quarantineUrl = serializer.Deserialize<List<string>>(url);
            }

        }
    }
}

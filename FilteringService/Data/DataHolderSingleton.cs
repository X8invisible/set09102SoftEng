using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<Sms> smsList = new List<Sms>();
        private List<Tuple<string,string>> quarantineUrl;
        private List<Email> emailList = new List<Email>();
        private List<Tweet> tweetList = new List<Tweet>();
        private List<RawMessage> fullList = new List<RawMessage>();
        private Dictionary<string, int> trendingList;
        private Dictionary<string, int> mentionList;
        private Dictionary<string, string> abbr;

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
                        string line = reader.ReadLine();
                        var values = line.Split(new[] { ',' }, 2);

                        abbr.Add(values[0], values[1]);
                    }
                }
            }
        }

        public void ConvertAbbr()
        {
            foreach(Sms s in smsList)
            {
                string[] elements = s.RawMessage.Split(new Char[] { ',', ' ', '.', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i=0; i< elements.Length; i++)
                {
                    Trace.WriteLine(elements[i]);
                    if (abbr.ContainsKey(elements[i].ToUpper())){
                        elements[i] += $" \u003C {abbr[elements[i].ToUpper()]} \u003E";
                    }
                }
                s.Message = string.Join(" ", elements);
            }

            foreach (Tweet t in tweetList)
            {
                string[] elements = t.RawMessage.Split(new Char[] { ',', ' ', '.', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < elements.Length; i++)
                {
                    if (abbr.ContainsKey(elements[i].ToUpper()))
                    {
                        elements[i] += $" \u003C {abbr[elements[i].ToUpper()]} \u003E";
                    }
                }
                t.Message = string.Join(" ", elements);
            }
        }

		public void ConvertUrl()
		{
            quarantineUrl = new List<Tuple<string, string>>();
			foreach(Email e in emailList)
			{
				string[] elements = e.RawMessage.Split(new Char[] { ',', ' ', '?', '!', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i=0; i < elements.Length; i++)
				{
                    Regex rule = new Regex(@"(http|https)://.*$");
					if (rule.IsMatch(elements[i]))
					{
                        Tuple<string, string> tp = new Tuple<string, string>(e.FullId, elements[i]);
                        quarantineUrl.Add(tp);
						elements[i] = " \u003C URL Quarantined \u003E";
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
        public List<Tuple<string, string>> QuarantineUrl
        {
            get
            {
                ConvertUrl();
                return quarantineUrl;
            }
        }
        public Dictionary<string,int> TrendList
        {
            get
            {
                UpdateTrendList();
                return trendingList;
            }
        }
        public Dictionary<string, int> MentionList
        {
            get
            {
                UpdateMentionList();
                return mentionList;
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
        public void UpdateMentionList()
        {
            mentionList = new Dictionary<string, int>();
            foreach (Tweet t in tweetList)
            {
                string[] elements = t.Message.Split(new Char[] { ',', ' ', '.', '?', '!', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i][0] == '@')
                    {

                        if (mentionList.ContainsKey(elements[i]))
                        {
                            mentionList[elements[i]]++;
                        }
                        else
                        {
                            mentionList.Add(elements[i], 1);
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

        }
    }
}

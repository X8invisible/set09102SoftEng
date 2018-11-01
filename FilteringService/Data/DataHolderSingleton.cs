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
        private static List<Sms> smsList = new List<Sms>();
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

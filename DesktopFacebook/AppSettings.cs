using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Facebook;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class AppSettings
    {
        public User           LastUser                   { get; set; }
        public BirthdayNode[] LastUserBirthdayDictionary { get; set; }
        public bool           RememberUser               { get; set; }
        public string         LastAccessToken            { get; set; }

        public AppSettings()
        {
            RememberUser               = false;
            LastAccessToken            = null;
            LastUserBirthdayDictionary = null;
        }

        internal void SaveToFile()
        {
            using (Stream stream = new FileStream(@"C:\Users\dorko\Desktop\appSettings.xml", FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);           
            }
        }

        internal static AppSettings LoadFromFile()
        {
            AppSettings obj = null;

            using (Stream stream = new FileStream(@"C:\Users\dorko\Desktop\appSettings.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                obj = serializer.Deserialize(stream) as AppSettings;
            }

            return obj;
        }
    }
}

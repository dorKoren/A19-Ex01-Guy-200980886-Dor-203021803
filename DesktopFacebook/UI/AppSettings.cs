using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace DesktopFacebook
{
    public class AppSettings
    {
        private const string k_Path = "C:\\Users\\Program Files\\DesktopFacebook\\appSettings.xml";

        public User               LastUser                   { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool               RememberUser               { get; set; }
        public string             LastAccessToken            { get; set; }

        public AppSettings()
        {
            this.RememberUser               = false;
            this.LastAccessToken            = null;
            this.LastUserBirthdayDictionary = null;
        }

        public static AppSettings LoadFromFile()
        {
            AppSettings obj = null;
            if (File.Exists(k_Path))
            {
                using (Stream stream = new FileStream(k_Path, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    obj = serializer.Deserialize(stream) as AppSettings;
                }
            }

            return obj;
        }

        public void SaveToFile()
        {
            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);           
            }
        }
    }
}

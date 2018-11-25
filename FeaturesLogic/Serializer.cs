using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Serializer
    {
        #region Class Members
        private const string k_Path = "C:\\Users\\Public";

        public User               LastUser                   { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool               RememberUser               { get; set; }
        public string             LastAccessToken            { get; set; }

        #endregion Class Members

        #region Constructor
        public Serializer()
        {
            RememberUser               = false;
            LastAccessToken            = null;
            LastUserBirthdayDictionary = null;
        }
        #endregion Constructor

        #region Public Static Methods
        public static Serializer LoadFromFile()
        {
            Serializer obj = null;

            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Serializer));
                obj = serializer.Deserialize(stream) as Serializer;
            }

            return obj;
        }
        #endregion Public static Methods

        #region Public Methods
        public void SaveToFile()
        {
            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }
        #endregion Public Methods
    }
}

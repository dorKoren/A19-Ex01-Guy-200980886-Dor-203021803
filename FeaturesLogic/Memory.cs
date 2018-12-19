using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Memory 
    {
        #region Class Members
        private const string k_Path = "C:\\Users\\dorko\\Desktop";

        public User               LastUser                   { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool               RememberUser               { get; set; }
        public string             LastAccessToken            { get; set; }

        #endregion Class Members

        #region Constructor
        public Memory()
        {
            RememberUser               = false;
            LastAccessToken            = null;
            LastUserBirthdayDictionary = null;
        }
        #endregion Constructor

        #region Public Methods
        public Memory LoadFromFile()
        {
            Loader loader = new Loader();
            loader.RunThread();
            return loader.InfoStream;
            // loader.Close();
        }

        public void SaveToFile()
        {
            Saver saver = new Saver();
            saver.RunThread();
            // saver.Close();
        }

        #endregion Private Methods

    }
}

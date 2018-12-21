using System.IO;
using System.Xml.Serialization;
using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Memory 
    {
        #region Class Members
        private static string k_Path; // "C:\\Users\\dorko\\Desktop";

        public User               LastUser                   { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool               RememberUser               { get; set; }
        public string             LastAccessToken            { get; set; }

        #endregion Class Members

        #region Constructor
        private Memory()
        {
            // Init path only once, and not everytime the memory is loaded
            if (k_Path == null || k_Path.Length == 0)
            {
                k_Path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            }
            RememberUser               = false;
            LastAccessToken            = null;
            LastUserBirthdayDictionary = null;
        }
        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// This method returns the last memory saved, or a new memory if this is first call.
        /// </summary>
        /// <returns></returns>
        public static Memory LoadFromFile()
        {
            Memory memory;
            if (k_Path != null && k_Path.Length > 0)
            {
                Loader loader = new Loader(k_Path);
                loader.RunThread();
                memory = loader.InfoStream;
                // loader.Close();
            }
            else
            {
                memory = new Memory();
            }
            return memory;
        }

        public void SaveToFile()
        {
            Saver saver = new Saver(k_Path);
            saver.RunThread();
            // saver.Close();
        }

        #endregion Private Methods

    }
}

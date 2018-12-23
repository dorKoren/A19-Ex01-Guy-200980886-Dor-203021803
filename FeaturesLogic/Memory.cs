using FacebookWrapper.ObjectModel;

namespace FeaturesLogic
{
    public class Memory
    {
        #region Class Members
        private static string k_Path; // "C:\\Users\\dorko\\Desktop";

        public User LastUser { get; set; }
        public BirthdayDictionary LastUserBirthdayDictionary { get; set; }
        public bool RememberUser { get; set; }
        public string LastAccessToken { get; set; }

        #endregion Class Members

        #region Constructor
        private Memory()
        {
            // Init path only once, and not everytime the memory is loaded
            if (k_Path == null || k_Path.Length == 0)
            {
                k_Path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            }
            RememberUser = false;
            LastAccessToken = null;
            LastUserBirthdayDictionary = null;
        }
        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// This method returns the last memory saved, if any.
        /// </summary>
        /// <returns></returns>
        public static Memory LoadFromFile()
        {
            Memory memory = null;
            if (k_Path != null && k_Path.Length > 0)
            {
                MemoryLoader loader = new MemoryLoader(k_Path);
                // loader.RunThread();
                memory = loader.InfoStream;
            }
            return memory;
        }

        /// <summary>
        /// This method creates a new Memory object.
        /// </summary>
        /// <returns></returns>
        public static Memory CreateNewMemory()
        {
            return new Memory();
        }

        public void SaveToFile()
        {
            MemorySaver saver = new MemorySaver(k_Path, this);
            // saver.RunThread();
        }

        #endregion Private Methods

    }
}

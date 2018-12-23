using System.IO;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class MemoryLoader : BaseThread
    {
        #region class members
        private string k_Path; // "C:\\Users\\dorko\\Desktop";
        internal Memory InfoStream { get; private set; } 

        internal MemoryLoader(string i_Path)
        {
            k_Path = i_Path;
            Run();
        }
        #endregion constructor

        #region Public Methods

        public override void Run()
        {
            InfoStream = loadFromFile();
        }
        #endregion Public Methods

        #region Private Methods
        private Memory loadFromFile()
        {
            Memory obj = null;

            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Memory));
                obj = serializer.Deserialize(stream) as Memory;
            }

            return obj;
        }
        #endregion Private Methods
    }
}

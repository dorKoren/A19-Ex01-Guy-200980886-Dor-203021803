using System.IO;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class Loader : BaseThread
    {
        #region class members
        private string k_Path; // "C:\\Users\\dorko\\Desktop";
        internal Memory InfoStream { get; private set; }
        #endregion class members

        #region constructor
        public Loader(string i_Path)
        {
            k_Path = i_Path;
        }
        #endregion constructor

        #region public methhods
        public override void RunThread()
        {
            InfoStream = loadFromFile();
        }
        #endregion public methhods

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

using System.IO;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class Saver : BaseThread
    {
        #region class members
        private string k_Path; // = "C:\\Users\\dorko\\Desktop";
        #endregion clas members

        #region constructor
        public Saver(string i_Path)
        {
            k_Path = i_Path;
        }
        #endregion constructor

        #region public methods
        public override void RunThread()
        {
            saveToFile();
        }
        #endregion public methods

        #region Private Methods

        private void saveToFile()
        {
            using (Stream stream = new FileStream(k_Path, FileMode.Truncate))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(stream, this);
            }
        }

        #endregion Private Methods
    }
}

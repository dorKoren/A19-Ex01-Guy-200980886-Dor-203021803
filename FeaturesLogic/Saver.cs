using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class Saver : BaseThread
    {
        private string k_Path; // = "C:\\Users\\dorko\\Desktop";

        public Saver(string i_Path)
        {
            k_Path = i_Path;
        }

        public override void RunThread()
        {
            saveToFile();
        }

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

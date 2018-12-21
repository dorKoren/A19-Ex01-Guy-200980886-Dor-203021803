using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class Loader : BaseThread
    {
        private string k_Path; // "C:\\Users\\dorko\\Desktop";
        internal Memory InfoStream { get; private set; }

        public Loader(string i_Path)
        {
            k_Path = i_Path;
        }

        public override void RunThread()
        {
            InfoStream = loadFromFile();
        }

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

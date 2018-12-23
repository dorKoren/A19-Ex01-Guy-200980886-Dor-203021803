using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeaturesLogic
{
    internal class MemorySaver : BaseThread
    {
        private string k_Path; // = "C:\\Users\\dorko\\Desktop";
        private Memory m_Memory = null;

        public MemorySaver(string i_Path, Memory i_Memory)
        {
            k_Path = i_Path;
            m_Memory = i_Memory;
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
                XmlSerializer serializer = new XmlSerializer(m_Memory.GetType());
                serializer.Serialize(stream, m_Memory);
            }
        }

        #endregion Private Methods
    }
}

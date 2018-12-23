using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeaturesLogic
{
    public abstract class BaseThread
    {
        #region Class Members

        private Thread m_ExecutingThread;

        #endregion Class Members

        protected BaseThread()
        {
            m_ExecutingThread = new Thread(new ThreadStart(this.RunThread));
        }

        #region Properties

        public void Start() => infoStream.Start();

        public void Join() => infoStream.Join();

        public bool IsAlive => infoStream.IsAlive;

        #endregion Properties

        #region Abstract Methods

        public abstract void Run();

        #endregion Abstract Methods
    }
}


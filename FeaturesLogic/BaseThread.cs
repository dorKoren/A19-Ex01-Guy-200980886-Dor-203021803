using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeaturesLogic
{
    abstract public class BaseThread
    {
        private Thread infoStream;

        protected BaseThread()
        {
            infoStream = new Thread(new ThreadStart(this.RunThread));
        }

        #region Properties

        public void Start() => infoStream.Start();

        public void Join() => infoStream.Join();

        public bool IsAlive => infoStream.IsAlive;

        #endregion Properties

        #region Abstract Methods

        public abstract void RunThread();

        #endregion Abstract Methods
    }
}


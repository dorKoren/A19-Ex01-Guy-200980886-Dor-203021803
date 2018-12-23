using System.Threading;

namespace FeaturesLogic
{
    abstract public class BaseThread
    {
        #region class members
        private Thread m_InfoStream;
        #endregion class members

        #region constructor
        protected BaseThread()
        {
            m_InfoStream = new Thread(new ThreadStart(this.RunThread));
        }
        #endregion constructor

        #region Properties
        public void Start() => m_InfoStream.Start();

        public void Join()  =>  m_InfoStream.Join();

        public bool IsAlive => m_InfoStream.IsAlive;
        #endregion Properties

        #region Abstract Methods
        public abstract void RunThread();
        #endregion Abstract Methods
    }
}


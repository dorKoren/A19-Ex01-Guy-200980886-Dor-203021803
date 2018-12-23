using System.Threading;

namespace FeaturesLogic
{
    public abstract class BaseThread
    {
        #region Class Members

        private Thread m_ExecutingThread;

        #endregion Class Members

        #region constructor
        protected BaseThread()
        {
            m_ExecutingThread = new Thread(new ThreadStart(this.RunThread));
        }
        #endregion constructor

        #region Properties
        public void Start() => m_InfoStream.Start();

        public void Join()  =>  m_InfoStream.Join();

        public bool IsAlive => m_InfoStream.IsAlive;
        #endregion Properties

        #region Abstract Methods

        public abstract void Run();

        #endregion Abstract Methods
    }
}


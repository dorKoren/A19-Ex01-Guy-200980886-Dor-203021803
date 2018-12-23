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
            m_ExecutingThread = new Thread(new ThreadStart(this.Run));
        }
        #endregion constructor

        #region Properties
        public void Start() => m_ExecutingThread.Start();

        public void Join() => m_ExecutingThread.Join();

        public bool IsAlive => m_ExecutingThread.IsAlive;
        #endregion Properties

        #region Abstract Methods

        public abstract void Run();

        #endregion Abstract Methods
    }
}


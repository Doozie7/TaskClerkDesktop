using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// The plugin user control
    /// </summary>
    [TaskClerkPluginAttribute]
    public class PluginUserControl : UserControl
    {
        private TaskClerkEngine _engine;
        private Component _hostControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginUserControl"/> class.
        /// </summary>
        public PluginUserControl()
        {
        }

        /// <summary>
        /// All addins get handed the EngineContext and the EnvironmentContext
        /// when they are initalized
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="hostControl">The host control.</param>
        public void TaskClerkInit(TaskClerkEngine engine, Component hostControl)
        {
            _engine = engine;
            _hostControl = hostControl;
            Router("OnTaskClerkInit");
        }

        /// <summary>
        /// provides a public routing mechanisim
        /// </summary>
        /// <param name="route">The route.</param>
        public void Router(string route)
        {
            switch (route)
            {
                case "OnTaskClerkInit":
                    OnTaskClerkInit();
                    break;
                case "OnTaskClerkShow":
                    OnTaskClerkShow();
                    break;
                case "OnTaskClerkHide":
                    OnTaskClerkHide();
                    break;
                case "OnTaskClerkSaveState":
                    OnTaskClerkSaveState();
                    break;
                case "OnTaskClerkDumpState":
                    OnTaskClerkDumpState();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected virtual void OnTaskClerkInit()
        {
        }

        /// <summary>
        /// TaskClerk is anout to show the control
        /// </summary>
        protected virtual void OnTaskClerkShow()
        {
        }

        /// <summary>
        /// TaskClerk is about to hide the control
        /// </summary>
        protected virtual void OnTaskClerkHide()
        {
        }

        /// <summary>
        /// TaskClerk is about to save the controls state
        /// </summary>
        protected virtual void OnTaskClerkSaveState()
        {
        }

        /// <summary>
        /// TaskClerk is about to dump the controls state
        /// </summary>
        protected virtual void OnTaskClerkDumpState()
        {
        }

        /// <summary>
        /// The data Engine
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        protected TaskClerkEngine Engine
        {
            get { return _engine; }
        }

        /// <summary>
        /// the control this component is loaded into
        /// </summary>
        [Browsable(false)]
        [XmlIgnore]
        protected Component HostControl
        {
            get { return _hostControl; }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // PluginUserControl
            // 
            Name = "PluginUserControl";
            Size = new Size(217, 215);
            ResumeLayout(false);
        }
    }
}
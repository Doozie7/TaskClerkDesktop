using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// The plugin component provides basic plugin component functionality for the Windows TaskClerk Client.
    /// </summary>
    [TaskClerkPluginAttribute]
    public partial class PluginComponent : Component
    {
        private TaskClerkEngine _engine;
        private Component _hostControl;

        private string _text;
        private Image _image;
        private object _tag;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginComponent"/> class.
        /// </summary>
        protected PluginComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginComponent"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected PluginComponent(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// All addins get handed the EngineContext and the EnvironmentContext
        /// when they are initalized
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="hostControl">The host control.</param>
        public void PluginInit(TaskClerkEngine engine, Component hostControl)
        {
            _engine = engine;
            _hostControl = hostControl;
            Router("OnTaskClerkInit");
        }

        /// <summary>
        /// provides a public routing mechanisim
        /// </summary>
        /// <param name="route"></param>
        public void Router(string route)
        {
            switch (route)
            {
                case "OnTaskClerkInit":
                    OnTaskClerkInit();
                    break;
                    //case "OnTaskClerkShow":
                    //    OnTaskClerkShow();
                    //    break;
                    //case "OnTaskClerkHide":
                    //    OnTaskClerkHide();
                    //    break;
                    //case "OnTaskClerkSaveState":
                    //    OnTaskClerkSaveState();
                    //    break;
                    //case "OnTaskClerkDumpState":
                    //    OnTaskClerkDumpState();
                    //    break;
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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public virtual string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public virtual Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public virtual object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
    }
}
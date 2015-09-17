using System.Windows.Forms;

namespace BritishMicro.Windows
{
    public partial class WizardUserControl : UserControl
    {
        private string _description;

        /// <summary>
        /// 
        /// </summary>
        public WizardUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// The PrepareContents operation is called when the control is made visible.
        /// </summary>
        public virtual void PrepareContents() {}

    }
}
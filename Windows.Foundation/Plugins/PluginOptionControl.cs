using BritishMicro.TaskClerk.Properties;
using System;
using System.ComponentModel;
using System.Drawing;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    [TaskClerkPlugin]
    public partial class PluginOptionControl : PluginUserControl
    {
        private string _displayName;
        private bool _isAdvanced;
        private int _displayOrder;
        private Image _image;
        private readonly Image _defaultImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginOptionControl"/> class.
        /// </summary>
        public PluginOptionControl()
        {
            InitializeComponent();
            _defaultImage = Resources.ConfigImage;
        }

        /// <summary>
        /// If set to true the control will be visible when 
        /// advanced options are set to visible
        /// </summary>
        public bool IsAdvanced
        {
            get { return _isAdvanced; }
            set { _isAdvanced = value; }
        }

        /// <summary>
        /// The name used in the selector control
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        /// <summary>
        /// Help the selector order options controls
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        /// <summary>
        /// The image used in the selector display
        /// </summary>
        public Image SelectorImage
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// The default image used in the selector display
        /// </summary>
        [Browsable(false)]
        public Image DefaultImage
        {
            get { return _defaultImage; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBaseControlA"></param>
        /// <param name="optionsBaseControlB"></param>
        /// <returns></returns>
        public static int CompareOnDisplayOrder(
            PluginOptionControl optionsBaseControlA,
            PluginOptionControl optionsBaseControlB)
        {
            if (optionsBaseControlA == null)
            {
                throw new ArgumentNullException("optionsBaseControlA");
            }
            if (optionsBaseControlB == null)
            {
                throw new ArgumentNullException("optionsBaseControlB");
            }

            return optionsBaseControlA.DisplayOrder.CompareTo(optionsBaseControlB.DisplayOrder);
        }
    }
}
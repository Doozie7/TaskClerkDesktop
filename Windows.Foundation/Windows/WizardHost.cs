using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    public partial class WizardHost : UserControl
    {
        [NonSerialized] private Collection<WizardUserControl> _items;
        private int _currentPage;

        /// <summary>
        /// 
        /// </summary>
        public WizardHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add a WizardUserControl to the Items collection
        /// </summary>
        /// <param name="userControl"></param>
        public void AddWizardItem(WizardUserControl userControl)
        {
            if (_items == null)
            {
                _items = new Collection<WizardUserControl>();
            }
            _items.Add(userControl);
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public Image HeadingImage
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Appearance")]
        public string HeadingText
        {
            get { return labelHeading.Text; }
            set
            {
                labelHeading.Text = value;
                //Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public ReadOnlyCollection<WizardUserControl> Items
        {
            get
            {
                ReadOnlyCollection<WizardUserControl> items = null;
                if (_items != null)
                {
                    items
                        = new ReadOnlyCollection<WizardUserControl>(_items);
                }
                return items;
            }
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        [Category("Appearance")]
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (Items != null)
                {
                    if (value >= Items.Count)
                    {
                        _currentPage = 0;
                    }
                    if (value < 0)
                    {
                        _currentPage = 0;
                    }
                    _currentPage = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// This starts the whole process after the Wizard items are added.
        /// </summary>
        public void Bind()
        {
            CurrentPage = 0;
        }

        /// <summary>
        /// Forces the control to invalidate its client area and immediately redraw itself and any child controls.
        /// </summary>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override void Refresh()
        {
            base.Refresh();
            if (_items.Count == 0)
            {
                buttonNext.Enabled = false;
                buttonPrevious.Enabled = false;
                buttonFinish.Enabled = false;
                buttonCancel.Enabled = true;
            }
            else
            {
                panelContainer.Controls.Clear();
                panelContainer.Controls.Add(Items[CurrentPage]);
                Items[CurrentPage].PrepareContents();
                labelHeading.Text = ((WizardUserControl)Items[CurrentPage]).Description;
                panelContainer.Controls[0].Dock = DockStyle.Fill;

                if ((CurrentPage == 0) && (Items.Count == 1))
                {
                    buttonNext.Enabled = false;
                    buttonPrevious.Enabled = false;
                    buttonFinish.Enabled = true;
                    buttonCancel.Enabled = true;
                }
                else if ((CurrentPage == 0) && (Items.Count > 1))
                {
                    buttonNext.Enabled = true;
                    buttonPrevious.Enabled = false;
                    buttonFinish.Enabled = true;
                    buttonCancel.Enabled = true;
                }
                else if ((CurrentPage > 0) && (CurrentPage < Items.Count - 1))
                {
                    buttonNext.Enabled = true;
                    buttonPrevious.Enabled = true;
                    buttonFinish.Enabled = true;
                    buttonCancel.Enabled = true;
                }
                else
                {
                    buttonNext.Enabled = false;
                    buttonPrevious.Enabled = true;
                    buttonFinish.Enabled = true;
                    buttonCancel.Enabled = true;
                }
            }
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            EventHandler<WizardEventArgs> handler = WizardHostActionEvent;
            if (handler != null)
            {
                WizardEventArgs w = new WizardEventArgs("finish", CurrentPage, -1);
                handler(this, w);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            EventHandler<WizardEventArgs> handler = WizardHostActionEvent;
            if (handler != null)
            {
                WizardEventArgs w = new WizardEventArgs("cancel", CurrentPage, -1);
                handler(this, w);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            WizardEventArgs w = new WizardEventArgs("next", CurrentPage, CurrentPage + 1);
            WizardHostActionEvent?.Invoke(this, w);
            if (w.Cancel == false)
            {
                CurrentPage++;
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            WizardEventArgs w = new WizardEventArgs("previous", CurrentPage, CurrentPage - 1);
            WizardHostActionEvent?.Invoke(this, w);
            if (w.Cancel == false)
            {
                CurrentPage--;
            }
        }

        /// <summary>
        /// Provides an event for the Wizard host action.
        /// </summary>
        public event EventHandler<WizardEventArgs> WizardHostActionEvent;
    }
}
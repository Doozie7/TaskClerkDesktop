//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 15th of June 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Diagnostics;
using BritishMicro.TaskClerk.Plugins;
using System.IO;

namespace BritishMicro.TaskClerk
{
    public partial class OpenDataFolder : PluginToolMenuItem
    {
        /// <summary>
        /// 
        /// </summary>
        public OpenDataFolder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the folder the data is stored in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDataFolder_Click(object sender, EventArgs e)
        {
            DirectoryInfo dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
            Process.Start(dataFolder.FullName);
        }
    }
}
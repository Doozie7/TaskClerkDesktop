using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// Provides user setting information from a file based hash table.
    /// </summary>
    public class FileUserMemoryProvider : UserMemoryProvider
    {

        private DirectoryInfo _dataFolder;

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their providers after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
            _dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
            LoadAll();
        }

        /// <summary>
        /// Provides access to settings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public override object Get(object key, object defaultValue)
        {
            return base.Get(key, defaultValue);
        }

        /// <summary>
        /// Allows settings to be stored in the UsersSessionMemory,
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="hint">The persistance hint.</param>
        public override void Set(object key, object value, PersistHint hint)
        {
            base.Set(key, value, hint);
            if (hint != PersistHint.None)
            {
                SaveAll();
            }
        }

        /// <summary>
        /// Loads all the UserConfiguration
        /// </summary>
        private void LoadAll()
        {
            if ((SettingsFile.Exists) && (SettingsFile.Length > 0))
            {
                using (FileStream fileStream = SettingsFile.OpenRead())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    try
                    {
                        //  ...we need to help the Assembly resolution process, we have assemlies
                        //  in a "Plugins" folder, this folder is not a "known path" and therefore
                        //  fails to load the assembly
                        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                        ReplaceUsersSessionMemory((Hashtable)bf.Deserialize(fileStream));
                        //fileStream.Flush();
                    }
                    catch (SerializationException ex)
                    {
                        fileStream.Close();
                        SettingsFile.CopyTo(SettingsFile.FullName + "." + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".corrupt");
                        SettingsFile.Delete();
                        throw new InvalidOperationException(
                            "The Settings Store has become corrupt, Please restart TaskClerk.", ex);
                    }
                    finally
                    {
                        //  ...ensure that we unwire from the event once the deserialize process is complete
                        AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the AssemblyResolve event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return AssemblyResolve(args);
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SaveAll()
        {
            using (FileStream fileStream = SettingsFile.Open(FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, UsersSessionMemory);
                fileStream.Flush();
            }
        }

        /// <summary>
        /// Gets the settings file.
        /// </summary>
        /// <value>The settings file.</value>
        private FileInfo SettingsFile
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                const string STRING_SETTINGSFILE = "TaskClerk.bin";
                string fileName = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0}\\{1}",
                    _dataFolder.FullName,
                    STRING_SETTINGSFILE);
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists == false)
                {
                    FileStream fs = fileInfo.Create();
                    fs.Close();
                }
                return fileInfo;
            }
        }
    }
}
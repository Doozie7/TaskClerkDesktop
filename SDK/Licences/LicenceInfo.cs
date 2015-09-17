using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// The license info class provides license information for use during the 
    /// startup of the application.
    /// </summary>
    public class LicenseInfo
    {

        private XmlDocument _licenseDoc;
        private License _license;

        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseInfo"/> class.
        /// </summary>
        public LicenseInfo()
        {
            _licenseDoc = new XmlDocument();
            _licenseDoc.LoadXml(Properties.Resources.DefaultLicense);
        }

        /// <summary>
        /// Initalises this instance.
        /// </summary>
        internal void Initalise(License license)
        {
            this._license = license;
            string licenseFile 
                = Path.GetDirectoryName(typeof(TaskClerkEngine).Module.FullyQualifiedName) 
                + @"\" + typeof(TaskClerkEngine).FullName + ".lic";
            if (File.Exists(licenseFile))
            {
                using (Stream stream 
                    = new FileStream(licenseFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        _licenseDoc.LoadXml(GetData(reader.ReadToEnd()));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string GetData(string input)
        {
            string output = input.Remove(0, _license.LicenseKey.Length).TrimStart();
            return output;
        }

        /// <summary>
        /// Gets the license xml doc.
        /// </summary>
        /// <value>The license doc.</value>
        public XmlDocument LicenseDoc
        {
            get { return _licenseDoc; }
        }
    }
}

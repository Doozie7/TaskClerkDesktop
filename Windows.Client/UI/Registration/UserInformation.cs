using System;
using System.Net;
using System.Threading;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using BritishMicro.TaskClerk.WebRegistration;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class UserInformation : UserControl
    {
        public UserInformation()
        {
            InitializeComponent();

            textBoxSystemUser.Text = AppContext.Current.IdentityProvider.Principal.Identity.Name;
            //textBoxOrganisation.Text =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserOrganisation", string.Empty);
            //textBoxSystemUser.Text =
            //    AppContext.Current.IdentityProvider.Principal.Identity.Name;
            //textBoxName.Text =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserName", string.Empty);
            //textBoxEmailAddress.Text =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserEmailAddress", string.Empty);
            //textBoxLanguage.Text =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserCulture",
            //                                                     Thread.CurrentThread.CurrentCulture.Name);
            //textBoxRegistrationCode.Text =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserRegistrationKey", string.Empty);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            //AppContext.Current.SettingsProvider.Set("CurrentUserName", textBoxName.Text, PersistHint.AcrossSessions);
        }

        private void textBoxEmailAddress_TextChanged(object sender, EventArgs e)
        {
            //AppContext.Current.SettingsProvider.Set("CurrentUserEmailAddress", textBoxEmailAddress.Text,
            //                                        PersistHint.AcrossSessions);
        }

        private void textBoxRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            //AppContext.Current.SettingsProvider.Set("CurrentUserRegistrationKey", textBoxRegistrationCode.Text,
            //                                        PersistHint.AcrossSessions);
        }

        private void textBoxLanguage_TextChanged(object sender, EventArgs e)
        {
            //AppContext.Current.SettingsProvider.Set("CurrentUserCulture", textBoxLanguage.Text,
            //                                        PersistHint.AcrossSessions);
        }

        private void textBoxOrganisation_TextChanged(object sender, EventArgs e)
        {
           // AppContext.Current.SettingsProvider.Set("CurrentUserOrganisation", textBoxOrganisation.Text,
           //                                         PersistHint.AcrossSessions);
        }

        private void buttonRegisterOnline_Click(object sender, EventArgs e)
        {
            //RegisterOnline();
        }

        private void buttonVerifyRegistration_Click(object sender, EventArgs e)
        {
            //VerifyRegistrationOnline();
        }

        private static void RegisterOnline()
        {
            //RegistrationRequest requestMessage = new RegistrationRequest();
            //requestMessage.Organisation =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserOrganisation", string.Empty);
            //requestMessage.SystemUser =
            //    (string) AppContext.Current.IdentityProvider.Principal.Identity.Name;
            //requestMessage.Name =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserName", string.Empty);
            //requestMessage.EmailAddress =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserEmailAddress", string.Empty);
            //requestMessage.Language =
            //    (string) AppContext.Current.SettingsProvider.Get("CurrentUserCulture", string.Empty);
            //requestMessage.AppInstanceKey =
            //    (string) AppContext.Current.SettingsProvider.Get("AppInstanceKey", string.Empty);
            //requestMessage.AppInstanceCreateDate =
            //    (string) AppContext.Current.SettingsProvider.Get("AppInstanceCreateDate", string.Empty);
            //requestMessage.ApplicationType = AppContext.AssemblyProduct;
            //requestMessage.VersionId = AppContext.AssemblyVersion;

            //RegistrationService rs = new RegistrationService();
            //try
            //{
                
            //    rs.Url = (string)AppContext.Current.SettingsProvider.Get("RegistrationServer", string.Empty);
            //    rs.Timeout = 2000;
            //    RegistrationResponse registrationResponse = rs.CreateRegistration(requestMessage);
            //    if (registrationResponse != null)
            //    {
            //        if (registrationResponse.Success == "True")
            //        {
            //            MessageBox.Show("An email has been sent to you, it contains your Registration Key. Thank you.",
            //                            "Registration Success",
            //                            MessageBoxButtons.OK,
            //                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            //        }
            //        else
            //        {
            //            MessageBox.Show(
            //                registrationResponse.FailReason,
            //                "Registration Error",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            //        }
            //    }
            //}
            //catch (WebException)
            //{
            //    MessageBox.Show("The registration services did not respond in time.",
            //                    "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            //}
            //catch (SoapException)
            //{
            //    MessageBox.Show(
            //        "The registration services was unable to submit the correct information, please fill in the User Details.",
            //        "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            //}
            //finally
            //{
            //    rs.Dispose();
            //}
        }

        private static void VerifyRegistrationOnline()
        {
        //    RegistrationRequest requestMessage = new RegistrationRequest();
        //    requestMessage.Organisation =
        //        (string) AppContext.Current.SettingsProvider.Get("CurrentUserOrganisation", string.Empty);
        //    requestMessage.SystemUser = AppContext.Current.IdentityProvider.Principal.Identity.Name;
        //    requestMessage.Name = (string) AppContext.Current.SettingsProvider.Get("CurrentUserName", string.Empty);
        //    requestMessage.EmailAddress =
        //        (string) AppContext.Current.SettingsProvider.Get("CurrentUserEmailAddress", string.Empty);
        //    requestMessage.Language =
        //        (string) AppContext.Current.SettingsProvider.Get("CurrentUserCulture", string.Empty);
        //    requestMessage.AppInstanceKey =
        //        (string) AppContext.Current.SettingsProvider.Get("AppInstanceKey", string.Empty);
        //    requestMessage.AppInstanceCreateDate =
        //        (string) AppContext.Current.SettingsProvider.Get("AppInstanceCreateDate", string.Empty);
        //    requestMessage.ApplicationType = AppContext.AssemblyProduct;
        //    requestMessage.VersionId = AppContext.AssemblyVersion;
        //    requestMessage.RegistrationKey =
        //        (string) AppContext.Current.SettingsProvider.Get("CurrentUserRegistrationKey", string.Empty);

        //    try
        //    {
        //        RegistrationService rs = new RegistrationService();
        //        rs.Url = (string) AppContext.Current.SettingsProvider.Get("RegistrationServer", string.Empty);
        //        rs.Timeout = 2000;
        //        RegistrationResponse responseMessage = rs.VerifyRegistration(requestMessage);
        //        if (responseMessage != null)
        //        {
        //            if (responseMessage.Success == "True")
        //            {
        //                MessageBox.Show("Your Registration Key is valid.",
        //                                "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        //            }
        //            else
        //            {
        //                MessageBox.Show(responseMessage.FailReason,
        //                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        //            }
        //        }
        //    }
        //    catch (WebException)
        //    {
        //        MessageBox.Show("The registration services did not respond in time.",
        //                        "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        //    }
        //    catch (SoapException)
        //    {
        //        MessageBox.Show(
        //            "The registration services was unable to submit the correct information, please fill in the User Details.",
        //            "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        //    }
        }
    }
}
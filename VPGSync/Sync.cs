using Google.Apis.Auth.OAuth2;
using Google.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGSync
{
    /// <summary>
    /// This is where the magic happens. ;-)
    /// </summary>
    internal class Sync
    {
        ProgressUpdater _status;
        
        /// <summary>
        /// Event updater to update GUI on progress
        /// </summary>
        public event Action<ProgressUpdater> ProgressUpdate;

        /// <summary>
        /// Start the Sync of contacts.
        /// </summary>
        public void DoSync()
        {

            _status = new ProgressUpdater();
            _status.IsRunning = true;
            _status.CurrentTask = "Getting VP Contacts";
            ProgressUpdate(_status);
            Dictionary<string, VPContact> vpContacts = VPConnect.GetContacts(Environment.UserName);
            if (vpContacts == null)
            {
                _status.HasError = true;
                _status.IsRunning = false;
                _status.Error = "Could not get contacts from VP, stopping sync";
                ProgressUpdate(_status);
                return;
            }
            _status.VPContacts = vpContacts.Count;
            ProgressUpdate(_status);

            _status.CurrentTask = "Logging into Google";
            ProgressUpdate(_status);
            var auth = GConnect.Authenticate();

            _status.CurrentTask = "Getting Google Contacts";
            ProgressUpdate(_status);
            var gContacts = GConnect.GetGroupContacts(auth);
            _status.GoogleContacts = gContacts.TotalResults;

            List<Contact> toBeDeleted;
            _status.CurrentTask = "Analyzing data......";
            ProgressUpdate(_status);

            vpContacts = CheckGoogleContacts(vpContacts, gContacts, out toBeDeleted);


            Dictionary<string, VPContact> toBeCreated = vpContacts.Where(item => item.Value.ActionNeeded == VPContact.SyncAction.Create)
                .ToDictionary(key => key.Key, value => value.Value);

            Dictionary<string, VPContact> toBeUpdated = vpContacts.Where(item => item.Value.ActionNeeded == VPContact.SyncAction.Update)
                .ToDictionary(key => key.Key, value => value.Value);

            _status.ToBeCreated = toBeCreated.Count;
            _status.ToBeUpdated = toBeUpdated.Count;
            _status.ToBeDeleted = toBeDeleted.Count;
            _status.CurrentTask = "Creating Google Contacts";
            ProgressUpdate(_status);

            CreateContacts(auth, toBeCreated);

            _status.CurrentTask = "Updating existing Google Contacts";
            ProgressUpdate(_status);
            UpdateContacts(auth, toBeUpdated);

            _status.CurrentTask = "Deleting unwanted Google Contacts";
            ProgressUpdate(_status);
            DeleteContacts(auth, toBeDeleted);


            _status.CurrentTask = "Done....";
            _status.IsRunning = false;
            ProgressUpdate(_status);

        }

        
        /// <summary>
        /// Updates Google Contacts one by one.
        /// </summary>
        /// <param name="auth">a valid Google token or refresh token</param>
        /// <param name="toBeUpdated"></param>
        private void UpdateContacts(UserCredential auth, Dictionary<string, VPContact> toBeUpdated)
        {
            try
            {
                foreach (var item in toBeUpdated)
                {
                    Contact gContact = item.Value.GetGoogleContact();

                    GConnect.UpdateContact(auth, gContact);
                    GConnect.UpdatePhoto(auth, gContact, item.Value.Picture);
                    //System.Threading.Thread.Sleep(3000);
                    _status.ToBeUpdated--;
                    ProgressUpdate(_status);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes Google Contacts one by one.
        /// </summary>
        /// <param name="auth">a valid Google token or refresh token</param>
        /// <param name="gContacts">contacts to be deleted.</param>
        private void DeleteContacts(UserCredential auth, List<Contact> gContacts)
        {
            try
            {
                foreach (var c in gContacts)
                {

                    if (c != null && !string.IsNullOrEmpty(c.ETag))
                    {
                        GConnect.DeleteContact(c, auth);
                        //System.Threading.Thread.Sleep(3000);
                    }

                    _status.ToBeDeleted--;
                    _status.GoogleContacts--;
                    ProgressUpdate(_status);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates Google Contacts one by one
        /// </summary>
        /// <param name="auth">a valid Google token or refresh token</param>
        /// <param name="contacts">contacts to be created</param>
        private void CreateContacts(UserCredential auth, Dictionary<string,VPContact> contacts)
        {

            try
            {
                foreach (KeyValuePair<string, VPContact> item in contacts)
                {
                    Contact newContact = item.Value.GetGoogleContact();

                    newContact = GConnect.CreateNewContact(newContact, auth);

                    //HACK! - dotnet lib does not support to set "nickname" while creating object ;-(
                    newContact.ContactEntry.Nickname = item.Value.Initials;
                    GConnect.UpdateContact(auth, newContact);

                    //update photo
                    GConnect.UpdatePhoto(auth, newContact, item.Value.Picture);

                    //Console.WriteLine("Created Contact: " + item.Key);
                    _status.ToBeCreated--;
                    _status.GoogleContacts++;
                    ProgressUpdate(_status);
                    //System.Threading.Thread.Sleep(3000);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Checks if the Google Contact and VP Contact is in sync
        /// </summary>
        /// <param name="vp">VP Contact</param>
        /// <param name="g">Google Contact</param>
        /// <returns>true if Google needs updating</returns>
        private bool ContactNeedUpdate(VPContact vp, Contact g)
        {
            //check names
            if (g.Name == null) return true;
            if (g.Name.GivenName != vp.NameGiven) return true;
            if (g.Name.FamilyName != vp.NameFamily) return true;


            //check emails
            if (g.Emails == null) return true;
            bool foundWorkEmail = false;
            bool foundPrivateEmail = false;
            foreach (var email in g.Emails)
            {
                if (email.Address == vp.EmailWork) foundWorkEmail = true;
                if (email.Address == vp.EmailPrivate) foundPrivateEmail = true;
            }
            //there is an email and its not found.
            if (!string.IsNullOrEmpty(vp.EmailWork) && !foundWorkEmail) return true;
            if (!string.IsNullOrEmpty(vp.EmailPrivate) && !foundPrivateEmail) return true;


            //Check phones
            bool foundPhoneWorkLandline = false;
            bool foundPhoneWorkMobile = false;
            bool foundPhonePrivateMobile = false;
            if (g.Phonenumbers == null) return true;
            foreach (var phone in g.Phonenumbers)
            {
                if (phone.Value == vp.PhonePrivateMobile) foundPhonePrivateMobile = true;
                if (phone.Value == vp.PhoneWorkLandline) foundPhoneWorkLandline = true;
                if (phone.Value == vp.PhoneWorkMobile) foundPhoneWorkMobile = true;
            }
            if (!string.IsNullOrEmpty(vp.PhonePrivateMobile) && !foundPhonePrivateMobile) return true;
            if (!string.IsNullOrEmpty(vp.PhoneWorkLandline) && !foundPhoneWorkLandline) return true;
            if (!string.IsNullOrEmpty(vp.PhoneWorkMobile) && !foundPhoneWorkMobile) return true;


            //check organization (department/work title).
            if (g.Organizations == null) return true;
            if (g.Organizations.Count == 0) return true;
            if (g.Organizations[0].Title != vp.WorkTitle) return true;
            if (g.Organizations[0].Name != vp.Department) return true;

            
            return false;
        } 


        /// <summary>
        /// Loops through all Google Contacts and checks if they are the same as
        /// the VP Contacts.
        /// </summary>
        /// <param name="vpContacts">VP Contacts</param>
        /// <param name="gContacts">Google Contacts</param>
        /// <param name="toBeDeleted">returns contacts that is in Google, but not in VP (has to be deleted in Google)</param>
        /// <returns>All VP Contacts, but with updated NeedsAction field.</returns>
        private Dictionary<string, VPContact> CheckGoogleContacts(Dictionary<string, VPContact> vpContacts, Feed<Contact> gContacts, out List<Contact> toBeDeleted)
        {
            
            toBeDeleted = new List<Contact>();

            foreach (Contact g in gContacts.Entries)
            {
                string initials = g.ContactEntry.Nickname;
                
                if (!string.IsNullOrEmpty(initials) && vpContacts.ContainsKey(initials))
                {
                    //contact found in Google, check for update
                    if (ContactNeedUpdate(vpContacts[initials], g))
                    {
                        vpContacts[initials].ActionNeeded = VPContact.SyncAction.Update;
                    }
                    else
                    {
                        vpContacts[initials].ActionNeeded = VPContact.SyncAction.InSync;
                    }
                    vpContacts[initials].GoogleContact = g;
                    continue;

                }
                //Contact not in VP, delete it.
                toBeDeleted.Add(g);

            }
            return vpContacts;
        }
    }
}

using Google.GData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGSync
{
    /// <summary>
    /// Class for VP Person
    /// </summary>
    internal class VPContact
    {
        SyncAction _actionNeeded = SyncAction.Create;
        byte[] _picture;

        /// <summary>
        /// does the contact need to be created, updated or is it InSync.
        /// Default contacts are in the "Create" state
        /// </summary>
        public enum SyncAction
        {
            Create,
            Update,
            InSync
        }

        /// <summary>
        /// what needs to happen to the contact?
        /// Default == Create
        /// </summary>
        public SyncAction ActionNeeded { get { return _actionNeeded; } set { _actionNeeded = value; } }

        /// <summary>
        /// VP-DB ID of contact.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Given name of contact
        /// </summary>
        public string NameGiven { get; set; }

        /// <summary>
        /// Family name of contact
        /// </summary>
        public string NameFamily { get; set; }

        /// <summary>
        /// Full name of contact, built from NameGive and NameFamily
        /// </summary>
        public string NameFull
        {
            get
            {
                if (string.IsNullOrEmpty(NameGiven)) return NameFamily;
                return NameGiven + " " + NameFamily;
                
            }
        }

        /// <summary>
        /// Work email address of contact (set as primary in Google Contact)
        /// </summary>
        public string EmailWork { get; set; }

        /// <summary>
        /// Private email adress of contact
        /// </summary>
        public string EmailPrivate { get; set; }

        /// <summary>
        /// Work landline
        /// </summary>
        public string PhoneWorkLandline { get; set; }

        /// <summary>
        /// Work phone
        /// </summary>
        public string PhoneWorkMobile { get; set; }

        /// <summary>
        /// Private mobile phone
        /// </summary>
        public string PhonePrivateMobile { get; set; }

        /// <summary>
        /// Title of Contact eg. "Network Architech" or "IT Supporter"
        /// </summary>
        public string WorkTitle { get; set; }
        
        //public int DepartmentId { get; set; }

        /// <summary>
        /// Department of Contact (not including parrent nor child departments)
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// initials of VP contact
        /// </summary>
        public string Initials { get; set; }

        ///public bool IsExternalContractor { get; set; }
        ///public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Google Contact, set your existing Google Contact and execute the
        /// GetGoogleContact method to update it.
        /// </summary>
        public Google.Contacts.Contact GoogleContact { get; set; }
        
    
        /// <summary>
        /// Converts the current VPContact to a Google Contact class.
        /// If the user is new, a new contact is created, if the Google Contact
        /// is entered, it will update that.
        /// </summary>
        /// <returns></returns>
        public Google.Contacts.Contact GetGoogleContact()
        {
            if (GoogleContact == null)
            {
                GoogleContact = new Google.Contacts.Contact();
            }

            //Name & Nick
            Name name = new Name();
            if (!string.IsNullOrEmpty(NameFamily)) name.FamilyName = NameFamily;
            if (!string.IsNullOrEmpty(NameGiven)) name.GivenName = NameGiven;
            if (!string.IsNullOrEmpty(NameFull)) name.FullName = NameFull;
            if (!string.IsNullOrEmpty(NameFamily) || !string.IsNullOrEmpty(NameGiven) || !string.IsNullOrEmpty(NameFull)) GoogleContact.Name = name;

            if (GoogleContact.ContactEntry != null && !string.IsNullOrEmpty(Initials)) GoogleContact.ContactEntry.Nickname = Initials;

            //organization
            if (GoogleContact.Organizations != null) GoogleContact.Organizations.Clear();
            Organization org = new Organization();
            if (!string.IsNullOrEmpty(Department)) org.Name = Department;
            if (!string.IsNullOrEmpty(WorkTitle)) org.Title = WorkTitle;
            if (!string.IsNullOrEmpty(Department) || !string.IsNullOrEmpty(WorkTitle))
            {
                org.Label = "Vestas";
                org.Primary = true;
                GoogleContact.Organizations.Add(org);
            }

            //Emails
            if (GoogleContact.Emails != null) GoogleContact.Emails.Clear();
            if (!string.IsNullOrEmpty(EmailPrivate))
            {
                GoogleContact.Emails.Add(new EMail
                {
                    Address = EmailPrivate,
                    Rel = "http://schemas.google.com/g/2005#home"
                });
            }

            if (!string.IsNullOrEmpty(EmailWork))
            {
                GoogleContact.Emails.Add(new EMail
                {
                    Address = EmailWork,
                    Primary = true,
                    Rel = "http://schemas.google.com/g/2005#work"
                });
            }


            //Phone
            if (GoogleContact.Phonenumbers != null) GoogleContact.Phonenumbers.Clear();
            if (!string.IsNullOrEmpty(PhoneWorkLandline))
            {
                GoogleContact.Phonenumbers.Add(new PhoneNumber
                {
                    Rel = ContactsRelationships.IsWork,
                    Value = PhoneWorkLandline
                });
            }

            if (!string.IsNullOrEmpty(PhoneWorkMobile))
            {
                GoogleContact.Phonenumbers.Add(new PhoneNumber
                {
                    Rel = ContactsRelationships.IsWorkMobile,
                    Value = PhoneWorkMobile,
                    Primary = true
                });
            }

            if (!string.IsNullOrEmpty(PhonePrivateMobile))
            {
                GoogleContact.Phonenumbers.Add(new PhoneNumber
                {
                    Rel = ContactsRelationships.IsMobile,
                    Value = PhonePrivateMobile
                });
            }



            return GoogleContact;

        }

        /// <summary>
        /// Picture of VP Contact, it will download the picture if none exist
        /// </summary>
        public byte[] Picture
        {
            get
            {
                if (_picture != null && _picture.Length != 0) return _picture;
                if (string.IsNullOrEmpty(Initials)) return null;

                _picture = DownloadPicture(Initials);
                return _picture;
            }
        }

        /// <summary>
        /// downloads picture from Vestas image database
        /// </summary>
        /// <param name="initials">internal initials of person</param>
        /// <returns>picture as byte array</returns>
        private static byte[] DownloadPicture(string initials)
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    byte[] img = wc.DownloadData("http://photos.vestas.net/" + initials + ".jpg");
                    return img;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

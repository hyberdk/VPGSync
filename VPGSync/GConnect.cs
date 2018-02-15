using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Contacts;
using Google.GData.Client;
using Google.GData.Contacts;
using NLog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace VPGSync
{
    /// <summary>
    /// Class to communicate with Google Contact API
    /// </summary>
    internal static class GConnect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string groupId;
        private static string defaultGroupId;


        /// <summary>
        /// Logs into Google using OAuth2
        /// </summary>
        /// <returns>UserAuthentication including AccessToken and RefreshToken</returns>
        public static UserCredential Authenticate()
        {


            string[] scopes = new string[] { "https://www.google.com/m8/feeds/" };     // view your basic profile info.
            try
            {
                ClientSecrets cs = new ClientSecrets
                {
                    ClientId = Constants.GoogleClientId,
                    ClientSecret = Constants.GoogleClientSecret
                };

                // Use the current Google .net client library to get the Oauth2 stuff.
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                                                                            cs,
                                                                                            scopes,
                                                                                            "VPGSync",
                                                                                            CancellationToken.None,
                                                                                            new FileDataStore("VPGSync")).Result;


                if (IsTokenExpired(credential))
                {
                    bool success = false;
                    RefreshToken(credential, out success);
                    if (!success) return null;
                }

                return credential;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Authentication error");
                return null;
            }
        }

        /// <summary>
        /// Checks if AccessToken is expired or not
        /// </summary>
        /// <param name="uc">UserCrendential class with valid AccessToken</param>
        /// <returns>True if expired</returns>
        static bool IsTokenExpired(UserCredential uc)
        {
            if (uc.Token.IssuedUtc.AddSeconds((double)uc.Token.ExpiresInSeconds - 120) <= DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Converts UserCredentials to the older OAuth2Parameters
        /// </summary>
        /// <param name="userCred"></param>
        /// <returns></returns>
        static OAuth2Parameters ToOauth2Parameters(UserCredential userCred)
        {
            return new OAuth2Parameters
            {
                AccessToken = userCred.Token.AccessToken,
                RefreshToken = userCred.Token.RefreshToken
            };
        }

        /// <summary>
        /// Refreshes AccessToken
        /// </summary>
        /// <param name="credentials">has to contain a valid RefreshToken</param>
        /// <param name="success">true if we succeded to get a new AccessToken</param>
        /// <returns>a fresh AccessToken</returns>
        static UserCredential RefreshToken(UserCredential credentials, out bool success)
        {
            success = false;
            if (!IsTokenExpired(credentials))
            {
                success = true;
                logger.Log(LogLevel.Debug, "Token do not need refresh");
                return credentials;
            }

            logger.Log(LogLevel.Info, "Refreshing token, its expired");
            try
            {
                Task<bool> task = credentials.RefreshTokenAsync(CancellationToken.None);

                task.Wait();
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    success = true;
                }

                return credentials;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Refresh of Token failed");
            }

            return null;
        }

        /// <summary>
        /// Converts a UserCredential class to a ContactRequest 
        /// </summary>
        /// <param name="creds"></param>
        /// <returns></returns>
        static ContactsRequest BuildContactsRequest(UserCredential creds)
        {
            RequestSettings settings = new RequestSettings("VPGSync", ToOauth2Parameters(creds));
            ContactsRequest cr = new ContactsRequest(settings);
            return cr;
        }

        /// <summary>
        /// Retrieved all contacts from Google
        /// </summary>
        /// <param name="creds">Has to contain a valid Token, if AccessToken is expired, it will refresh it</param>
        /// <returns>All your Google Contacts</returns>
        public static Feed<Contact> GetAllContacts(UserCredential creds)
        {

            RefreshToken(creds, out bool success);
            if (!success)
            {
                logger.Warn("could not refresh token, returns no contacts");
                return null;
            }

            try
            {

                var cr = BuildContactsRequest(creds);

                Feed<Contact> f = cr.GetContacts();
                f.AutoPaging = true;
                return f;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not download contacts ;-(");
            }
            return null;
        }

        /// <summary>
        /// Deletes a contact from Google
        /// </summary>
        /// <param name="contact">Google Contact, has to be full contact</param>
        /// <param name="creds">credentials with valid token/refresh token</param>
        public static void DeleteContact(Contact contact, UserCredential creds)
        {

            var cr = BuildContactsRequest(creds);

            try
            {
                cr.Delete(contact);
            }
            //catch (GDataVersionConflictException e)
            catch (Exception ex)
            {
                logger.Error(ex, "error deleting contact from Google");
            }


        }

        /// <summary>
        /// Get all contacts that contains the group label used. (see Constants class)
        /// </summary>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns>Contacts found, null is error occured</returns>
        public static Feed<Contact> GetGroupContacts(UserCredential creds)
        {

            string groupId = GetGroupId(creds);

            if (string.IsNullOrEmpty(groupId))
            {
                logger.Warn("GroupID is empty, we have to abort the getting contacts");
                return null;
            }

            var cr = BuildContactsRequest(creds);

            try
            {
                RefreshToken(creds, out bool success);
                if (!success) return null;

                ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
                query.Group = groupId;

                Feed<Contact> feed = cr.Get<Contact>(query);
                feed.AutoPaging = true;
                logger.Info("Google contacts returned: " + feed.TotalResults);
                return feed;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not get contacts from Google");
                return null;
            }
        }

        /// <summary>
        /// Updates a single Google Contact
        /// </summary>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <param name="contact">full google contact</param>
        /// <returns>updated contact, null if error occured</returns>
        public static Contact UpdateContact(UserCredential creds, Contact contact)
        {
            try
            {
                RefreshToken(creds, out bool success);
                if (!success) return null;

                var cr = BuildContactsRequest(creds);

                Contact updatedContact = cr.Update(contact);
                //Console.WriteLine("Updated: " + updatedEntry.Updated.ToString());
                return updatedContact;
            }
            catch (GDataVersionConflictException e)
            {
                logger.Error(e, "Error occured while updating contact");
            }
            return null;

        }

        /// <summary>
        /// Creates new contact
        /// </summary>
        /// <param name="newEntry">New contact item.</param>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns>The new contact, null is returned if error occured</returns>
        public static Contact CreateNewContact(Contact newEntry, UserCredential creds)
        {
            RefreshToken(creds, out bool success);

            if (!success)
            {
                logger.Warn("Refresh of Token failed, no contact created.");
                return null;
            }

            newEntry = CheckGroupMembership(newEntry, creds);

            var cr = BuildContactsRequest(creds);

            // Insert the contact.
            Uri feedUri = new Uri(ContactsQuery.CreateContactsUri("default"));
            Contact createdEntry = cr.Insert(feedUri, newEntry);
            
            return createdEntry;


        }

        /// <summary>
        /// Check if the contact is a member of the correct groups. 
        /// If its not it will add it.
        /// </summary>
        /// <param name="contact">Google Contact</param>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns>updated contact with the correct membership</returns>
        static Contact CheckGroupMembership(Contact contact, UserCredential creds)
        {
            bool idFound = false;
            bool defaultIdFound = false;
            foreach (var item in contact.GroupMembership)
            {
                if (item.HRef == GetGroupId(creds)) idFound = true;
                if (item.HRef == GetDefaultGroupId(creds)) defaultIdFound = true;
                if (idFound && defaultIdFound) break;

            }

            if (!defaultIdFound)
            {
                contact.GroupMembership.Add(new GroupMembership
                {
                    HRef = GetDefaultGroupId(creds)
                });
            }

            if (!idFound)
            {

                contact.GroupMembership.Add(new GroupMembership
                {
                    HRef = GetGroupId(creds)
                });

            }

            return contact;
        }

        /// <summary>
        /// Get the default group/label ID/reference from Google.
        /// This is the system group "System Group: My Contacts"
        /// </summary>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns>returns blank on error</returns>
        static string GetDefaultGroupId(UserCredential creds)
        {
            if (!string.IsNullOrEmpty(defaultGroupId)) return defaultGroupId;
            FillGroups(creds);
            return defaultGroupId;
        }

        static void FillGroups(UserCredential creds)
        {
            RefreshToken(creds, out bool success);
            if (!success) return;

            ContactsRequest cr = BuildContactsRequest(creds);

            try
            {
                Feed<Group> fg = cr.GetGroups();
                fg.AutoPaging = true;

                foreach (Group group in fg.Entries)
                {
                    if (group.Title == Constants.GroupName) groupId = group.Id;
                    if (group.Title == "System Group: My Contacts") defaultGroupId = group.Id;
                    if (!string.IsNullOrEmpty(groupId) && !string.IsNullOrEmpty(defaultGroupId)) break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not get groups from Google");
            }
        }

        /// <summary>
        /// Get the VPGsync group/label ID/reference from Google.
        /// This is the system group set in Constants class
        /// </summary>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns></returns>
        static string GetGroupId(UserCredential creds)
        {
            if (!string.IsNullOrEmpty(groupId)) return groupId;

            FillGroups(creds);
            return groupId;

        }

        /// <summary>
        /// Creates a Contact group/label in Google Contacts. This is only used on the first
        /// sync where we need to create the new group/label
        /// </summary>
        /// <param name="creds">credentials with valid token/refresh token</param>
        /// <returns></returns>
        static string CreateContactGroup(UserCredential creds)
        {

            ContactsRequest cr = BuildContactsRequest(creds);
            try
            {
                RefreshToken(creds, out bool success);
                if (!success) return "";

                Group newGroup = new Group();
                newGroup.Title = Constants.GroupName;

                Group createdGroup = cr.Insert(new Uri("https://www.google.com/m8/feeds/groups/default/full"),
                    newGroup);

                logger.Log(LogLevel.Info, Constants.GroupName + " Group Atom Id: " + createdGroup.Id);
                return createdGroup.Id;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error creating group " + Constants.GroupName);
                return "";
            }

        }

        public static void UpdatePhoto(UserCredential creds, Contact contact, byte[] photo)
        {
            ContactsRequest cr = BuildContactsRequest(creds);

            try
            {
                Stream io = new MemoryStream(photo);
                cr.SetPhoto(contact, io);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Could not update picture in Google Contact");
            }
            //catch (GDataVersionConflictException e)
            //{
            //    // Etags mismatch: handle the exception.
            //}

        }

    }



    }


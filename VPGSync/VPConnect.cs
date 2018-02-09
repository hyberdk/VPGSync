using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGSync
{
    /// <summary>
    /// Class to communicate with vp-db.vestas.net
    /// </summary>
    internal static class VPConnect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string GetConnectionString()
        {
            return Constants.VPDBConnectionString;
        }


        /// <summary>
        /// Get data from SQL server.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="connection">Connection string on how to connect</param>
        /// <returns>raw data returned from SQL server, null is error occured.</returns>
        private static DataSet GetData(string sql, SqlConnection connection)
        {

            try
            {
                DataSet data = new DataSet();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connection);
                sqlDataAdapter.Fill(data);

                return data;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "database communication error");
            }
            return null;
        }

        /// <summary>
        /// Get the contacts from VP-DB that is marked by initials
        /// </summary>
        /// <param name="initials">initials of user who initiates the sync</param>
        /// <returns>all the contacts, null if error occurs</returns>
        public static Dictionary<string, VPContact> GetContacts(string initials)
        {
            DataSet data;

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    //Get ID of user
                    int id = GetVpId(initials, connection);
                    if (id == 0) return null;

                    //Get all contacts users
                    data = GetContacts(id, connection);
                    if (data == null) return null;

                }

                Dictionary<string, VPContact> vpcontacts = Convert(data);
                return vpcontacts;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting contacts");
                return null;
            }

        }

        /// <summary>
        /// Converts the SQL DataSet, to the VPContact class
        /// </summary>
        /// <param name="vpUsers">the "raw" output from the SQL server</param>
        /// <returns>dict of VPContact users, label is user initials. Returns null on error</returns>
        private static Dictionary<string, VPContact> Convert(DataSet vpUsers)
        {
            Dictionary<string, VPContact> users = new Dictionary<string, VPContact>();

            if (vpUsers == null || vpUsers.Tables.Count == 0) return null;
            if (vpUsers.Tables[0].Rows.Count == 0) return users;

            try
            {
                foreach (DataRow row in vpUsers.Tables[0].Rows)
                {
                    VPContact user = new VPContact();

                    if (row["Id"] != null) user.Id = (int)row["Id"];
                    if (row["Sign"] != null) user.Initials = (string)row["Sign"];
                    //FormattedName
                    if (row["FirstName"] != null) user.NameGiven = (string)row["FirstName"];
                    if (row["LastName"] != null) user.NameFamily = (string)row["LastName"];
                    //Department
                    //Location
                    if (row["Title"] != null) user.WorkTitle = (string)row["Title"];
                    if (row["LocalTelephone"] != null) user.PhoneWorkLandline = (string)row["LocalTelephone"];
                    if (row["MobileTelephone"] != null) user.PhoneWorkMobile = (string)row["MobileTelephone"];
                    //HiddenMobileTelephone
                    if (row["Email"] != null) user.EmailWork = (string)row["Email"];
                    //if (row["ExternalContractor"] != null) user.IsExternalContractor = (bool)row["ExternalContractor"];
                    if (row["PrivateMobileTelephone"] != null) user.PhonePrivateMobile = (string)row["PrivateMobileTelephone"];
                    if (row["PrivateEmail"] != null) user.EmailPrivate = (string)row["PrivateEmail"];
                    //if (row["ChngDate"] != null) user.ChangeDate = (DateTime)row["ChngDate"];
                    if (row["Opt2"] != null) user.Department = (string)row["Opt2"];

                    users.Add(user.Initials, user);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occured while creating VPcontacts");
                return null;
            }

            return users;

        }

        /// <summary>
        /// Get all contacts marked as to be synced in VP-DB
        /// </summary>
        /// <param name="id">VP-ID of user</param>
        /// <param name="connection">SQL connection string</param>
        /// <returns>"raw" dataset, null on error.</returns>
        private static DataSet GetContacts(int id, SqlConnection connection)
        {
            string sql =
                @"select distinct
		            VPPerson.Id,
		            VPPerson.Sign,
		            VPPerson.FormattedName,
		            VPPerson.FirstName,
		            VPPerson.LastName,
		            VPPerson.Department,
		            VPPerson.Location,
		            VPPerson.Title,
		            VPPerson.LocalTelephone,
		            VPPerson.MobileTelephone,
		            VPPerson.HiddenMobileTelephone,
		            VPPerson.Email,
		            VPPerson.ExternalContractor,
		            VPPerson.PrivateMobileTelephone,
		            VPPerson.PrivateEmail,
		            VPPerson.ChngDate,
		            VPPerson.Opt2,
		            VPPerson.HasLeft from dbo.VPPerson VPPerson
              full join dbo.VPDptOutlookSync VPDptOutlookSync on VPPerson.Department=VPDptOutlookSync.RequestedVPDptID
              full join dbo.VPPersonOutlookSync VPPersonOutlookSync on VPPerson.Id=VPPersonOutlookSync.RequestedVPPersonID
              where (VPDptOutlookSync.SubscriberVPPersonID = " + id + " OR VPPersonOutlookSync.SubscriberVPPersonID = " + id + ") AND VPPerson.HasLeft = 'false';";

            try
            {
                DataSet data = GetData(sql, connection);
                return data;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not get VP Contacts from ID");
                return null;
            }
        }

        /// <summary>
        /// Get the VP-DB ID from the initials
        /// </summary>
        /// <param name="initials">initils of person who initiates the sync</param>
        /// <param name="connection">SQL connection string.</param>
        /// <returns>0 on error, or the user ID</returns>
        private static int GetVpId(string initials, SqlConnection connection)
        {
            string sql = "select Id from dbo.VPPerson where sign = '" + initials + "';";

            try
            {
                DataSet data = GetData(sql, connection);
                int id = (int)data.Tables[0].Rows[0]["Id"];
                return id;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not get user ID");
                return 0;
            }



        }

    }
}

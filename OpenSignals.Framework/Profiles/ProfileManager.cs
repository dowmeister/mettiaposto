using System;
using System.Collections.Generic;
using System.Web.Security;
using log4net;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Profiles
{
    /// <summary>
    /// Custom implementation of ASP.NET MembershipProvider based on NHibernate
    /// </summary>
    class ProfileManager : MembershipProvider
    {
        private NHibernateSessionManager connection = new NHibernateSessionManager();

        private ILog _log = null;
        private System.Collections.Specialized.NameValueCollection config;

        /// <summary>
        /// Gets the log.
        /// </summary>
        private ILog log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger("System");

                return _log;
            }
        }

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// The name of the application using the custom membership provider.
        ///   </returns>
        public override string ApplicationName
        {
            get
            {
                return "opensignals";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The name of the provider is null.
        ///   </exception>
        ///   
        /// <exception cref="T:System.ArgumentException">
        /// The name of the provider has a length of zero.
        ///   </exception>
        ///   
        /// <exception cref="T:System.InvalidOperationException">
        /// An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.
        ///   </exception>
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            this.config = config;
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            try
            {
                MembershipProfile user = new MembershipProfile();
                user.UserName = username;
                user.ProviderUserKey = Guid.NewGuid();
                user.Email = email;
                user.PasswordQuestion = passwordQuestion;
                user.Comment = string.Empty;
                user.IsApproved = true;
                user.ApplicationName = this.ApplicationName;
                user.CreationDate = DateTime.Now;
                user.IsLockedOut = false;
                user.LastActivityDate = DateTime.Now;
                user.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
                user.PasswordQuestion = passwordQuestion;
                user.ProviderName = this.GetType().ToString();
                status = MembershipCreateStatus.Success;

                connection.OpenSession();
                connection.OpenTransaction();
                connection.Session.Save(user);
                connection.CommitTransaction();

                return user;
            }
            catch (Exception)
            {
                connection.RollbackTransaction();                
                throw;
            }
            finally
            {
            }
            
        }

        /// <summary>
        /// Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile mp = (MembershipProfile)this.GetUser(username, false);
                connection.OpenTransaction();
                connection.Session.Delete(mp);
                connection.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                connection.RollbackTransaction();
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
        ///   </returns>
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        ///   </returns>
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            try
            {
                connection.OpenSession();

                totalRecords = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("Email", emailToMatch))
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>().Value;

                IList<MembershipProfile> mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("Email", emailToMatch))
                    .SetFirstResult(pageIndex*pageSize)
                    .SetMaxResults((pageIndex*pageSize)+pageSize)
                    .List<MembershipProfile>();

                MembershipUserCollection mpu = new MembershipUserCollection();
                foreach(MembershipProfile u in mp)
                {
                    mpu.Add(u);
                }
                return mpu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            try
            {
                connection.OpenSession();

                totalRecords = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("UserName", usernameToMatch))
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>().Value;

                IList<MembershipProfile> mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("UserName", usernameToMatch))
                    .SetFirstResult(pageIndex * pageSize)
                    .SetMaxResults((pageIndex * pageSize) + pageSize)
                    .List<MembershipProfile>();

                MembershipUserCollection mpu = new MembershipUserCollection();
                foreach (MembershipProfile u in mp)
                {
                    mpu.Add(u);
                }
                return mpu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            try
            {
                connection.OpenSession();

                totalRecords = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .SetProjection(Projections.RowCount())
                    .FutureValue<int>().Value;

                IList<MembershipProfile> mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .SetFirstResult(pageIndex * pageSize)
                    .SetMaxResults((pageIndex * pageSize) + pageSize)
                    .List<MembershipProfile>();

                MembershipUserCollection mpu = new MembershipUserCollection();
                foreach (MembershipProfile u in mp)
                {
                    mpu.Add(u);
                }
                return mpu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for.</param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("UserName", username))
                    .UniqueResult<MembershipProfile>();

                if (userIsOnline)
                {
                    mp.LastActivityDate = DateTime.Now;
                    connection.Session.SaveOrUpdate(mp);
                }

                return mp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("ProviderUserKey", providerUserKey))
                    .UniqueResult<MembershipProfile>();

                if (userIsOnline)
                {
                    mp.LastActivityDate = DateTime.Now;
                    connection.Session.SaveOrUpdate(mp);
                }

                return mp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        public override string GetUserNameByEmail(string email)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile mp = connection.Session.CreateCriteria(typeof(MembershipProfile))
                    .Add(Restrictions.Eq("Email", email))
                    .UniqueResult<MembershipProfile>();

                return mp.UserName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents the user to update and the updated information for the user.</param>
        public override void UpdateUser(MembershipUser user)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile mp = (MembershipProfile)this.GetUser(user.UserName, false);
                connection.OpenTransaction();
                connection.Session.SaveOrUpdate(mp);
                connection.CommitTransaction();
            }
            catch (Exception ex)
            {
                connection.RollbackTransaction();
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        public override bool ValidateUser(string username, string password)
        {
            try
            {
                connection.OpenSession();
                MembershipProfile user = (MembershipProfile)this.GetUser(username, true);

                if (user != null)
                {
                    if (FormsAuthentication.HashPasswordForStoringInConfigFile(password,"sha1").Equals(user.Password))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
            }
            
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        ///   </returns>
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        ///   </returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>
        /// The minimum length required for a password.
        ///   </returns>
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        ///   </returns>
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.
        ///   </returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        ///   </returns>
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        ///   </returns>
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>
        /// A regular expression used to evaluate a password.
        ///   </returns>
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}

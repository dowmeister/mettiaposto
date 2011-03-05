using System;
using System.Web.Security;

namespace OpenSignals.Framework.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class MembershipProfile : MembershipUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipProfile"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="name">The name.</param>
        /// <param name="providerUserKey">The provider user key.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="isLockedOut">if set to <c>true</c> [is locked out].</param>
        /// <param name="creationDate">The creation date.</param>
        /// <param name="lastLoginDate">The last login date.</param>
        /// <param name="lastActivityDate">The last activity date.</param>
        /// <param name="lastPasswordChangedDate">The last password changed date.</param>
        /// <param name="lastLockoutDate">The last lockout date.</param>
        public MembershipProfile(string providerName, string name, object providerUserKey, string email, string passwordQuestion,
            string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate,
            DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
            : base(providerName, name, providerUserKey, email, passwordQuestion,
                comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipProfile"/> class.
        /// </summary>
        public MembershipProfile() { }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets the user identifier from the membership data source for the user.
        /// </summary>
        /// <returns>
        /// The user identifier from the membership data source for the user.
        ///   </returns>
        public new virtual object ProviderUserKey { get; set;  }

        /// <summary>
        /// Gets the most recent date and time that the membership user was locked out.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> object that represents the most recent date and time that the membership user was locked out.
        ///   </returns>
        public new virtual DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets the password question for the membership user.
        /// </summary>
        /// <returns>
        /// The password question for the membership user.
        ///   </returns>
        public new virtual string PasswordQuestion { get; set; }

        /// <summary>
        /// Gets the name of the membership provider that stores and retrieves user information for the membership user.
        /// </summary>
        /// <returns>
        /// The name of the membership provider that stores and retrieves user information for the membership user.
        ///   </returns>
        public new virtual string ProviderName { get; set; }
        /// <summary>
        /// Gets the logon name of the membership user.
        /// </summary>
        /// <returns>
        /// The logon name of the membership user.
        ///   </returns>
        public new virtual string UserName { get; set; }
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public virtual string ApplicationName { get; set; }
        /// <summary>
        /// Gets a value indicating whether the membership user is locked out and unable to be validated.
        /// </summary>
        /// <returns>true if the membership user is locked out and unable to be validated; otherwise, false.
        ///   </returns>
        public new virtual bool IsLockedOut { get; set; }
        /// <summary>
        /// Gets the date and time when the user was added to the membership data store.
        /// </summary>
        /// <returns>
        /// The date and time when the user was added to the membership data store.
        ///   </returns>
        public new virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the last password change date.
        /// </summary>
        /// <value>
        /// The last password change date.
        /// </value>
        public virtual DateTime LastPasswordChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>
        /// The user ID.
        /// </value>
        public virtual int UserID { get; set; }
        
        /// <summary>
        /// Gets whether the user is currently online.
        /// </summary>
        /// <returns>true if the user is online; otherwise, false.
        ///   </returns>
        public new virtual bool IsOnline { get; set; }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Gets the password for the membership user from the membership data store.
        /// </summary>
        /// <returns>
        /// The password for the membership user.
        /// </returns>
        public new virtual string GetPassword() { return string.Empty; }
    }
}

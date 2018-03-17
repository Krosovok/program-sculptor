
using System;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Data access interface for users to enter the system.
    /// </summary>
    public interface IUserDao
    {
        /// <summary>
        /// Tries to login in user with given password.
        /// </summary>
        /// <param name="username">Username of user to login.</param>
        /// <param name="password">Password given by a user to authenticate.</param>
        /// <exception cref="UnauthorizedAccessException">Occures when password is wrong for given username.</exception>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        /// <returns>True, if logged in successfully, false if user with given name not found.</returns>
        bool Login(string username, string password);

        /// <summary>
        /// Returns currently logged user for this program instance or special value "Guest" if there is no currently logged user.
        /// </summary>
        string CurrentUser { get; }
    }
}

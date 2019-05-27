using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP2000V_DesktopApp_g11.Models;

namespace APP2000V_DesktopApp_g11.Controllers
{
    class EmployeeController
    {
        Persistence Db = new Persistence();

        private bool ValidateUserInfo(User user)
        {
            if (user.Username == null || user.Username.Equals(""))
            {
                Log.Error("You need to provide a username!");
                return false;
            }
            if (user.FirstName == null || user.FirstName.Equals(""))
            {
                Log.Error("You need to provide a first name!");
                return false;
            }
            if (user.LastName == null || user.LastName.Equals(""))
            {
                Log.Error("You need to provide a last name!");
                return false;
            }
            return true;
        }

        internal bool CreateUser(User user)
        {
            user.Role = 1;
            if (!ValidateUserInfo(user))
            {
                return false;
            }

            if (user.Password == null || user.Password.Equals(""))
            {
                Log.Error("You need to provide a password!");
                return false;
            }

            if (Db.GetSingleUserByUsername(user.Username) != null)
            {
                Log.Error("This username is already taken! Please provide a different username.");
                return false;
            }

            if (Db.CreateUser(user) == 0)
            {
                Log.Message("User is registered!", "The user was successfully registered.");
                return true;
            }
            else
            {
                Log.Error("Could not update user information!");
                return false;
            }
        }

        internal bool UpdateUser(User user)
        {
            if (!ValidateUserInfo(user))
            {
                return false;
            }

            if (Db.GetSingleUserByUsername(user.Username) == null)
            {
                Log.Error("There is no user with this username!");
                return false;
            }

            int result = Db.UpdateUser(user);
            if (result == 0)
            {
                Log.Message("User updated!", "The user information was updated successfully.");
                return true;
            }
            else if (result == 2)
            {
                Log.Error("Could not find user!");
                return false;
            }
            else if (result == 1)
            {
                Log.Error("Could not update user!");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
            }

        }

        internal bool DeleteUser(User user)
        {
            if (Db.GetSingleUserByUsername(user.Username) == null)
            {
                Log.Error("There is no user with this username!");
                return false;
            }

            int result = Db.DeleteUser(user.Username);
            if (result == 0)
            {
                Log.Message("User deleted!", "The user was successfully deleted.");
                return true;
            }
            else if (result == 2)
            {
                Log.Error("Could not find user");
                return false;
            }
            else
            {
                Log.Error("Unknown error!");
                return false;
            }
        }
    }
}

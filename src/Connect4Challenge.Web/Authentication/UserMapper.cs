using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web.Authentication
{
    public class UserMapper : IUserMapper
    {
        public UserMapper()
        {

        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            //var cls = new List<Claim>();
            //if (Users.ContainsKey(identifier))
            //{
            //    if (context.Items.ContainsKey("UserLevel"))
            //    {
            //        var user = Users[identifier];
            //        cls.Add(new Claim("UserLevel", context.Items["UserLevel"].ToString(), ClaimValueTypes.Integer));
            //        user.Claims = cls.Select(c => c.ToString());
            //        Users[identifier] = user;
            //    }
            //}
            //else
            //{
            //    cls.Add(new Claim("UserLevel", "10"));
            //    Users.Add(identifier, new UserIdentity { UserName = "alex", Claims = cls.Select(c => c.ToString()) });
            //}

            //return Users[identifier];

            return new UserIdentity { UserName = "admin" };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RideSharingApp.BLL.Interfaces;
using RideSharingApp.WEB.Controllers;

namespace RideSharingAPP.WEB.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //IAccountInformationService accountService;

        //public CustomRoleProvider(IAccountInformationService serv)
        //{
        //    accountService = serv;
        //}

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            if (/*!string.IsNullOrEmpty(accountService.FindEmail(username)) &&*/ !string.IsNullOrEmpty(AuthorizationController.Role))
            {
                roles = new string[] { AuthorizationController.Role };
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = new string[] { };
            if (/*!string.IsNullOrEmpty(accountService.FindEmail(username)) && */!string.IsNullOrEmpty(AuthorizationController.Role) && AuthorizationController.Role == roleName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
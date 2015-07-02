using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Urbanit.Polls
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManagePolls = new Permission
        {
            Description = "Managing Polls",
            Name = "ManagePolls"
        };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
                   {
                       ManagePolls,
                   };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
                   {
                       new PermissionStereotype
                       {
                           Name = "Administrator",
                           Permissions = new[] {ManagePolls}
                       },
                       new PermissionStereotype
                       {
                           Name = "Editor",
                           Permissions = new[] {ManagePolls}
                       },
                       new PermissionStereotype
                       {
                           Name = "Moderator",
                       },
                       new PermissionStereotype
                       {
                           Name = "Author",
                           Permissions = new[] {ManagePolls}
                       },
                       new PermissionStereotype
                       {
                           Name = "Contributor",
                       },
                   };
        }
    }
}
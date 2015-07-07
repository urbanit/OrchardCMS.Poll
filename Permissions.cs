using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Urbanit.Polls
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManagePolls = new Permission
        {
            Category = "Management for Urbanit Polls module",
            Description = "Allow the management of polls.",
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
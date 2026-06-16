using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MilkTeaPOS.Models;

namespace MilkTeaPOS.Helpers
{
    /// <summary>
    /// Provides runtime permission checking utilities for forms
    /// </summary>
    public static class PermissionChecker
    {
        /// <summary>
        /// Get the current user's role name from the database
        /// </summary>
        public static string GetCurrentRoleName()
        {
            try
            {
                var currentUserId = PostgresContext.CurrentUserId;
                if (!currentUserId.HasValue)
                    return "Cashier"; // Default if no user logged in

                using var context = new PostgresContext();
                var user = context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == currentUserId.Value);

                return user?.Role?.Name ?? "Cashier";
            }
            catch
            {
                // If DB query fails, default to most restrictive
                return "Cashier";
            }
        }

        /// <summary>
        /// Check if current user has access to a form
        /// </summary>
        public static bool HasAccessToForm(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.HasAccess(formName, roleName);
        }

        /// <summary>
        /// Get current user's permission level for a form
        /// </summary>
        public static PermissionLevel GetPermissionLevel(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.GetPermissionLevel(formName, roleName);
        }

        /// <summary>
        /// Check if current user can create records
        /// </summary>
        public static bool CanCreate(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.CanCreate(formName, roleName);
        }

        /// <summary>
        /// Check if current user can update records
        /// </summary>
        public static bool CanUpdate(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.CanUpdate(formName, roleName);
        }

        /// <summary>
        /// Check if current user can delete records
        /// </summary>
        public static bool CanDelete(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.CanDelete(formName, roleName);
        }

        /// <summary>
        /// Check if current user can view (read-only)
        /// </summary>
        public static bool CanView(string formName)
        {
            var roleName = GetCurrentRoleName();
            return RolePermissions.CanView(formName, roleName);
        }

        /// <summary>
        /// Apply UI restrictions based on permissions (hide/disable buttons)
        /// </summary>
        public static void ApplyPermissionsToButtons(string formName, 
            params System.Windows.Forms.Button[] buttons)
        {
            var permissionLevel = GetPermissionLevel(formName);

            foreach (var btn in buttons)
            {
                if (btn == null) continue;

                switch (permissionLevel)
                {
                    case PermissionLevel.None:
                        btn.Visible = false;
                        break;
                    case PermissionLevel.ReadOnly:
                        // Hide create/edit/delete buttons
                        btn.Visible = false;
                        break;
                    case PermissionLevel.Edit:
                        // Allow view/edit, hide delete
                        break; // Buttons remain visible, form logic handles restrictions
                    case PermissionLevel.Full:
                        // All buttons visible
                        break;
                }
            }
        }
    }
}

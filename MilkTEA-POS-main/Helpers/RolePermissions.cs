using System;
using System.Collections.Generic;

namespace MilkTeaPOS.Helpers
{
    /// <summary>
    /// Defines permission levels for different user roles
    /// </summary>
    public enum PermissionLevel
    {
        None,           // No access
        ReadOnly,       // View only
        Edit,           // View and edit
        Full            // Full access (CRUD + delete)
    }

    /// <summary>
    /// Permission configuration for each role
    /// </summary>
    public static class RolePermissions
    {
        // Permission matrix: Feature -> Role -> PermissionLevel
        private static readonly Dictionary<string, Dictionary<string, PermissionLevel>> _permissions = new()
        {
            // Dashboard - All roles can view
            { "frmDashboard", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.ReadOnly },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Categories - Admin full, Staff read-only, Cashier read-only
            { "frmCategories", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Products - Admin full, Staff edit, Cashier read-only
            { "frmProducts", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.Edit },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Toppings - Admin full, Staff read-only, Cashier read-only
            { "frmToppings", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Tables - Admin full, Staff edit, Cashier read-only
            { "frmTables", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.Edit },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // POS (Orders) - All roles can use
            { "frmOrders", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.Full },
                    { "Cashier", PermissionLevel.Full }
                }
            },

            // Order History - All roles can view
            { "frmOrderHistory", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.ReadOnly },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Customers - Admin full, Staff edit, Cashier read-only
            { "frmCustomers", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.Edit },
                    { "Cashier", PermissionLevel.ReadOnly }
                }
            },

            // Memberships - Admin full, Staff read-only, Cashier none
            { "frmMemberships", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.None }
                }
            },

            // Vouchers - Admin full, Staff read-only, Cashier none
            { "frmVouchers", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.None }
                }
            },

            // Reports - Admin full, Staff read-only, Cashier none
            { "frmSalesReport", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.ReadOnly },
                    { "Cashier", PermissionLevel.None }
                }
            },

            // Users Management - Admin only
            { "frmUsers", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.Full },
                    { "Staff", PermissionLevel.None },
                    { "Cashier", PermissionLevel.None }
                }
            },

            // Audit Log - Admin only
            { "frmAuditLog", new Dictionary<string, PermissionLevel>
                {
                    { "Admin", PermissionLevel.ReadOnly },
                    { "Staff", PermissionLevel.None },
                    { "Cashier", PermissionLevel.None }
                }
            }
        };

        /// <summary>
        /// Check if a role has access to a specific feature
        /// </summary>
        public static bool HasAccess(string formName, string roleName)
        {
            if (!_permissions.ContainsKey(formName))
                return false;

            if (!_permissions[formName].ContainsKey(roleName))
                return false;

            return _permissions[formName][roleName] != PermissionLevel.None;
        }

        /// <summary>
        /// Get permission level for a role on a specific feature
        /// </summary>
        public static PermissionLevel GetPermissionLevel(string formName, string roleName)
        {
            if (!_permissions.ContainsKey(formName))
                return PermissionLevel.None;

            if (!_permissions[formName].ContainsKey(roleName))
                return PermissionLevel.None;

            return _permissions[formName][roleName];
        }

        /// <summary>
        /// Check if user can perform CRUD operations
        /// </summary>
        public static bool CanCreate(string formName, string roleName)
        {
            var level = GetPermissionLevel(formName, roleName);
            return level == PermissionLevel.Full;
        }

        public static bool CanUpdate(string formName, string roleName)
        {
            var level = GetPermissionLevel(formName, roleName);
            return level == PermissionLevel.Full || level == PermissionLevel.Edit;
        }

        public static bool CanDelete(string formName, string roleName)
        {
            var level = GetPermissionLevel(formName, roleName);
            return level == PermissionLevel.Full;
        }

        public static bool CanView(string formName, string roleName)
        {
            var level = GetPermissionLevel(formName, roleName);
            return level != PermissionLevel.None;
        }
    }
}

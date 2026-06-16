using System.ComponentModel;

namespace MilkTeaPOS
{
    /// <summary>
    /// Helper to ensure app is configured at both runtime and design time.
    /// Prevents designer crashes caused by EF Core/Npgsql initialization issues.
    /// </summary>
    public static class DesignTimeHelper
    {
        private static bool _configured;
        private static readonly object _lock = new();

        /// <summary>
        /// Ensures app configuration is loaded. Safe to call from constructors at design time.
        /// Call this as the FIRST line in form constructors after InitializeComponent().
        /// </summary>
        public static void EnsureConfigured()
        {
            if (_configured) return;

            lock (_lock)
            {
                if (_configured) return;

                // Configure EF Core/Npgsql at design time to prevent crashes
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                _configured = true;
            }
        }

        /// <summary>
        /// Returns true if in design mode. Use this to skip runtime code in the designer.
        /// </summary>
        public static bool IsDesignMode =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
}

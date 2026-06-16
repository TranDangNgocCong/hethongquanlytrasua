using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace MilkTeaPOS.Models;

public partial class PostgresContext : DbContext
{
    // Global current user tracking for audit logs
    public static Guid? CurrentUserId { get; set; }
    public static string CurrentUserIP { get; set; } = "127.0.0.1"; // Default fallback

    // Static constructor - auto-detect local IP
    static PostgresContext()
    {
        try
        {
            // Get first non-loopback IPv4 address
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                    !System.Net.IPAddress.IsLoopback(ip))
                {
                    CurrentUserIP = ip.ToString();
                    return;
                }
            }
            // Keep default 127.0.0.1
        }
        catch
        {
            // Keep default 127.0.0.1
        }
    }

    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Automatically set audit context before save changes
    /// Uses connection-level SET so trigger can access it in same transaction
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Always set both user_id and ip_address if available
        if (CurrentUserId.HasValue || !string.IsNullOrEmpty(CurrentUserIP))
        {
            try
            {
                var conn = Database.GetDbConnection();
                bool wasClosed = conn.State == System.Data.ConnectionState.Closed;
                if (wasClosed) await conn.OpenAsync(cancellationToken);

                // Set user_id
                if (CurrentUserId.HasValue)
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SET app.current_user_id = '{CurrentUserId.Value}'";
                    await cmd.ExecuteNonQueryAsync(cancellationToken);
                }

                // Set client_ip (always if not empty)
                if (!string.IsNullOrEmpty(CurrentUserIP))
                {
                    using var cmd2 = conn.CreateCommand();
                    cmd2.CommandText = $"SET app.client_ip = '{CurrentUserIP}'";
                    await cmd2.ExecuteNonQueryAsync(cancellationToken);
                }
            }
            catch { }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        // Always set both user_id and ip_address if available
        if (CurrentUserId.HasValue || !string.IsNullOrEmpty(CurrentUserIP))
        {
            try
            {
                var conn = Database.GetDbConnection();
                bool wasClosed = conn.State == System.Data.ConnectionState.Closed;
                if (wasClosed) conn.Open();

                // Set user_id
                if (CurrentUserId.HasValue)
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SET app.current_user_id = '{CurrentUserId.Value}'";
                    cmd.ExecuteNonQuery();
                }

                // Set client_ip (always if not empty)
                if (!string.IsNullOrEmpty(CurrentUserIP))
                {
                    using var cmd2 = conn.CreateCommand();
                    cmd2.CommandText = $"SET app.client_ip = '{CurrentUserIP}'";
                    cmd2.ExecuteNonQuery();
                }
            }
            catch { }
        }

        return base.SaveChanges();
    }

    /// <summary>
    /// Factory method to create DbContext with audit context automatically set
    /// Usage: using var context = PostgresContext.CreateWithAudit();
    /// </summary>
    public static PostgresContext CreateWithAudit()
    {
        return new PostgresContext();
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderTopping> OrderToppings { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Topping> Toppings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(
            "Host=aws-1-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.tisoidtsgtqwifjfunrs;Password=nliwmgmwbwAnhZk7;SSL Mode=Require;Trust Server Certificate=true;Pooling=true;Minimum Pool Size=5;Maximum Pool Size=100;Timeout=30;Command Timeout=60;Keepalive=30;Max Auto Prepare=0;No Reset On Close=true;",
            o =>
            {
                o.EnableRetryOnFailure(3);
            });
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("ice_level", new[] { "0", "25", "50", "75", "100" })
            .HasPostgresEnum("membership_tier", new[] { "none", "silver", "gold", "platinum", "diamond" })
            .HasPostgresEnum("order_detail_status", new[] { "pending", "preparing", "ready", "served", "cancelled" })
            .HasPostgresEnum("order_status", new[] { "pending", "preparing", "ready", "served", "cancelled" })
            .HasPostgresEnum("payment_method", new[] { "cash", "card", "qr_code", "bank_transfer", "e_wallet" })
            .HasPostgresEnum("payment_status", new[] { "pending", "completed", "failed", "refunded" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("size_type", new[] { "S", "M", "L" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresEnum("sugar_level", new[] { "0", "25", "50", "75", "100" })
            .HasPostgresEnum("table_status", new[] { "available", "occupied", "reserved", "maintenance" })
            .HasPostgresEnum("voucher_status", new[] { "active", "inactive", "expired", "used_up" })
            .HasPostgresEnum("voucher_type", new[] { "percentage", "fixed_amount", "free_item", "buy_one_get_one" })
            .HasPostgresExtension("extensions", "hypopg")
            .HasPostgresExtension("extensions", "index_advisor")
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs");

            entity.HasIndex(e => e.Action, "idx_audit_logs_action");

            entity.HasIndex(e => e.CreatedAt, "idx_audit_logs_created_at");

            entity.HasIndex(e => e.CreatedAt, "idx_audit_logs_created_at_brin").HasMethod("brin");

            entity.HasIndex(e => e.RecordId, "idx_audit_logs_record_id");

            entity.HasIndex(e => e.TableName, "idx_audit_logs_table_name");

            entity.HasIndex(e => new { e.UserId, e.Action }, "idx_audit_logs_user_action");

            entity.HasIndex(e => e.UserId, "idx_audit_logs_user_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.NewValues)
                .HasColumnType("jsonb")
                .HasColumnName("new_values");
            entity.Property(e => e.OldValues)
                .HasColumnType("jsonb")
                .HasColumnName("old_values");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .HasColumnName("table_name");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("audit_logs_user_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories", tb => tb.HasComment("Product categories for menu organization"));

            entity.HasIndex(e => e.DisplayOrder, "idx_categories_display_order");

            entity.HasIndex(e => e.IsActive, "idx_categories_is_active");

            entity.HasIndex(e => e.Name, "idx_categories_name");

            entity.HasIndex(e => new { e.DisplayOrder, e.CreatedAt }, "idx_categories_order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0)
                .HasColumnName("display_order");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "customers_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "customers_phone_key").IsUnique();

            entity.HasIndex(e => e.Email, "idx_customers_email");

            entity.HasIndex(e => e.Name, "idx_customers_name");

            entity.HasIndex(e => e.Phone, "idx_customers_phone");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("memberships_pkey");

            entity.ToTable("memberships");

            entity.HasIndex(e => e.CustomerId, "idx_memberships_customer_id");

            entity.HasIndex(e => e.Points, "idx_memberships_points");

            entity.HasIndex(e => e.CustomerId, "memberships_customer_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("joined_at");
            entity.Property(e => e.LastOrderAt).HasColumnName("last_order_at");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.Tier)
                .HasColumnType("membership_tier")
                .HasColumnName("tier")
                .HasConversion<string>();
            entity.Property(e => e.TotalOrders)
                .HasDefaultValue(0)
                .HasColumnName("total_orders");
            entity.Property(e => e.TotalSpent)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("total_spent");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Customer).WithOne(p => p.Membership)
                .HasForeignKey<Membership>(d => d.CustomerId)
                .HasConstraintName("memberships_customer_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders", tb => tb.HasComment("Customer orders with status tracking"));

            entity.HasIndex(e => e.CreatedAt, "idx_orders_created_at");

            entity.HasIndex(e => e.CustomerId, "idx_orders_customer_id");

            entity.HasIndex(e => e.CustomerPhone, "idx_orders_customer_phone");

            entity.HasIndex(e => e.IsDelivery, "idx_orders_is_delivery");

            entity.HasIndex(e => e.MembershipDiscount, "idx_orders_membership_discount");

            entity.HasIndex(e => e.OrderNumber, "idx_orders_order_number");

            entity.HasIndex(e => e.ServedAt, "idx_orders_served_at");

            entity.HasIndex(e => e.TableId, "idx_orders_table_id");

            entity.HasIndex(e => e.TotalAmount, "idx_orders_total_amount");

            entity.HasIndex(e => e.UpdatedAt, "idx_orders_updated_at");

            entity.HasIndex(e => e.UserId, "idx_orders_user_id");

            entity.HasIndex(e => e.VoucherDiscount, "idx_orders_voucher_discount");

            entity.HasIndex(e => e.OrderNumber, "orders_order_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CancelledAt).HasColumnName("cancelled_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(20)
                .HasColumnName("customer_phone");
            entity.Property(e => e.Discount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("discount");
            entity.Property(e => e.IsDelivery)
                .HasDefaultValue(false)
                .HasColumnName("is_delivery");
            entity.Property(e => e.MembershipDiscount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("membership_discount");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .HasColumnName("order_number");
            entity.Property(e => e.ServedAt).HasColumnName("served_at");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("subtotal");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoucherDiscount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("voucher_discount");
            entity.Property(e => e.Status)
                .HasColumnType("order_status")
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_customer_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_table_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_user_id_fkey");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_details_pkey");

            entity.ToTable("order_details", tb => tb.HasComment("Line items in each order with customization (size, sugar, ice)"));

            entity.HasIndex(e => e.CreatedAt, "idx_order_details_created_at");

            entity.HasIndex(e => new { e.OrderId, e.CreatedAt }, "idx_order_details_order_created");

            entity.HasIndex(e => e.OrderId, "idx_order_details_order_id");

            entity.HasIndex(e => e.ProductId, "idx_order_details_product_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductImage)
                .HasComment("Snapshot of product image at order time")
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasComment("Snapshot of product name at order time (immutable)")
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.SpecialInstructions).HasColumnName("special_instructions");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasComment("Final price: (unit_price + topping_total) * quantity")
                .HasColumnName("subtotal");
            entity.Property(e => e.ToppingTotal)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasComment("Total price of all toppings for this item")
                .HasColumnName("topping_total");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(12, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_details_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_details_product_id_fkey");
        });

        modelBuilder.Entity<OrderTopping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_toppings_pkey");

            entity.ToTable("order_toppings", tb => tb.HasComment("Toppings added to each order detail"));

            entity.HasIndex(e => e.OrderDetailId, "idx_order_toppings_order_detail_id");

            entity.HasIndex(e => e.ToppingId, "idx_order_toppings_topping_id");

            entity.HasIndex(e => new { e.OrderDetailId, e.ToppingId }, "idx_order_toppings_unique_lookup");

            entity.HasIndex(e => new { e.OrderDetailId, e.ToppingId }, "unique_order_topping").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasComment("Quantity of this topping (prevents duplicates via UPSERT)")
                .HasColumnName("quantity");
            entity.Property(e => e.ToppingId).HasColumnName("topping_id");
            entity.Property(e => e.ToppingName)
                .HasMaxLength(100)
                .HasComment("Snapshot of topping name at order time (immutable)")
                .HasColumnName("topping_name");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.OrderToppings)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("order_toppings_order_detail_id_fkey");

            entity.HasOne(d => d.Topping).WithMany(p => p.OrderToppings)
                .HasForeignKey(d => d.ToppingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_toppings_topping_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments", tb => tb.HasComment("Payment records with split amount tracking (received/paid/change) and status enforcement"));

            entity.HasIndex(e => e.CreatedAt, "idx_payments_created_at");

            entity.HasIndex(e => e.OrderId, "idx_payments_order_id");

            entity.HasIndex(e => e.PaidAt, "idx_payments_paid_at");

            entity.HasIndex(e => e.TransactionId, "idx_payments_transaction_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ChangeAmount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasComment("Change returned to customer (received_amount - paid_amount)")
                .HasColumnName("change_amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaidAmount)
                .HasPrecision(12, 2)
                .HasComment("Actual amount charged (after discount/rounding)")
                .HasColumnName("paid_amount");
            entity.Property(e => e.PaidAt)
                .HasComment("Timestamp when payment was completed")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentInfo).HasColumnName("payment_info");
            entity.Property(e => e.ReceivedAmount)
                .HasPrecision(12, 2)
                .HasComment("Amount customer gave (for cash payments)")
                .HasColumnName("received_amount");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Method)
                .HasColumnType("payment_method")
                .HasColumnName("method");
            entity.Property(e => e.Status)
                .HasColumnType("payment_status")
                .HasColumnName("status");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("payments_order_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products", tb => tb.HasComment("Individual drink items with base pricing"));

            entity.HasIndex(e => e.BasePrice, "idx_products_base_price");

            entity.HasIndex(e => new { e.CategoryId, e.IsAvailable }, "idx_products_category_available");

            entity.HasIndex(e => e.CategoryId, "idx_products_category_id");

            entity.HasIndex(e => e.CreatedAt, "idx_products_created_at");

            entity.HasIndex(e => e.IsAvailable, "idx_products_is_available");

            entity.HasIndex(e => e.IsFeatured, "idx_products_is_featured");

            entity.HasIndex(e => e.Name, "idx_products_name");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BasePrice)
                .HasPrecision(12, 2)
                .HasColumnName("base_price");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.IsFeatured)
                .HasDefaultValue(false)
                .HasColumnName("is_featured");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.PreparationTime)
                .HasDefaultValue(5)
                .HasColumnName("preparation_time");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("products_category_id_fkey");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_sizes_pkey");

            entity.ToTable("product_sizes", tb => tb.HasComment("Size variants (S/M/L) with different prices"));

            entity.HasIndex(e => e.Price, "idx_product_sizes_price");

            entity.HasIndex(e => e.ProductId, "idx_product_sizes_product_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSizes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_sizes_product_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles", tb => tb.HasComment("User roles: Admin, Staff, Cashier"));

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tables_pkey");

            entity.ToTable("tables", tb => tb.HasComment("Physical tables in the shop with location and capacity"));

            entity.HasIndex(e => e.Capacity, "idx_tables_capacity");

            entity.HasIndex(e => e.Location, "idx_tables_location");

            entity.HasIndex(e => e.Name, "idx_tables_name");

            entity.HasIndex(e => e.Name, "tables_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Capacity)
                .HasDefaultValue(2)
                .HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasDefaultValueSql("'main'::character varying")
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("toppings_pkey");

            entity.ToTable("toppings", tb => tb.HasComment("Add-on toppings with individual pricing"));

            entity.HasIndex(e => e.IsAvailable, "idx_toppings_is_available");

            entity.HasIndex(e => e.Name, "idx_toppings_name");

            entity.HasIndex(e => e.Price, "idx_toppings_price");

            entity.HasIndex(e => e.Name, "toppings_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Employee accounts with role-based access"));

            entity.HasIndex(e => e.CreatedAt, "idx_users_created_at");

            entity.HasIndex(e => e.IsActive, "idx_users_is_active");

            entity.HasIndex(e => e.RoleId, "idx_users_role_id");

            entity.HasIndex(e => e.Username, "idx_users_username");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vouchers_pkey");

            entity.ToTable("vouchers");

            entity.HasIndex(e => e.Code, "idx_vouchers_code");

            entity.HasIndex(e => e.ValidUntil, "idx_vouchers_valid_until");

            entity.HasIndex(e => e.Code, "vouchers_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DiscountValue)
                .HasPrecision(12, 2)
                .HasColumnName("discount_value");
            entity.Property(e => e.MaxDiscountAmount)
                .HasPrecision(12, 2)
                .HasColumnName("max_discount_amount");
            entity.Property(e => e.MinOrderAmount)
                .HasPrecision(12, 2)
                .HasDefaultValue(0m)
                .HasColumnName("min_order_amount");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsageCount)
                .HasDefaultValue(0)
                .HasColumnName("usage_count");
            entity.Property(e => e.UsageLimit).HasColumnName("usage_limit");
            entity.Property(e => e.ValidFrom)
                .HasDefaultValueSql("now()")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidUntil).HasColumnName("valid_until");
            
            // Map PostgreSQL ENUM types
            entity.Property(e => e.status)
                .HasColumnType("voucher_status")
                .HasColumnName("status")
                .HasConversion<string>();
            entity.Property(e => e.VoucherType)
                .HasColumnType("voucher_type")
                .HasColumnName("voucher_type")
                .HasConversion<string>();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vouchers_created_by_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

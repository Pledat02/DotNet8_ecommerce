using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Voucher_User> Voucher_Users { get; set; }
        public DbSet<ProductVoucher> ProductVouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.id_user);
                e.Property(u => u.username).IsRequired().HasMaxLength(100);
                e.Property(u => u.email).IsRequired().HasMaxLength(100);
                e.Property(u => u.password).IsRequired().HasMaxLength(100);
                e.Property(u => u.address).HasMaxLength(200);
                e.Property(u => u.phone).HasMaxLength(15);
            });

            modelBuilder.Entity<Comment>(e =>
            {
                e.ToTable("Comments");
                e.HasKey(c => c.id_comment);
                e.Property(c => c.text).IsRequired().HasMaxLength(500);
                e.Property(c => c.created_date).IsRequired();
                e.Property(c => c.rating).IsRequired();

                e.HasOne(c => c.User)
                 .WithMany(u => u.Comments)
                 .HasForeignKey(c => c.id_user)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(c => c.Product)
                 .WithMany(p => p.Comments)
                 .HasForeignKey(c => c.id_product)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(c => c.id_category);
                e.Property(c => c.name).IsRequired().HasMaxLength(100);
                e.Property(c => c.description).HasMaxLength(200);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(p => p.id_product);
                e.Property(p => p.name).IsRequired().HasMaxLength(100);
                e.Property(p => p.quantity_in_stock).IsRequired();
                e.Property(p => p.price).IsRequired().HasColumnType("decimal(18,2)");
                e.Property(p => p.description).HasMaxLength(500);
                e.Property(p => p.url_image).HasMaxLength(200);

                e.HasOne(p => p.Category)
                 .WithMany(c => c.Products)
                 .HasForeignKey(p => p.id_category)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Supplier)
                 .WithMany(s => s.Products)
                 .HasForeignKey(p => p.id_supplier)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(e =>
            {
                e.ToTable("Suppliers");
                e.HasKey(s => s.id_supplier);
                e.Property(s => s.name_company).IsRequired().HasMaxLength(100);
                e.Property(s => s.address).HasMaxLength(200);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(o => o.id_order);
                e.Property(o => o.name).IsRequired().HasMaxLength(100);
                e.Property(o => o.order_time).IsRequired();
                e.Property(o => o.description).HasMaxLength(500);
                e.Property(o => o.price).IsRequired().HasColumnType("decimal(18,2)");
                e.Property(o => o.status).IsRequired().HasMaxLength(50);

                e.HasOne(o => o.User)
                 .WithMany(u => u.Orders)
                 .HasForeignKey(o => o.id_user)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(o => o.Staff)
                 .WithMany(s => s.Orders)
                 .HasForeignKey(o => o.id_staff)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Bill>(e =>
            {
                e.ToTable("Bills");
                e.HasKey(b => b.id_order);
                e.Property(b => b.payment_method).IsRequired().HasMaxLength(50);
                e.Property(b => b.date).IsRequired();
                e.Property(b => b.amount).IsRequired().HasColumnType("decimal(18,2)");

                e.HasOne(b => b.Order)
                 .WithOne(o => o.Bill)
                 .HasForeignKey<Bill>(b => b.id_order)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetails");
                e.HasKey(od => new { od.id_order, od.id_product });
                e.Property(od => od.total_price).IsRequired().HasColumnType("decimal(18,2)");
                e.Property(od => od.quantity).IsRequired();
                e.Property(od => od.price).IsRequired().HasColumnType("decimal(18,2)");

                e.HasOne(od => od.Order)
                 .WithMany(o => o.OrderDetails)
                 .HasForeignKey(od => od.id_order)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(od => od.Product)
                 .WithMany(p => p.OrderDetails)
                 .HasForeignKey(od => od.id_product)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Voucher>(e =>
            {
                e.ToTable("Vouchers");
                e.HasKey(v => v.id_voucher);
                e.Property(v => v.percent_discount).IsRequired().HasColumnType("decimal(5,2)");
                e.Property(v => v.start_date).IsRequired();
                e.Property(v => v.finish_date).IsRequired();
            });

            modelBuilder.Entity<Voucher_User>(e =>
            {
                e.ToTable("Voucher_Users");
                e.HasKey(vu => new { vu.id_voucher, vu.id_user });

                e.HasOne(vu => vu.Voucher)
                 .WithMany(v => v.Voucher_Users)
                 .HasForeignKey(vu => vu.id_voucher)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(vu => vu.User)
                 .WithMany(u => u.Voucher_Users)
                 .HasForeignKey(vu => vu.id_user)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductVoucher>(e =>
            {
                e.ToTable("ProductVouchers");
                e.HasKey(pv => new { pv.id_product, pv.id_voucher });

                e.HasOne(pv => pv.Product)
                 .WithMany(p => p.ProductVouchers)
                 .HasForeignKey(pv => pv.id_product)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(pv => pv.Voucher)
                 .WithMany(v => v.ProductVouchers)
                 .HasForeignKey(pv => pv.id_voucher)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Staff>(e =>
            {
                e.ToTable("Staffs");
                e.HasKey(s => s.id_staff);
                e.Property(s => s.name_staff).IsRequired().HasMaxLength(100);
                e.Property(s => s.type_staff).IsRequired().HasMaxLength(50);
                e.Property(s => s.position).IsRequired().HasMaxLength(50);
                e.Property(s => s.sex).IsRequired().HasMaxLength(10);
                e.Property(s => s.email).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(r => r.id_role);
                e.Property(r => r.role_name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<StaffRole>(e =>
            {
                e.ToTable("StaffRoles");
                e.HasKey(sr => new { sr.id_staff, sr.id_role });

                e.HasOne(sr => sr.Staff)
                 .WithMany(s => s.StaffRoles)
                 .HasForeignKey(sr => sr.id_staff)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(sr => sr.Role)
                 .WithMany(r => r.StaffRoles)
                 .HasForeignKey(sr => sr.id_role)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

using ApiStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<DetalleCarrito> DetalleCarritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Producto
            modelBuilder.Entity<Productos>()
                .HasKey(p => p.IdProductos);

            modelBuilder.Entity<Productos>()
                .Property(p => p.Nombre)
                .HasMaxLength(300)
                .IsRequired(true);

            modelBuilder.Entity<Productos>()
                .Property(p => p.Descripcion)
                .HasMaxLength(300)
                .IsRequired(true);

            modelBuilder.Entity<Productos>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(true);

            modelBuilder.Entity<Productos>()
                .Property(p => p.Stock);

            modelBuilder.Entity<Productos>()
                .Property(p => p.Imagen)
                .HasMaxLength(400)
                .IsRequired(false);

            // Configuración de Usuario
            modelBuilder.Entity<Usuarios>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Nombre)
                .HasMaxLength(100)
                .IsRequired(true);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Email)
                .HasMaxLength(400)
                .IsRequired(true);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Imagen)
                .HasMaxLength(200)
                .IsRequired(true);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Contrasenia)
                .HasMaxLength(400);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.CategoriaPreferida)
                .HasMaxLength(200)
                .IsRequired(true);

            modelBuilder.Entity<Usuarios>()
                .Property(u => u.Activo)
                .IsRequired(true);

            // Configuración de Carrito
            modelBuilder.Entity<Carrito>()
                .HasKey(c => c.IdCarrito);

            modelBuilder.Entity<Carrito>()
                .Property(c => c.PrecioTotalCarrito)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(true);

            modelBuilder.Entity<Carrito>()
                .Property(c => c.FechaCreacion)
                .IsRequired(true);

            modelBuilder.Entity<Carrito>()
                .Property(c => c.Descripcion)
                .HasMaxLength(100)
                .IsRequired(true);

            modelBuilder.Entity<Carrito>()
                .HasOne<Usuarios>() // Relación con Usuarios
                .WithMany() // Relación inversa
                .HasForeignKey(c => c.IdUsuario) // Clave foránea en Carrito
                .IsRequired(true);

            // Configuración de DetalleCarrito
            modelBuilder.Entity<DetalleCarrito>()
                .HasKey(dc => dc.IdDetalleCarrito);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(dc => dc.PrecioTotalDetalleCarrito)
                .HasColumnType("decimal(18, 2)")
                .IsRequired(true);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(dc => dc.FechaFactura)
                .IsRequired(true);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(dc => dc.DetalleFactura)
                .HasMaxLength(800)
                .IsRequired(true);

            modelBuilder.Entity<DetalleCarrito>()
                .Property(dc => dc.FechaCreacionFactura)
                .IsRequired(true);

            modelBuilder.Entity<DetalleCarrito>()
                .HasOne<Carrito>() // Relación con Carrito
                .WithMany() // Relación inversa
                .HasForeignKey(dc => dc.IdCarrito) // Clave foránea en DetalleCarrito
                .IsRequired();

            // Forzar nombres de tablas en singular
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Productos>().ToTable("Productos");
            modelBuilder.Entity<Carrito>().ToTable("Carrito");
            modelBuilder.Entity<DetalleCarrito>().ToTable("DetalleCarrito");
        }
    }
}

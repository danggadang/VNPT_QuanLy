namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HeThong : DbContext
    {
        public HeThong()
            : base("name=HeThong")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTieu> ChiTieux { get; set; }
        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhanVien_KhachHang> NhanVien_KhachHang { get; set; }
        public virtual DbSet<Nhom> Nhoms { get; set; }
        public virtual DbSet<NhomDV> NhomDVs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<TrangThaiHopDong> TrangThaiHopDongs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTieu>()
                .Property(e => e.DoanhThu)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TongTienDV)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.GiamGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);
        }
    }
}

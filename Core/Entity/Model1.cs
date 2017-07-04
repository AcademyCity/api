namespace Core.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Vip> Vip { get; set; }
        public virtual DbSet<VipLoginLog> VipLoginLog { get; set; }
        public virtual DbSet<VipSource> VipSource { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vip>()
                .Property(e => e.VipId)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.VipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.VipPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.VipHeadImg)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.AddMan)
                .IsUnicode(false);

            modelBuilder.Entity<Vip>()
                .Property(e => e.ModifyMan)
                .IsUnicode(false);

            modelBuilder.Entity<VipLoginLog>()
                .Property(e => e.VipLoginLogId)
                .IsUnicode(false);

            modelBuilder.Entity<VipLoginLog>()
                .Property(e => e.VipId)
                .IsUnicode(false);

            modelBuilder.Entity<VipLoginLog>()
                .Property(e => e.LoginIP)
                .IsUnicode(false);

            modelBuilder.Entity<VipSource>()
                .Property(e => e.VipSourceId)
                .IsUnicode(false);

            modelBuilder.Entity<VipSource>()
                .Property(e => e.VipId)
                .IsUnicode(false);

            modelBuilder.Entity<VipSource>()
                .Property(e => e.SourceName)
                .IsUnicode(false);

            modelBuilder.Entity<VipSource>()
                .Property(e => e.SourceId)
                .IsUnicode(false);
        }
    }
}

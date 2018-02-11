namespace ContactDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContactContext : DbContext
    {
        public ContactContext()
            : base("name=ContactContext")
        {
        }

        public virtual DbSet<tbl_contact> tbl_contact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_contact>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength();
        }
    }
}

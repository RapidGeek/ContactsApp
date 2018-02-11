namespace ContactDAL
{
    using System.Data.Entity;

    public partial class ContactContext : DbContext
    {
        public ContactContext()
            : base("name=ContactContext")
        {
        }

        public virtual DbSet<tbl_contact> tbl_contact { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

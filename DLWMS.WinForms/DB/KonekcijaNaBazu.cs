
using DLWMS.WinForms.IB180085;
using System.Data.Entity;

namespace DLWMS.WinForms.DB
{

    //DLWMSContext
    public class KonekcijaNaBazu : DbContext
    {
        public KonekcijaNaBazu() : base("DLWMSPutanja")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("Studenti");                      
        }       
        public DbSet<Student> Studenti { get; set; }
        public DbSet<PredmetIB180085> Predmeti { get; set; }
        public DbSet<StudentPredmetIB180085> StudentiPredmeti { get; set; }
        public DbSet<StudentKonsultacijeIB180085> StudentKonsultacije { get; set; }
       
    }
}
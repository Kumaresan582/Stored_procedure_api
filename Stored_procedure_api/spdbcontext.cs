using Microsoft.EntityFrameworkCore;

namespace Stored_procedure_api
{
    public class spdbcontext:DbContext
    {
        public spdbcontext(DbContextOptions<spdbcontext> options): base(options)
        {

        }

        public DbSet<StudentModel> GetStudents { get; set; }
        public DbSet<StudentModel> GetStudentById { get; set; }
        public DbSet<UserIdGen> UserIdGenerate { get; set; }
    }
}

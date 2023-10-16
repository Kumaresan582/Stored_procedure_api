using System.ComponentModel.DataAnnotations;

namespace Stored_procedure_api
{
    public class StudentModel
    {
        [Key]
        public int? Student_Id { get; set; }
        public string? Student_Name { get; set; }
        public int? Students_Age { get; set; }
        public string? Students_Course { get; set; }
        public int? Students_Mark { get; set; }
    }

    public class UserIdGen
    {
        [Key]
        public int? Student_Id { get; set; }
        public string? UserId { get; set; }
        public string? Student_Name { get; set; }
        public int? Students_Age { get; set; }
        public string? Students_Course { get; set; }
        public int? Students_Mark { get; set; }
    }
}

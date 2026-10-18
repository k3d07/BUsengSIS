namespace BUsengSIS.Models
{
    public class Student
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int? StudentId { get; set; }
    }
}
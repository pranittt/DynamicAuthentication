namespace DynamicAuthentication.Models
{
    public class Student
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }


    
    
}

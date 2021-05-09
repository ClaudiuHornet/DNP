namespace Assignment_1.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int SecurityLevel { get; set; }
        
        public override string ToString()
                {
                    return UserName + ", " + Password;
                }
    }
}
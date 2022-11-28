namespace WebApp.Models
{
    public class UserAccess
    {
        public string token { get; set; } = null!;
        public int role { get; set; }
    }
}
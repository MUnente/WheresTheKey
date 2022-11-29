namespace WebApp.Models
{
    public class UserChangeStatus
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public int rolePersonId { get; set; }
        public int accountStatusId { get; set; }
    }
}
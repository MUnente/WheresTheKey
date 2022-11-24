namespace Server.Models
{
    public class FilterUserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? RolePersonId { get; set; }
        public int? AccountStatusId { get; set; }
    }
}
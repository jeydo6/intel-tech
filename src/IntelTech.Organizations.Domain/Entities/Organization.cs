using System.Collections.Generic;

namespace IntelTech.Organizations.Domain.Entities
{
    public sealed class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}

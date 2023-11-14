using System;
using IntelTech.Organizations.Application.Models;

namespace IntelTech.Organizations.Presentation.Contracts
{
    public sealed class GetOrganizationUsersResponse
    {
        public User[] Users {get; set;} = Array.Empty<User>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Responses
{
    public record UserResponse(
        Guid Id,
        string Name,
        string Surname,
        string Email,
        string Role);
}

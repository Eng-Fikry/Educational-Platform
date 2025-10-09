using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Presistance.Identity;

namespace Presistance.DataSeeding
{
    public class DataSeed(RoleManager<IdentityRole> role) : IDataSeed
    {
        public async Task IdentityDataSeed()
        {
            if (!role.Roles.Any()) 
            {
                await role.CreateAsync(new IdentityRole("Teacher"));
                await role.CreateAsync(new IdentityRole("Student"));
            }
        }
    }
}

using Domain.Contracts;
using Domain.Models;
using Domain.Models.identity;
using Domain.Models.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitializer(StoreDbContext _storedbcontext,
                                UserManager<ApplicationUser> _usermanager,
                                RoleManager<IdentityRole> _rolemanager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            //deployment

            //if ((await _storedbcontext.Database.GetPendingMigrationsAsync()).Any())
            //{
            //    await _storedbcontext.Database.MigrateAsync();
            //}

            try
            {


                if (!_storedbcontext.Set<DelivaryMethod>().Any())
                {
                    var text = await File.ReadAllTextAsync(@"..\Presistance\Data\seeding\deliveryMethods.json");
                    var data = JsonSerializer.Deserialize<List<DelivaryMethod>>(text);

                    if (data is not null && data.Any())
                    {
                        await _storedbcontext.Set<DelivaryMethod>().AddRangeAsync(data);
                        await _storedbcontext.SaveChangesAsync();
                    }
                }


                if (!_storedbcontext.Set<ProductBrand>().Any())
                {
                    var text = await File.ReadAllTextAsync(@"..\Presistance\Data\seeding\brands.json");
                    var data = JsonSerializer.Deserialize<List<ProductBrand>>(text);

                    if (data is not null && data.Any())
                    {
                        await _storedbcontext.Set<ProductBrand>().AddRangeAsync(data);
                        await _storedbcontext.SaveChangesAsync();
                    }
                }

                if (!_storedbcontext.ProductTypes.Any())
                {
                    var text = await File.ReadAllTextAsync(@"..\Presistance\Data\seeding\types.json");
                    var data = JsonSerializer.Deserialize<List<ProductType>>(text);
                    if (data is not null && data.Any())
                    {
                        await _storedbcontext.ProductTypes.AddRangeAsync(data);
                        await _storedbcontext.SaveChangesAsync();
                    }
                }


                if (!_storedbcontext.products.Any())
                {
                    var text = await File.ReadAllTextAsync(@"..\Presistance\Data\seeding\products.json");
                    var data = JsonSerializer.Deserialize<List<Product>>(text);

                    if (data is not null && data.Any())
                    {
                        await _storedbcontext.products.AddRangeAsync(data);
                        await _storedbcontext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task InitializeIdentity()
        {

            if (!_rolemanager.Roles.Any())
            {
                await _rolemanager.CreateAsync(new IdentityRole("Admin"));
                await _rolemanager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!_usermanager.Users.Any())
            {
                var SuperAdminUser = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    DisplayName = "Super Admin"
                };

                var AdminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    DisplayName = "Admin"
                };

                await _usermanager.CreateAsync(SuperAdminUser, "P@ssword");
                await _usermanager.CreateAsync(AdminUser, "Password");

                await _usermanager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _usermanager.AddToRoleAsync(AdminUser, "Admin");

            }
        }

        
    }
}

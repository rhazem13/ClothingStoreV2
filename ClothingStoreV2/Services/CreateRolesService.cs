using Microsoft.AspNetCore.Identity;
namespace ClothingStoreV2.Services
{
    public class CreateRolesService
    {
        private static RoleManager<IdentityRole> _roleManager;
        public CreateRolesService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //
            //
            //initializing custom roles 
            //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult; 
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}

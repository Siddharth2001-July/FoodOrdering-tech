using FoodOrdering.Data;
using FoodOrdering.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FoodOrdering.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FoodDBContext _foodDBContext;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, FoodDBContext foodDBContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _foodDBContext = foodDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OrderModel> Orders = await _foodDBContext.Orders.Include(d => d.Dishes).OrderByDescending(o => o.OrderTime).ToListAsync();
            return View(Orders);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var AllRoles = await _roleManager.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStaff(RegisterModel NewStaff)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    UserName = NewStaff.Email,
                    Email = NewStaff.Email
                };

                // Store user data in AspNetUsers database table
                var result = await _userManager.CreateAsync(user, NewStaff.Password);
                //Adding Role as Staff
                await _userManager.AddToRoleAsync(user, "Staff");
                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(NewStaff);
        }
    }
}
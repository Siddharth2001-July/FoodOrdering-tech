using FoodOrdering.Data;
using FoodOrdering.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FoodDBContext _foodDBContext;

        public StaffController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, FoodDBContext foodDBContext)
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
    }
}

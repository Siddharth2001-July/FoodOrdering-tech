using FoodOrdering.Data;
using FoodOrdering.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodOrdering.Controllers
{
    [Authorize(Roles ="User")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FoodDBContext _foodDBContext;

        public HomeController(FoodDBContext foodDBContext, UserManager<IdentityUser> userManager)
        {
            _foodDBContext = foodDBContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.User);
            List<OrderModel> Orders = await _foodDBContext.Orders.Include(d => d.Dishes).Where(o => o.CustomerEmail == user.Email).OrderByDescending(o => o.OrderTime).ToListAsync();
            return View(Orders);
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Dish newDishes)
        {
            if(newDishes != null && ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                var newOrder = new OrderModel();
                newOrder.CustomerEmail = user.Email;
                newOrder.Dishes.Add(newDishes);
                _foodDBContext.Orders.Add(newOrder);
                await _foodDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newDishes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
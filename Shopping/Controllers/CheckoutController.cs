
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Repository;
using System.Security.Claims;

namespace Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        public CheckoutController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail == null)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                var ordercode = Guid.NewGuid().ToString();
                var oderItem = new OderModel();
                oderItem.OrderCode = ordercode;
                oderItem.UserName = userEmail;
                oderItem.Status = 1;
                oderItem.CreateDate = DateTime.Now;
                _dataContext.Add(oderItem);
                _dataContext.SaveChanges();
                TempData["success"] = "Đơn hàng đã được tạo";
                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach (var cart in cartItems)
                {
                    var orderDetail = new OderDetail();
                    orderDetail .UserName = userEmail;
                    orderDetail .OrderCode = ordercode;
                    orderDetail.ProductId = cart.ProductId;
                    orderDetail.Price = cart.Price;
                    orderDetail.Quantity = cart.Quantity;
                    _dataContext.Add(orderDetail);
                    _dataContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Checkout thành công , vui lòng chờ duyệt đơn hàng ";
                return RedirectToAction("Index", "Cart");


            }
           return View();
        }
    }
}

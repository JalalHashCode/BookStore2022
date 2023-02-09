using BookStore2022.DataAccess.Repository.IRepository;
using BookStore2022.Models;
using BookStore2022.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol;
using System.Security.Claims;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 
        public int OrderTotal { get; set; }
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity ;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product")
            };

            foreach( var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuanitiy(cart.Count, cart.Product.Price, cart.Product.Price50,
                    cart.Product.Price100);
                shoppingCartVM.CartTotal += (cart.Price * cart.Count); 
            }
            return View(shoppingCartVM);
        }

        public IActionResult Plus(int cardId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cardId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cardId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cardId);
            _unitOfWork.ShoppingCart.DeccrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cardId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cardId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        private double GetPriceBasedOnQuanitiy(double quanitiy, double price , double price50, double price100)
        {
            if ( quanitiy <= 50)
            {
                return price;
            }
            else
            {
                if( quanitiy <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}

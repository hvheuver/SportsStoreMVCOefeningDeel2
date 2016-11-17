using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SportsStore.Models.Domain;

namespace SportsStore.Filters
{
        public class CartSessionFilter : ActionFilterAttribute
        {
            private readonly IProductRepository _productRepository;
            private Cart _cart;

            public CartSessionFilter(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                _cart = ReadCartFromSession(context.HttpContext);
                context.ActionArguments["cart"] = _cart;
                base.OnActionExecuting(context);
            }

            public override void OnActionExecuted(ActionExecutedContext context)
            {
                WriteCartToSession(_cart, context.HttpContext);
                base.OnActionExecuted(context);
            }

            private Cart ReadCartFromSession(HttpContext context)
            {
                Cart cart = context.Session.GetString("cart") == null ? 
                    new Cart() : JsonConvert.DeserializeObject<Cart>(context.Session.GetString("cart"));
                foreach (var l in cart.CartLines)
                    l.Product = _productRepository.GetById(l.Product.ProductId);
                return cart;
            }

            private void WriteCartToSession(Cart cart, HttpContext context)
            {
                context.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            }
        }
    }


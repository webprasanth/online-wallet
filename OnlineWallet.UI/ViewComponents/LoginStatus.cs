using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.UI.ViewComponents
{
    public class LoginStatus : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmailClaim = UserClaimsPrincipal.FindFirst(ClaimTypes.Email);
                string userEmail = userEmailClaim.Value;

                return View("LoggedIn", userEmail);
            }

            return View();
        }
    }
}

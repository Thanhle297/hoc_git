using System.Diagnostics;
using System.Security.Claims;
using DATN.Data;
using DATN.Data.Entities;
using DATN.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext dbContext,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _logger = logger;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var normalizedEmail = NormalizeEmail(model.Email);
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.NormalizedEmail == normalizedEmail);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View(model);
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View(model);
            }

            if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            await SignInUserAsync(user, model.RememberMe);

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return LocalRedirect(model.ReturnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var normalizedEmail = NormalizeEmail(model.Email);
            var emailExists = await _dbContext.Users.AnyAsync(user => user.NormalizedEmail == normalizedEmail);
            if (emailExists)
            {
                ModelState.AddModelError(nameof(model.Email), "Email này đã được đăng ký.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Email = model.Email.Trim(),
                NormalizedEmail = normalizedEmail,
                PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim(),
                Role = ApplicationUser.UserRole,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            await SignInUserAsync(user, isPersistent: false);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task SignInUserAsync(ApplicationUser user, bool isPersistent)
        {
            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, fullName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddDays(14) : null
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToUpperInvariant();
        }
    }
}

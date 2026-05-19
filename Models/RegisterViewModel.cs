using System.ComponentModel.DataAnnotations;

namespace DATN.Models;

public class RegisterViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Vui lòng nhập họ.")]
    [StringLength(100, ErrorMessage = "Họ không được vượt quá 100 ký tự.")]
    [Display(Name = "Họ")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên.")]
    [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
    [Display(Name = "Tên")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(256, ErrorMessage = "Email không được vượt quá 256 ký tự.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
    [Display(Name = "Số điện thoại")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Điều khoản sử dụng")]
    public bool AgreeToTerms { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!AgreeToTerms)
        {
            yield return new ValidationResult(
                "Bạn cần đồng ý với điều khoản sử dụng.",
                new[] { nameof(AgreeToTerms) });
        }
    }
}

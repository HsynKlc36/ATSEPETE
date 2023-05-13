namespace AtSepete.UI.Models
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

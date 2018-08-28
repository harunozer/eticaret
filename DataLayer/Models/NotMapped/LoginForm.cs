using DataLayer.ValidationAttributes;

namespace DataLayer.Models.NotMapped
{
    public class LoginForm
    {
        [ValidationRequired]
        public string EMail { get; set; }

        [ValidationRequired]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

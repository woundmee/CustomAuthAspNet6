using System;
namespace CustomAuthAspNet6.Models
{

    public class VMLogin    // [!] Создал -> View Model Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isKeepLoggedIn { get; set; }
    }

}


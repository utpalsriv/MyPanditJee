using MyPanditJee.Models;

namespace MyPanditJee.Service.Interface
{
    public interface ILoginService
    {
        public LoginModel validateCredential(LoginModel loginModel);
    }
}

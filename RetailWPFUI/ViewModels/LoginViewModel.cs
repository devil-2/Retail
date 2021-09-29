using Caliburn.Micro;
using RetailWPFUI.Library.Api;
using System;
using System.Threading.Tasks;

namespace RetailWPFUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            { 
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public bool IsErrorVisible
        {
            get { return ErrorMessage?.Length>0; }
        }

        private readonly IApiHelper _apiHelper;

        public string UserName
        {
            get { return _userName; }
            set
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn 
        {
            get { return UserName?.Length > 0 && Password?.Length > 0; }
        }

        public LoginViewModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task LogIn()
        {
            ErrorMessage = string.Empty;
            try
            {
                await _apiHelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

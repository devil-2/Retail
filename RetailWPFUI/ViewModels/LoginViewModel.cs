using Caliburn.Micro;
using RetailWPFUI.EventModels;
using RetailWPFUI.Library.Api;
using System;
using System.Threading.Tasks;

namespace RetailWPFUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName="vm2508.cs@gmail.com";
        private string _password="Vikas@123";
        private string _errorMessage;
        private readonly IAuthApi _apiHelper;
        private readonly IEventAggregator _events;

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

        public LoginViewModel(IAuthApi apiHelper,IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        public async Task LogIn()
        {
            ErrorMessage = string.Empty;
            try
            {
                await _apiHelper.Authenticate(UserName, Password);
                await _events.PublishOnUIThreadAsync(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

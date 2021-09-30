using AutoMapper;
using Caliburn.Micro;
using RetailWPFUI.Helpers;
using RetailWPFUI.Library.Api;
using RetailWPFUI.Library.Helpers;
using RetailWPFUI.Library.Models;
using RetailWPFUI.Models;
using RetailWPFUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RetailWPFUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        protected override void Configure()
        {
            ConfigureAutoMapper();
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IConfigHelper, ConfigHelper>()
                .Singleton<ApiHelper>();

            _container.PerRequest<IAuthApi, AuthApi>()
                .PerRequest<IProductApi, ProductApi>()
                .PerRequest<ISaleApi, SaleApi>();


            RegisterCommonTypes("ViewModel");
        }

        private  void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });
            var mapper = config.CreateMapper();
            _container.Instance(mapper);
        }

        private void RegisterCommonTypes(string type)
        {
            GetType().Assembly.GetTypes()
                .Where(t => t.IsClass)
                .Where(t => t.Name.EndsWith(type))
                .ToList()
                .ForEach(view => _container.RegisterPerRequest(view, view.ToString(), view));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }

}

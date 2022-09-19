using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using RideSharingApp.BLL.Services;
using RideSharingApp.BLL.Interfaces;
using RideSharingAPP.BLL.Interfaces;
using RideSharingAPP.BLL.Services;
using RideSharingAPP.BLL.Interfaces.IValidations;
using RideSharingAPP.BLL.Services.Validations;
using AutoMapper;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using RideSharingAPP.WEB.Models.Authorization;

namespace RideSharingAPP.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            kernel.Unbind<ModelValidatorProvider>();
            AddBindings();
        }

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            kernel.Unbind<ModelValidatorProvider>();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAccountInformationService>().To<AccountInformationService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            kernel.Bind<IClientService>().To<ClientService>();
            kernel.Bind<ICarService>().To<CarService>();
            kernel.Bind<IDriverLicensesService>().To<DriverLicensesService>();
            kernel.Bind<ITripService>().To<TripService>();
            kernel.Bind<IPassingPointService>().To<PassingPointService>();
            kernel.Bind<ICompanionService>().To<CompanionService>();

            kernel.Bind<ICompanionValidation>().To<CompanionValidation>();
            kernel.Bind<IPassingPointValidation>().To<PassingPointValidation>();
            kernel.Bind<IAccountInformationValidation>().To<AccountInformationValidation>();
            kernel.Bind<IDriverValidation>().To<DriverValidation>();
            kernel.Bind<IClientValidation>().To<ClientValidation>();
            kernel.Bind<ICarValidation>().To<CarValidation>();
            kernel.Bind<IDriverLicensesValidation>().To<DriverLicensesValidation>();
            kernel.Bind<ITripValidation>().To<TripValidation>();
        }
    }
}
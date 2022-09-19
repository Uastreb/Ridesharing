using RideSharinAPP.COMMON.Enums;
using RideSharingApp.BLL.Interfaces;
using RideSharingApp.BLL.Services;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using RideSharingAPP.BLL.DTO.CarDTO;
using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.DTO.DriverDTO;
using RideSharingAPP.BLL.DTO.DriverLicensesDTO;
using RideSharingAPP.BLL.DTO.PassingPointDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces;
using RideSharingAPP.BLL.Interfaces.IValidations;
using RideSharingAPP.WEB.Attributes;
using RideSharingAPP.WEB.Models.Authorization;
using RideSharingAPP.WEB.Models.Car;
using RideSharingAPP.WEB.Models.Client;
using RideSharingAPP.WEB.Models.Driver;
using RideSharingAPP.WEB.Models.DriverLicenses;
using RideSharingAPP.WEB.Models.Route;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Controllers
{
    /// <summary>The controller in which all actions will be carried out by the passenger.</summary>
    public class DriverController : Controller
    {
        IAccountInformationValidation accountInformationValidation;
        ITripValidation tripValidation;
        IPassingPointValidation passingPointValidation;
        IDriverLicensesValidation driverLicensesValidation;
        IDriverValidation driverValidation;
        ICarValidation carValidation;
        ICompanionValidation companionValidation;

        IAccountInformationService accountInformationService;
        ITripService tripService;
        IPassingPointService passingPointService;
        IDriverLicensesService driveLicensesService;
        IDriverService driverService;
        ICarService carService;
        ICompanionService companionService;

        /// <summary>Controller constructor.</summary>
        /// <value>Accepts the interface of the validation service as well as the service tables of: drivers, drivers licenses, cars.</value>
        /// <remarks>Constructor - a function that fires before other constructor methods.</remarks>
        public DriverController(IDriverService driverserv, ICarService carserv, IDriverLicensesService driverLicensesserv, ITripService tripserv, IPassingPointService passPointserv,
            ICarValidation carVal, IDriverValidation driverVal, IDriverLicensesValidation driverLicensesVal, ITripValidation tripval, IPassingPointValidation passPointval,
            IAccountInformationValidation accInformationVal, IAccountInformationService accInformationServ, ICompanionService companionServ, ICompanionValidation companionVal)
        {

            driverLicensesValidation = driverLicensesVal;
            driverValidation = driverVal;
            carValidation = carVal;
            tripValidation = tripval;
            passingPointValidation = passPointval;
            accountInformationValidation = accInformationVal;
            companionValidation = companionVal;

            companionService = companionServ;
            accountInformationService = accInformationServ;
            passingPointService = passPointserv;
            tripService = tripserv;
            driveLicensesService = driverLicensesserv;
            driverService = driverserv;
            carService = carserv;
        }

        /// <summary>Checks the availability of data about the driver, his driver’s license and the car, if no data are redirected to the forms to fill.</summary>
        /// <returns>View() or Redirect()</returns>
        [Authorize(Roles = "Driver"), HttpGet]
        public async Task<ActionResult> Index()
        {
            var driver = await driverService.FindAccount(Convert.ToInt32(HttpContext.Request.Cookies["AccountInformationId"].Value));
            var mappingDriver = new AutoMap<DriverViewModel, DriverDTOCreate>().Initialize(driver);
            ViewBag.PageTitle = "Данные о пользователе";
            return View(mappingDriver);
        }

        /// <summary>Returns the form of creating a new car.</summary>
        /// <remarks>Displays a form for filling in data about a new car.</remarks>
        /// <returns>View()</returns>
        [Authorize(Roles = "Driver"), HttpGet]
        public ActionResult CreateCar()
        {
            ViewBag.PageTitle = "Добавление автомобиля";
            return View();
        }

        /// <summary>Returns the form of creating out driver license information.</summary>
        /// <remarks>Displays a form for filling out driver license information.</remarks>
        /// <returns>View()</returns>
        [Authorize(Roles = "Driver"), HttpGet]
        public ActionResult CreateDriverLicenses()
        {
            var driverLicenses = new DriverLicensesViewModel();
            ViewBag.PageTitle = "Водительское удостоверение";

            
            var categoriesEnum = new Categories().GetList();
            var categoriesList = categoriesEnum.Select(arg => new { idCategory = arg.idCategory, Categories = arg.Categories }).ToList();
            var categoriesSelectedList = new SelectList(categoriesList, "idCategory", "Categories");
            driverLicenses.ListCategories = categoriesSelectedList;
            return View(driverLicenses);
        }

        /// <summary>Sends data about a new driver license information to the server.</summary>
        /// <param name="driverLicense"> model containing data about a new driver licenses information.</param>
        [Authorize(Roles = "Driver"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDriverLicenses(DriverLicensesViewModel driverLicense)
        {
            var validDriverLicenses = new AutoMap<DriverLicensesDTOCreate, DriverLicensesViewModel>().Initialize(driverLicense);
            var error = driverLicensesValidation.IsValid(validDriverLicenses);
            if (error == null)
            {
                var mappingDriversLicense = new AutoMap<DriverLicensesDTOCreate, DriverLicensesViewModel>().Initialize(driverLicense);
                mappingDriversLicense.DriverID = Convert.ToInt32(HttpContext.Request.Cookies["DriverId"].Value);
                var newDriversLicensesId = await driveLicensesService.Create(mappingDriversLicense);
                return RedirectToAction("CreateRoute");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }

            var categoriesEnum = new Categories().GetList();
            var categoriesList = categoriesEnum.Select(arg => new { idCategory = arg.idCategory, Categories = arg.Categories }).ToList();
            var categoriesSelectedList = new SelectList(categoriesList, "idCategory", "Categories");
            driverLicense.ListCategories = categoriesSelectedList;
            return View(driverLicense);
        }

        [Authorize(Roles = "Driver"), HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult>ChangePassword(ChangePasswordViewModel account)
        {
            var mappedAccount = new AutoMap<AccountInformationDTOChangePassword, ChangePasswordViewModel>().Initialize(account);
            var error = accountInformationValidation.IsValidChangePassword(mappedAccount);
            if (error == null)
            {
                mappedAccount.AccountInformationId = Convert.ToInt32(HttpContext.Request.Cookies["AccountInformationId"].Value);
                var editAccountId = await accountInformationService.ChangePassword(mappedAccount);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View(account);

        }

        [Authorize(Roles = "Driver"), HttpPost ,ValidateAntiForgeryToken, SetTempDataModelState]
        public async Task<ActionResult> DeleteCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var error = await carValidation.DeleteCarIsValid(Convert.ToInt32(id));
            if (error == null)
            {
                var checkResult = await carService.DeleteCar(Convert.ToInt32(id));
                return RedirectToAction("CarList");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
                return RedirectToAction("CarList");
            }
        }

        [Authorize(Roles = "Driver"), HttpGet]
        public async Task<ActionResult> EditDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.PageTitle = "Изменение личных данных";

            string accountInformationId = HttpContext.Request.Cookies["AccountInformationId"].Value;
            var driver = await driverService.Get(Convert.ToInt32(id));
            var mappedDriver = new AutoMap<DriverCreateViewModel, DriverDTOCreate>().Initialize(driver);
            return View(mappedDriver);
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> EditDriver(DriverCreateViewModel driver)
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }
            var mappedDriver = new AutoMap<DriverDTOCreate, DriverCreateViewModel>().Initialize(driver);
            mappedDriver.AccountInformationID = Convert.ToInt32(HttpContext.Request.Cookies["AccountInformationId"].Value);
            var error = driverValidation.IsValid(mappedDriver);
            if (error == null)
            {
                var editDriverId = await driverService.Update(mappedDriver);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View(driverId);
        }

        [Authorize(Roles = "Driver"), HttpGet]
        public async Task<ActionResult> EditCar(int? id)
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var car = await carService.Get(Convert.ToInt32(id));
            var mappedCar = new AutoMap<EditCarComments, CarDTOCreate>().Initialize(car);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(mappedCar);
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> EditCar(EditCarComments car)
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }

            var mappedCar = new AutoMap<CarDTOCreate, EditCarComments>().Initialize(car);
            var error = carValidation.EditCarModelIsValid(mappedCar);
            if (error == null)
            {
                mappedCar.DriverId = Convert.ToInt32(driverId);
                var editCarId = await carService.EditCar(mappedCar);
                return RedirectToAction("CarList");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View(car.id);
        }

        /// <summary>Sends data about a new car to the server.</summary>
        /// <param name="car"> model containing data about a new car.</param>
        [Authorize(Roles = "Driver"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCar(CarViewModel car)
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }

            var mappedCar = new AutoMap<CarDTOCreate, CarViewModel>().Initialize(car);
            mappedCar.DriverId = Convert.ToInt32(HttpContext.Request.Cookies["DriverId"].Value);
            var error = carValidation.IsValid(mappedCar);
            if (error == null)
            {
                var newCarsId = await carService.Create(mappedCar);
                return RedirectToAction("CarList");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            ViewBag.PageTitle = "Добавление автомобиля";
            return View(car);
        }

        [Authorize(Roles = "Driver"), HttpGet, RestoreModelStateFromTempData]
        public async Task<ActionResult> CarList()
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }
            var A = ModelState;
            ViewBag.PageTitle = "Список автомобилей";
            
            var cars = await carService.FindCars(Convert.ToInt32(driverId));
            var mappListCars = cars.Select(p => new AutoMap<CarViewModel, CarDTOCreate>().Initialize(p)).ToList();
            return View(mappListCars);
        }

        /// <summary>Returns the form of creating a new driver.</summary>
        /// <remarks>Displays a form for filling out driver license information.</remarks>
        /// <returns>View()</returns>
        [Authorize(Roles = "Driver"), HttpGet]
        public ActionResult Create()
        {
            ViewBag.PageTitle = "Добавление данных о водителе";
            return View();
        }

        /// <summary>Sends data about a new driver to the server.</summary>
        /// <param name="driver"> model containing data about a new driver.</param>
        [Authorize(Roles = "Driver"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DriverCreateViewModel driver)
        {
            var validDriver = new AutoMap<DriverDTOCreate, DriverCreateViewModel>().Initialize(driver);
            var error = driverValidation.IsValid(validDriver);
            if (error == null)
            {
                string AccountInformationId = HttpContext.Request.Cookies["AccountInformationId"].Value;
                var mappingDriver = new AutoMap<DriverDTOCreate, DriverCreateViewModel>().Initialize(driver);
                mappingDriver.AccountInformationID = Convert.ToInt32(AccountInformationId);
                var newDriversId = await driverService.Create(mappingDriver);
                return RedirectToAction("CreateRoute");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View();
        }

        [Authorize(Roles = "Driver"), HttpGet]
        public async Task<ActionResult> CreateRoute()
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }

            var carsId = await IsExistDriversCarData(Convert.ToInt32(driverId));
            if (carsId == null)
            {
                return RedirectToAction("CreateCar");
            }

            ViewBag.PageTitle = "Добавление маршрута";
            var carEnum = await carService.GetAllForDropDown(Convert.ToInt32(driverId));
            var carList = carEnum.Select(arg => new { CarId = arg.id, registrationNumber = arg.RegistrationNumber }).ToList();
            var carSelectedList = new SelectList(carList, "CarId", "registrationNumber");
            TripViewModel trip = new TripViewModel();
            trip.ListCars = carSelectedList;
            return View(trip);
        }

        [Authorize(Roles = "Driver"), HttpGet, RestoreModelStateFromTempData]
        public async Task<ActionResult> RouteList()
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }

            var carsId = await IsExistDriversCarData(Convert.ToInt32(driverId));
            if (carsId == null)
            {
                return RedirectToAction("CreateCar");
            }

            var trips = await tripService.GetDriversTrips(Convert.ToInt32(driverId));
            var mappedTrips = trips.Select(p => new AutoMap<TripsListViewModel, TripDTOList>().Initialize(p)).ToList();

            foreach (var x in mappedTrips)
            {
                var points = await passingPointService.GetRoutesPassingPoints(x.id);
                var pointsSelectedTrip = points.ToList();
                if (pointsSelectedTrip != null)
                {
                    x.Points = pointsSelectedTrip.Select(p => new AutoMap<PassingPointViewModel, PassingPointDTOCreate>().Initialize(p)).ToList();
                }
                var car = await carService.Get(x.CarId);
                var mappedCar = new AutoMap<CarViewModel, CarDTOCreate>().Initialize(car);
                x.Car = mappedCar;
                var clients = await companionService.GetClients(x.id);
                var mappedClients = new AutoMap<ClientWithCompanionDateViewModel, ClientDTOwithCompanionDate>().GetAll(clients);
                x.Clients = mappedClients;
            }

            ViewBag.PageTitle = "Активные маршруты";
            return View(mappedTrips);
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost] 
        public async Task<ActionResult> CreateRoute(TripViewModel trip)
        {
            var driverId = await IsExistDriverData();
            if (driverId == null)
            {
                return RedirectToAction("Create");
            }

            var driverLicensesId = await IsExistDriverLicensesData(Convert.ToInt32(driverId));
            if (driverLicensesId == null)
            {
                return RedirectToAction("CreateDriverLicenses");
            }

            var carsId = await IsExistDriversCarData(Convert.ToInt32(driverId));
            if (carsId == null)
            {
                return RedirectToAction("CreateCar");
            }

            ViewBag.PageTitle = "Добавление маршрута";

            var validRoute = new AutoMap<TripDTOCreate, TripViewModel>().Initialize(trip);
            var errorRoute = tripValidation.IsValid(validRoute);

            int? newTripsId = null;
            if (errorRoute == null)
            {
                trip.DriverId = Convert.ToInt32(driverId);
                var mappedTrip = new AutoMap<TripDTOCreate, TripViewModel>().Initialize(trip);
                newTripsId = await tripService.Create(mappedTrip);
               
                var mappedPassingPointList = trip.Points.Select(p => new AutoMap<PassingPointDTOCreate, PassingPointViewModel>().Initialize(p)).ToList();
                var errorPassingPoint = passingPointValidation.IsValid(mappedPassingPointList);
                if (errorPassingPoint == null && newTripsId != null)
                {
                    int tripId = Convert.ToInt32(newTripsId);
                    foreach(var a in mappedPassingPointList)
                    {
                        a.TripID = tripId;
                        
                    }
                    var newPassingPointsId = await passingPointService.Create(mappedPassingPointList, newTripsId);
                    return RedirectToAction("RouteList");
                }
                else
                {
                    ModelState.AddModelError("", errorPassingPoint.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", errorRoute.Message);
            }

            var carEnum = await carService.GetAllForDropDown(Convert.ToInt32(driverId));
            var carList = carEnum.Select(arg => new { CarId = arg.id, registrationNumber = arg.RegistrationNumber }).ToList();
            var carSelectedList = new SelectList(carList, "CarId", "registrationNumber");
            trip.ListCars = carSelectedList;
            
            return View(trip);
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost, SetTempDataModelState]
        public async Task<ActionResult> DeleteTrip(int? tripId)
        {
            var errorRoute = await tripValidation.DeleteTripIsValid(tripId);
            if (errorRoute == null)
            {
                await tripService.SetStatusDeleted(Convert.ToInt32(tripId));
            }
            else
            {
                ModelState.AddModelError("", errorRoute.Message);
            }
            return RedirectToAction("RouteList");
        }

        [Authorize(Roles = "Driver"), ValidateAntiForgeryToken, HttpPost, SetTempDataModelState]
        public async Task<ActionResult> CompleteTrip(int? tripId)
        {
            var errorRoute = await tripValidation.CompleteTripIsValid(tripId);
            if (errorRoute == null)
            {
                await tripService.SetStatusCompleted(Convert.ToInt32(tripId));
            }
            else
            {
                ModelState.AddModelError("", errorRoute.Message);
            }
            return RedirectToAction("RouteList");
        }

        private async Task<int?> IsExistDriverData()
        {
            string accountInformationId = HttpContext.Request.Cookies["AccountInformationId"].Value;
            var userId = int.Parse(accountInformationId);
            var driver = await driverService.FindAccount(userId);
            if (driver == null)
            {
                return null;
            }
            HttpContext.Response.Cookies["DriverId"].Value = driver.id.ToString();
            return driver.id;
        }

        private async Task<int?> IsExistDriverLicensesData(int driverId)
        {
            var driverLicenses = await driveLicensesService.FindDriverLicenses(driverId);
            if (driverLicenses == null)
            {
                return null;
            }
            return driverLicenses.id;
        }

        private async Task<IEnumerable<int>> IsExistDriversCarData(int driverId)
        {
            var cars = await carService.FindCars(driverId);
            if (Enumerable.Count(cars) == 0)
            {
                return null;
            }
            return cars.Select(p => p.id).AsEnumerable();
        }

        /// <summary>Invokes a method to free database connection memory.</summary>
        protected override void Dispose(bool disposing)
        {
            driveLicensesService.Dispose();
            passingPointService.Dispose();
            accountInformationService.Dispose();
            companionService.Dispose();
            driverService.Dispose();
            carService.Dispose();
            tripService.Dispose();
            base.Dispose(disposing);
        }

    }


}

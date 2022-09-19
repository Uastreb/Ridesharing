using RideSharingApp.BLL.Interfaces;
using RideSharingApp.BLL.Services;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using RideSharingAPP.BLL.DTO.CarDTO;
using RideSharingAPP.BLL.DTO.ClientDTO;
using RideSharingAPP.BLL.DTO.CompanionDTO;
using RideSharingAPP.BLL.DTO.DriverDTO;
using RideSharingAPP.BLL.DTO.TripDTO;
using RideSharingAPP.BLL.Interfaces;
using RideSharingAPP.BLL.Interfaces.IValidations;
using RideSharingAPP.WEB.Attributes;
using RideSharingAPP.WEB.Models.Authorization;
using RideSharingAPP.WEB.Models.Car;
using RideSharingAPP.WEB.Models.Client;
using RideSharingAPP.WEB.Models.Companion;
using RideSharingAPP.WEB.Models.Driver;
using RideSharingAPP.WEB.Models.PassingPoint;
using RideSharingAPP.WEB.Models.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RideSharingAPP.WEB.Controllers
{
    /// <summary>The controller in which all actions will be carried out by the passenger.</summary>
    public class ClientController : Controller
    {
        IAccountInformationValidation accountInformationValidation;
        IClientValidation clientValidation;
        ITripValidation tripValidation;
        ICarValidation carValidation;
        IDriverValidation driverValidation;
        ICompanionValidation companionValidation;

        IAccountInformationService accountInformationService;
        ICompanionService companionService;
        IDriverService driverService;
        ICarService carService;
        ITripService tripService;
        IClientService clientService;

        /// <summary>Controller constructor.</summary>
        /// <value>Accepts the interface of the validation service as well as the service table of clients.</value>
        /// <remarks>Constructor - a function that fires before other constructor methods.</remarks>
        public ClientController(IClientService clientServ, IClientValidation clientValid, ITripValidation tripValid, ITripService tripServ,
            IDriverService driverServ, IDriverValidation driverValid, ICarService carServ, ICarValidation carValid, ICompanionService companionServ,
            ICompanionValidation companionValid, IAccountInformationService accountServ, IAccountInformationValidation accountVal)
        {
            carValidation = carValid;
            driverValidation = driverValid;
            clientValidation = clientValid;
            tripValidation = tripValid;
            companionValidation = companionValid;
            accountInformationValidation = accountVal;

            accountInformationService = accountServ;
            companionService = companionServ;
            driverService = driverServ;
            carService = carServ;
            tripService = tripServ;
            clientService = clientServ;
        }

        /// <summary>Sends data about a new user to the server</summary>
        /// <param name="client"> model containing data about a new user.</param>
        [Authorize(Roles = "Passenger"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Create(ClientViewModel client)
        {
            var mappedClient = new AutoMap<ClientDTOCreate, ClientViewModel>().Initialize(client);
            var error = clientValidation.IsValid(mappedClient);
            if (error == null)
            {
                mappedClient.AccountInformationID = int.Parse(HttpContext.Request.Cookies["AccountInformationId"].Value);
                var newClientsId = await clientService.Create(mappedClient);
                return RedirectToAction("SearchRoute");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View();
        }

        [Authorize(Roles = "Passenger"), HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

       
        [Authorize(Roles = "Passenger"), HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel account)
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

        [Authorize(Roles = "Passenger"), HttpGet]
        public async Task<ActionResult> Index()
        {
            var client = await clientService.FindAccount(Convert.ToInt32(HttpContext.Request.Cookies["AccountInformationId"].Value));
            var mappingClient = new AutoMap<ClientViewModel, ClientDTOCreate>().Initialize(client);
            ViewBag.PageTitle = "Данные о пользователе";
            return View(mappingClient);
        }

        /// <summary>Returns the form of creating a new client.</summary>
        /// <remarks>Displays a form for filling in data about a new user.</remarks>
        /// <returns>View()</returns>
        [Authorize(Roles = "Passenger"), HttpGet]
        public ActionResult Create()
        {
            ViewBag.PageTitle = "Личные данные";

            return View();
        }

        /// <summary>Checks for customer data in the database, if there is no data, sends it to the data filling form.</summary>
        /// <returns>View() or Redirect()</returns>
        [Authorize(Roles = "Passenger")]
        private async Task<int?> IsExistClientData()
        {
            string AccountInformationId = HttpContext.Request.Cookies["AccountInformationId"].Value;
            var userId = int.Parse(AccountInformationId);
            var passenger = await clientService.FindPassenger(userId);
            if (passenger == null)
            {
                return null;
            }
            return passenger.id;
        }

        [Authorize(Roles = "Passenger"), HttpGet]
        public async Task<ActionResult> SearchRoute()
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }

            ViewBag.PageTitle = "Поиск маршрута";
            return View();
        }

        [Authorize(Roles = "Passenger"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> SearchRoute(PointsViewModel route)
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }

            var mappingRoute = new AutoMap<TripDTOPoints, PointsViewModel>().Initialize(route);
            var error = tripValidation.PointsIsValid(mappingRoute);
            if (error == null)
            {
                return RedirectToAction("RoutesList", route);
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            ViewBag.PageTitle = "Поиск маршрута";
            return View();
        }

        [Authorize(Roles = "Passenger"), ValidateAntiForgeryToken, HttpPost, SetTempDataModelState]
        public async Task<ActionResult> RegisterForRoute(TripSearchedListViewModel tripInfo)
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }
            var error = await companionValidation.IsValid(Convert.ToInt32(clientId), tripInfo.id, int.Parse(HttpContext.Request.Cookies["AccountInformationId"].Value));
            if (error == null)
            {
                var companion = new CompanionDTOCreate
                {
                    TripID = tripInfo.id,
                    ClientID = Convert.ToInt32(clientId),
                    DateAndTimeOfArrival = tripInfo.DateAndTimeOfArrival,
                    DateAndTimeOfDepartue = tripInfo.DateAndTimeOfDepartue,
                    OriginCoordinates = tripInfo.OriginCoordinates,
                    EndCoordinates = tripInfo.EndCoordinates,
                    TotalCost = tripInfo.TotalСost
                };
                var newCompanionId = await companionService.RegistrationForTheRoute(companion);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            ViewBag.PageTitle = "Поиск маршрута";
            return RedirectToAction("RegistrationRoutesList");
        }

        [Authorize(Roles = "Passenger"), HttpGet]
        public async Task<ActionResult> EditClient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.PageTitle = "Изменение личных данных";

            string accountInformationId = HttpContext.Request.Cookies["AccountInformationId"].Value;
            var client = await clientService.Get(Convert.ToInt32(id));
            var mappedClient = new AutoMap<ClientViewModel, ClientDTOCreate>().Initialize(client);
            return View(mappedClient);
        }

        [Authorize(Roles = "Passenger"), ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> EditClient(ClientViewModel client)
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }
            var mappedClient = new AutoMap<ClientDTOCreate, ClientViewModel>().Initialize(client);
            mappedClient.AccountInformationID = Convert.ToInt32(HttpContext.Request.Cookies["AccountInformationId"].Value);
            var error = clientValidation.IsValid(mappedClient);
            if (error == null)
            {
                var editClientId = await clientService.Update(mappedClient);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View(clientId);
        }

        [Authorize(Roles = "Passenger"), HttpGet, RestoreModelStateFromTempData]
        public async Task<ActionResult> RegistrationRoutesList()
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }
            var activeUserRoutes = await companionService.GetActiveUserRoutes(Convert.ToInt32(clientId));
            var mappedListActiveUserRoutes = new AutoMap<CompanionViewModel, CompanionDTOCreate>().GetAll(activeUserRoutes);
            foreach (var item in mappedListActiveUserRoutes)
            {
                item.Telephone = await companionService.GetDriverTelephone(item.TripID);
            }
            ViewBag.PageTitle = "Зарегестрированные маршруты";
            return View(mappedListActiveUserRoutes);
        }

        [Authorize(Roles = "Passenger"), HttpGet]
        public async Task<ActionResult> RoutesList(PointsViewModel route)
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }

            var mappedTrip = new AutoMap<TripDTOPoints, PointsViewModel>().Initialize(route);
            var relevanteTrips = await tripService.SearchTrips(mappedTrip);
            List<TripSearchedListViewModel> mappedList = new List<TripSearchedListViewModel>();
            foreach (var x in relevanteTrips)
            {
                var companions = await companionService.GetAll();
                var thatRouteCompanions = companions.Where(p => p.TripID == x.id);

                mappedList.Add(new TripSearchedListViewModel
                {
                    CarId = x.CarId,
                    DriverId = x.DriverId,
                    DateAndTimeOfArrival = x.DateAndTimeOfArrival,
                    DateAndTimeOfDepartue = x.DateAndTimeOfDepartue,
                    OriginCoordinates = x.OriginCoordinates,
                    EndCoordinates = x.EndCoordinates,
                    id = x.id,
                    NumberOfSeats = x.NumberOfSeats - thatRouteCompanions.Count(),
                    RegistrationEndDate = x.RegistrationEndDate,
                    Status = x.Status,
                    TotalСost = x.TotalСost,
                    PassingPoints = new AutoMap<PassingPointForSearhedListViewModel, TripDTOPoints>().GetAll(x.PassingPoints),
                    Driver = new AutoMap<DriverViewModel, DriverDTOCreate>().Initialize(await driverService.Get(x.DriverId)),
                    Car = new AutoMap<CarViewModel, CarDTOCreate>().Initialize(await carService.Get(x.CarId))
                });
            }
            ViewBag.PageTitle = "Список маршрутов";
            return View(mappedList);
        }

        [Authorize(Roles = "Passenger"), ValidateAntiForgeryToken, HttpPost, SetTempDataModelState]
        public async Task<ActionResult> Unregister(int? companionId)
        {
            var clientId = await IsExistClientData();
            if (clientId == null)
            {
                return RedirectToAction("Create");
            }

            var errorCompanion = await companionValidation.CheckId(companionId);
            if (errorCompanion == null)
            {
                await companionService.Unregister(Convert.ToInt32(companionId));
            }
            else
            {
                ModelState.AddModelError("", errorCompanion.Message);
            }
            return RedirectToAction("RegistrationRoutesList");
        }

        /// <summary>Invokes a method to free database connection memory.</summary>
        protected override void Dispose(bool disposing)
        {
            accountInformationService.Dispose();
            companionService.Dispose();
            driverService.Dispose();
            carService.Dispose();
            tripService.Dispose();
            clientService.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System;
using RideSharingApp.DAL.Entities;

namespace RideSharingApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IDriverRepository Drivers { get; }
        ICarRepository Cars { get; }
        ITripRepository Trips { get; }
        IPassingPointRepository PassingPoints { get; }
        ICompanionRepository Companions { get; }
        IAccountInformationRepository AccountsInformation { get; }
        IDriverLicensesRepository DriversLicenses { get; }

        void Commit();
    }
}

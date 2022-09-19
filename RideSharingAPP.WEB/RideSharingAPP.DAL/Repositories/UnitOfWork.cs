using RideSharingApp.DAL.EF;
using RideSharingApp.DAL.Entities;
using RideSharingApp.DAL.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RideSharingApp.DAL.Repositories
{
    /// <summary>Class providing database connection.</summary>
    public class UnitOfWork : IUnitOfWork
    {
        private SqlConnection connection;
        private SqlTransaction transaction;

        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private ClientRepository clientRepository;
        private AccountInformationRepository accountInformationRepository;
        private DriversLicenseRepository driverLicenseRepository;
        private TripRepository tripRepository;
        private CompanionRepository companionRepository;
        private PassingPointRepository passingPointRepository;


        /// <summary>Provides a connection to the database.</summary>
        public UnitOfWork(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        /// <summary>Work with Cars table in the database.</summary>
        public ICarRepository Cars
        {
            get
            {
                if (carRepository == null)
                {
                    carRepository = new CarRepository(transaction.Connection.ConnectionString);
                }
                return carRepository;
            }
        }

        /// <summary>Work with Drivers table in the database.</summary>
        public IDriverRepository Drivers
        {
            get
            {
                if (driverRepository == null)
                {
                    driverRepository = new DriverRepository(transaction.Connection.ConnectionString);
                }
                return driverRepository;
            }
        }

        /// <summary>Work with Clients table in the database.</summary>
        public IClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new ClientRepository(transaction.Connection.ConnectionString);
                }
                return clientRepository;
            }
        }

        /// <summary>Work with DriversLicenses table in the database.</summary>
        public IDriverLicensesRepository DriversLicenses
        {
            get
            {
                if (driverLicenseRepository == null)
                {
                    driverLicenseRepository = new DriversLicenseRepository(transaction.Connection.ConnectionString);
                }
                return driverLicenseRepository;
            }
        }

        /// <summary>Work with Trips table in the database.</summary>
        public ITripRepository Trips
        {
            get
            {
                if (tripRepository == null)
                {
                    tripRepository = new TripRepository(transaction.Connection.ConnectionString);
                }
                return tripRepository;
            }
        }

        /// <summary>Work with PassingPoints table in the database.</summary>
        public IPassingPointRepository PassingPoints
        {
            get
            {
                if (passingPointRepository == null)
                {
                    passingPointRepository = new PassingPointRepository(transaction.Connection.ConnectionString);
                }
                return passingPointRepository;
            }
        }

        /// <summary>Work with AccountsInformation table in the database.</summary>
        public IAccountInformationRepository AccountsInformation
        {
            get
            {
                if (accountInformationRepository == null)
                {
                    accountInformationRepository = new AccountInformationRepository(transaction.Connection.ConnectionString);
                }
                return accountInformationRepository;
            }
        }

        /// <summary>Work with Companions table in the database.</summary>
        public ICompanionRepository Companions
        {
            get
            {
                if (companionRepository == null)
                {
                    companionRepository = new CompanionRepository(transaction.Connection.ConnectionString);
                }
                return companionRepository;
            }
        }

        /// <summary>Saving changes to the database.</summary>
        public void Commit()
        {
            try
            {
                transaction.Commit();
            } 
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                resetRepositories();
            }
        }

        /// <summary>Clearing repositories.</summary>
        private void resetRepositories()
        {
            accountInformationRepository = null;
            carRepository = null;
            clientRepository = null;
            companionRepository = null;
            driverRepository = null;
            driverLicenseRepository = null;
            passingPointRepository = null;
            tripRepository = null;
        }

        /// <summary>Check dispose.</summary>
        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>Delete database connections.</summary>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposedValue = true;
            }
        }

        /// <summary>Delete database connections.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Destructor unit of works.</summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}

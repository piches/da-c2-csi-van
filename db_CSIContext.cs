using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CsiApi
{

    public class Logger : ILogger, IDisposable
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var s = formatter.Invoke(state, exception); 

            Console.WriteLine(s);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Logger()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    public class LoggingFactory : ILoggerFactory
    {
        private Logger _logger = new Logger(); 

        public void AddProvider(ILoggerProvider provider)
        {
            return;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _logger;
        }

        public void Dispose()
        {
            _logger.Dispose();
        }
    }
    public partial class db_CSIContext : DbContext
    {
        private static readonly ILoggerFactory loggerFactory = new LoggingFactory();


        public db_CSIContext()
        {
        
        }

        public db_CSIContext(DbContextOptions<db_CSIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Investigation> Investigation { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonPhone> PersonPhone { get; set; }
        public virtual DbSet<PersonVehicle> PersonVehicle { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<PhoneCall> PhoneCall { get; set; }
        public virtual DbSet<SurveillanceObservation> SurveillanceObservation { get; set; }
        public virtual DbSet<SurveillancePerson> SurveillancePerson { get; set; }
        public virtual DbSet<SurveillanceVehicle> SurveillanceVehicle { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskAction> TaskAction { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleFix> VehicleFix { get; set; }
        public virtual DbSet<VehicleStop> VehicleStop { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder//.UseLoggerFactory(loggerFactory)  //tie-up DbContext with LoggerFactory object
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlite("Data Source=C:\\Users\\piches\\Documents\\Digital Academy\\Practicum\\Data\\db_CSI.db");

                
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("document");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.DocumentType).HasColumnName("document_type");

                entity.Property(e => e.FileExtension).HasColumnName("file_extension");

                entity.Property(e => e.FileLocation).HasColumnName("file_location");

                entity.Property(e => e.FileName).HasColumnName("file_name");
            });

            modelBuilder.Entity<Investigation>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("investigation");

                entity.Property(e => e.CaseDateTime)
                    .HasColumnName("case_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.CaseId).HasColumnName("case_id");

                entity.Property(e => e.CaseName).HasColumnName("case_name");

                entity.Property(e => e.FileNumber).HasColumnName("file_number");

                entity.Property(e => e.PrimaryInvestigator).HasColumnName("primary_investigator");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("person");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.DriversLicenceNumber).HasColumnName("drivers_licence_number");

                entity.Property(e => e.EyeColour).HasColumnName("eye_colour");

                entity.Property(e => e.GivenName1).HasColumnName("given_name_1");

                entity.Property(e => e.GivenName2).HasColumnName("given_name_2");

                entity.Property(e => e.HairColour).HasColumnName("hair_colour");

                entity.Property(e => e.HomeCity).HasColumnName("home_city");

                entity.Property(e => e.HomeProvince).HasColumnName("home_province");

                entity.Property(e => e.HomeStreetAddress).HasColumnName("home_street_address");

                entity.Property(e => e.PersonHeight).HasColumnName("person_height");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.PersonImageLink).HasColumnName("person_image_link");

                entity.Property(e => e.PersonRole).HasColumnName("person_role");

                entity.Property(e => e.PersonWeight).HasColumnName("person_weight");

                entity.Property(e => e.ProvinceOfInsurance).HasColumnName("province_of_insurance");

                entity.Property(e => e.Race).HasColumnName("race");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.TargetNumber).HasColumnName("target_number");
            });

            modelBuilder.Entity<PersonPhone>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(e => new { e.PersonId , e.PhoneId });
                

                entity.ToTable("person_phone");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.PhoneId).HasColumnName("phone_id");

                
            });

            modelBuilder.Entity<PersonVehicle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("person_vehicle");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("phone");

                //entity.Property(e => e.AreaCode).HasColumnName("area_code");

                entity.Property(e => e.PhoneId).HasColumnName("phone_id");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.PhoneProvider).HasColumnName("phone_provider");

                entity.Property(e => e.SubscriberName).HasColumnName("subscriber_name");

                entity.Property(e => e.SubscriberAddress).HasColumnName("subscriber_address");
            });

             modelBuilder.Entity<PhoneCall>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("phone_call");

                entity.Property(e => e.CallDirection).HasColumnName("call_direction");

                entity.Property(e => e.CallDuration)
                    .HasColumnName("call_duration")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.CallStartDateTime)
                    .HasColumnName("call_start_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.CallType).HasColumnName("call_type");

                entity.Property(e => e.ContactPhoneId).HasColumnName("contact_phone_id");

                entity.Property(e => e.EndCellTowerAddress).HasColumnName("end_cell_tower_address");

                entity.Property(e => e.EndCellTowerLatitude)
                    .HasColumnName("end_cell_tower_latitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.EndCellTowerLongitude)
                    .HasColumnName("end_cell_tower_longitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.PhoneCallId).HasColumnName("phone_call_id");

                entity.Property(e => e.StartCellTowerAddress).HasColumnName("start_cell_tower_address");

                entity.Property(e => e.StartCellTowerLatitude)
                    .HasColumnName("start_cell_tower_latitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.StartCellTowerLongitude)
                    .HasColumnName("start_cell_tower_longitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.TargetPhoneId).HasColumnName("target_phone_id");
            });

            modelBuilder.Entity<SurveillanceObservation>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("surveillance_observation");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.ObservationCity).HasColumnName("observation_city");

                entity.Property(e => e.ObservationDateTime)
                    .HasColumnName("observation_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.ObservationId).HasColumnName("observation_id");

                entity.Property(e => e.ObservationLatitude)
                    .HasColumnName("observation_latitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.ObservationLongitude)
                    .HasColumnName("observation_longitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.ObservationProvince).HasColumnName("observation_province");

                entity.Property(e => e.ObservationStreetAddress).HasColumnName("observation_street_address");

                entity.Property(e => e.ObservationText).HasColumnName("observation_text");
            });

            modelBuilder.Entity<SurveillancePerson>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("surveillance_person");

                entity.Property(e => e.ObservationId).HasColumnName("observation_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");
            });

            modelBuilder.Entity<SurveillanceVehicle>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("surveillance_vehicle");

                entity.Property(e => e.ObservationId).HasColumnName("observation_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            });

            modelBuilder.Entity<Task>(entity =>
            {
               // entity.HasNoKey();

                entity.ToTable("task");

                entity.Property(e => e.CaseId).HasColumnName("case_id");

                entity.Property(e => e.TaskAssignedDateTime)
                    .HasColumnName("task_assigned_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.TaskAssigneeName).HasColumnName("task_assignee_name");

                entity.Property(e => e.TaskCompletedDateTime)
                    .HasColumnName("task_completed_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.TaskDescription).HasColumnName("task_description");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.TaskNumber).HasColumnName("task_number");

                entity.Property(e => e.TaskType).HasColumnName("task_type");
            });

            modelBuilder.Entity<TaskAction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("task_action");

                entity.Property(e => e.ActionCompletedDateTime)
                    .HasColumnName("action_completed_date_time")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.ActionDescription).HasColumnName("action_description");

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.ActionNumber).HasColumnName("action_number");

                entity.Property(e => e.ActionType).HasColumnName("action_type");

                entity.Property(e => e.TaskId).HasColumnName("task_id");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("vehicle");

                entity.Property(e => e.VehicleColour).HasColumnName("vehicle_colour");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.VehicleLicensePlate).HasColumnName("vehicle_license_plate");

                entity.Property(e => e.VehicleMake).HasColumnName("vehicle_make");

                entity.Property(e => e.VehicleModel).HasColumnName("vehicle_model");

                entity.Property(e => e.VehicleProvince).HasColumnName("vehicle_province");

                entity.Property(e => e.VehicleRegisteredOwner).HasColumnName("vehicle_registered_owner");

                entity.Property(e => e.VehicleVin).HasColumnName("vehicle_vin");

                entity.Property(e => e.VehicleYear).HasColumnName("vehicle_year");
            });

            modelBuilder.Entity<VehicleFix>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("vehicle_fix");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.HasKey(e => new { e.FixId , e.VehicleId });

                entity.Property(e => e.FixId).HasColumnName("fix_id");

                entity.Property(e => e.VehicleFixDateTime)
                    .HasColumnName("vehicle_fix_date_time")
                    .HasColumnType("NUMERIC");

                //entity.Property(e => e.VehicleFixId).HasColumnName("vehicle_fix_id");


                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.VehicleLatitude)
                    .HasColumnName("vehicle_latitude")
                    .HasColumnType("NUMERIC");

                entity.Property(e => e.VehicleLongitude)
                    .HasColumnName("vehicle_longitude")
                    .HasColumnType("NUMERIC");

            

            });

             modelBuilder.Entity<VehicleStop>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("vehicle_stop");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.VehicleLatitude).HasColumnName("vehicle_latitude");

                entity.Property(e => e.VehicleLongitude).HasColumnName("vehicle_longitude");

                entity.Property(e => e.VehicleStopDuration).HasColumnName("vehicle_stop_duration");

                entity.Property(e => e.VehicleStopEndDateTime).HasColumnName("vehicle_stop_end_date_time");

                entity.Property(e => e.VehicleStopId).HasColumnName("vehicle_stop_id");

                entity.Property(e => e.VehicleStopNumber).HasColumnName("vehicle_stop_number");

                entity.Property(e => e.VehicleStopStartDateTime)
                    .HasColumnName("vehicle_stop_start_date_time")
                    .HasColumnType("NUMERIC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

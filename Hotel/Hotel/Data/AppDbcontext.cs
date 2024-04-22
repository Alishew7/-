using Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class AppDbcontext: DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
		public DbSet<Floor> Floors { get; set; }

		public DbSet<RoomType> RoomType { get; set; }

		public DbSet<Room> Room { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
		public DbSet<User> User { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<RoomType>().Property(x=>x.Price).HasColumnType("decimal(18,2)");
			modelBuilder.Entity<Reservation>().Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
			modelBuilder.Entity<User>().HasData(
				new User { Id = 1, UserName = "Admin", Password = "123", Role = Role.Admin },
				new User { Id = 2, UserName = "Receptionist", Password = "123", Role = Role.Receptionist }
				) ;
			modelBuilder.Entity<ReservationStatus>().HasData(
			new ReservationStatus { Id = 1, Title = "Reserved" },
			new ReservationStatus { Id = 2, Title = "Paid" },
            new ReservationStatus { Id = 3, Title = "Finished" },
            new ReservationStatus { Id = 4, Title = "Canceled" }
            );
            modelBuilder.Entity<Room>()
				.HasOne(r=> r.RoomType)
				.WithMany(rt => rt.Rooms)
				.HasForeignKey(r=> r.RoomTypeId)
				.OnDelete(DeleteBehavior.Restrict);


		}

	}
}

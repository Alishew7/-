using Hotel.Data;
using Hotel.Filters;
using Hotel.Models;
using Hotel.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
	[AuthFilter]
	public class ReservationController : Controller
	{
		private readonly AppDbcontext _db;

		public ReservationController(AppDbcontext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Reservation> objListReservations = _db.Reservations.Include(r=> r.ReservationStatus).Include(r=> r.Client).Include(r => r.Room);
			return View(objListReservations);
		}

		public IActionResult Details(int id) {
			Reservation reservation = _db.Reservations
				.Include(r => r.ReservationStatus)
				.Include(r => r.Client)
				.Include(r => r.Room).Include(r=>r.Room.RoomType).Include(r=>r.Room.Floor)
				.FirstOrDefault(r => r.Id == id);
            return View(reservation);
		}

        [HttpGet]
        public IActionResult Create() { 
			
			ReservationVM reservationVM = new ReservationVM();
			reservationVM.Clients = _db.Clients.ToList();
			reservationVM.Rooms = _db.Room.Include(r => r.RoomType).Include(r=>r.Floor).ToList();
			reservationVM.Statuses = _db.ReservationStatuses.ToList();


			return View(reservationVM); 
		
		}


		[HttpPost]
		public IActionResult Create(ReservationVM model)
		{
			if (!ModelState.IsValid)
			{
                model.Clients = _db.Clients.ToList();
                model.Rooms = _db.Room.Include(r => r.RoomType).Include(r => r.Floor).ToList();
                model.Statuses = _db.ReservationStatuses.ToList();


                return View(model);
            }

			Reservation reservation = new Reservation();
			reservation.RservationStart = model.RservationStart;
			reservation.RservationEnd = model.RservationEnd;

			reservation.ReservationStatus = _db.ReservationStatuses.SingleOrDefault(s => s.Id == model.statusId);
			reservation.StatusId = model.statusId;

            reservation.Client = _db.Clients.SingleOrDefault(s => s.Id == model.Client_id);
            reservation.Client_id = model.Client_id;

            reservation.Room = _db.Room.SingleOrDefault(s => s.Id == model.Room_id);
            reservation.Room_id = model.Room_id;

            reservation.TotalPrice = model.TotalPrice;

			_db.Reservations.Add(reservation);
			_db.SaveChanges();
            return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Reservation reservation = _db.Reservations
                .Include(r => r.ReservationStatus)
                .Include(r => r.Client)
                .Include(r => r.Room).Include(r => r.Room.RoomType).Include(r => r.Room.Floor)
                .FirstOrDefault(r => r.Id == id);

            if(reservation == null)
            {
                return RedirectToAction("Index");
            }
            ReservationVM reservationVM = new ReservationVM();
            reservationVM.Clients = _db.Clients.ToList();
            reservationVM.Rooms = _db.Room.Include(r => r.RoomType).Include(r => r.Floor).ToList();
            reservationVM.Statuses = _db.ReservationStatuses.ToList();
            reservationVM.RservationStart = reservation.RservationStart;
            reservationVM.RservationEnd = reservation.RservationEnd;
            reservationVM.Client_id = reservation.Client.Id;
            reservationVM.Id = reservation.Id;
            reservationVM.statusId = reservation.Id;
            reservationVM.Room_id = reservation.Room.Id;
            reservationVM.TotalPrice = reservation.TotalPrice;

            return View(reservationVM);

        }


        [HttpPost]
        public IActionResult Edit(ReservationVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Clients = _db.Clients.ToList();
                model.Rooms = _db.Room.Include(r => r.RoomType).Include(r => r.Floor).ToList();
                model.Statuses = _db.ReservationStatuses.ToList();


                return View(model);
            }

            Reservation reservation = new Reservation();
            reservation.Id = model.Id;

            reservation.RservationStart = model.RservationStart;
            reservation.RservationEnd = model.RservationEnd;

            reservation.ReservationStatus = _db.ReservationStatuses.SingleOrDefault(s => s.Id == model.statusId);
            reservation.StatusId = model.statusId;

            reservation.Client = _db.Clients.SingleOrDefault(s => s.Id == model.Client_id);
            reservation.Client_id = model.Client_id;

            reservation.Room = _db.Room.SingleOrDefault(s => s.Id == model.Room_id);
            reservation.Room_id = model.Room_id;

            reservation.TotalPrice = model.TotalPrice;

            _db.Reservations.Update(reservation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

		public IActionResult Delete(int id)
		{

			//намира от базата данни етаж който има същото id като подаденото id
			Reservation reservation = _db.Reservations.SingleOrDefault(x => x.Id == id);
			if (reservation != null)
			{
				_db.Remove(reservation);
				_db.SaveChanges();
			}

			return RedirectToAction("Index");

		}
	}
}


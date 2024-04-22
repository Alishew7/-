using Hotel.Data;
using Hotel.Filters;
using Hotel.Models;
using Hotel.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [AuthFilter]
    public class RoomController : Controller
    {
        private readonly AppDbcontext _db;
        public RoomController(AppDbcontext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Room> objListRoom = _db.Room.Include(r => r.RoomType).Include(r=> r.Floor);
            return View(objListRoom);
        }

        [HttpGet]
        [RoleFilter]
        public IActionResult Create()
        {


            RoomVM model = new RoomVM();
            model.floors = _db.Floors.ToList();
            model.roomTypes = _db.RoomType.ToList();



            return View(model);
        }

        [HttpPost]
        [RoleFilter]
        public IActionResult Create(RoomVM model)
        {
            if (!ModelState.IsValid)
            {
               

                model.floors = _db.Floors.ToList();
                model.roomTypes = _db.RoomType.ToList();


                return View(model);
            }
            Room room = new Room();
            room.RoomNumber = model.RoomNumber;
            room.RoomTypeId = model.RoomTypeId;
            room.RoomType = _db.RoomType.SingleOrDefault(rt => rt.Id == model.RoomTypeId);

            int floor_id = model.FloorId;
            room.FloorId = floor_id;
            room.Floor = _db.Floors.SingleOrDefault(rt => rt.Id == floor_id);

            _db.Room.Add(room);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Room room = _db.Room.Include(r=>r.Floor).SingleOrDefault(x => x.Id == id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            RoomVM roomVM = new RoomVM();
            roomVM.FloorId = room.FloorId;
            roomVM.floors = _db.Floors.ToList();
            roomVM.roomTypes = _db.RoomType.ToList();
            roomVM.RoomNumber = room.RoomNumber;
            roomVM.RoomTypeId = room.RoomTypeId;
            return View(roomVM);

        }

        [HttpPost]
        public IActionResult Edit(RoomVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Room room = new Room();
            room.FloorId = model.FloorId;
            room.RoomNumber = model.RoomNumber;
            room.RoomTypeId = model.RoomTypeId;
            _db.Room.Update(room);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Room room = _db.Room.SingleOrDefault(x => x.Id == id);
            if (room != null)
            {
                _db.Remove(room);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

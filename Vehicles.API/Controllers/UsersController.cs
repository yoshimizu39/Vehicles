using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.API.Models;
using Vehicles.Common.Enums;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public UsersController(DataContext context, IUserHelper userHelper, ICombosHelper combosHelper, IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Include(u => u.DocumentType)
                                            .Include(u => u.Vehicles)
                                            .Where(u => u.UserType == UserType.User)
                                            .ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users.Include(x => x.DocumentType)
                                            .Include(x => x.Vehicles)
                                            .ThenInclude(x => x.Brand)
                                            .Include(x => x.Vehicles)
                                            .ThenInclude(x => x.VehicleType)
                                            .Include(x => x.Vehicles)
                                            .ThenInclude(x => x.VehiclePhotos)
                                            .Include(x => x.Vehicles)
                                            .ThenInclude(x => x.Histories)
                                            .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {                
                DocumentTypes = _combosHelper.GetComboDocumentTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageId = string.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _imageHelper.UpLoadImageAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, true);
                user.UserType = UserType.User;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString()); //creamos el role de tipo usser

                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model = _converterHelper.ToUserViewModel(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _imageHelper.UpLoadImageAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, false);
                await _userHelper.UpdateUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
                        
            User user = await _userHelper.GetUserAsync(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            //_imageHelper.DeleteBlobAsync(user);
            await _userHelper.DeleteUserAsync(user);

            return RedirectToAction(nameof(Index));
        }


        //---------------------------------VEHICLE---------------------------------------//

        public async Task<IActionResult> AddVehicle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users.Include(x => x.Vehicles)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            VehicleViewModel model = new VehicleViewModel
            {
                Brands = _combosHelper.GetComboBands(),
                UserId = user.Id,
                VehicleTypes = _combosHelper.GetComboVehicleTypes()                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel vehicleViewModel)
        {
            User user = await _context.Users.Include(x => x.Vehicles)
                                            .FirstOrDefaultAsync(x => x.Id == vehicleViewModel.UserId);
            if (user == null)
            {
                return NotFound();
            }

            string imageId = string.Empty;

            if (vehicleViewModel.ImageFile != null)
            {
                imageId = await _imageHelper.UpLoadImageAsync(vehicleViewModel.ImageFile, "vehiclephotos");
            }

            Vehicle vehicle = await _converterHelper.ToVehicleAsync(vehicleViewModel, true);

            //if (vehicleViewModel.ImageFile != null)
            //{
            //    imageId = await _imageHelper.UpLoadImageAsync(vehicleViewModel.ImageFile, "vehiclephotos");

            //    vehicle.VehiclePhotos = new List<VehiclePhoto>
            //    {
            //        new VehiclePhoto
            //        {
            //            ImageId = imageId
            //        }
            //    };
            //}


            if (vehicle.VehiclePhotos == null)
            {
                vehicle.VehiclePhotos = new List<VehiclePhoto>();
            }

            vehicle.VehiclePhotos.Add(new VehiclePhoto
            {
                ImageId = imageId
            });

            try
            {
                //vehicle.Id = 0;
                user.Vehicles.Add(vehicle);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = user.Id });
            }
            catch (DbUpdateException db)
            {
                if (db.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Placa existente para el vehículo");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, db.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            vehicleViewModel.Brands = _combosHelper.GetComboBands();
            vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();

            return View(vehicleViewModel);
        }

        public async Task<IActionResult> EditVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.Include(x => x.User)
                                                     .Include(x => x.VehiclePhotos)
                                                     .Include(x => x.Brand)
                                                     .Include(x => x.VehicleType)
                                                     .FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            VehicleViewModel model = _converterHelper.ToVehicleViewModel(vehicle);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicle(int id, VehicleViewModel vehicleViewModel)
        {
            if (id != vehicleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vehicle vehicle = await _converterHelper.ToVehicleAsync(vehicleViewModel, false);
                    _context.Vehicles.Update(vehicle);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { id = vehicleViewModel.UserId });
                }
                catch (DbUpdateException db)
                {
                    if (db.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esta placa");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, db.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            vehicleViewModel.Brands = _combosHelper.GetComboBands();
            vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();

            return View(vehicleViewModel);
        }

        public async Task<IActionResult> DeleteVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.Include(x => x.User)
                                          .Include(x => x.VehiclePhotos)
                                          .Include(x => x.Histories)
                                          .ThenInclude(x => x.Details)
                                          .FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = vehicle.User.Id });
        }

        public async Task<IActionResult> DeleteImageVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehiclePhoto vehiclePhoto = await _context.VehiclePhotos.Include(x => x.Vehicle)
                                                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (vehiclePhoto == null)
            {
                return NotFound();
            }

            //await _blobHelper.DeleteBlobAsync(vehiclePhoto.ImageId, "vehiclephotos");

            _context.VehiclePhotos.Remove(vehiclePhoto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditVehicle), new { id = vehiclePhoto.Vehicle.Id });
        }

        public async Task<IActionResult> AddVehicleImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            VehiclePhotoViewModel model = new()
            {
                VehicleId = vehicle.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicleImage(VehiclePhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageId = await _imageHelper.UpLoadImageAsync(model.ImageFile, "vehiclephotos");

                Vehicle vehicle = await _context.Vehicles.Include(x => x.VehiclePhotos)
                                                         .FirstOrDefaultAsync(x => x.Id == model.VehicleId);

                if (vehicle.VehiclePhotos == null)
                {
                    vehicle.VehiclePhotos = new List<VehiclePhoto>();
                }

                vehicle.VehiclePhotos.Add(new VehiclePhoto
                {
                    ImageId = imageId
                });

                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(EditVehicle), new { id = vehicle.Id });
            }

            return View(model);
        }
    }
}

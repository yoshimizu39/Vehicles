using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.Common.Enums;

namespace Vehicles.API.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckVehiclesTypeAsync();
            await CheckBrandsAsync();
            await CheckDocumentTypesAsync();
            await CheckProcedures();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Yoshi Yoél", "Muñoz Shimizu", "yoshi@yopmail.com", "955221095", "Urb. Lima D-12", UserType.User);
            await CheckUserAsync("1010", "Denni David", "Muñoz Shimizu", "dennis@yopmail.com", "888888888", "Urb. Lima D-12", UserType.User);
            await CheckUserAsync("1010", "Edgar Emmanuel", "Muñoz Shimizu", "emanuel@yopmail.com", "777777777", "Urb. Lima D-12", UserType.Admin);
        }

        private async Task CheckUserAsync(string document, string firsName, string lastName, string email, string telefono, string direccion, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Document = document,
                    FirstName = firsName,
                    LastName = lastName,
                    UserName = email,
                    PhoneNumber = telefono,
                    Address = direccion,
                    Email = email,
                    UserType = userType,
                    DocumentType = _context.DocumentTypes.FirstOrDefault(x => x.Description == "CÉDULA")
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
        } 

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckProcedures()
        {
            if (!_context.Procedures.Any())
            {
                _context.Procedures.Add(new Procedure { Description = "ALINEACIÓN", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LUBRICACIÓN DE SUSPENSIÓN DELANTERA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LUBRICACIÓN DE SUSPENSIÓN TRASERA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "FRENOS DELANTEROS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "FRENOS TRASEROS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LÍQUIDO FRENOS DELANTEROS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LÍQUIDO FRENOS TRASEROS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CALIBRACIÓN DE VÁLVULAS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "ALINEACIÓN DE CARBURADOR", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "ACEITE MOTOR", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "ACEITE CAJA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "FILTRO DE AIRE", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "SISTEMA ELÉCTRICO", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "GUAYAS", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CAMBIO LLANTA DELANTERA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CAMBIO LLANTA TRASERA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "REPARACIÓN DE MOTOR", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "KIT ARRASTRE", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "BANDA TRANSMISIÓN", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CAMBIO BATERÍA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LAVADO SISTEMA DE INYECCIÓN", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "LAVADA DE TANQUE", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CAMBIO DE BUJÍA", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "CAMBIO RODAMIENTO DELANTERO", Price = 1000 });
                _context.Procedures.Add(new Procedure { Description = "ACCESORIOS", Price = 1000 });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Description = "CÉDULA" });
                _context.DocumentTypes.Add(new DocumentType { Description = "TARJETA DE IDENTIDAD" });
                _context.DocumentTypes.Add(new DocumentType { Description = "NIT" });
                _context.DocumentTypes.Add(new DocumentType { Description = "PASAPORTE" });
                _context.DocumentTypes.Add(new DocumentType { Description = "DNI" });
                _context.DocumentTypes.Add(new DocumentType { Description = "RUC" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckBrandsAsync()
        {
            if (!_context.Brands.Any())
            {
                _context.Brands.Add(new Brand { Description = "DUCATI" });
                _context.Brands.Add(new Brand { Description = "HARLEY DAVIDSON" });
                _context.Brands.Add(new Brand { Description = "KTM" });
                _context.Brands.Add(new Brand { Description = "BMX" });
                _context.Brands.Add(new Brand { Description = "TRIUMPH" });
                _context.Brands.Add(new Brand { Description = "VICTORIA" });
                _context.Brands.Add(new Brand { Description = "HONDA" });
                _context.Brands.Add(new Brand { Description = "SUZUKI" });
                _context.Brands.Add(new Brand { Description = "HAWASAKY" });
                _context.Brands.Add(new Brand { Description = "TVS" });
                _context.Brands.Add(new Brand { Description = "BAJAJ" });
                _context.Brands.Add(new Brand { Description = "AKT" });
                _context.Brands.Add(new Brand { Description = "YAMAHA" });
                _context.Brands.Add(new Brand { Description = "CHEVROLET" });
                _context.Brands.Add(new Brand { Description = "MAZDA" });
                _context.Brands.Add(new Brand { Description = "RENAULT" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckVehiclesTypeAsync()
        {
            if (!_context.VehicleTypes.Any())
            {
                _context.VehicleTypes.Add(new VehicleType { Description = "CARRO" });
                _context.VehicleTypes.Add(new VehicleType { Description = "MOTO" });
            }

            await _context.SaveChangesAsync();
        }
    }
}

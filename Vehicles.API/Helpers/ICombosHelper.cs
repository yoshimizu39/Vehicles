using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Vehicles.API.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboDocumentTypes();
        IEnumerable<SelectListItem> GetComboDocumentProcedures();
        IEnumerable<SelectListItem> GetComboVehicleTypes();
        IEnumerable<SelectListItem> GetComboBands();
    }
}

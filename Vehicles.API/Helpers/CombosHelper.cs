using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Vehicles.API.Data;

namespace Vehicles.API.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboBands()
        {
            List<SelectListItem> list = _context.Brands.Select(b => new SelectListItem
            {
                Text = b.Description,
                Value = $"{b.Id}" //conversión a string
            })
                .OrderBy(b => b.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una marca]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDocumentProcedures()
        {
            List<SelectListItem> list = _context.Procedures.Select(p => new SelectListItem
            {
                Text = p.Description,
                Value = $"{p.Id}"
            })
                .OrderBy(p => p.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar un procedimiento]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDocumentTypes()
        {
            List<SelectListItem> list = _context.DocumentTypes.Select(dt => new SelectListItem
            {
                Text = dt.Description,
                Value = $"{dt.Id}"
            })
                .OrderBy(dt => dt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionar un tipo de documento]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboVehicleTypes()
        {
            List<SelectListItem> list = _context.VehicleTypes.Select(vt => new SelectListItem
            {
                Text = vt.Description,
                Value = $"{vt.Id}"
            })
                .OrderBy(vt => vt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccionat un tipo de vehículo]",
                Value = "0"
            });

            return list;
        }
    }
}

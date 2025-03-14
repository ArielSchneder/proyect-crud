using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentaProducto.Models
{
	public class Producto
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
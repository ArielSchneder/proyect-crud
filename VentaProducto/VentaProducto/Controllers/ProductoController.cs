using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using VentaProducto.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System;

namespace VentaProducto.Controllers
{
    public class ProductoController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ProductContext"].ConnectionString;

        // GET: Producto
        public ActionResult Index()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = (decimal)reader["Precio"],
                        Stock = (int)reader["Stock"]
                    });
                }
            }
            return View(productos);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Convierte el precio a decimal usando CultureInfo.InvariantCulture
                //producto.Precio = Convert.ToDecimal(producto.Precio.ToString().Replace(",", "."), CultureInfo.InvariantCulture);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES (@Nombre, @Descripcion, @Precio, @Stock)", conn);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    producto.Id = (int)reader["Id"];
                    producto.Nombre = reader["Nombre"].ToString();
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = (decimal)reader["Precio"];
                    producto.Stock = (int)reader["Stock"];
                }
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Convierte el precio a decimal usando CultureInfo.InvariantCulture
                //producto.Precio = Convert.ToDecimal(producto.Precio.ToString().Replace(",", "."), CultureInfo.InvariantCulture);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Id", producto.Id);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    producto.Id = (int)reader["Id"];
                    producto.Nombre = reader["Nombre"].ToString();
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = (decimal)reader["Precio"];
                    producto.Stock = (int)reader["Stock"];
                }
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Productos WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
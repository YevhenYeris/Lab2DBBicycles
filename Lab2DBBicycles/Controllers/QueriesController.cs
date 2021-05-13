using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2DBBicycles.Models;

namespace Lab2DBBicycles.Controllers
{
    public class QueriesController : Controller
    {
        private readonly Lab2DbContext _context;

        public QueriesController(Lab2DbContext context)
        {
            _context = context;
        }

        // GET: QueriesController
        public ActionResult Index()
        {
            ViewData["Store"] = new SelectList(_context.Stores, "Name", "Name");
            ViewData["Country"] = new SelectList(_context.Countries, "Name", "Name");
            ViewData["Year"] = new SelectList(_context.Bicycles.Select(e => e.Year).Distinct());
            return View();
        }

        // GET: QueriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QueriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QueriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QueriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QueriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QueriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QueriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SimpleOne(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleOne.sql");
            queryText = queryText.Replace("StoreA", query.StoreName1);
            queryText = queryText.Replace("StoreB", query.StoreName2);

            query.Id = 1;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        query.BicycleNames = new List<string>();
                        while (reader.Read())
                        {
                            query.BicycleNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SimpleTwo(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleTwo.sql");
            queryText = queryText.Replace("CountryA", query.CountryName);

            query.Id = 2;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        query.StoreNames = new List<string>();
                        while (reader.Read())
                        {
                            query.StoreNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SimpleThree(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleThree.sql");
            queryText = queryText.Replace("StoreCount", $"{query.StoreCount}");

            query.Id = 3;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    var result = command.ExecuteScalar();
                    query.StoreCount = (int)result;
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SimpleFour(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleFour.sql");
            queryText = queryText.Replace("StoreA", query.StoreName1);

            query.Id = 4;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        query.BrandNames = new List<string>();
                        query.Prices = new List<double>();
                        while (reader.Read())
                        {
                            query.BrandNames.Add(reader.GetString(0));
                            query.Prices.Add(reader.GetDouble(1));
                        }
                    }
                }
                connection.Close();
            }

            queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleFour_Helper.sql");
            queryText = queryText.Replace("StoreA", query.StoreName1);

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.StoreName1 = reader.GetString(0);
                            query.AvgPrice = reader.GetDouble(1);
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SimpleFive(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SimpleFive.sql");
            queryText = queryText.Replace("StoreA", query.StoreName1);
            queryText = queryText.Replace("PriceNumber", $"{query.Price}");

            query.BicycleNames = new List<string>();
            query.Id = 5;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.BicycleNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetOne(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SetOne.sql");
            queryText = queryText.Replace("Price1", $"{query.Price}");
            queryText = queryText.Replace("Price2", $"{query.Price1}");

            query.StoreNames = new List<string>();
            query.Id = 6;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.StoreNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetTwo(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SetTwo.sql");
            queryText = queryText.Replace("Store1", query.StoreName1);
            queryText = queryText.Replace("Store2", query.StoreName2);

            query.BrandNames = new List<string>();
            query.Id = 7;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.BrandNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetThree(Query query)
        {
            string queryText = System.IO.File.ReadAllText(@"D:\Shkola\2ndCourse\DB\Lab2DBBicycles\Lab2DBBicycles\Queries\SetThree.sql");
            queryText = queryText.Replace("Count1", $"{query.Count1}");
            queryText = queryText.Replace("Count2", $"{query.Count2}");

            query.BicycleNames = new List<string>();
            query.Id = 8;

            using (var connection = new SqlConnection("Server= DESKTOP-7P6VKRI; Database=Lab2Db; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                connection.Open();
                using (var command = new SqlCommand(queryText, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            query.BicycleNames.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Output", query);
        }

        public IActionResult Output(Query query)
        {
            return View(query);
        }
    }
}

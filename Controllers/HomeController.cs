using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
  public class HomeController : Controller
  {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=109771;Uid=109771;Pwd=msOMBrAn;";



        public IActionResult Index()
        {
            return View();
        }


        [Route("Menu")]
        public IActionResult Menu()
    {
            return View();
    }

    private Performers GetPerformers(string id)
    {
      List<Performers> performers = new List<Performers>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("select * from performers", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Performers p = new Performers
            {
              Id = Convert.ToInt32(reader["id"]),
              Name = reader["name"].ToString(),
              Descr = reader["Description"].ToString(),
              Date = DateTime.Parse(reader["date"].ToString())
            };
            performers.Add(p);
          }
        }
      }

      return performers[0];
    }

        private List<Voorstelling> GetVoorstellingen()
        {
            List<Voorstelling> voorstellingen = new List<Voorstelling>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from voorstelling", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Voorstelling v = new Voorstelling
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),
                     
                        };
                        voorstellingen.Add(v);
                    }
                }
            }

            return voorstellingen;
        }

        public IActionResult Privacy()
    {
      return View();
    }

        [Route("FAQ")]
        public IActionResult FAQ()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("Zakelijk")]
        public IActionResult Zakelijk()
        {
            return View();
        }

        [Route("Zakelijk")]
        [HttpPost]

        public IActionResult Zakelijk(PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SavePerson(model);

            ViewData["formsucces"] = "ok";

            return View();
        }

        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, email, telefoon) VALUES(?naam,?email,?telefoon)", conn);

                cmd.Parameters.Add("?naam", MySqlDbType.VarChar).Value = person.name;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.email;
                cmd.Parameters.Add("?telefoon", MySqlDbType.VarChar).Value = person.name;
                cmd.ExecuteNonQuery();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}

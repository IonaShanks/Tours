using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TourProject.Models;

namespace TourProject.Controllers
{
    public class ToursController : Controller
    {

        TourContext db = new TourContext();
        // GET: Tours
        public ActionResult Index(string city, string searchTitle, string country, string sort)
        {
            IQueryable<Tour> tours = db.Tours;

            //Creating Select lists for each             
            ViewBag.city = new SelectList(GetDepartureList());
            //ViewBag.country = new SelectList(GetCountryList());
            ViewBag.sort = new SelectList(GetSortList());

            //sort by categories
            if (!string.IsNullOrEmpty(sort))
            {
                tours = SortBy(sort);
            }

            //Search by departure city
            if (!string.IsNullOrEmpty(city))
            {
                //Adds the tours to the list that are from the selected city
                cityResult = "City: " + city + ". ";
                tours = tours.Where(c => c.Departure.City.ToString() == city);
            }

            //Search by partial title
            if (!string.IsNullOrEmpty(searchTitle))
            {
                //Adds the tours to the list that include the title searched
                titleResult = "Title that includes: " + searchTitle + ". ";
                tours = tours.Where(c => c.Title.Contains(searchTitle));
            }

            //Calling the DB to read in the json file if db is empty
            if (!db.Tours.Any())
            {
                Add();
            }
            return View(tours);
        }

        private void Add()
        {            
            StreamReader r = new StreamReader(Server.MapPath("~/App_Data/api_tours.json"));
            string json = r.ReadToEnd();
            List<Tour> tours = JsonConvert.DeserializeObject<List<Tour>>(json);            
            db.Tours.AddRange(tours);
            db.SaveChanges();            
        }

        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        public static TextInfo myTI = new CultureInfo("en-IE", false).TextInfo;
        public static string TitleCase(string q)
        {
            q = myTI.ToTitleCase(q);
            return q;
        }

        //For use in view
        private string cityResult = "";
        private string sortResult = "";
        private string titleResult = "";

        public IQueryable<Tour> SortBy(string sort)
        {
            IQueryable<Tour> tours = db.Tours;
            //Orders the tour list based on the sort selected from the list.
            if (sort == "Title (A-Z)")
            {
                tours = tours.OrderBy(t => t.Title);
            }
            else if (sort == "Title (Z-A)")
            {
                tours = tours.OrderByDescending(t => t.Title);
            }
            else if (sort == "Price (H-L)")
            {
                tours = tours.OrderByDescending(t => t.Pricing.Price);
            }
            else if (sort == "Price (L-H)")
            {
                tours = tours.OrderBy(t => t.Pricing.Price);
            }
            else
            {
                tours = tours.OrderBy(t => t.Id);
            }
            sortResult = "Sorted by: " + sort + ". ";
            return tours;
        }

        public List<string> GetDepartureList()
        {
            //Creates a list of distinct cities that have departures from them.
            var CityList = new List<string>();
            var CityQry = db.Tours.Select(c => c.Departure.City.ToString());
            CityList.AddRange(CityQry.Distinct());
            return CityList;
        }

        public List<string> GetSortList()
        {
            //Select list to sort the table
            var SortList = new List<string>();
            SortList.Add("Title (A-Z)");
            SortList.Add("Title (Z-A)");
            SortList.Add("Price (H-L)");
            SortList.Add("Price (L-H)");
            return SortList;
        }
    }
}

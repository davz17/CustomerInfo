using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerInfo.Models;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;

namespace CustomerInfo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Display(string sortOrder, string searchTitle, string searchGivenName, string searchMiddleInitial, string searchSurname, string searchGender, string searchCompany, string searchOccupation, string searchEmailAddress, string columnName, int NoOfRows, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SearchTitle = searchTitle;
            ViewBag.SearchGivenName = searchGivenName;
            ViewBag.SearchMiddleInitial = searchMiddleInitial;
            ViewBag.SearchSurname = searchSurname;
            ViewBag.SearchGender = searchGender;
            ViewBag.SearchCompany = searchCompany;
            ViewBag.SearchOccupation = searchOccupation;
            ViewBag.SearchEmailAddress = searchEmailAddress;

            var context = new CustomersContext();
            var customersSort = from s in context.Customers 
                                select s;

            if (!String.IsNullOrEmpty(searchTitle))
            {
                customersSort = customersSort.Where(s => s.Title.Equals(searchTitle));
            }
            if (!String.IsNullOrEmpty(searchGivenName))
            {
                customersSort = customersSort.Where(s => s.GivenName.Contains(searchGivenName));
            }
            if (!String.IsNullOrEmpty(searchMiddleInitial))
            {
                customersSort = customersSort.Where(s => s.MiddleInitial.Equals(searchMiddleInitial));
            }
            if (!String.IsNullOrEmpty(searchSurname))
            {
                customersSort = customersSort.Where(s => s.Surname.Contains(searchSurname));
            }
            if (!String.IsNullOrEmpty(searchGender))
            {
                customersSort = customersSort.Where(s => s.Gender.Equals(searchGender));
            }
            if (!String.IsNullOrEmpty(searchCompany))
            {
                customersSort = customersSort.Where(s => s.Company.Contains(searchCompany));
            }
            if (!String.IsNullOrEmpty(searchOccupation))
            {
                customersSort = customersSort.Where(s => s.Occupation.Equals(searchOccupation));
            }
            if (!String.IsNullOrEmpty(searchEmailAddress))
            {
                customersSort = customersSort.Where(s => s.EmailAddress.Equals(searchEmailAddress));
            }

            switch (columnName)
            {
                case "Title":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.Title) : customersSort.OrderByDescending(s => s.Title);
                    break;
                case "Given Name":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.GivenName) : customersSort.OrderByDescending(s => s.GivenName);
                    break;
                case "Middle Initial":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.MiddleInitial) : customersSort.OrderByDescending(s => s.MiddleInitial);
                    break;
                case "Surname":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.Surname) : customersSort.OrderByDescending(s => s.Surname);
                    break;
                case "Gender":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.Gender) : customersSort.OrderByDescending(s => s.Gender);
                    break;
                case "Company":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.Company) : customersSort.OrderByDescending(s => s.Company);
                    break;
                case "Occupation":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.Occupation) : customersSort.OrderByDescending(s => s.Occupation);
                    break;
                case "Email Address":
                    customersSort = ViewBag.NameSortParm == "name_desc" ? customersSort.OrderBy(s => s.EmailAddress) : customersSort.OrderByDescending(s => s.EmailAddress);
                    break;
                default:
                    customersSort = customersSort.OrderBy(s => s.Title);
                    break;
            }
            //return View(customersSort);
            
            int pageSize = NoOfRows == 0 ? 10: NoOfRows;
            int pageNumber = (page ?? 1);
            return View(customersSort.ToPagedList(pageNumber, pageSize));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

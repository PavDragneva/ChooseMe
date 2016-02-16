﻿namespace ChooseMe.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using ChooseMe.Web.Models.Organization;
    using Common.Constants;
    using Ninject;
    using PagedList;
    using Services.Contracts;
    using System;
    using System.Linq;
    using System.Web.Mvc;


    public class OrganizationsController:Controller
    {
        [Inject]
        public IOrganizationService OrganizationService { get; set; }

        [HttpGet]
        public ActionResult All(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var organizations = OrganizationService
                .GetAll()
                .ProjectTo<OrganizationsListView>();

            if (!string.IsNullOrEmpty(searchString))
            {
                organizations = organizations
                .Where(a => a.Name.ToLower().Contains(searchString.ToLower()));
            }

            int pageNumber = (page ?? 1);
            var sorted = this.Sorted(organizations, sortOrder);

            return View(sorted.ToPagedList(pageNumber, ControllersConst.PageSizeOrg));
        }

        private IQueryable<OrganizationsListView> Sorted(IQueryable<OrganizationsListView> all, string sortOrder)
        {
            ViewBag.NumberAnimalsSortParm = sortOrder == "NAnimals" ? "nanimals":"NAnimals";
            ViewBag.LookingVolunteersSortParm = sortOrder == "LFVolunteers" ? "lfvolunteers_desc" : "LFVolunteers";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            switch (sortOrder)
            {
                case "LFVolunteers":
                    all = all.OrderBy(o => o.IsLookingForVolunteers);
                    break;
                case "lfvolunteers_desc":
                    all = all.OrderByDescending(o => o.IsLookingForVolunteers);
                    break;
                case "Name":
                    all = all.OrderBy(o => o.Name);
                    break;
                case "name_desc":
                    all = all.OrderByDescending(o => o.Name);
                    break;
                case "NAnimals":
                    all = all.OrderByDescending(o => o.AnimalsNumber);
                    break;
                case "nanimals":
                    all = all.OrderBy(o => o.AnimalsNumber);
                    break;
                default:
                    all = all.OrderBy(o => o.AnimalsNumber);
                    break;
            }

            return all;
        }
    }
}
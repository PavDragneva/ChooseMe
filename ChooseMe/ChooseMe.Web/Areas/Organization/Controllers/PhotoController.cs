﻿namespace ChooseMe.Web.Areas.Organization.Controllers
{
    using ChooseMe.Models;
    using Models.Animal;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class PhotoController:Controller
    {
        private IPhotoService photos;
        private IAnimalService animal;

        public PhotoController(IPhotoService photos, IAnimalService animal)
        {
            this.photos = photos;
            this.animal = animal;
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int id)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var folderPath = Server.MapPath("~/Images/Upload/" + id);
                    string imagePath = folderPath + "/" + fileName;

                    if (!Directory.Exists(folderPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderPath);
                    }

                    file.SaveAs(imagePath);

                    var photo = new Photo();
                    photo.Address = "/Images/Upload/" + id + "/" + fileName;
                    photo.AnimalId = id;
                    photo.Animal = this.animal.GetById(id).FirstOrDefault();
                    this.photos.AddNew(photo);

                    return this.RedirectToAction("Details", "Animals", new { area = "", id = id });
                }
            }
            return this.View();
        }
    }
}
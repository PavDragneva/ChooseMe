﻿namespace ChooseMe.Services.Contracts
{
    using ChooseMe.Models;
    using System.Linq;

    public interface IAdoptionFormService
    {
        IQueryable<AdoptionForm> GetAllByAnimalId(int id);

        AdoptionForm GetById(int id);

        AdoptionForm AddNew(AdoptionForm form);

        void DeleteAdoptionForm(int id);
    }
}
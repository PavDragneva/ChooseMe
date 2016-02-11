﻿namespace ChooseMe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Organization: User
    {
        private ICollection<Animal> animals;

        private ICollection<VolunteerForm> volunteerForms;

        public Organization()
        {
            this.animals = new HashSet<Animal>();
            this.volunteerForms = new HashSet<VolunteerForm>();
        }

        [Required]
        [MinLength(5, ErrorMessage="Organization name must be at leasr 5 characters long")]
        public string Name { get; set; }

        public DateTime ActiveSince { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsLookingForVolunteers { get; set; }

        public virtual ICollection<Animal> Animals
        {
            get { return this.animals; }
            set { this.animals = value; }
        }

        public virtual ICollection<VolunteerForm> VolunteerForms
        {
            get { return this.volunteerForms; }
            set { this.volunteerForms = value; }
        }
    }
}
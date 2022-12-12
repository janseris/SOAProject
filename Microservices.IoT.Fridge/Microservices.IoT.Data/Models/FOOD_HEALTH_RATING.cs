﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.Models
{
    public partial class FOOD_HEALTH_RATING
    {
        public FOOD_HEALTH_RATING()
        {
            FOOD_TYPE = new HashSet<FOOD_TYPE>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("HealthRating")]
        public virtual ICollection<FOOD_TYPE> FOOD_TYPE { get; set; }
    }
}
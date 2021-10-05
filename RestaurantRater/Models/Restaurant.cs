﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace RestaurantRater.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();


        public double Rating
        {
            get
            {
                double totalAverageRating = 0;

                //add all ratings
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //get average from total
                return Ratings.Count > 0
                    ? Math.Round(totalAverageRating / Ratings.Count, 2) // if ratings.count > 0
                    : 0;  // if ratings.count < 0
            }
        }

        public bool IsRecommended
        {
            get
            {
                return Rating > 8;
            }
        }

        //average food rating

        //average clean rating

        //average Environment rating
    }
}
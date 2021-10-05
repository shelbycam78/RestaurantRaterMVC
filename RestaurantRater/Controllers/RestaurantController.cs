﻿using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //POST (create method)
        //api/Restaurant
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant([FromBody] Restaurant model)
        {

            if (model is null)
            {
                return BadRequest("Your Request body cannot be empty.");
            }
            //if the model is valid...
            if (ModelState.IsValid)
            {
                //store the model in the database
                _context.Restaurants.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("your restaurant was created!");
            }
            //the model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }

        //GET ALL
        //api/Restaurant
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }


        //GET BY ID
        //api/Restaurant/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound();

        }


        //PUT (update method)
        //api/Restaurant/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            //check the ids to see if they match
            if (id != updatedRestaurant?.Id)
            {
                return BadRequest("ids do not match.");

            }
            //check the ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //find restaurant in the database
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);


            //if the restaunt doesn't exist, then do something
            if (restaurant is null)
            {
                return NotFound();
            }
            //update the properties
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;
            

            //save the changes
            await _context.SaveChangesAsync();

            return Ok("the restaurant was updated!");

        }

        //delete (delete)
        //api/Restaurant/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return NotFound();

            }
            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The restaurant was deleted");

            }

            return InternalServerError();
        }


    }

}

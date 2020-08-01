using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface IMealDAO
    {
        /// <summary>
        /// Adds a meal to database
        /// </summary>
        /// <param name="meal"></param>
        /// <returns>Meal Object</returns>
        public Meal AddMeal(Meal meal);

        /// <summary>
        /// Lists all the meals by the session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <returns>List<Meal> Object</returns>
        public List<Meal> GetAllMealsBySession(int sessionId, int careTakerId);
        /// <summary>
        /// Gets a meal by id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <param name="mealId"></param>
        /// <returns>Meal Object</returns>
        public Meal GetAMealBySession(int sessionId, int careTakerId, int mealId);
        /// <summary>
        /// Updates a meal
        /// </summary>
        /// <param name="meal"></param>
        /// <param name="careTakerId"></param>
        /// <returns>Meal Object</returns>
        public Meal UpdateMeal(Meal meal, int careTakerId);
    }
}

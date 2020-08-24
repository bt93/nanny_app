using NannyApi.Models;
using System.Collections.Generic;

namespace NannyApi.DAL
{
    public interface ICareTakerDAO
    {
        /// <summary>
        /// Gets all the caretakers in the Database
        /// </summary>
        /// <returns>IList of CareTaker objects</returns>
        public IList<CareTaker> GetAllCareTakers();
        /// <summary>
        /// Gets a single caretaker by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CareTaker Object</returns>
        public CareTaker GetCareTakerById(int id);
        /// <summary>
        /// Adds a caretaker to the caretaker list
        /// </summary>
        /// <param name="careTaker"></param>
        /// <returns>Intiger of rows affected</returns>
        public CareTaker AddCareTaker(CareTaker careTaker);

        /// <summary>
        /// Updates a caretaker
        /// </summary>
        /// <param name="careTaker"></param>
        /// <returns>Caretaker Object</returns>
        public CareTaker UpdateCareTaker(CareTaker careTaker);
        /// <summary>
        /// Deletes a caretaker
        /// </summary>
        /// <param name="careTaker"></param>
        /// <returns>Bool</returns>
        public void DeleteCareTaker(CareTaker careTaker);
        /// <summary>
        /// Gets a caretaker by their email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Caretaker</returns>
        public CareTaker GetCareTakerByEmail(string email);
        /// <summary>
        /// Allows the user to update their password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="careTakerId"></param>
        /// <returns>Bool</returns>
        bool UpdatePassword(string password, int careTakerId);
    }
}

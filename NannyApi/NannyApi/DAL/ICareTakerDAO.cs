using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// Gets a single caretaker by first and last name
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>CareTaker Object</returns>
        public CareTaker GetCareTakerByName(string firstName, string lastName);
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
        public void AddCareTaker(CareTaker careTaker);
    }
}

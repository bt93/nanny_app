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

        public CareTaker UpdateCareTaker(CareTaker careTaker);

        public bool DeleteCareTaker(int caretakerId);
        public CareTaker GetCareTakerByEmail(string email);
    }
}

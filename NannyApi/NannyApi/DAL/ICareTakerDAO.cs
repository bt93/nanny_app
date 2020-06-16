using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NannyApi.DAL
{
    interface ICareTakerDAO
    {
        /// <summary>
        /// Gets all the caretakers
        /// </summary>
        /// <returns>IList<CareTaker></returns>
        IList<CareTaker> GetCareTakers();
    }
}

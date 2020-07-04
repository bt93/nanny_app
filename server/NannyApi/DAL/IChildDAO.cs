using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface IChildDAO
    {
        /// <summary>
        /// Returns a list of children
        /// </summary>
        /// <returns>List<Child> Objects</returns>
        List<Child> GetChildren(int careTakerId);
    }
}

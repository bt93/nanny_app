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
        /// Creates a new child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        Child AddChild(Child child);

        /// <summary>
        /// Returns a child by its id
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="careTakerId"></param>
        /// <returns></returns>
        Child GetChildById(int childId, int careTakerId);

        /// <summary>
        /// Returns a list of children
        /// </summary>
        /// <returns>List<Child> Objects</returns>
        List<Child> GetChildren(int careTakerId);
    }
}

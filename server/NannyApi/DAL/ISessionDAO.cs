using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface ISessionDAO
    {
        Session CreateNewSession(Session session, int caretakerId, int childId);
    }
}

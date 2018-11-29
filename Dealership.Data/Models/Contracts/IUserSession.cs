using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Data.Models.Contracts
{
    public interface IUserSession
    {
        User CurrentUser { get; set; }
    }
}

using KN_Order_Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_Order_Storage.Logic.Interfaces
{
    public interface IOptionsProvider
    {
        ClientSourceOption GetClientSourceOption();

        //AreaOption GetAreaOption();
    }
}
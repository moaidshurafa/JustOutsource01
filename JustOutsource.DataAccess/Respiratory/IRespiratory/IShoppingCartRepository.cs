﻿using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory.IRespiratory
{
    public interface IShoppingCartRepository : IRespiratory<ShoppingCart>
    {
        void Update(ShoppingCart obj);

    }
}
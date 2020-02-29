using OngTDD.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OngTDD.Services
{
    public class PurchaseBeverage
    {
        public double Calculate(double customerMoney, double totalAmountOfBeverage)
        {
            return customerMoney - totalAmountOfBeverage;
        }
    }
}

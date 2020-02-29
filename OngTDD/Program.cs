using OngTDD.Models;
using OngTDD.Services;
using System;
using System.Collections.Generic;

namespace OngTDD
{
    class Program
    {
        private static readonly List<int> _moneyAcceptedWholeNumber = new List<int> { 100, 50, 20, 10, 5, 1};
        private static readonly List<double> _moneyAcceptedDecimal = new List<double> { 0.5, 0.25 };
        

        static void Main(string[] args)
        {
            DisplayMessage displayMessage = new DisplayMessage();
            displayMessage.OpenVendoMachine();
        }
    }
}

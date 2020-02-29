using System;
using System.Collections.Generic;
using System.Text;

namespace OngTDD.Services
{
    public class InsertMoney
    {
        public List<double> GetAcceptableMoney()
        {
            List<double> moneyList = new List<double>() { 100, 50, 20, 10, 5, 1, 0.50, 0.25 };
            return moneyList;
        }

        public List<double> GetAcceptablePesos()
        {
            List<double> pesoList = new List<double>() { 100, 50, 20, 10, 5, 1};
            return pesoList;
        }

        public List<double> GetAcceptableCents()
        {
            List<double> centList = new List<double>() { .50, .25 };
            return centList;
        }

        public bool ValidateInsertedMoney(double amount)
        {
            var peso = (int)amount;
            var cents = amount - peso;

            return ValidatePeso(peso) && ValidateCent(cents);
        }

        public bool ValidatePeso(int peso)
        {
            if (peso == 0)
            {
                return true;
            }

            foreach (var acceptedPeso in GetAcceptablePesos())
            {
                if (peso / acceptedPeso == 1)
                {
                    return true;
                }
                continue;
            }
            return false;
        }

        public bool ValidateCent(double cent)
        {
            if (cent == 0.0)
            {
                return true;
            }

            foreach (var acceptedCent in GetAcceptableCents())
            {
                if (cent / acceptedCent == 1)
                {
                    return true;
                }
                continue;
            }
            return false;
        }
    }
}

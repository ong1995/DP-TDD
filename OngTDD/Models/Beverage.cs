using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OngTDD.Models
{
    public class Beverage
    {
        public int BeverageId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<Beverage> GetAvailableBeverages()
        {
            List<Beverage> beverages = new List<Beverage>()
            {
                new Beverage() { BeverageId = 1, Name = "Coke\t", Price =  25},
                new Beverage() { BeverageId = 2, Name = "Pepsi\t", Price =  35},
                new Beverage() { BeverageId = 3, Name = "Soda\t", Price =  45},
                new Beverage() { BeverageId = 4, Name = "Chocolate Bar", Price =  20.25},
                new Beverage() { BeverageId = 5, Name = "Chewing Gum", Price =  10.50},
                new Beverage() { BeverageId = 6, Name = "Bottled Water", Price =  15}
            };
            return beverages;
        }
        public Beverage AddCartBeverage(int beverageId)
        {
            List<Beverage> beverages = GetAvailableBeverages();
            return beverages.Where(x => x.BeverageId == beverageId).FirstOrDefault();
        }

        public bool ValidateIfBeverageExist(int beverageId)
        {
            List<Beverage> beverages = GetAvailableBeverages();
            return beverages.Where(x => x.BeverageId == beverageId).ToList().Count > 0;
        }
    }

    public enum BeveragesEnum
    {
        Coke = 1,
        Pepsi = 2,
        Soda = 3,
        ChocolateBar = 4,
        ChewingGum = 5,
        BottledWater = 6
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OngTDD.Models
{
    public class Customer
    {
        public List<Beverage> Beverages { get; set; }
        public List<double> Money { get; set; }

        public double GetTotalAmount(Customer customer)
        {
            return customer.Money?.Sum() ?? 0;
        }

        public string GetAllBeverageNameAdded(Customer customer)
        {
            var names = customer.Beverages.Select(x => x.Name.Replace("\t", "")).ToList();
            return string.Join(", ", names);
        }

        public double GetAllBeveragePriceAdded(Customer customer)
        {
            var price = customer.Beverages.Select(x => x.Price).ToList();
            return price.Sum();
        }
    }

    public class CustomerAction
    {
        public string Name { get; set; }
        public int Position { get; set; }

        public List<CustomerAction> GetActions()
        {
            List<CustomerAction> customerActions = new List<CustomerAction>()
            {
                new CustomerAction() { Name = "Insert Money", Position = 1 },
                new CustomerAction() { Name = "Purchase Beverage", Position = 2 },
                new CustomerAction() { Name = "Cancel Order", Position = 3 }
            };
            return customerActions;
        }
    }

    public enum CustomerActionEnum
    {
        InsertMoney = 1,
        PurchaseBeverage = 2,
        CancelOrder = 3
    }
}

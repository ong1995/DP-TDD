using OngTDD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OngTDD.Services
{
    public class DisplayMessage
    {
        private readonly InsertMoney _insertMoney = new Services.InsertMoney();
        private readonly PurchaseBeverage _purchaseBeverage = new Services.PurchaseBeverage();
        Customer _customer = new Customer();
        Beverage _beverage = new Beverage();
        CustomerAction _customerAction = new CustomerAction();


        public void OpenVendoMachine()
        {
            _customer.Money = new List<double>();
            _customer.Beverages = new List<Beverage>();
            Welcome(_beverage.GetAvailableBeverages());
            CustomerTotalAmount(_customer);
            CustomerOptions(_customerAction.GetActions(), _customer);
            Console.ReadLine();
        }


        public void Welcome(List<Beverage> beverages)
        {
            Console.Clear();
            Console.WriteLine("======== Welcome to JP's Vendo Machine ========");
            ListOfBeverages(beverages);
        }

        public void ListOfBeverages(List<Beverage> beverages)
        {
            Console.WriteLine("Here are the available products: \n");
            Console.WriteLine("\tCode ID \tBeverage \t\tPrice \n");
            foreach (var beverage in beverages)
            {
                Console.WriteLine($"\t{beverage.BeverageId} \t\t{beverage.Name} \t\tP{beverage.Price}");
            }
        }

        public void CustomerOptions(List<CustomerAction> customerActions, Customer customer)
        {
            Console.WriteLine("\nPlease select: \n");
            foreach (var customerAction in customerActions.OrderBy(x=>x.Position))
            {
                if(_customer.GetTotalAmount(customer) < 1 && (customerAction.Position == (int)CustomerActionEnum.PurchaseBeverage || customerAction.Position == (int)CustomerActionEnum.CancelOrder))
                {
                    continue;
                }
                Console.WriteLine($"\t [{customerAction.Position}] {customerAction.Name}");
            }
            SelectAction();
        }

        public void SelectAction()
        {
            Console.Write("\n Please enter the number you want to do:  ");
            var selectedAction = Console.ReadLine();
            int.TryParse(selectedAction, out int action);
            ExecuteAction(action);
        }
        
        public void CustomerTotalAmount(Customer customer, bool includeTransaction = false)
        {
            var amount = _customer.GetTotalAmount(customer);
            if (includeTransaction)
            {
                Console.WriteLine($"\nYour transaction history : P{string.Join(", ", customer.Money)}");
            }
            if (amount > 0)
            {
                Console.WriteLine($"\nYour total money : P{amount}\n");
            }
        }

        public string ExecuteAction(int action)
        {
            switch (action)
            {
                case (int)CustomerActionEnum.InsertMoney:
                    InsertMoneyMessage();
                    break;
                case (int)CustomerActionEnum.PurchaseBeverage:
                    OrderBeverages();
                    break;
                case (int)CustomerActionEnum.CancelOrder:
                    CancelPurchase();
                    break;
                default:
                    Console.Write("\n Input number is invalid. Please try again.");
                    SelectAction();
                    break;
            }
            return string.Empty;
        }

        public void InsertMoneyMessage()
        {
            List<double> moneyList = _insertMoney.GetAcceptableMoney();
            Console.Write($"\n These are the acceptable money: {string.Join(", ", moneyList)}");
            Console.Write("\n NOTE: This vendo machine can accept cents at the same time. (e.g: 1.25, 2.50 ...etc)\n");
            InsertMoney();
        }

        public void InsertMoney(bool isBecauseInsufficent=false)
        {
            Console.Write("\n Please enter the amount:  ");
            var insertedAmount = Console.ReadLine();
            double.TryParse(insertedAmount, out double amount);
            var isAmountValid = _insertMoney.ValidateInsertedMoney(amount);

            if (isAmountValid)
            {
                _customer.Money.Add(amount);
                CustomerTotalAmount(_customer, true);
                Console.Write("\n Do you want to insert more money? [press y for yes/press any to cancel]  ");
                var tryAgain = Console.ReadLine();
                if (tryAgain == "y")
                {
                    InsertMoney();
                }

                // must not go to welcome message if you just add money for insufficent fund
                if (!isBecauseInsufficent)
                {
                    Welcome(_beverage.GetAvailableBeverages());
                    CustomerTotalAmount(_customer);
                    CustomerOptions(_customerAction.GetActions(), _customer);
                }
            }
            else
            {
                Console.Write("\n The amount was invalid: ");
                InsertMoney();
            }
        }

        public void OrderBeverages()
        {
            Welcome(_beverage.GetAvailableBeverages());
            CustomerTotalAmount(_customer);
            Console.Write("\n What's your order: ");
            var selectedBeverage = Console.ReadLine();
            int.TryParse(selectedBeverage, out int beverageId);
            AddCartBeverage(beverageId);
        }

        public void AddCartBeverage(int beverageId)
        {
            if (_beverage.ValidateIfBeverageExist(beverageId))
            {
                _customer.Beverages.Add(_beverage.AddCartBeverage(beverageId));
                GetCustomerAddedCart(_customer);
                Console.Write("\n Added to cart. Want to add more?  [press y for yes/press any to cancel] ");
                var tryAgain = Console.ReadLine();
                if (tryAgain == "y")
                {

                    OrderBeverages();
                }
                else
                {
                    CalculateTheOrder(_customer.GetTotalAmount(_customer), _customer.GetAllBeveragePriceAdded(_customer));
                }
            }
            else
            {
                Console.Write("\n Your order is not on the list: ");
                OrderBeverages();
            }
        }

        public void GetCustomerAddedCart(Customer customer)
        {

            Console.Write($"\n Order: {_customer.GetAllBeverageNameAdded(customer)} - Total: P{_customer.GetAllBeveragePriceAdded(customer)} \n");
        }

        public void CalculateTheOrder(double customerMoney, double totalAmountOfBeverage)
        {
            var amount = _purchaseBeverage.Calculate(customerMoney, totalAmountOfBeverage);
            if (amount < 0)
            {
                Console.Write($"\n Your money is insufficient P{-(amount)} to by the beverage. Please add money [press y for yes] or Cancel  [press any to cancel]?");
                var answer = Console.ReadLine();
                if (answer == "y")
                {
                    InsertMoney(true);
                    CalculateTheOrder(_customer.GetTotalAmount(_customer), _customer.GetAllBeveragePriceAdded(_customer));
                }
                else
                {
                    CancelPurchase();
                }
            }
            else
            {
                SuccessPurchase(amount);
            }
        }

        public void SuccessPurchase(double change)
        {
            Console.Write($"\n****Thank you for buying! FROM: JP Boy****");
            Console.Write($"\nHere's your reciept");
            Console.WriteLine($"\nMoney inserted history : P{string.Join(", P", _customer.Money)}");
            Console.WriteLine($"Your Money : P{_customer.GetTotalAmount(_customer)}");
            Console.Write($"\n Ordered: {_customer.GetAllBeverageNameAdded(_customer)} - Total: P{_customer.GetAllBeveragePriceAdded(_customer)} \n");
            Console.WriteLine($"Change : P{change}");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void CancelPurchase()
        {
            Console.Write($"\n****You cancel your order****");
            Console.Write($"\nHere's your refund");
            Console.WriteLine($"\nMoney inserted history : P{string.Join(", P", _customer.Money)}");
            Console.WriteLine($"Your Money : P{_customer.GetTotalAmount(_customer)}");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Fase3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Create a variable per each bill up to 500 and one to store the change
             2 arrays, one for the menu (5 options) and other that will 
             contain the price of each plate*/
            var b5 = 0;
            var b10 = 0;
            var b20 = 0;
            var b50 = 0;
            var b100 = 0;
            var b200 = 0;
            var b500 = 0;
            var change = 0.0;
            var menu = new string[5];
            var price = new decimal[5];

            //Add the name and price of each dish to the corresponding array
            var tuple = DishesAndPrices(menu.Length);
            menu = tuple.Item1;
            price = tuple.Item2;

            //Show each plate with its price
            ShowMenu(menu, price);

            List<string> orderList = new List<string>();

            /*Get the order and find out if the client is ready or wants to add 
             * something else*/
            var keepOrdering = 1;
            while (keepOrdering == 1)
            {
                orderList.Add(GetOrder());
                //Ask if ready or not
                Console.WriteLine("\nWould you like to order something else? (1=yes 0=no)");
                keepOrdering = int.Parse(Console.ReadLine());
            }

            //Sum the dish prices and present the bill
            GetBill(orderList, menu, price);
        }

        private static void GetBill(List<string> aOrder, string[] aMenu, decimal[] aPrice)
        {
            decimal bill = 0;
            int i;

            foreach (var item in aOrder)
            {
                if (aMenu.Contains(item))
                {
                    i = Array.IndexOf(aMenu, item);
                    bill += aPrice[i];
                }
                else
                {
                    Console.WriteLine($"Sorry, but our menu does not have {item}");
                }
            }
            Console.WriteLine($"\nYour total is {bill}");
        }
        private static string GetOrder()
        {
            Console.WriteLine("\nPlease, place your order:");
            return Console.ReadLine();
        }
        private static void ShowMenu(string[] aMenu, decimal[] aPrice)
        {
            Console.WriteLine("\nOur menu options are:");
            for (int i = 0; i < aMenu.Length; i++)
            {
                Console.WriteLine($"{aMenu[i]}: {aPrice[i]}");
            }
        }

        private static Tuple<string[], decimal[]> DishesAndPrices(int aSize)
        {
            var aMenu = new string[aSize];
            var aPrice = new decimal[aSize];

            for (int i = 0; i < aSize; i++)
            {
                Console.WriteLine($"Enter menu dish number {i + 1}:");
                aMenu[i] = Console.ReadLine();

                Console.WriteLine($"Enter the price for menu dish number {i + 1}:");
                while (!(decimal.TryParse(Console.ReadLine(), out aPrice[i])))
                {
                    Console.WriteLine($"Error: The value entered is not a price, please, enter a proper price for menu dish number {i + 1}:");
                }
            }
            return new Tuple<string[], decimal[]>(aMenu, aPrice);
        }
    }
}
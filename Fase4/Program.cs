using System;
using System.Collections.Generic;
using System.Linq;

namespace Fase4
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
            var price = new int[5];

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
            var totalBill = GetBill(orderList, menu, price);

            #region Calculate the bills and the change

            int billRounded;
            bool isRoundedUp;

            //Round the bill to the nearest 5
            billRounded = 5 * (int)Math.Round(totalBill / 5.0);

            //Find out whether it was rounded up or down
            isRoundedUp = billRounded > totalBill;

            if (isRoundedUp) // To give back
            {
                change = billRounded - totalBill;
            }
            else // To add to the list of bills
            {
                change = totalBill - billRounded;
            }

            //Store billType => amount
            var cashIn = new Dictionary<string, int>();

            //Iterate the bill type over the remainder
            var stillRemainder = true;
            while (stillRemainder)
            {
                var billList = billAmount(billRounded, out int remainder);
                cashIn.Add(billList.Item1, billList.Item2);
                stillRemainder = remainder > 0;
                if (stillRemainder)
                {
                    billRounded = remainder;
                }
            }
            
            //Offer a way to pay
            Console.WriteLine("\nYou can pay as follows: \n ");
            foreach (var kvp in cashIn)
            {
                Console.WriteLine($"Bills of {kvp.Key} euro: {kvp.Value}");
            }

            if (change > 0)
            {
                if (isRoundedUp)
                {
                    Console.WriteLine($"\nYour change is {change} euro");
                }
                else
                {
                    Console.WriteLine($"with {change} euro");
                }
            }

            #endregion
        }

        private static Tuple<string, int> billAmount(int aBill, out int remainder)
        {
            var result = 0;
            var billType = "none";
            var base5 = (aBill / 5);
            remainder = 0;

            if (base5 >= 100) //Suitable for 500€ bills
            {
                result = Math.DivRem(aBill, 500, out remainder);
                billType = "500";
            }
            else if (base5 >= 40) //Suitable for 200€ bills
            {
                result = Math.DivRem(aBill, 200, out remainder);
                billType = "200";
            }
            else if (base5 >= 20) //Suitable for 100€ bills
            {
                result = Math.DivRem(aBill, 100, out remainder);
                billType = "100";
            }
            else if (base5 >= 10) //Suitable for 50€ bills
            {
                result = Math.DivRem(aBill, 50, out remainder);
                billType = "50";
            }
            else if (base5 >= 4) //Suitable for 20€ bills
            {
                result = Math.DivRem(aBill, 20, out remainder);
                billType = "20";
            }
            else if (base5 >= 2) //Suitable for 10€ bills
            {
                result = Math.DivRem(aBill, 10, out remainder);
                billType = "10";
            }
            else if (base5 >= 1) //Suitable for 5€ bills
            {
                result = Math.DivRem(aBill, 5, out remainder);
                billType = "5";
            }
            return new Tuple<string, int>(billType, result);
        }


        private static int GetBill(List<string> aOrder, string[] aMenu, int[] aPrice)
        {
            int bill = 0;
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
            return bill;
        }
        private static string GetOrder()
        {
            Console.WriteLine("\nPlease, place your order:");
            return Console.ReadLine();
        }
        private static void ShowMenu(string[] aMenu, int[] aPrice)
        {
            Console.WriteLine("\nOur menu options are:");
            for (int i = 0; i < aMenu.Length; i++)
            {
                Console.WriteLine($"{aMenu[i]}: {aPrice[i]}");
            }
        }

        private static Tuple<string[], int[]> DishesAndPrices(int aSize)
        {
            var aMenu = new string[aSize];
            var aPrice = new int[aSize];

            for (int i = 0; i < aSize; i++)
            {
                Console.WriteLine($"Enter menu dish number {i + 1}:");
                aMenu[i] = Console.ReadLine();

                Console.WriteLine($"Enter the price for menu dish number {i + 1}:");
                while (!(int.TryParse(Console.ReadLine(), out aPrice[i])))
                {
                    Console.WriteLine($"Error: The value entered is not a price, please, enter a proper price for menu dish number {i + 1}:");
                }
            }
            return new Tuple<string[], int[]>(aMenu, aPrice);
        }
    }
}

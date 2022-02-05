using CM.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> commandsList = new List<string> { Commands.CreateCampaign, Commands.CreateOrder, Commands.CreateProduct, Commands.GetCampaign, Commands.GetProduct, Commands.IncreaseTime };

            Startup.ConfigureServices();
            Console.Clear();
            Console.WriteLine(Engine.GetTime());
            Console.WriteLine("Commands:");
            Console.WriteLine("1) create_product PRODUCTCODE PRICE STOCK (Creates product in the system with given product information.)");
            Console.WriteLine("2) get_product_info PRODUCTCODE (Prints product information for given product code.)");
            Console.WriteLine("3) create_order PRODUCTCODE QUANTITY (Creates order in the system with given information.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("create_order command changes the discount rate of product in active campaign via: PMLIMIT x (1 - ((Total Sales Count) / Target))))");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("4) create_campaign NAME PRODUCTCODE DURATION PMLIMIT TARGETSALESCOUNT (Creates campaign in your system with given information)");
            Console.WriteLine("5) get_campaign_info NAME (Prints campaign information for given campaign name)");
            Console.WriteLine("6) increase_time HOUR (Increases time in the system.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("increase_time command changes the discount rate of active Campaigns via  PMLIMIT x (1 - ((Total Sales Count) / Target)))");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("7) exit (Exits the system.)");
            Console.Write("\r\nYour Command: ");
            string commandLine = Console.ReadLine();

            while (commandLine != "exit")
            {
                var items = commandLine.TrimStart().TrimEnd().Split(" ").ToArray();
                var command = items[0];
                if (commandsList.Contains(command))
                {
                    try
                    {
                        switch (command)
                        {
                            case Commands.CreateProduct:
                                if (items.Length != 4)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.CreateProduct(items[1].ToString(), Convert.ToDecimal(items[2]), Convert.ToInt16(items[3]));
                                    Console.WriteLine(res);
                                }
                                break;
                            case Commands.GetProduct:
                                if (items.Length != 2)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.GetProduct(items[1].ToString());
                                    Console.WriteLine(res);
                                }
                                break;
                            case Commands.CreateOrder:
                                if (items.Length != 3)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.CreateOrder(items[1].ToString(), Convert.ToInt16(items[2]));
                                    Console.WriteLine(res);
                                }
                                break;
                            case Commands.CreateCampaign:
                                if (items.Length != 6)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.CreateCampaign(items[1].ToString(), items[2].ToString(), Convert.ToInt16(items[3]), Convert.ToDecimal(items[4]), Convert.ToInt16(items[5]));
                                    Console.WriteLine(res);
                                }
                                break;
                            case Commands.GetCampaign:
                                if (items.Length != 2)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.GetCampaignInfo(items[1].ToString());
                                    Console.WriteLine(res);
                                }
                                break;
                            case Commands.IncreaseTime:
                                if (items.Length != 2)
                                    throw new Exception("Invalid Parameter");
                                else
                                {
                                    var res = Engine.AddHour(Convert.ToInt16(items[1]));
                                    Console.WriteLine(res);
                                }
                                break;
                        }
                        Console.Write("\r\nYour Command: ");
                        commandLine = Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: Invalid Parameter Type or Amount Used. Please Check Commands!");
                        Console.Write("\r\nYour Command: ");
                        commandLine = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid Command!");
                    Console.Write("\r\nYour Command: ");
                    commandLine = Console.ReadLine();
                }
            }
            Startup.DisposeServices();
        }

    }
}

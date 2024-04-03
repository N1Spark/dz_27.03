using dz_27._03.Model;
using Dapper;
using System;
namespace dz_27._03
{
    public class Program 
    {
        static void Main(string[] args) 
        {
            using (var repository = new Repository())
            {
                Console.WriteLine("1. Подключиться, 2. Отключиться");
                var tmp = Console.ReadLine();
                switch (tmp)
                {
                    case "1": 
                    {
                            using (var connection = repository.OpenConnection()) ;
                        break;
                    }
                    case "2":
                        {
                            return;
                        }
                }
                while (true)
                {
                    Menu();
                }
            }
                
        }
        public static void Menu() 
        {
            Console.Clear();
            Console.WriteLine("1. Вся информация из таблицы с овощами и фруктами");
            Console.WriteLine("2. Все названия овощей и фруктов");
            Console.WriteLine("3. Все цвета");
            Console.WriteLine("4. Максимальная калорийность");
            Console.WriteLine("5. Минимальная калорийность");
            Console.WriteLine("6. Средняя калорийность");
            Console.WriteLine("7. Количество овощей");
            Console.WriteLine("8. Количество фруктов");
            Console.WriteLine("9. Количество овощей и фруктов заданного цвета");
            Console.WriteLine("10. Количество овощей фруктов каждого цвета");
            Console.WriteLine("11. Овощи и фрукты с калорийностью ниже указанной");
            Console.WriteLine("12. Овощи и фрукты с калорийностью выше указанной");
            Console.WriteLine("13. Овощи и фрукты с калорийностью в указанном диапазоне");
            Console.WriteLine("14. Все овощи и фрукты, у которых цвет желтый или красный");
            Console.Write("Введите действие: ");
            var tmp = Console.ReadLine();
            switch (tmp) 
            {
                case "1":
                    {
                        DisplayAllProducts();
                        break;
                    }
                case "2":
                    {
                        ShowNameProducts();
                        break;
                    }
                case "3":
                    {
                        ShowColorProducts();
                        break;
                    }
                case "4":
                    {
                        ShowMaxCalories();
                        break;
                    }
                case "5":
                    {
                        ShowMinCalories();
                        break;
                    }
                case "6": 
                    {
                        ShowAvgCalories();
                        break;
                            
                    }
                case "7": 
                    {
                        ShowCountVegetables();
                        break;
                    }
                case "8":
                    {
                        ShowCountFruits();
                        break;
                    }
                case "9":
                    {
                        ShowCountProductByColor();
                        break;
                    }
                case "10":
                    {
                        ShowCountByColor();
                        break;
                    }
                case "11":
                    {
                        ShowProductsBelowCalories();
                        break;
                    }
                case "13":
                    {
                        ShowProductInRange();
                        break;
                    }
                case "14": 
                    {
                        ShowProductsByColor();
                        break;
                    }
            }
        }

        public static void DisplayAllProducts()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connection = r.OpenConnection())
                {
                    var fruitsVegetables = connection.Query<Product>("SELECT * FROM Products");
                    foreach (var item in fruitsVegetables)
                    {
                        Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
                    }
                }
            }
            Console.ReadKey();
        }

        public static void ShowNameProducts()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connection = r.OpenConnection())
                {
                    var showNAme = connection.Query<Product>("Select Name from Products");
                    foreach (var n in showNAme)
                    {
                        Console.WriteLine($"Name:{n.Name}");
                    }
                }
            }
            Console.ReadKey();
        }

        public static void ShowColorProducts() 
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connection = r.OpenConnection())
                {
                    var showColor = connection.Query<Product>("Select [Name], [Color] from Products");
                    foreach (var i in showColor)
                    {
                        Console.WriteLine($"Name:{i.Name}\tColor:{i.Color}");
                    }
                }
            }
            Console.ReadKey();
        }
        public static void ShowMaxCalories()
        {
            Console.Clear();
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var maxColoria = connect.QuerySingleOrDefault<int>("SELECT MAX(Calories) FROM Products");

                if (maxColoria != null)
                {
                    Console.WriteLine($"Максимальная колорийность = {maxColoria}");
                }
              
            }
            Console.ReadKey();
        }
        public static void ShowMinCalories()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connection = r.OpenConnection())
                {
                    var minCaloria = connection.QuerySingleOrDefault<int>("SELECT MIN(Calories) FROM Products");
                    if (minCaloria!=null)
                    {
                         Console.WriteLine($"Min Calories = {minCaloria}");
                    }
                   
                }
            }
            Console.ReadKey();
        }

        public static void ShowAvgCalories()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connection = r.OpenConnection())
                {
                    var minCaloria = connection.QuerySingleOrDefault<int>("SELECT AVG(Calories) FROM Products");
                    if (minCaloria != null)
                    {
                        Console.WriteLine($"AVG caloria = {minCaloria}");
                    }

                }
            }
            Console.ReadKey();
        }
        public static void ShowCountVegetables()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connect = r.OpenConnection())
                {
                    var showCount = connect.QuerySingleOrDefault<int>("Select COUNT(*) from Products where Type = 'Vegetable'");
                    Console.WriteLine($"Количество овощей = {showCount}");
                }
            }
            Console.ReadKey();
        }
        public static void ShowCountFruits()
        {
            Console.Clear();
            using (Repository r = new Repository())
            {
                using (var connect = r.OpenConnection())
                {
                    var showCount = connect.QuerySingleOrDefault<int>("Select COUNT(*) from Products where Type = 'Fruit'");
                    Console.WriteLine($"Количество фруктов = {showCount}");
                }
            }
            Console.ReadKey();
        }
        public static void ShowCountProductByColor()
        {
            Console.Clear();
            Console.Write("Введите цвет: ");
            string color =  Console.ReadLine()!;
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var count = connect.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM Products WHERE Color = @Color", new { Color = color });
                Console.WriteLine($"Количество овощей и фруктов цвета {color}: {count}");
            }
            Console.ReadKey();
        }
        public static void ShowCountByColor()
        {
            Console.Clear();
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var counts = connect.Query<(string Color, int Count)>("SELECT Color, COUNT(*) AS Count FROM Products GROUP BY Color");

                foreach (var count in counts)
                {
                    Console.WriteLine($"Цвет: {count.Color}, Количество: {count.Count}");
                }
            }
            Console.ReadKey();
        }
        public static void ShowProductsBelowCalories()
        {
            Console.Clear();
            Console.Write("Введите кол-во калорий: ");
            int calories = Convert.ToInt32(Console.ReadLine());
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var items = connect.Query<Product>("SELECT * FROM Products WHERE Calories < @Calories", calories );
                foreach (var item in items)
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
            Console.ReadKey();
        }

        public static void ShowProductInRange()
        {
            Console.Clear();
            Console.Write("Введите min кол-во калорий: ");
            int minCalories = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите max кол-во калорий: ");
            int maxCalories = Convert.ToInt32(Console.ReadLine());
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var items = connect.Query<Product>("SELECT * FROM Products WHERE Calories BETWEEN @MinCalories AND @MaxCalories", (minCalories, maxCalories ));
                foreach (var item in items)
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
            Console.ReadKey();
        }
        public static void ShowProductsByColor()
        {
            Console.Clear();
            using (Repository r = new Repository())
            using (var connect = r.OpenConnection())
            {
                var items = connect.Query<Product>("SELECT * FROM Products WHERE Color IN ('Yellow', 'Red')");
                foreach (var item in items)
                    Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
            Console.ReadKey();
        }


    }
}

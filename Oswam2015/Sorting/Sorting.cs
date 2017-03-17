using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Oswam2015.Models;
using System.Data.SqlClient;

namespace Oswam2015.Sorting
{

    public class Shelf
    {
        public int ID { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public int maxWeight = 200;
        public int maxVolume = 200;
        public decimal? currentWeight { get; set; }
        public decimal? currentVolume { get; set; }
        public List<Item> itemList { get; set; }
    }

    public class Item
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal? Weight { get; set; } //lb
        public decimal? Volume { get; set; } //in inches^3
        public short? Quantity { get; set; }
    }

    public class Sorter
    {
        public static OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        public static List<Item> Inventory = new List<Item>();
        public static List<Shelf> Warehouse = new List<Shelf>();

        // Add 50 random guys into the inventory list 
        /*public static void LoadItems()
        {
            int i;
            Random random = new Random();

            for (i = 0; i < 50; i++)
            {
                Item next = new Item();
                next.ID = i;
                next.Name = Path.GetRandomFileName();
                next.Weight = random.Next(0, 20);
                next.Volume = random.Next(0, 20);
                next.Quantity = random.Next(0, 10);
                //System.Diagnostics.Debug.WriteLine(next.ID + " " + next.Name + " " + next.Weight);
                Inventory.Add(next);
            }
        }*/

        //add the actual inventory
        public static void LoadInventory()
        {
            System.Diagnostics.Debug.WriteLine("help");
            var productList = dataContext.GetInventoryProducts(null, null, 0, 0, 0, 0);
            var list = productList.ToList();
            foreach (var element in list)
            {
                Item next = new Item();
                next.ID = element.ID;
                next.Name = element.ItemName;
                next.Weight = element.Weight;
                next.Volume = (element.DimHeight * element.DimLength * element.DimWidth);
                next.Quantity = element.Quantity;
                //System.Diagnostics.Debug.WriteLine(next.ID + " " + next.Name + " " + next.Weight);
                Inventory.Add(next);
            }

        }

        static void SortAlphabetically()
        {
            bool swap;
            Item temp; //change this too

            do
            {
                swap = false;

                for (int index = 0; index < (Inventory.Count - 1); index++)
                {
                    if (string.Compare(Inventory[index].Name, Inventory[index + 1].Name) > 0)
                    {
                        //swap
                        temp = Inventory[index];
                        Inventory[index] = Inventory[index + 1];
                        Inventory[index + 1] = temp;

                        swap = true;
                    }
                }
            } while (swap == true);

            /*
            for(int i = 0; i < Inventory.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(i+" "+Inventory[i].Name+" "+ Inventory[i].Weight);
            }*/
        }

        public static void ShelfAlphabetically()
        {
            SortAlphabetically();

            int shelfID = 0;
            int invCounter = 0;

            shelfID++;

            Shelf shelf = new Shelf();
            shelf.ID = shelfID;
            shelf.itemList = new List<Item>();

            //while there are items in inventory
            do
            {
                //if there is an item here to look at
                if (Inventory[invCounter].Quantity > 0)
                {
                    //check incoming weight/volume
                    decimal? remainWeight = shelf.maxWeight - shelf.currentWeight;
                    decimal? remainVolume = shelf.maxVolume - shelf.currentVolume;
                    decimal? incomingWeight = Inventory[invCounter].Quantity * Inventory[invCounter].Weight;
                    decimal? incomingVolume = Inventory[invCounter].Quantity * Inventory[invCounter].Volume;

                    //if it fits
                    if (incomingWeight < remainWeight && incomingVolume < remainVolume)
                    {
                        //update the shelf
                        shelf.itemList.Add(Inventory[invCounter]);
                        shelf.currentWeight += Inventory[invCounter].Weight;
                        shelf.currentVolume += Inventory[invCounter].Volume;

                        //remove the item from inventory
                        Inventory.RemoveAt(invCounter);
                    }
                    else //we need a new shelf 
                    {
                        //add the working shelf
                        Warehouse.Add(shelf);
                        System.Diagnostics.Debug.WriteLine("-------------" + shelf.ID);
                        //begin making the next one
                        shelfID++;
                        shelf = new Shelf();
                        shelf.ID = shelfID;
                        shelf.itemList = new List<Item>();
                    }
                }
                else
                {
                    Inventory.RemoveAt(invCounter);
                }


            } while (Inventory.Count > 0);
        }

        static void SortVolume() //smallest volume and up
        {
            bool swap;
            Item temp; //change this too

            do
            {
                swap = false;

                for (int index = 0; index < (Inventory.Count - 1); index++)
                {
                    if (Inventory[index].Volume > Inventory[index + 1].Volume)
                    {
                        //swap
                        temp = Inventory[index];
                        Inventory[index] = Inventory[index + 1];
                        Inventory[index + 1] = temp;

                        swap = true;
                    }
                }
            } while (swap == true);


            /*for (int i = 0; i < Inventory.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(i + " " + Inventory[i].Name + " " + Inventory[i].Volume);
            }*/
        }

        static void ShelfVolume()
        {
            SortVolume();

            int shelfID = 0;
            int invCounter = 0;

            shelfID++;

            Shelf shelf = new Shelf();
            shelf.ID = shelfID;
            shelf.itemList = new List<Item>();

            //while there are items in inventory
            do
            {
                //if there is an item here to look at
                if (Inventory[invCounter].Quantity > 0)
                {
                    //check incoming weight/volume
                    decimal? remainWeight = shelf.maxWeight - shelf.currentWeight;
                    decimal? remainVolume = shelf.maxVolume - shelf.currentVolume;
                    decimal? incomingWeight = Inventory[invCounter].Quantity * Inventory[invCounter].Weight;
                    decimal? incomingVolume = Inventory[invCounter].Quantity * Inventory[invCounter].Volume;

                    //if it fits
                    if (incomingWeight < remainWeight && incomingVolume < remainVolume)
                    {
                        //update the shelf
                        shelf.itemList.Add(Inventory[invCounter]);
                        shelf.currentWeight += Inventory[invCounter].Weight;
                        shelf.currentVolume += Inventory[invCounter].Volume;

                        //remove the item from inventory
                        Inventory.RemoveAt(invCounter);
                    }
                    else //we need a new shelf 
                    {
                        //add the working shelf
                        Warehouse.Add(shelf);
                        
                        //begin making the next one
                        shelfID++;
                        shelf = new Shelf();
                        shelf.ID = shelfID;
                        shelf.itemList = new List<Item>();
                    }
                }
                else
                {
                    Inventory.RemoveAt(invCounter);
                }


            } while (Inventory.Count > 0);
        }

        public static void PrintWarehouse()
        {
            foreach (Shelf shelf in Warehouse)
            {
                System.Diagnostics.Debug.WriteLine("-------------" + shelf.ID);
                foreach (Item item in shelf.itemList)
                {
                    System.Diagnostics.Debug.WriteLine(item.Name + " - " + item.Quantity);
                }
            }
        }

        public static void PrintInventory()
        {
            int i = 0;
            foreach (Item item in Inventory)
            {
                if (i < 10)
                { System.Diagnostics.Debug.WriteLine(i+ " "+item.Name); i++; }
            }
        }

        public static void test()
        {
            LoadInventory();
            ShelfAlphabetically();
            //ShelfVolume();
            PrintWarehouse();
        }
        
        static void Main(string[] args)
        {
            LoadInventory();
            ShelfAlphabetically();
            //ShelfVolume();
            PrintWarehouse();
        }
    }
}

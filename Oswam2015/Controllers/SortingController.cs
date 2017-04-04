using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oswam2015.Models;

namespace Oswam2015.Controllers
{
    public class SortingController : Controller
    {
        public static OSWAM_DataEntities dataContext = new OSWAM_DataEntities();
        public static List<GetInventoryProducts_Result> Inventory = new List<GetInventoryProducts_Result>();
        public static List<LocalShelf> Warehouse = new List<LocalShelf>();

        // GET: Sorting
        public ActionResult Index()
        {
            int algorithmIndex = Convert.ToInt32(dataContext.GetPreferenceValue("SelectedSortingAlg").ToList().FirstOrDefault());

            System.Diagnostics.Debug.WriteLine("Sorting Called");

            LoadInventory();

            //convert saved preference to sorting method
            switch(algorithmIndex)
            {
                case 0:
                    ShelfAlphabetically();
                    break;
                case 1:
                    ShelfVolume();
                    break;
                default:
                    return RedirectToAction("Index","Settings");
            }

            PrintWarehouse();

            return View(); // change to redirect to floor grid after sorting when debugged
        }

        //add the actual inventory
        public static void LoadInventory()
        {
            System.Diagnostics.Debug.WriteLine("Loading From Database");
            var productList = dataContext.GetInventoryProducts(null, null, 0, 0, 0, 0);
            Inventory  = productList.ToList();

            /*foreach (var element in list)
            {
                Product next = new Product();
                next.ID = element.ID;
                next.ItemName = element.ItemName;
                next.Weight = element.Weight;
                next.Volume = element.Volume;
                next.Quantity = element.Quantity;
                //System.Diagnostics.Debug.WriteLine(next.ID + " " + next.Name + " " + next.Weight);
                Inventory.Add(next);
            }*/

        }

        static void SortAlphabetically()
        {
            System.Diagnostics.Debug.WriteLine("Sorting Alphabetically");
            bool swap;
            GetInventoryProducts_Result temp;

            do
            {
                swap = false;

                for (int index = 0; index < (Inventory.Count - 1); index++)
                {
                    if (string.Compare(Inventory[index].ItemName, Inventory[index + 1].ItemName) > 0)
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

        public static void QuickAlphabetically(List<GetInventoryProducts_Result> items, int left, int right)
        {
            int i = left, j = right;
            GetInventoryProducts_Result pivot = items[(left + right) / 2];

            while (i <= j)
            {
                while (string.Compare(items[i].ItemName, pivot.ItemName) < 0)
                {
                    i++;
                }

                while (string.Compare(items[j].ItemName, pivot.ItemName) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    GetInventoryProducts_Result temp = items[i];
                    items[i] = items[j];
                    items[j] = temp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickAlphabetically(items, left, j);
            }

            if (i < right)
            {
                QuickAlphabetically(items, i, right);
            }
        }

        public static void ShelfAlphabetically()
        {
            //SortAlphabetically();
            QuickAlphabetically(Inventory, 0, (Inventory.Count-1));

            int shelfID = 0;
            int invCounter = 0;

            shelfID++;

            LocalShelf shelf = new LocalShelf();
            shelf.ID = shelfID;
            shelf.itemList = new List<GetInventoryProducts_Result>();

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

                    System.Diagnostics.Debug.WriteLine(Inventory[invCounter].ID);
                    System.Diagnostics.Debug.WriteLine(Inventory[invCounter].Quantity);
                    System.Diagnostics.Debug.WriteLine(Inventory[invCounter].Volume);
                    System.Diagnostics.Debug.WriteLine(Inventory[invCounter].Weight);
                    System.Diagnostics.Debug.WriteLine(incomingWeight);
                    System.Diagnostics.Debug.WriteLine(remainWeight);
                    System.Diagnostics.Debug.WriteLine(incomingVolume);
                    System.Diagnostics.Debug.WriteLine(remainVolume);

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
                    else if(shelf.itemList.Count == 0)//if the shelf is empty but we still dont have enouhg room, split the quantities
                    {
                        int i = 0;
                        //figure out how to split it up
                        do
                        {
                            i++;
                            incomingWeight = i * Inventory[invCounter].Weight;
                            incomingVolume = i * Inventory[invCounter].Volume;
                        } while (incomingWeight < 200 || incomingVolume < 200);

                        i--; //this is the most we can have without overflowing

                        //update the shelf
                        int? remainder = Inventory[invCounter].Quantity - i;
                        Inventory[invCounter].Quantity = (short)i;
                        shelf.itemList.Add(Inventory[invCounter]);
                        shelf.currentWeight += Inventory[invCounter].Weight*i;
                        shelf.currentVolume += Inventory[invCounter].Volume*i;
                        //update the quantity
                        Inventory[invCounter].Quantity = (short)remainder;

                        //add the working shelf
                        Warehouse.Add(shelf);
                        System.Diagnostics.Debug.WriteLine("isit-------------" + shelf.ID);
                        //begin making the next one
                        shelfID++;
                        shelf = new LocalShelf();
                        shelf.ID = shelfID;
                        shelf.currentWeight = 0;
                        shelf.currentVolume = 0;
                        shelf.itemList = new List<GetInventoryProducts_Result>();

                    }
                    else //we need a new shelf 
                    {
                        //add the working shelf
                        Warehouse.Add(shelf);
                        System.Diagnostics.Debug.WriteLine("isit-------------" + shelf.ID);
                        //begin making the next one
                        shelfID++;
                        shelf = new LocalShelf();
                        shelf.ID = shelfID;
                        shelf.currentWeight = 0;
                        shelf.currentVolume = 0;
                        shelf.itemList = new List<GetInventoryProducts_Result>();
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
            System.Diagnostics.Debug.WriteLine("Sorting Volume");
            bool swap;
            GetInventoryProducts_Result temp; //change this too

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

        public static void QuickVolume(List<GetInventoryProducts_Result> items, int left, int right)
        {
            int i = left, j = right;
            GetInventoryProducts_Result pivot = items[(left + right) / 2];

            while (i <= j)
            {
                while (items[i].Volume < pivot.Volume)
                {
                    i++;
                }

                while (items[i].Volume > pivot.Volume)
                {
                    j--;
                }

                if (i <= j)
                {
                    GetInventoryProducts_Result temp = items[i];
                    items[i] = items[j];
                    items[j] = temp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                QuickVolume(items, left, j);
            }

            if (i < right)
            {
                QuickVolume(items, i, right);
            }
        }

        static void ShelfVolume()
        {
            SortVolume();
            //QuickVolume(Inventory, 0, (Inventory.Count - 1));

            int shelfID = 0;
            int invCounter = 0;

            shelfID++;

            LocalShelf shelf = new LocalShelf();
            shelf.ID = shelfID;
            shelf.itemList = new List<GetInventoryProducts_Result>();

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

                    else if (shelf.itemList.Count == 0)//if the shelf is empty but we still dont have enouhg room, split the quantities
                    {
                        int i = 0;
                        //figure out how to split it up
                        do
                        {
                            i++;
                            incomingWeight = i * Inventory[invCounter].Weight;
                            incomingVolume = i * Inventory[invCounter].Volume;
                        } while (incomingWeight < 200 || incomingVolume < 200);

                        i--; //this is the most we can have without overflowing

                        //update the shelf
                        int? remainder = Inventory[invCounter].Quantity - i;
                        Inventory[invCounter].Quantity = (short)i;
                        shelf.itemList.Add(Inventory[invCounter]);
                        shelf.currentWeight += Inventory[invCounter].Weight * i;
                        shelf.currentVolume += Inventory[invCounter].Volume * i;
                        //update the quantity
                        Inventory[invCounter].Quantity = (short)remainder;

                        //add the working shelf
                        Warehouse.Add(shelf);
                        System.Diagnostics.Debug.WriteLine("isit-------------" + shelf.ID);
                        //begin making the next one
                        shelfID++;
                        shelf = new LocalShelf();
                        shelf.ID = shelfID;
                        shelf.currentWeight = 0;
                        shelf.currentVolume = 0;
                        shelf.itemList = new List<GetInventoryProducts_Result>();
                    }

                    else //we need a new shelf 
                    {
                        //add the working shelf
                        Warehouse.Add(shelf);

                        //begin making the next one
                        shelfID++;
                        shelf = new LocalShelf();
                        shelf.ID = shelfID;
                        shelf.currentWeight = 0;
                        shelf.currentVolume = 0;
                        shelf.itemList = new List<GetInventoryProducts_Result>();
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
            foreach (LocalShelf shelf in Warehouse)
            {
                System.Diagnostics.Debug.WriteLine("-------------" + shelf.ID);
                foreach (GetInventoryProducts_Result item in shelf.itemList)
                {
                    System.Diagnostics.Debug.WriteLine(item.ItemName + " - " + item.Quantity);
                }
            }
        }

        public static void PrintInventory()
        {
            int i = 0;
            foreach (GetInventoryProducts_Result item in Inventory)
            {
                if (i < 10)
                { System.Diagnostics.Debug.WriteLine(i + " " + item.ItemName); i++; }
            }
        }

        public static void test()
        {
            System.Diagnostics.Debug.WriteLine("Sorting Called");

            LoadInventory();

            //for sampling
            /*List<GetInventoryProducts_Result> fuckmemory = new List<GetInventoryProducts_Result>();
            for (int i = 0; i < 1000; i++)
            {
                fuckmemory.Add(Inventory[i]);
            }
            Inventory.Clear();
            System.Diagnostics.Debug.WriteLine("-------------"+Inventory.Count);
            System.Diagnostics.Debug.WriteLine(fuckmemory.Count);
            for (int i = 0; i < 1000; i++)
            {
                System.Diagnostics.Debug.WriteLine(i+": "+fuckmemory[i]);
            }

            for (int i = 0; i < 1000; i++)
            {
                Inventory.Add(fuckmemory[i]);
            }*/

            //convert saved preference to sorting method
            switch (1)
            {
                case 0:
                    ShelfAlphabetically();
                    break;
                case 1:
                    ShelfVolume();
                    break;
            }

            PrintWarehouse();
        }
    }

    public class LocalShelf
    {
        public int ID { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public int maxWeight = 1000; //lbs
        public int maxVolume = 165888; //inches
        public decimal? currentWeight { get; set; }
        public decimal? currentVolume { get; set; }
        public List<GetInventoryProducts_Result> itemList { get; set; }
    } 
}
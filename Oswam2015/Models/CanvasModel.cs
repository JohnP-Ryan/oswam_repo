using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oswam2015.Models
{
    public class CanvasModel
    {
        int xCellDimension, yCellDimension, maxShelfNum, cellSizeFt;
        int placedShelfNum = 0;
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        List<List<Cell>> gridCells;

        public CanvasModel()
        {
            //Read preference values from database to configure display
            maxShelfNum = Convert.ToInt32(dataContext.GetPreferenceValue("NumShelves").ToList().FirstOrDefault());
            cellSizeFt = Convert.ToInt32(dataContext.GetPreferenceValue("ShelfSideLength").ToList().FirstOrDefault()); 
            xCellDimension = Convert.ToInt32(dataContext.GetPreferenceValue("WarehouseLength").ToList().FirstOrDefault()) / cellSizeFt;
            yCellDimension = Convert.ToInt32(dataContext.GetPreferenceValue("WarehouseWidth").ToList().FirstOrDefault()) / cellSizeFt;

            System.Diagnostics.Debug.WriteLine("GridDim: " + xCellDimension + "   " + yCellDimension);

            gridCells = new List<List<Cell>>();

            for(var row = 0; row < xCellDimension; row++)
            {
                List<Cell> sublist = new List<Cell>();
                for(var column = 0; column < yCellDimension; column++)
                {
                    Cell tempCell = new Cell(row, column);

                    sublist.Add(tempCell);
                }
                gridCells.Add(sublist);
            }
            //collect shelves from database, read into cells that match coordinates (foreach)
            var shelfData = dataContext.GetAllShelves().ToList();
            
            foreach(var dbItem in shelfData)
            {
                int shelfXCoord = Convert.ToInt32(dbItem.LocationX);
                int shelfYCoord = Convert.ToInt32(dbItem.LocationY);
                int freeVolume = Convert.ToInt32(dbItem.availableVolume);
                int freeWeight = Convert.ToInt32(dbItem.availableWeight);
                int shelfId = Convert.ToInt32(dbItem.ID);

                gridCells[shelfXCoord][shelfYCoord].BindToShelf(freeVolume, freeWeight, shelfId);
            }

            //System.Diagnostics.Debug.WriteLine("" + gridCells[3][5].getShelfId());
            //System.Diagnostics.Debug.WriteLine("" + gridCells[19][10].getXCoord() + "," + gridCells[19][10].getYCoord());
            //testDisplay();
        }

        public int getXCellDimension() { return xCellDimension; }
        public int getYCellDimension() { return yCellDimension; }

        public List<List<Cell>> getCellGrid() { return gridCells; }

        //testdisplay function - debug only
        public void testDisplay()
        {
            foreach (var sublist in gridCells)
            {
                System.Diagnostics.Debug.WriteLine("Row");
                foreach (var cell in sublist)
                {
                    System.Diagnostics.Debug.WriteLine("" + cell.getXCoord() + ',' + cell.getYCoord());
                }
            }
        }
    }


    public class Cell
    {
        int xLocation, yLocation, availableVolume, availableWeight;
        int shelfID = -1;

        public Cell(int xLoc, int yLoc, int id = -1)
        {
            xLocation = xLoc;
            yLocation = yLoc;
            shelfID = id;
        }

        public void BindToShelf(int freeVolume, int freeWeight, int linkedShelfID)
        {
            availableVolume = freeVolume;
            availableWeight = freeWeight;
            shelfID = linkedShelfID;
        }


        public int getXCoord() { return xLocation; }
        public int getYCoord() { return yLocation; }
        public int getShelfId() { return shelfID; }
    }
}
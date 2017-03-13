using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oswam2015.Models
{
    public class CanvasModel
    {
        int xCellDimension, yCellDimension;
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        List<List<Cell>> gridCells;

        public CanvasModel()
        {
            var xData = dataContext.GetPreferenceValue("WarehouseLength").ToList();
            var yData = dataContext.GetPreferenceValue("WarehouseWidth").ToList();
            int cellSize = Convert.ToInt32(dataContext.GetPreferenceValue("ShelfSideLength").ToList().FirstOrDefault()); 
            xCellDimension = Convert.ToInt32(xData.FirstOrDefault()) / cellSize;
            yCellDimension = Convert.ToInt32(yData.FirstOrDefault()) / cellSize;

            System.Diagnostics.Debug.WriteLine("GridDim: " + xCellDimension + "   " + yCellDimension);

            gridCells = new List<List<Cell>>();

            for(var row = 0; row < yCellDimension; row++)
            {
                List<Cell> sublist = new List<Cell>();
                for(var column = 0; column < xCellDimension; column++)
                {

                    //TO DO - read database shelf data to create real cells - pass shelf id in 3rd parameter
                    Cell tempCell = new Cell(column, row);

                    sublist.Add(tempCell);
                }
                gridCells.Add(sublist);
            }
            //System.Diagnostics.Debug.WriteLine("" + gridCells[19][10].getXCoord() + "," +gridCells[19][10].getYCoord());
            //testDisplay();
        }

        public int getXCellDimension() { return xCellDimension; }
        public int getYCellDimension() { return yCellDimension; }

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
        int xLocation, yLocation;
        int shelfID = -1;

        public Cell(int xLoc, int yLoc, int id = -1)
        {
            xLocation = xLoc;
            yLocation = yLoc;
            shelfID = id;
        }

        public int getXCoord() { return xLocation; }
        public int getYCoord() { return yLocation; }
    }
}
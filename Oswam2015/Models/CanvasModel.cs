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

        public CanvasModel()
        {
            var xData = dataContext.GetPreferenceValue("WarehouseLength").ToList();
            var yData = dataContext.GetPreferenceValue("WarehouseWidth").ToList();
            int cellSize = Convert.ToInt32(dataContext.GetPreferenceValue("ShelfSideLength").ToList().FirstOrDefault()); 
            xCellDimension = Convert.ToInt32(xData.FirstOrDefault()) / cellSize;
            yCellDimension = Convert.ToInt32(yData.FirstOrDefault()) / cellSize;

            System.Diagnostics.Debug.WriteLine("" + xCellDimension + "   " + yCellDimension);
        }

        public int getXCellDimension() { return xCellDimension; }
        public int getYCellDimension() { return yCellDimension; }
    }


    public class Cell
    {
        int xLocation, yLocation;
    }
}
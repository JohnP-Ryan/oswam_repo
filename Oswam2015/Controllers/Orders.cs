using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Oswam2015.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSWAM.Controllers
{
    public class OrdersController : Controller
    {

        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        public ActionResult Index()
        {
            var orderData = dataContext.GetOrderCount();
            return View(orderData.ToList());
        }

        public string DetailsPartial(string orderID)
        {
            String html = "";

            var orderTotalPrice = dataContext.GetOrderTotalPrice(orderID).ToList();
            var orderTotalWeight = dataContext.GetOrderTotalWeight(orderID).ToList();
            var orderTotalVolumeFt = dataContext.GetOrderTotalVolume(orderID).ToList();

            html += "<tr><td><h6>" + "Total Order Price:" + "</h6></td><td><h6>$" + orderTotalPrice[0].Value + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Total Order Weight:" + "</h6></td><td><h6>" + orderTotalWeight[0].Value + " Pounds</h6></td></tr>";
            html += "<tr><td><h6>" + "Total Order Volume:" + "</h6></td><td><h6>" + orderTotalVolumeFt[0].Value + " Cubic Feet</h6></td></tr>";
            html += "<tr style=\"border-top: 3px solid black;\"><td><h4><u>" + "Product Name" + "</u></h4></td><td><h4><u>" + "Product ID" + "</u></h4></td></tr>";

            //html += "</tbody><hr><tbody>";

            var orderProductList = dataContext.GetOrderProducts(orderID).ToList();

            foreach (GetOrderProducts_Result item in orderProductList)
            {
                html += "<tr><td><h6>" + item.Name + "</h6></td><td><h6>" + item.ProductID + "</h6></td></tr>";
            }
            
            return html;
        }

        [HttpPost]
        public ActionResult GenerateOrder()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var preferenceReturnMin = dataContext.GetPreferenceValue("OrderGeneratorMinProductNum").ToList();
            var preferenceReturnMax = dataContext.GetPreferenceValue("OrderGeneratorMaxProductNum").ToList();

            int minSize = preferenceReturnMin[0].Value;
            int maxSize = preferenceReturnMax[0].Value;
            int orderSize;

            if(minSize == maxSize)
            {
                orderSize = rand.Next(minSize, maxSize);
            }
            else
            {
                orderSize = rand.Next(minSize, maxSize + 1);
            }

            dataContext.orderGenerator(orderSize);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult ProcessOrders()
        {
            dataContext.ProcessAllOrders();

            return RedirectToAction("Index");
        }

    }
}

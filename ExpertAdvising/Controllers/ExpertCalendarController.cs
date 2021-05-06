using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using trial7.Models;
using System.Globalization;

namespace trial7.Controllers
{
    public class ExpertCalendarController : Controller
    {
        // GET: ExpertCalendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            Expert currentExpert = (Expert)Session["user"];

            string query = "select * from Calendar where ExpertId = (select ExpertId from Expert where id = " + currentExpert.Id + ")" ;

            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);


            int size = dataSet.Tables[0].Rows.Count;
            QueryHandler qh = new QueryHandler();

            qh.id = currentExpert.Id;
            for (int i = 0; i < size; i++)
            {
                trial7.Models.Calendar calendar = new trial7.Models.Calendar();
                calendar.ExpertId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                calendar.StartingFrom = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[2]);
                calendar.EndingAt = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[3]);

                //newly edited
                DateTime dateTime = (DateTime)dataSet.Tables[0].Rows[i].ItemArray[4];
                calendar.BookingDate = dateTime.ToString("dd/MM/yyyy");

                qh.CalendarList.Add(calendar);
            }

            //newly edited
            ViewBag.bookedDate = qh.CalendarList;
            return View(qh);
        }

        public ActionResult FreeTimeDetails(string date)
        {

            DateTime datee = DateTime.ParseExact(date, "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);

            Session["expertDate"] = datee;

            return View(new trial7.Models.Calendar());
        }

        [HttpPost]
        public ActionResult FreeTimeDetails(trial7.Models.Calendar model)
        {
            Expert expert = (Expert)Session["user"];

            DateTime selectedDate = (DateTime)Session["expertDate"];

            DateTime startingFrom = selectedDate.AddHours(model.startingHour);
            DateTime endingAt = selectedDate.AddHours(model.endingHour);

            string query = "Insert into calendar values ('" + expert.EmailId + "', '" + startingFrom + "', '" + endingAt + "', '" + selectedDate + "')";

            DatabaseHandler databaseHandler = new DatabaseHandler();
            databaseHandler.update(query);

            
            return RedirectToAction("Calendar","ExpertCalendar");
        }
    }
}
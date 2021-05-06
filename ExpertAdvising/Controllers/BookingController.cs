using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using trial7.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace trial7.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Booking(int id)
        {
            //here id is the expert id
            //query = select * from Calendar where ExpertId = (select ExpertId from Expert where id = 12)
            

            string query = "select * from Calendar where ExpertId = (select ExpertId from Expert where id = " + id + ") and StartingFrom >= '" + DateTime.Now +"'";

            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);


            int size = dataSet.Tables[0].Rows.Count;
            QueryHandler qh = new QueryHandler();

            qh.id = id;
            for (int i=0; i<size; i++)
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

        [HttpGet]
        public ActionResult BookingDetails(string dateTime, int id)     //this will be used to store data in the database
        {
            if (Session["user"] != null)
            {
                DateTime datee = DateTime.ParseExact(dateTime, "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);

                System.Diagnostics.Debug.WriteLine(datee);

                string query = "select * from Calendar where ExpertId = (select ExpertId from Expert where id = " + id + ") and date = '" + datee + "'";

                DatabaseHandler dh = new DatabaseHandler();
                DataSet dataSet = new DataSet();
                dataSet = dh.selectFunction(query);


                int size = dataSet.Tables[0].Rows.Count;
                QueryHandler qh = new QueryHandler();

                qh.id = id;
                for (int i = 0; i < size; i++)
                {

                    trial7.Models.Calendar calendar = new trial7.Models.Calendar();

                    calendar.ExpertId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                    calendar.StartingFrom = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[2]);
                    calendar.EndingAt = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[3]);

                    qh.CalendarList.Add(calendar);
                }

                ViewBag.date = qh.CalendarList[0].StartingFrom.ToShortDateString();

                ViewBag.startTime = qh.CalendarList[0].StartingFrom.Hour;
                ViewBag.endTime = qh.CalendarList[0].EndingAt.Hour;

                Session["date"] = qh.CalendarList[0].StartingFrom.Date;

                List<int> availableHours = new List<int>();

                for (int i = qh.CalendarList[0].StartingFrom.Hour; i < qh.CalendarList[0].EndingAt.Hour; i++)
                {
                    availableHours.Add(i);
                }

                ViewBag.availableHouse = availableHours;
                Session["id"]= id;

                //pass Date, Free Time, SelectTime(??) using ViewBag

                //query with dateTime and id to find the data to be passed to the View

                return View(new Booking());
            }

            else
                return RedirectToAction("Login", "User");
            
        }

        [HttpPost]

        public ActionResult BookingDetails(Booking model)
        {
            try
            {
                Userinfo current = (Userinfo)Session["user"];
                //Expert currentExpert = (Expert) 

                string query = "select ExpertId from Expert where Id = " + Convert.ToInt32(Session["id"]);



                DatabaseHandler databaseHandler = new DatabaseHandler();

                DataSet dataSet = new DataSet();

                dataSet = databaseHandler.selectFunction(query);

                string ExpertId = "";

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    ExpertId = dataSet.Tables[0].Rows[i].ItemArray[0].ToString();
                }

                query = "insert into Session (UserId,ExpertId,CompletionStatus,Topic) values ('" + current.UserId + "','" + ExpertId + "','PENDING','" + model.topic + "')";

                databaseHandler.update(query);

                query = "select Fee from Expertise where ExpertId = '" + ExpertId + "'";

                dataSet = databaseHandler.selectFunction(query);

                Decimal Fee = 0.0M;

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Fee = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[0]);
                }

                int SessionId = 0;

                query = "select max(SessionId) from Session";

                dataSet = databaseHandler.selectFunction(query);


                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    SessionId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                }

                DateTime requestDate = (DateTime)Session["date"];
                DateTime startingFrom = requestDate.AddHours(model.selectedHour);
                
                
                double addHour = Convert.ToDouble(model.selectedHour + (model.durationInMinutes / 60.0));

                DateTime endingAt = requestDate.AddHours(addHour);

                query = "insert into SessionDetails values (" + SessionId + "," + model.durationInMinutes + "," + model.durationInMinutes * Fee + ",'"+startingFrom+"','"+endingAt+"','" + model.briefDescription + "','" + requestDate + "')";

                databaseHandler.update(query);

                return RedirectToAction("My_Profile", "Profile");
            }
            catch
            {
                return View();
            }
        }
            
    }
}
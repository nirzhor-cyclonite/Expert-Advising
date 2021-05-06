using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using trial7.Models;
using System.IO;

namespace trial7.Controllers
{
    public class ExpertController : Controller
    {
        // GET: Expert
        string connectString = @"Data Source = XPS-CYCLONITE\SQLEXPRESS; Initial Catalog = ExpertAdvisingTrial2; Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Expert/Details/5
        public ActionResult MyProfile()
        {
            QueryHandler qh = new QueryHandler();
            if (Session["user"] != null)
            {
                Expert current = new Expert();
                current = (Expert) Session["user"];

                string ExpertId = current.EmailId;

                BrowseController obj = new BrowseController();
                qh.Rating = obj.GetRating(ExpertId);

                qh.ExpertList = GetExpertDetails(ExpertId);

                qh.ExpertiseList = obj.GetExpertise(ExpertId);


                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");

        }

        public ActionResult UpcomingSession()
        {
            string ExpertId = "";

            QueryHandler qh = new QueryHandler();

            if (Session["user"] != null)
            {
                Expert current = new Expert();
                current = (Expert)Session["user"];

                ExpertId = current.EmailId;

                qh = GetSessions(ExpertId, "UPCOMING", "asc");

                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");
        }

        public ActionResult CompletedSession()
        {
            string ExpertId = "";

            QueryHandler qh = new QueryHandler();

            if (Session["user"] != null)
            {
                Expert current = new Expert();
                current = (Expert)Session["user"];

                ExpertId = current.EmailId;

                qh = GetSessions(ExpertId, "COMPLETED", "desc");

                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");
            
        }

        public ActionResult PendingRequest()
        {
            string ExpertId = "";

            QueryHandler qh = new QueryHandler();

            if (Session["user"] != null)
            {
                Expert current = new Expert();
                current = (Expert)Session["user"];

                ExpertId = current.EmailId;

                qh = GetSessions(ExpertId, "PENDING", "asc");

                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");


        }

        public ActionResult AcceptRequest(int id)
        {
            //here id is Session Id
            
            string query = "update Session set CompletionStatus='UPCOMING' where SessionId = "+id;

            
            DatabaseHandler dh = new  DatabaseHandler();

            dh.update(query);

            //delete calendar information

            Expert expert = (Expert)Session["user"];

            query = "delete from Calendar where ExpertId='"+expert.EmailId+"' and Date = (select Date from SessionDetails where SessionId="+id+")";
            dh.update(query);

            TempData["msg"] = "<script>alert('Request Accepted!');</script>";

            return RedirectToAction("PendingRequest", "Expert");
           // return View();
        }
        public ActionResult DeclineRequest(int id)
        {
            //Here id is session id

            string query = "update Session set CompletionStatus='DECLINED' where SessionId = " + id;
            
            DatabaseHandler dh = new DatabaseHandler();

            dh.update(query);

            TempData["msg"] = "<script>alert('Request Has Been Declined!');</script>";
            return RedirectToAction("PendingRequest", "Expert");
        }
        public ActionResult UploadContent()
        {
            return View(new Content());
        }

        [HttpPost]
        public ActionResult UploadContent(HttpPostedFileBase file, Content model)
        {
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string pdfpath = Path.Combine(Server.MapPath("~/Contents/"), filename);
                file.SaveAs(pdfpath);

            }

            //ExpertId comes from Session
            //Content type Default Blog
            //UploadDate comes from DateTime.Now()
            //Item is demonstrated here

            Expert expert = (Expert)Session["user"];

            string ExpertId = expert.EmailId;

            

            //get topic from view

            string topic = model.Topic;
            string query = "insert into Content (ExpertId, Topic, Item, ContentType, UploadDate, Date) values ('"+ExpertId+"','"+topic+"','~/Contents/"+file.FileName+"','pdf','"+DateTime.Now+"','"+DateTime.Now.Date+"')";

            DatabaseHandler databaseHandler = new DatabaseHandler();

            databaseHandler.update(query);

            //sqlCommand.Parameters.AddWithValue("@Item", "~/Contents/" + file.FileName);
           // TempData["msg"] = "<script>alert('Chapter has been added!');</script>";
            return View();
        }


        // GET: Expert/Create
        public ActionResult Register()
        {

            TempData["alertMessage"] = "Hi";
            return View(new Expert());
        }
        [HttpPost]
        public ActionResult Register(Expert model)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectString))
            {
                sqlConnection.Open();
                string query = "insert into Expert (ExpertId,FirstName,LastName,Password,ExpertiseSector) values (@ExpertId,@FirstName,@LastName,@Password,@ExpertiseSector)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ExpertId", model.EmailId);
                sqlCommand.Parameters.AddWithValue("@FirstName", model.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", model.LastName);
                sqlCommand.Parameters.AddWithValue("@Password", model.Password);
                sqlCommand.Parameters.AddWithValue("@ExpertiseSector", model.ExpertiseSector);

                sqlCommand.ExecuteNonQuery();

                
            }

            return RedirectToAction("Login", "User");
        }

        private List<Expert> GetExpertDetails(string ExpertId)
        {
            string query = "select * from Expert where ExpertId = '" + ExpertId + "' and ActiveStatus = 'ACTIVE'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);
            QueryHandler queryHandler = new QueryHandler();

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Expert expert = new Expert();

                expert.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                expert.EmailId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                //expert.ProfileImage = 
                var nullcheck = dataSet.Tables[0].Rows[i].ItemArray[4];

                if (nullcheck is DBNull)
                {
                    expert.ProfileImage = "~/UserInfo/blank.jpg";
                }
                else
                {
                    expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                }

                expert.Password = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();
                expert.WalletStatus = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[6]);
                expert.ExpertiseSector = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();
                expert.BankAccount = dataSet.Tables[0].Rows[i].ItemArray[8].ToString();

                queryHandler.ExpertList.Add(expert);

            }
            return queryHandler.ExpertList;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public QueryHandler GetSessions(string ExpertId, string CompletionStatus, string Order)
        {
            string query = "select * from SessionDetails where SessionId in (select SessionId from Session where ExpertId='" + ExpertId + "' and CompletionStatus='" + CompletionStatus + "') order by StartTime " + Order;

            DatabaseHandler dh = new DatabaseHandler();

            QueryHandler queryHandler = new QueryHandler();

            DataSet dataSet = new DataSet();

            dataSet = dh.selectFunction(query);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                SessionDetails sessionDetails = new SessionDetails();
                Userinfo userinfo = new Userinfo();

                sessionDetails.SessionId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                sessionDetails.DurationInMinutes = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[1]);
                sessionDetails.Cost = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[2]);

                var nullCheck = dataSet.Tables[0].Rows[i].ItemArray[3];
                if(!(nullCheck is DBNull))
                    sessionDetails.StartTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[3]);

                nullCheck = dataSet.Tables[0].Rows[i].ItemArray[4];
                if(!(nullCheck is DBNull))
                    sessionDetails.EndTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[4]);
                sessionDetails.Brief_Description = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]);

                queryHandler.SessionDetailsList.Add(sessionDetails);

                userinfo = GetUserInfo(sessionDetails.SessionId);

                queryHandler.UserInfoList.Add(userinfo);


            }

            return queryHandler;

        }

        public Userinfo GetUserInfo(int SessionId)
        {
            string query = "select * from UserInfo where UserId in (select UserId from Session where SessionId=" + SessionId + ")";

            DatabaseHandler dh = new DatabaseHandler();

            Userinfo userinfo = new Userinfo();

            DataSet dataSet = new DataSet();

            dataSet = dh.selectFunction(query);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                userinfo = new Userinfo();

                userinfo.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                userinfo.UserId = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]);
                userinfo.FirstName = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]);
                userinfo.LastName = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]);
                userinfo.ProfileImage = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]);
                userinfo.Password = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]);
                userinfo.WalletStatus = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[6]);
                userinfo.Interest = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]);

            }

            return userinfo;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using trial7.Models;

namespace trial7.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult My_Profile()
        {
            //do a select query using the userId to get all the information
            //related to the user
            if (Session["user"] != null)
            {
                QueryHandler qh = new QueryHandler();

                Userinfo current = new Userinfo();
                current = (Userinfo)Session["user"];

                string userId = current.UserId;

                string query = "select * from UserInfo where UserId='" + userId + "'";
                DatabaseHandler dh = new DatabaseHandler();
                DataSet dataSet = new DataSet();
                dataSet = dh.selectFunction(query);
                int RowCount = dataSet.Tables[0].Rows.Count;
                Userinfo userInfo = new Userinfo();
                for (int i = 0; i < RowCount; i++)
                {
                    userInfo.UserId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                    userInfo.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                    userInfo.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();

                    var nullcheck = dataSet.Tables[0].Rows[i].ItemArray[4];

                    if(nullcheck is DBNull)
                    {
                        userInfo.ProfileImage = "~/UserInfo/blank.jpg";
                    }
                    else
                    {
                        userInfo.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                    }
                    userInfo.Password = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();
                    userInfo.WalletStatus = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[6]);
                    userInfo.Interest = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();

                    qh.UserInfoList.Add(userInfo);

                }

                Session["user"] = qh.UserInfoList[0];

                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");

        }

        public ActionResult Edit_Profile()
        {
            return View();
        }

        public ActionResult UpcomingSessions()
        {
            QueryHandler qh = new QueryHandler();
            Userinfo current = new Userinfo();
            current = (Userinfo)Session["user"];

            //current contains all userInfo
            //do query here
            if (Session["user"] != null)
            {
                string UserName = current.UserId;
                string CompletionStatus = "UPCOMING";
                qh.ExpertList = GetExpert(UserName, CompletionStatus);
                qh.ExpertiseList = GetExpertise(UserName, CompletionStatus);
                qh.SessionDetailsList = GetSessionDetails(UserName, CompletionStatus);


                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");
        }

        public ActionResult PendingSessions()
        {
            QueryHandler qh = new QueryHandler();
            Userinfo current = new Userinfo();
            current = (Userinfo)Session["user"];

            //current contains all userInfo
            //do query here
            if (Session["user"] != null)
            {
                string UserName = current.UserId;
                string CompletionStatus = "PENDING";
                qh.ExpertList = GetExpert(UserName, CompletionStatus);
                qh.ExpertiseList = GetExpertise(UserName, CompletionStatus);
                qh.SessionDetailsList = GetSessionDetails(UserName, CompletionStatus);


                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");
        }

        public ActionResult CompletedSessions()
        {
            QueryHandler qh = new QueryHandler();
            Userinfo current = new Userinfo();
            current = (Userinfo)Session["user"];

            //current contains all userInfo
            //do query here

            if (Session["user"] != null)
            {
                string UserName = current.UserId;
                string CompletionStatus = "COMPLETED";
                qh.ExpertList = GetExpert(UserName, CompletionStatus);
                qh.ExpertiseList = GetExpertise(UserName, CompletionStatus);
                qh.SessionDetailsList = GetSessionDetails(UserName, CompletionStatus);

                return View(qh);
            }
            else
                return RedirectToAction("Login", "User");

            
        }

        private List<Expert> GetExpert(string UserName, string CompletionStatus)
        {
            QueryHandler queryHandler = new QueryHandler();
            string query = "select * from Expert where ExpertId in (select TOP 1000 ExpertId from Session where UserId='" + UserName + "' and CompletionStatus='" + CompletionStatus + "' ORDER BY SessionId)";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Expert expert = new Expert();
                expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();

                queryHandler.ExpertList.Add(expert);
            }
            return queryHandler.ExpertList;
        }

        private List<Expertise> GetExpertise(string UserName, string CompletionStatus)
        {
            string query = "select * from Expertise where ExpertId in (select TOP 1000 ExpertId from Session where (UserId='" + UserName + "' and CompletionStatus='" + CompletionStatus + "') ORDER BY SessionId)";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);
            QueryHandler queryHandler = new QueryHandler();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Expertise expertise = new Expertise();
                expertise.Fee = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[4]);
                expertise.Bio = dataSet.Tables[0].Rows[i].ItemArray[6].ToString();


                queryHandler.ExpertiseList.Add(expertise);
            }
            return queryHandler.ExpertiseList;
        }

        private List<SessionDetails> GetSessionDetails(string UserName, string CompletionStatus)
        {
            string query = "select * from SessionDetails where SessionId in (select TOP 1000 SessionId from Session where UserId = '" + UserName + "' and CompletionStatus = '" + CompletionStatus + "'  ORDER BY SessionId)";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);
            QueryHandler queryHandler = new QueryHandler();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                SessionDetails sessionDetails = new SessionDetails();

                sessionDetails.SessionId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                sessionDetails.DurationInMinutes = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[1]);
                sessionDetails.Cost = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[2]);
                var nullCheck = dataSet.Tables[0].Rows[i].ItemArray[3];
                if(!(nullCheck is DBNull))
                    sessionDetails.StartTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[3]);

                nullCheck = dataSet.Tables[0].Rows[i].ItemArray[4];
                if (!(nullCheck is DBNull))
                    sessionDetails.EndTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[4]);
                sessionDetails.Brief_Description = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();

                queryHandler.SessionDetailsList.Add(sessionDetails);
            }
            return queryHandler.SessionDetailsList;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}

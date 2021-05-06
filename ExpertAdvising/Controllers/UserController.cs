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
    public class UserController : Controller
    {
        static Userinfo currentUser;
        List<Userinfo> lst;
        bool isExpert = false;

        int alreadyRegistered; 

        // GET: Expert
        string connectString = @"Data Source = XPS-CYCLONITE\SQLEXPRESS; Initial Catalog = ExpertAdvisingTrial2; Integrated Security=True";

        DatabaseHandler db = new DatabaseHandler();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new Userinfo());
        }


        [HttpPost]
        public ActionResult Login(Userinfo model)
        {
            currentUser = new Userinfo();

            QueryHandler qh = new QueryHandler();

            string query = "select * from UserInfo where UserId='" + model.UserId + "'";
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
                userInfo.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                userInfo.Password = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();
                userInfo.WalletStatus = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[6]);
                userInfo.Interest = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();

                qh.UserInfoList.Add(userInfo);

            }


            string passwordFromUser = model.Password;
            if(qh.UserInfoList.Count==0)
            {
                //check if expert exist
                query = "select * from Expert where ExpertId = '" + model.UserId + "' and ActiveStatus = 'ACTIVE'";

                dataSet = dh.selectFunction(query);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Expert expert = new Expert();

                    expert.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                    expert.EmailId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                    expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                    expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                    expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                    expert.Password = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();
                    expert.WalletStatus = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[6]);
                    expert.ExpertiseSector = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();
                    expert.BankAccount = dataSet.Tables[0].Rows[i].ItemArray[8].ToString();

                    qh.ExpertList.Add(expert);

                }

                if (qh.ExpertList.Count > 0)
                    isExpert = true;

                if (isExpert)
                {
                    if(qh.ExpertList[0].Password == passwordFromUser)
                    {
                        Session["user"] = qh.ExpertList[0];
                        Session["expert"] = qh.ExpertList[0];

                        return RedirectToAction("MyProfile", "Expert");
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Wrong Password!');</script>";
                        return View();
                    }
                }
                else
                {
                    TempData["msg"] = "<script>alert('This username does not exist!');</script>";
                    return View();
                }
            }

            else if (qh.UserInfoList[0].Password == passwordFromUser)
            {

                currentUser = qh.UserInfoList[0];

                Session["user"] = currentUser;
                //  Profile();
                return RedirectToAction("My_Profile", "Profile");
            }
            else
            {
                TempData["msg"] = "<script>alert('Wrong Password!');</script>";
                return View();
            }
        }
        // GET: Expert/Create
        public ActionResult Register()
        {

            TempData["alertMessage"] = "Hi";
            return View(new Userinfo());
        }

        [HttpPost]
        public ActionResult Register(Userinfo userinfo)
        {

            //run a select query with the userinfo.UserId
            //if found, flag = 1, the user will not be registered
            //show error message

            alreadyRegistered = CheckExistingUsers(userinfo.UserId);

            if(alreadyRegistered == 0) //not registered yet
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectString))
                {
                    sqlConnection.Open();
                    string query = "insert into UserInfo (UserId,FirstName,LastName,Password) values (@UserId,@FirstName,@LastName,@Password)";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@UserId", userinfo.UserId);
                    sqlCommand.Parameters.AddWithValue("@FirstName", userinfo.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", userinfo.LastName);
                    sqlCommand.Parameters.AddWithValue("@Password", userinfo.Password);

                    sqlCommand.ExecuteNonQuery();

                    return RedirectToAction("Login", "User");
                }
            }

            else
            {
                TempData["msg"] = "<script>alert('This username already exists!');</script>";
                return View();

            }

        }

        private int CheckExistingUsers(string userId)
        {
            string query = "select UserId from UserInfo where UserId='" + userId + "'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            int flag;
            if (dataSet.Tables[0].Rows.Count > 0)
                flag = 1;
            else
                flag = 0;

            return flag;
        }


        // POST: Expert/Create
    }
}
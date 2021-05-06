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
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            //query here to get all the information
            //we will pass the information using ViewBag or something else, whatever suits
            int architectureCount = ExpertCount("Architecture");
            int financeCount = ExpertCount("Finance");
            int medicalCount = ExpertCount("Medical");
            int lawCount = ExpertCount("Law");
            int softwareCount = ExpertCount("Software & Technology");
            int textileCount = ExpertCount("Textile");

            ViewBag.architecture = architectureCount;
            ViewBag.finance = financeCount;
            ViewBag.medical = medicalCount;
            ViewBag.law = lawCount;
            ViewBag.software = softwareCount;
            ViewBag.textile = textileCount;

            return View();
        }

        //CHANGE INTERNAL MESSAGE IN ALL THE BROWSE PAGES
        public ActionResult Architecture()
        {
            int architectureCount = ExpertCount("Architecture");
            ViewBag.architecture = architectureCount;

            QueryHandler dataList = GetListofExperts("Architecture");

            return View(dataList);
        }

        public ActionResult Finance()
        {
            int financeCount = ExpertCount("Finance");
            ViewBag.finance = financeCount;

            QueryHandler dataList = GetListofExperts("Finance");

            return View(dataList);
        }

        public ActionResult Law()
        {
            //Change Header Image
            QueryHandler dataList = GetListofExperts("Law");

            int lawCount = ExpertCount("Law");
            ViewBag.law = lawCount;


            return View(dataList);
        }

        public ActionResult Medical()
        {
            //Change Header Image
            QueryHandler dataList = GetListofExperts("Medical");

            int medicalCount = ExpertCount("Medical");
            ViewBag.medical = medicalCount;


            return View(dataList);
        }

        public ActionResult Software()
        {
            //Change Header Image
            QueryHandler dataList = GetListofExperts("Software & Technology");

            int softwareCount = ExpertCount("Software & Technology");
            ViewBag.software = softwareCount;

            return View(dataList);
        }

        public ActionResult Textile()
        {
            //Change Header Image
            QueryHandler dataList = GetListofExperts("Textile");

            int textileCount = ExpertCount("Textile");
            ViewBag.textile = textileCount;

            return View(dataList);
        }

        public ActionResult ViewExpert(int id)
        {
            ////string me = id;
            QueryHandler currentExpert = GetExpertDetails(id);
            currentExpert.ContentList = GetContents(id);


            return View(currentExpert);
            //return View();
        }

        public ActionResult ViewContent(int id)
        {
            //id is Content id

            QueryHandler qh = new QueryHandler();

            qh.ContentList = GetContentsExpert(id);

            qh.currentContent = GetCurrentContent(id);

           // qh.ContentList = GetContents(id);
            
            return View(qh);
        }

        public Content GetCurrentContent(int id)
        {
            string query = "select * from Content where ContentId=" + id ;
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);

            Content content = new Content();
            
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                content = new Content();
                content.ContentId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                content.ExpertId = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]);
                content.Topic = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]);
                content.Item = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]);
                content.ContentType = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]);
                content.UploadDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[5]);

               

            }
            return content;
        }


        public List<Content> GetContentsExpert(int id)
        {
            string query = "select * from Content where ExpertId = (select ExpertId from Content where ContentId="+id+")";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            List<Content> list = new List<Content>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Content content = new Content();
                content.ContentId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                content.ExpertId = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]);
                content.Topic = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]);
                content.Item = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]);
                content.ContentType = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]);
                content.UploadDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[5]);

                list.Add(content);

            }
            return list;
        }



        public List<Content> GetContents(int id)
        {
            string query = "select* from Content where ExpertId=(select ExpertId from Expert where Id="+id+")";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            List<Content> list = new List<Content>();
            for(int i=0;i<dataSet.Tables[0].Rows.Count;i++)
            {
                Content content = new Content();
                content.ContentId = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                content.ExpertId = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]);
                content.Topic = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]);
                content.Item = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]);
                content.ContentType = Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]);
                content.UploadDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[5]);

                list.Add(content);

            }
            return list;
        }

        public QueryHandler GetExpertDetails(int ExpertId)
        {
            string query = "select * from Expert where Id='" + ExpertId + "'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            int size = dataSet.Tables[0].Rows.Count;

            QueryHandler qh = new QueryHandler();
            for (int i = 0; i < size; i++)
            {
                Expert expert = new Expert();

                expert.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                expert.EmailId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                expert.ExpertiseSector = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();

                qh.ExpertList.Add(expert);
            }

            String ExpertName = qh.ExpertList[0].EmailId;


            qh.ExpertiseList = GetExpertise(ExpertName);

            qh.Rating = GetRating(ExpertName);

            qh.Session_Count = GetSessionCount(ExpertName);

            qh.ReviewList = GetUserReview(ExpertName);

            return qh;
        }

        public List<Expertise> GetExpertise(String Name)
        {
            String query = "select * from Expertise where ExpertId ='" + Name + "'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);
            int size = dataSet.Tables[0].Rows.Count;
            QueryHandler queryHandler = new QueryHandler();
            for (int i = 0; i < size; i++)
            {
                Expertise expertise = new Expertise();

                expertise.ExpertId = dataSet.Tables[0].Rows[i].ItemArray[0].ToString();
                expertise.Education = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expertise.WorkingArea = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expertise.ExpertiseSectors = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expertise.Fee = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[4]);
                expertise.Bio = dataSet.Tables[0].Rows[i].ItemArray[6].ToString();

                queryHandler.ExpertiseList.Add(expertise);
            }
            return queryHandler.ExpertiseList;
        }

        public List<Review> GetUserReview(String ExpertName)
        {
            String query = "select UserReview from Review where SessionId in (select SessionId from Session where ExpertId='" + ExpertName + "')";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet another = new DataSet();
            another = dh.selectFunction(query);
            QueryHandler queryHandler = new QueryHandler();

            for (int i = 0; i < another.Tables[0].Rows.Count; i++)
            {
                Review review = new Review();
                review.UserReview = another.Tables[0].Rows[i].ItemArray[0].ToString();
                queryHandler.ReviewList.Add(review);
            }
            return queryHandler.ReviewList;
        }

        public Decimal GetRating(String Name)
        {
            String query = "select avg(Rating) from Review where SessionId in (select SessionId from Session where ExpertId = '" + Name + "' and CompletionStatus = 'COMPLETED')";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = dh.selectFunction(query);

            int size = (int)dataSet.Tables[0].Rows.Count;

            var nullCheck = dataSet.Tables[0].Rows[0].ItemArray[0];
            bool dataFound = true;
            Decimal Rating = 0;

            if (nullCheck is DBNull)
            {
                dataFound = false;
                Rating = 0.00M;
            }

            for (int i = 0; i < size && dataFound; i++)
            {
                Rating = Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }
            return Rating;
        }

        public int GetSessionCount(String Name)
        {
            int SessionCount = 0;
            String query = "select count(SessionId) from Session where CompletionStatus='COMPLETED' and ExpertId='" + Name + "'";
            DatabaseHandler dh = new DatabaseHandler();

            DataSet dataSet = dh.selectFunction(query);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                SessionCount = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }

            return SessionCount;
        }

        public int ExpertCount(string ExpertiseSector)
        {
            string query = "select count(ExpertId) from Expert where ExpertiseSector='" + ExpertiseSector + "'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            int size = dataSet.Tables[0].Rows.Count;
            int x = 0;
            for (int i = 0; i < size; i++)
            {
                x = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
            }
            return x;
        }

        public QueryHandler GetListofExperts(string ExpertiseSector)
        {
            string query = "select * from Expert where ExpertiseSector='" + ExpertiseSector + "'";
            DatabaseHandler dh = new DatabaseHandler();
            QueryHandler qh = new QueryHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);

            int size = dataSet.Tables[0].Rows.Count;

            for (int i = 0; i < size; i++)
            {
                Expert expert = new Expert();

                expert.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                expert.EmailId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                expert.ExpertiseSector = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();

                qh.ExpertList.Add(expert);

                qh.ExpertiseList.AddRange(GetExpertise(expert.EmailId));
            }

            return qh;
        }

    }
}
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
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string id)
        {
            QueryHandler qh1 = new QueryHandler();
            QueryHandler qh2 = new QueryHandler();

            QueryHandler search = new QueryHandler();


            /* qh1 = SearchBy_Name_And_ExpertiseSector(id);

           //  qh2.ExpertList = qh1.ExpertList;
             //qh2.ExpertiseList = qh1.ExpertiseList;

             qh2.ExpertList.AddRange(qh1.ExpertList);
             qh2.ExpertiseList.AddRange(qh1.ExpertiseList);

             qh1.ExpertList.Clear();
             qh1.ExpertiseList.Clear();

             qh1 = SearchBy_Expertise_And_Bio(id);

             //qh2.ExpertList.Union(qh1.ExpertList);
             //qh2.ExpertiseList.Union(qh1.ExpertiseList);


             search.ExpertList = qh2.ExpertList.Union(qh1.ExpertList).ToList();
             search.ExpertiseList = qh2.ExpertiseList.Union(qh1.ExpertiseList).ToList();*/

            search = AllResults(id);
            search.AttributeList = GetAttributes(search);

            return View(search);

        }

        public List<Search> GetAttributes(QueryHandler queryHandler)
        {
            int size = queryHandler.ExpertList.Count;
            BrowseController browseController = new BrowseController();
            List<Search> list = new List<Search>();
            for (int i = 0; i < size; i++)
            {
                Search obj = new Search();
                string ExpertId = queryHandler.ExpertList[i].EmailId;
                obj.Rating = browseController.GetRating(ExpertId);
                obj.SessionCount = browseController.GetSessionCount(ExpertId);
                obj.ReviewCount = browseController.GetUserReview(ExpertId).Count;

                list.Add(obj);

            }
            return list;
        }


        public QueryHandler SearchBy_Name_And_ExpertiseSector(string id)
        {
            string query = "select * from Expert where (FirstName like '%" + id + "%' or LastName like '%" + id + "%' or ExpertiseSector like '%" + id + "%') and ActiveStatus='ACTIVE'";

            QueryHandler queryHandler = new QueryHandler();

            DatabaseHandler dh = new DatabaseHandler();

            DataSet dataSet = new DataSet();

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

                queryHandler.ExpertList.Add(expert);

                BrowseController browseController = new BrowseController();
                //edited here
                queryHandler.ExpertiseList.AddRange(browseController.GetExpertise(expert.EmailId));
            }
            return queryHandler;
        }

        public QueryHandler SearchBy_Expertise_And_Bio(string id)
        {
            string query = "select * from Expertise where ExpertiseSectors like '%" + id + "%' or Bio like '%" + id + "%'";

            QueryHandler queryHandler = new QueryHandler();

            DatabaseHandler dh = new DatabaseHandler();

            DataSet dataSet = new DataSet();

            dataSet = dh.selectFunction(query);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                Expertise expertise = new Expertise();

                BrowseController browseController = new BrowseController();

                expertise.ExpertId = dataSet.Tables[0].Rows[i].ItemArray[0].ToString();
                expertise.Education = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expertise.WorkingArea = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expertise.ExpertiseSectors = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expertise.Fee = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[4]);
                expertise.Rating = browseController.GetRating(expertise.ExpertId);
                expertise.Bio = dataSet.Tables[0].Rows[i].ItemArray[6].ToString();


                queryHandler.ExpertiseList.Add(expertise);

                Expert expert = new Expert();

                expert = GetExpert(expertise.ExpertId);

                queryHandler.ExpertList.Add(expert);
            }
            return queryHandler;
        }

        public Expert GetExpert(string ExpertId)
        {
            string query = "select * from Expert where ExpertId='" + ExpertId + "'";
            DatabaseHandler dh = new DatabaseHandler();
            DataSet dataSet = new DataSet();
            dataSet = dh.selectFunction(query);
            int size = dataSet.Tables[0].Rows.Count;

            Expert expert = new Expert();

            for (int i = 0; i < size; i++)
            {
                expert = new Expert();

                expert.Id = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[0]);
                expert.EmailId = dataSet.Tables[0].Rows[i].ItemArray[1].ToString();
                expert.FirstName = dataSet.Tables[0].Rows[i].ItemArray[2].ToString();
                expert.LastName = dataSet.Tables[0].Rows[i].ItemArray[3].ToString();
                expert.ProfileImage = dataSet.Tables[0].Rows[i].ItemArray[4].ToString();
                expert.Password = dataSet.Tables[0].Rows[i].ItemArray[5].ToString();
                expert.WalletStatus = Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[6]);
                expert.ExpertiseSector = dataSet.Tables[0].Rows[i].ItemArray[7].ToString();
                expert.BankAccount = dataSet.Tables[0].Rows[i].ItemArray[8].ToString();


            }
            return expert;

        }

        public QueryHandler AllResults(string id)
        {
            string query = @"select * from Expert where (FirstName like '%" + id + "%' or LastName like '%" + id +
                "%' or ExpertiseSector like '%" + id + "%') and ActiveStatus='ACTIVE'" +
                " union " + "select * from Expert where ExpertId in (select ExpertId from Expertise where ExpertiseSectors like'%" + id + "%' or Bio like '%" + id + "%')";


            QueryHandler queryHandler = new QueryHandler();

            DatabaseHandler dh = new DatabaseHandler();

            DataSet dataSet = new DataSet();

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

                queryHandler.ExpertList.Add(expert);

                BrowseController browseController = new BrowseController();
                //edited here
                queryHandler.ExpertiseList.AddRange(browseController.GetExpertise(expert.EmailId));
            }
            return queryHandler;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class QueryHandler
    {
        public QueryHandler()
        {
            initializeList();
        }
        public Content currentContent { get; set; }

        public int Session_Count { get; set; }
        public decimal Rating { get; set; }
        public List<Models.Calendar> CalendarList { get; set; }
        public List<Models.CancelledSession> CancelledSessionList { get; set; }
        public List<Models.Content> ContentList { get; set; }
        public List<Models.Expert> ExpertList { get; set; }
        public List<Models.Expertise> ExpertiseList { get; set; }
        public List<Models.Review> ReviewList { get; set; }
        public List<Models.Session> SessionList { get; set; }
        public List<Models.SessionDetails> SessionDetailsList { get; set; }
        public List<Models.SessionNote> SessionNoteList { get; set; }
        public List<Models.Userinfo>UserInfoList { get; set; }
        public List<Models.Search> AttributeList { get; set; }

        public int id { get; set; }


        public void initializeList()
        {
            CalendarList = new List<Calendar>();
            CancelledSessionList = new List<CancelledSession>();
            ContentList = new List<Content>();
            ExpertList = new List<Expert>();
            ExpertiseList = new List<Expertise>();
            ReviewList = new List<Review>();
            SessionList = new List<Session>();
            SessionDetailsList = new List<SessionDetails>();
            SessionNoteList = new List<SessionNote>();
            UserInfoList = new List<Userinfo>();
            AttributeList = new List<Search>();
        }
    }
}
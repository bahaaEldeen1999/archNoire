using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.DBControllers
{
    public class PageDBController
    {
        DBManager dBManager;
        public PageDBController()
        {
            dBManager = new DBManager();
        }


        // INSERT METHODS

        public int insertPage(int page_id, string name, string info, string location, string business_email, string password, int phone_no)
        {
            string sql = String.Format("insert into [PAGE] values({0},'{1}','{2}','{3}','{4}','{5}',{6})", page_id, name, info, location, business_email, password, phone_no);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertEvent(int page_id, int event_id, string date, string location, int ticket_price, int no_of_people_going)
        {
            string sql = String.Format("insert into [EVENT] values({0},{1},'{2}','{3}',{4},{5})", page_id, event_id, date, location, ticket_price, no_of_people_going);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertEventPhoto(int page_id, int event_id, int event_photo_id, string source)
        {
            string sql = String.Format("insert into [EVENT_PHOTO] values({0},{1},{2},'{3}'", page_id, event_id, event_photo_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPageLike(int user_id, int page_id)
        {
            string sql = String.Format("insert into [PAGE_LIKES] values({0},{1})", user_id, page_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePost(int page_id, int page_post_id, string date, string location, int no_of_likes, string text)
        {
            string sql = String.Format("insert into [PAGE_POST] values({0},{1},'{2}','{3}',{4},'{5}')",page_id,page_post_id,date,location,no_of_likes,text);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostComment(int user_commented_id, int page_id, int page_post_id, int page_post_comment_id, string text, int no_of_likes)
        {
            string sql = String.Format("insert into [PAGE_POST_COMMENT] values({0},{1},{2},{3},'{4}',{5})", user_commented_id, page_id, page_post_id, page_post_comment_id, text, no_of_likes);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostCommentLike(int user_liked_id, int user_commented_id, int page_id, int page_post_id, int page_post_comment_id)
        {
            string sql = String.Format("insert into [PAGE_POST_COMMENT_LIKES] values({0},{1},{2},{3},{4})", user_liked_id, user_commented_id, page_id, page_post_id, page_post_comment_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostLike(int user_id, int page_id, int page_post_id)
        {
            string sql = String.Format("insert into [PAGE_POST_LIKES] values({0},{1},{2})", user_id, page_id, page_post_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPageReview(int user_id, int page_id, string text)
        {
            string sql = String.Format("insert into [PAGE_REVIEWS] values({0},{1},'{2}')", user_id, page_id, text);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePhoto(int page_id, int page_photo_id, string source)
        {
            string sql = String.Format("insert into [PAGE_PHOTO] values({0},{1},'{2}')", page_id, page_photo_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostPhoto(int page_id, int page_post_id, int page_post_photo_id, string source)
        {
            string sql = String.Format("insert into [PAGE_POST_PHOTO] values({0},{1},{2},'{3}')", page_id, page_post_id, page_post_photo_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }




        // DELETE METHODS

        public int DeletePage(int page_id)
        {
            string sql = "DELETE FROM PAGE WHERE page_id = '" + page_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePagePhoto(int page_id, int page_photo_id)
        {
            string sql = "DELETE FROM PAGE_PHOTO WHERE page_id = '" + page_id + "' AND page_photo_id = '" + page_photo_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePageLike(int user_id, int page_id)
        {
            string sql = "DELETE FROM PAGE_LIKES WHERE user_id = '" + user_id + "' AND page_id = '" + page_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePagePost(int page_id, int page_post_id)
        {
            string sql = "DELETE FROM PAGE_POST WHERE page_id = '" + page_id + "' AND page_post_id = '" + page_post_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePagePostLike(int user_id, int page_id, int page_post_id)
        {
            string sql = "DELETE FROM PAGE_POST_LIKES WHERE user_id = '" + user_id + "' AND page_id = '" + page_id + "' AND page_post_id = '" + page_post_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePagePostComment(int user_commented_id, int page_id, int page_post_id, int page_post_comment_id)
        {
            string sql = "DELETE FROM PAGE_POST_COMMENT WHERE user_commented_id = '" + user_commented_id + "' AND page_id = '" + page_id + "' AND page_post_id = '" + page_post_id + "' AND page_post_comment_id = '" + page_post_comment_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePagePostCommentLike(int user_liked_id, int user_commented_id, int page_id, int page_post_id, int page_post_comment_id)
        {
            string sql = "DELETE FROM PAGE_POST_COMMENT_LIKES WHERE user_liked_id = '" + user_liked_id + "' AND user_commented_id = '" + user_commented_id + "' AND page_id = '" + page_id + "' AND page_post_id = '" + page_post_id + "' AND page_post_comment_id = '" + page_post_comment_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePostPhoto(int page_id, int page_post_id, int page_post_photo_id)
        {
            string sql = "DELETE FROM PAGE_POST_PHOTO WHERE page_id = '" + page_id + "' AND page_post_id = '" + page_post_id + "' AND page_post_photo_id = '" + page_post_photo_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }
        
        public int DeletePageReview(int user_id, int page_id)
        {
            string sql = "DELETE FROM PAGE_REVIEWS WHERE user_id = '" + user_id + "' AND page_id = '" + page_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteEvent(int page_id, int event_id)
        {
            string sql = "DELETE FROM EVENT WHERE page_id = '" + page_id + "' AND event_id = '" + event_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteEventPhoto(int page_id, int event_id, int event_photo_id)
        {
            string sql = "DELETE FROM EVENT_PHOTO WHERE page_id = '" + page_id + "' AND event_id = '" + event_id + "' AND event_photo_id = '" + event_photo_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteGoingToEvent(int user_id, int page_id, int event_id)
        {
            string sql = "DELETE FROM GOING_TO_EVENT WHERE user_id = '" + user_id + "' AND page_id = '" + page_id + "' AND event_id = '" + event_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }
    }
}
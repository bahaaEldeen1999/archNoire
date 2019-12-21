using archNoire.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int insertPage(string name, string info, string location, string business_email, string password, string phone_no)
        {
            string sql = String.Format("insert into [PAGE] values('{0}','{1}','{2}','{3}','{4}','{5}')", name, info, location, business_email, password, phone_no);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertEvent(int page_id, DateTime date, string location, int ticket_price, int no_of_people_going)
        {
            string sql = String.Format("insert into [EVENT] values({0},'{1}','{2}',{3},{4})", page_id, date.Date, location, ticket_price, no_of_people_going);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertEventPhoto(int page_id, int event_id, string source)
        {
            string sql = String.Format("insert into [EVENT_PHOTO] values({0},{1},'{2}')", page_id, event_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPageLike(int user_id, int page_id)
        {
            string sql = String.Format("insert into [PAGE_LIKES] values({0},{1})", user_id, page_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePost(int page_id,  DateTime date, string location, int no_of_likes, string text)
        {
            string sql = String.Format("insert into [PAGE_POST] values({0},'{1}','{2}',{3},'{4}')", page_id,  date.Date, location, no_of_likes, text);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostComment(int user_commented_id, int page_id, int page_post_id, string text, int no_of_likes)
        {
            string sql = String.Format("insert into [PAGE_POST_COMMENT] values({0},{1},{2},'{3}',{4})", user_commented_id, page_id, page_post_id, text, no_of_likes);
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

        public int insertPagePhoto(int page_id,  string source)
        {
            string sql = String.Format("insert into [PAGE_PHOTO] values({0},'{1}')", page_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertPagePostPhoto(int page_id, int page_post_id, string source)
        {
            string sql = String.Format("insert into [PAGE_POST_PHOTO] values({0},{1},'{2}')", page_id, page_post_id, source);
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
        // gets
        public DataTable getPageInfo(string email, string password)
        {
            string sql = "select * from dbo.[PAGE] where dbo.[PAGE].business_email = '" + email + "' and dbo.[PAGE].password ='" + password + "'";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPageById(int pageID)
        {
            string sql = "select * from dbo.[PAGE] as p left join PAGE_PHOTO as o on o.page_id = p.page_id   where p.page_id = "+pageID;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPagePhoto(int id)
        {
            string sql = "select * from dbo.[PAGE_PHOTO] where page_id = " + id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPagePosts(int id)
        {
            string sql = "select * from dbo.[PAGE_POST] as u left join PAGE_POST_PHOTO as p on u.page_id = p.page_id and u.page_post_id = p.page_post_id join [PAGE] as g on g.page_id = u.page_id left join PAGE_PHOTO as d on d.page_id = u.page_id  where u.page_id =" + id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPageEvents(int id)
        {
            string sql = "select * from dbo.[Event] as e left join EVENT_PHOTO as p on e.event_id = p.event_id join [PAGE] as g on g.page_id = e.page_id left join PAGE_PHOTO as d on d.page_id = e.page_id where e.page_id = " + id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPageEvent(int event_id)
        {
            string sql = "select * from dbo.[Event] as e left join EVENT_PHOTO as p on e.event_id = p.event_id join [PAGE] as g on g.page_id = e.page_id left join PAGE_PHOTO as d on d.page_id = e.page_id where e.event_id = " + event_id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPagePostComments(int page_posted_id, int PostId)
        {
            string sql = "select * from  PAGE_POST_COMMENT as c join [USER] as u on c.user_commented_id = u.user_id left join USER_PHOTO as ph on ph.user_id = u.user_id   where c.page_id = " + page_posted_id + " and  c.page_post_id = " + PostId;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getLastInsertedPost()
        {
            string sql = "SELECT TOP 1 page_post_id FROM PAGE_POST ORDER BY page_post_id DESC;";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getLastInsertedEvent()
        {
            string sql = "SELECT TOP 1 event_id FROM EVENT ORDER BY event_id DESC;";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfLikesOfPagePost(int page_id, int PostId)
        {
            string sql = "select * from  PAGE_POST_LIKES where page_id = " + page_id + "and  page_post_id = " + PostId;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfGoingToEvent(int page_id, int event_id)
        {
            string sql = "select * from  GOING_TO_EVENT where page_id = " + page_id + " and  event_id = " + event_id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfLikesOfPageComment(int page_posted_id, int PostId, int commentID)
        {
            string sql = "select * from  PAGE_POST_COMMENT_LIKES where page_id = " + page_posted_id + " and  page_post_id = " + PostId + " and page_post_comment_id = " + commentID;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPageByName(string name)
        {
            string sql = "select * from dbo.[PAGE] where dbo.[PAGE].name = '" + name+"'" ;
            return dBManager.ExecuteReader(sql);

        }

        public DataTable getPageREview(int pageID)
        {
            string sql = "select * from dbo.[PAGE_REVIEWS] as p join [USER] as u on p.user_id = u.user_id left join USER_PHOTO as ph on ph.user_id = u.user_id where page_id = " + pageID;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfLikesOfUserCommentOnPage(int user_commented_id, int pageID, int page_post_id,int page_post_comment_id)
        {
            string sql = "select * from PAGE_POST_COMMENT_LIKES where user_commented_id = " + user_commented_id + "and  page_id = " + pageID + " and page_post_id = " + page_post_id+ " and page_post_comment_id =  "+page_post_comment_id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPagePostsUserLike(int userID)
        {
            string sql = "select * from PAGE_POST as p join PAGE_LIKES as l on l.page_id = p.page_id left join PAGE_POST_PHOTO as i on i.page_post_id = p.page_post_id join PAGE as q on q.page_id = p.page_id join PAGE_PHOTO as ph on ph.page_id = p.page_id where l.user_id = "+userID;
            return dBManager.ExecuteReader(sql);
        }

        public DataTable getPageByEmail(string email)
        {
            string sql = "select * from dbo.[PAGE] where dbo.[PAGE].business_email = '" + email + "'";
            return dBManager.ExecuteReader(sql);

        }

        // updates
        public int updatePostNoOfLikes(int page_id, int post_id, int no)
        {
            string sql = String.Format("update PAGE_POST set no_of_likes = {1} where page_id = {0} and page_post_id = {2} ", page_id, no, post_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updateEventNoOfLikes(int page_id, int event_id, int no)
        {
            string sql = String.Format("update EVENT set no_of_people_going = {1} where page_id = {0} and event_id = {2} ", page_id, no, event_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updateCommentNoOfLikes(int page_id, int post_id, int comment_id, int no)
        {
            string sql = String.Format("update PAGE_POST_COMMENT set no_of_likes = {1} where page_id = {0} and page_post_id = {2} and page_post_comment_id = {3} ", page_id, no, post_id, comment_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        // from user 
      /*  
       *  public int updateUserInfo(int userID, string name, string bio, string gender, string phone_no, string location, DateTime birth_date, string personal_email, string password)
        {
            string sql = String.Format("update [USER] set name ='{0}',bio = '{1}',gender = '{2}' , phone_no = '{3}',location = '{4}',birth_date = '{5}',personal_email='{6}',password='{7}' where user_id = {8} ", name, bio, gender, phone_no, location, birth_date.Date, personal_email, password, userID);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int UpdateUserBio(int user_id, string bio)
        {
            string sql = String.Format("update [USER] set bio = '{1}' where user_id = {0} ", user_id, bio);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int UpdateUserPhoto(int user_id, string source)
        {
            string sql = String.Format("update USER_PHOTO set source = '{1}' where user_id = {0} ", user_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updatePostNoOfLikes(int user_id, int post_id, int no)
        {
            string sql = String.Format("update USER_POST set no_of_likes = {1} where user_id = {0} and post_id = {2} ", user_id, no, post_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updateCommentNoOfLikes(int user_id, int post_id, int comment_id, int no)
        {
            string sql = String.Format("update USER_POST_COMMENT set no_of_likes = {1} where user_posted_id = {0} and post_id = {2} and comment_id = {3} ", user_id, no, post_id, comment_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        */
    }
}
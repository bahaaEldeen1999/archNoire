using archNoire.App_Start;
using archNoire.DBControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace archNoire.DBControllers
{
    public class AdminDBController
    {
        DBManager dBManager;

        public AdminDBController()
        {
            dBManager = new DBManager();
        }


        public System.Data.DataTable getAllAdmins()
        {
            string sql = "SELECT * FROM [dbo].[ADMIN]";
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getAdmin(int admin_id)
        {
            string sql = "SELECT * FROM [dbo].[ADMIN] WHERE admin_id = " + admin_id;
            return dBManager.ExecuteReader(sql);
        }

        public bool checkAdmin(int admin_id,string password)
        {
            string sql = String.Format("SELECT * FROM [dbo].[ADMIN] WHERE admin_id = {0} AND password = '{1}'", admin_id, password);
            if (dBManager.ExecuteReader(sql) != null)
                return true;
            else
                return false;
        }

        public int deleteAdmin(int admin_id)
        {
            string sql = "DELETE FROM [dbo].[ADMIN] WHERE admin_id = " + admin_id;
            return dBManager.ExecuteNonQuery(sql);
        }

        public int updateAdmin(int admin_id,string name,string admin_email,string password)
        {
            string sql = String.Format("UPDATE [dbo].[ADMIN] SET [name] = '{1}', [admin_email] = '{2}', [password] = '{3}' WHERE [admin_id] = {0}", admin_id, name, admin_email, password);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int createAdmin(int admin_id,string name,string admin_email,string password)
        {
            string sql = String.Format("INSERT INTO [dbo].[ADMIN] VALUES ({0},'{1}','{2}','{3}')", admin_id, name, admin_email, password);
            return dBManager.ExecuteNonQuery(sql);
        }


        public System.Data.DataTable getUser(int user_id)
        {
            string sql = "SELECT [user_id],[name],[bio],[gender],[phone_no],[location],[birth_date],[personal_email] FROM [dbo].[USER] WHERE [user_id] = " + user_id;
            return dBManager.ExecuteReader(sql);
        }

        //public System.Data.DataTable getUserFriends(int user_id)
        //{
        //    string sql = String.Format("(SELECT * FROM [dbo].[FRIENDS] WHERE user1_id = {0} OR user2_id = {0})", user_id);
        //    return dBManager.ExecuteReader(sql);
        //}

        public System.Data.DataTable getPage(int page_id)
        {
            string sql = "SELECT [page_id],[name],[info],[location],[business_email],[phone_no] FROM [dbo].[PAGE] WHERE [page_id] = " + page_id;
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getUserFriends(int user_id)
        {
            string sql = String.Format("SELECT user2_id as friend_id FROM FRIENDS WHERE user1_id = {0} UNION SELECT user1_id as friend_id FROM FRIENDS WHERE user2_id = {0}", user_id);
            return dBManager.ExecuteReader(sql);
        }


        public System.Data.DataTable getUserPosts(int user_id)
        {
            string sql = String.Format("SELECT post.user_id,post.post_id,post.text,post.date,post.postLocation,post.no_of_likes FROM [USER] JOIN USER_POST as post ON [USER].user_id = post.user_id WHERE [USER].user_id = {0}", user_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getUserPosts(DateTime dateTime1 , DateTime dateTime2)
        {
            string sql = String.Format("SELECT * FROM [dbo].[USER_POST] WHERE [USER_POST].[date] >= '{0}' AND [USER_POST].[date] <= '{1}'", dateTime1.ToString("MM/dd/yyyy"), dateTime2.ToString("MM/dd/yyyy"));
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPagePosts(DateTime dateTime1, DateTime dateTime2)
        {
            string sql = String.Format("SELECT * FROM [dbo].[PAGE_POST] WHERE date >= '{0}' AND date <= '{1}'", dateTime1.ToString("MM/dd/yyyy"), dateTime2.ToString("MM/dd/yyyy"));
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getUserPost(int user_id,int post_id)
        {
            string sql = String.Format("SELECT post.user_id,post.post_id,post.text,post.date,post.postLocation,post.no_of_likes FROM [USER] JOIN USER_POST as post ON [USER].user_id = post.user_id WHERE [USER].user_id = {0} AND post_id = {1}", user_id, post_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPagePosts(int page_id)
        {
            string sql = String.Format("SELECT post.page_id,post.page_post_id,post.date,post.postLocation,post.no_of_likes,post.text FROM [PAGE] JOIN PAGE_POST as post ON [PAGE].page_id = post.page_id WHERE [PAGE].page_id = {0}", page_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPagePost(int page_id,int page_post_id)
        {
            string sql = String.Format("SELECT post.page_id,post.page_post_id,post.date,post.postLocation,post.no_of_likes,post.text FROM [PAGE] JOIN PAGE_POST as post ON [PAGE].page_id = post.page_id WHERE [PAGE].page_id = {0} AND page_post_id = {1}", page_id,page_post_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPostComments(int user_id,int post_id)
        {
            string sql = String.Format("SELECT * FROM [dbo].[USER_POST_COMMENT] WHERE user_posted_id = {0} AND post_id = {1}", user_id, post_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPagePostComments(int page_id, int page_post_id)
        {
            string sql = String.Format("SELECT * FROM [dbo].[PAGE_POST_COMMENT] WHERE page_id = {0} AND page_post_id = {1}", page_id, page_post_id);
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getPageEvents(int page_id)
        {
            string sql = "SELECT * FROM [dbo].[EVENT] WHERE page_id = " + page_id;
            return dBManager.ExecuteReader(sql);
        }

        public int deleteEvent(int page_id, int event_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[EVENT] WHERE page_id = {0} AND event_id = {1} ", page_id, event_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deleteUser(int user_id)
        {
            string sql = "DELETE FROM [dbo].[USER] WHERE [user_id] = '" + user_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deletePage(int page_id)
        {
            string sql = "DELETE FROM [dbo].[PAGE] WHERE [page_id] = '" + page_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deleteComment(int user_posted_id, int post_id, int comment_id, int user_commented_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[USER_POST_COMMENT] WHERE user_posted_id = {0} AND post_id = {1} AND comment_id = {2} AND user_commented_id = {3}", user_posted_id, post_id, comment_id, user_commented_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deletePagePostComment(int user_commented_id, int page_id, int page_post_id, int page_post_comment_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[PAGE_POST_COMMENT] WHERE user_commented_id = {0} AND page_id = {1} AND page_post_id = {2} AND page_post_comment_id = {3}", user_commented_id, page_id, page_post_id, page_post_comment_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public System.Data.DataTable getAllUsers()
        {
            string sql = "SELECT [user_id],[name],[bio],[gender],[phone_no],[location],[birth_date],[personal_email] FROM [dbo].[USER]";
            return dBManager.ExecuteReader(sql);
        }

        public System.Data.DataTable getAllPages()
        {
            string sql = "SELECT [page_id],[name],[info],[location],[business_email],[phone_no] FROM [dbo].[PAGE]";
            return dBManager.ExecuteReader(sql);
        }

        public int getUsersCount()
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[USER]";
            return (int)dBManager.ExecuteScalar(sql);
        }

        public int getPagesCount()
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[PAGE]";
            return (int)dBManager.ExecuteScalar(sql);
        }
        public int getUsersPostsCount()
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[USER_POST]";
            return (int)dBManager.ExecuteScalar(sql);
        }

        public int getPagesPostsCount()
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[PAGE_POST]";
            return (int)dBManager.ExecuteScalar(sql);
        }

        public int getEventsCount()
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[EVENT]";
            return (int)dBManager.ExecuteScalar(sql);
        }

        public System.Data.DataTable getPageReviews(int page_id)
        {
            string sql = "SELECT Review.user_id,name,page_id,text FROM [dbo].[PAGE_REVIEWS] as Review join [dbo].[USER] as reviewer on Review.user_id = reviewer.user_id WHERE page_id = " + page_id;
            return dBManager.ExecuteReader(sql);
        }

        public int deleteReview(int user_id, int page_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[PAGE_REVIEWS] WHERE user_id = {0} AND page_id = {1}", user_id, page_id);
            return dBManager.ExecuteNonQuery(sql);
        }
       
        public int updateUser(int user_id,string name,string bio,string gender,string phone_no,string location,DateTime birth_date,string personal_email)
        {
            string sql = String.Format("UPDATE [dbo].[USER] SET [name] = '{1}',[bio] = '{2}',[gender] = '{3}',[phone_no] = '{4}',[location] = '{5}',[birth_date] = '{6}',[personal_email] = '{7}' WHERE [user_id]={0}",user_id,name,bio,gender,phone_no,location,birth_date,personal_email);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int updatePage(int page_id,string name, string info,string location,string business_email,string phone_no)
        {
            string sql = String.Format("UPDATE [dbo].[PAGE] SET [name] = '{1}',[info] = '{2}',[location] = '{3}',[business_email] = '{4}',[phone_no] = {5} WHERE [page_id] = {0}",page_id,name,info,location,business_email,phone_no);
            return dBManager.ExecuteNonQuery(sql);
        }
     
        public int deleteFriend(int user1_id, int user2_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[FRIENDS] WHERE (user1_id = {0} AND user2_id = {1}) OR (user1_id = {1} AND user2_id = {0}) ", user1_id, user2_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deletePost(int user_id, int post_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[USER_POST] WHERE user_id = {0} AND post_id = {1}", user_id, post_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int deletePagePost(int page_id,int page_post_id)
        {
            string sql = String.Format("DELETE FROM [dbo].[PAGE_POST] WHERE page_id = {0} AND page_post_id = {1}", page_id, page_post_id);
            return dBManager.ExecuteNonQuery(sql);
        }
    }
}
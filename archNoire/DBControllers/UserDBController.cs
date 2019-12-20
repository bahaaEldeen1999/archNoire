using archNoire.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace archNoire.DBControllers
{
    public class UserDBController
    {
        DBManager dBManager;
        public UserDBController()
        {
            dBManager = new DBManager();
        }


        public int insertUser( string name, string bio, string gender, string phone_no, string location, DateTime birth_date, string personal_email, string password)
        {
            string sql = String.Format("insert into [USER] values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", name, bio, gender, phone_no, location, birth_date.Date, personal_email, password);
            return dBManager.ExecuteNonQuery(sql);
        }



        public int insertGoingToEvent(int user_id, int page_id, int event_id)
        {
            string sql = String.Format("insert into [GOING_TO_EVENT] values({0},{1},{2})", user_id, page_id, event_id);
            return dBManager.ExecuteNonQuery(sql);
        }



        public int insertUserPost(int user_id, string text, DateTime date, string location, int no_of_likes)
        {
            string sql = String.Format("insert into [USER_POST] values({0},'{1}','{2}','{3}',{4})", user_id, text, date.Date, location, no_of_likes);
            return dBManager.ExecuteNonQuery(sql);
        }
    
        public int insertUserPostComment(int user_posted_id, int post_id, int user_commented, string text, int no_of_likes)
        {
            string sql = String.Format("insert into [USER_POST_COMMENT](user_posted_id,post_id,user_commented_id,text,no_of_likes) values({0},{1},{2},'{3}',{4})", user_posted_id, post_id, user_commented, text,no_of_likes);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostCommentLike(int user_commented_id, int user_posted_id, int post_id, int comment_id, int user_liked_id)
        {
            string sql = String.Format("insert into [USER_POST_COMMENT_LIKES] values({0},{1},{2},{3},{4})", user_commented_id, user_posted_id, post_id, comment_id, user_liked_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostLike(int user_posted_id, int post_id, int user_liked_id)
        {
            string sql = String.Format("insert into [USER_POST_LIKES] values({0},{1},{2})", user_posted_id, post_id, user_liked_id);
            return dBManager.ExecuteNonQuery(sql);
        }


        public int insertUserPhoto(int user_id, string source)
        {
            string sql = String.Format("insert into [USER_PHOTO] values({0},'{1}')", user_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostPhoto(int user_id, int post_id, string source)
        {
            string sql = String.Format("insert into [USER_POST_PHOTO](user_id,post_id,source) values({0},{1},'{2}')", user_id, post_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int insertUserFriend(int user_id1, int user_id2)
        {
            string sql = String.Format("insert into [FRIENDS] values({0},{1})", user_id1, user_id2);
            return dBManager.ExecuteNonQuery(sql);
        }
        // updates
        public int updateUserInfo(int userID,string name, string bio, string gender, string phone_no, string location, DateTime birth_date, string personal_email, string password)
        {
            string sql = String.Format("update [USER] set name ='{0}',bio = '{1}',gender = '{2}' , phone_no = '{3}',location = '{4}',birth_date = '{5}',personal_email='{6}',password='{7}' where user_id = {8} ", name,bio,gender,phone_no,location,birth_date.Date,personal_email,password,userID);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int UpdateUserBio(int user_id, string bio)
        {
            string sql = String.Format("update [USER] set bio = '{1}' where user_id = {0} ", user_id,bio);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int UpdateUserPhoto(int user_id, string source)
        {
            string sql = String.Format("update USER_PHOTO set source = '{1}' where user_id = {0} ", user_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updatePostNoOfLikes(int user_id, int post_id,int no)
        {
            string sql = String.Format("update USER_POST set no_of_likes = {1} where user_id = {0} and post_id = {2} ", user_id,no,post_id);
            return dBManager.ExecuteNonQuery(sql);
        }
        public int updateCommentNoOfLikes(int user_id, int post_id,int comment_id,int no)
        {
            string sql = String.Format("update USER_POST_COMMENT set no_of_likes = {1} where user_posted_id = {0} and post_id = {2} and comment_id = {3} ", user_id,no,post_id,comment_id);
            return dBManager.ExecuteNonQuery(sql);
        }


        // deletes
        public int Deleteuser(int user_id)
        {
            string sql = "DELETE FROM USER WHERE user_id = '" + user_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteuserPhoto(int user_id, int user_photo_id)
        {
            string sql = "DELETE FROM USER_PHOTO WHERE user_id = '" + user_id + "' AND user_photo_id = '" + user_photo_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteuserPost(int user_id, int post_id)
        {
            string sql = "DELETE FROM USER_POST WHERE user_id = '" + user_id + "' AND post_id = '" + post_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteuserPostLike(int user_posted_id, int user_liked_id, int post_id)
        {
            string sql = "DELETE FROM USER_POST_LIKES WHERE user_posted_id = '" + user_posted_id + "' AND user_liked_id = '" + user_liked_id + "' AND post_id = '" + post_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteuserPostComment(int user_posted_id, int post_id, int comment_id, int user_commented_id)
        {
            string sql = "DELETE FROM USER_POST_COMMENT WHERE user_posted_id = '" + user_posted_id + "' AND post_id = '" + post_id + "' AND comment_id = '" + comment_id + "' AND user_commented_id = '" + user_commented_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteuserPostCommentLike(int user_posted_id, int post_id, int comment_id, int user_commented_id, int user_liked_id)
        {
            string sql = "DELETE FROM USER_POST_COMMENT_LIKES WHERE user_liked_id = '" + user_liked_id + "' AND user_commented_id = '" + user_commented_id + "' AND user_posted_id = '" + user_posted_id + "' AND post_id = '" + post_id + "' AND comment_id = '" + comment_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeletePostPhoto(int user_id, int post_id, int user_post_photo_id)
        {
            string sql = "DELETE FROM USER_POST_PHOTO WHERE user_id = '" + user_id + "' AND post_id = '" + post_id + "' AND user_post_photo_id = '" + user_post_photo_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }

        public int DeleteFriend(int user1_id, int user2_id)
        {
            string sql = "DELETE FROM FRIENDS WHERE (user1_id = " + user1_id + " AND user2_id = " + user2_id + ") or (user1_id = " + user2_id + " AND user2_id = " + user1_id + ")";
            return dBManager.ExecuteNonQuery(sql);
        }

        // gets 
        public DataTable getUserInfo(string email,string password)
        {
            string sql = "select * from dbo.[USER] where dbo.[USER].personal_email = '" + email + "' and dbo.[USER].password ='" + password + "'";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getUserByEmail(string email)
        {
            string sql = "select * from dbo.[USER] where dbo.[USER].personal_email = '" + email + "' ";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getUserPhoto(int id)
        {
            string sql = "select * from dbo.[USER_PHOTO] where user_id = " +id;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getUserPosts(int id)
        {
            string sql = "select *   from dbo.[USER_POST] as u left join USER_POST_PHOTO as p on u.user_id = p.user_id and u.post_id = p.post_id  join [USER] as g on g.user_id = u.user_id join USER_PHOTO as d on d.user_id = u.user_id  where u.user_id =" + id;
            return dBManager.ExecuteReader(sql);
        }

        public DataTable getUserFromName(string name)
        {
            string sql = "select * from dbo.[USER] where name='" + name + "'";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getUserInfoFromId(int id)
        {
            string sql = "select * from dbo.[USER] where user_id=" + id ;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getIfUserIsFriend(int user1ID,int user2ID)
        {
            string sql = "select * from dbo.[FRIENDS] where (user1_id=" + user1ID + " and user2_id = "+user2ID+ " )or (user1_id = "+user2ID+ " and user2_id = "+ user1ID + ") ";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfLikesOfUserPost(int userID, int PostId)
        {
            string sql = "select * from  USER_POST_LIKES where user_posted_id = " + userID + "and  post_id = " + PostId;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getNoOfLikesOfUserComment(int user_posted_id, int PostId,int commentID)
        {
            string sql = "select * from  USER_POST_COMMENT_LIKES where user_posted_id = " + user_posted_id + "and  post_id = " + PostId + " and comment_id = "+commentID;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getPostComments(int user_posted_id, int PostId)
        {
            string sql = "select * from  USER_POST_COMMENT where user_posted_id = " + user_posted_id + " and  post_id = " + PostId;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getFriendsAndUsersPosts(int userID)
        {
            string sql = "select  * from USER_POST as f join FRiENDS on user1_id = "+userID+ " or user2_id = "+ userID + " left join USER_POST_PHOTO as p on p.post_id = f.post_id  where f.user_id = user1_id or f.user_id = user2_id ";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getUserPostFromDate(int userId,DateTime date)
        {
            string sql = "select * from USER_POST where user_id = "+userId+ " and date = '"+date.Date+"'" ;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getLastInsertedPost()
        {
            string sql = "SELECT TOP 1 post_id FROM USER_POST ORDER BY post_id DESC;";
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getIfUserLikePage(int userID, int pageID)
        {
            string sql = "select * from dbo.[PAGE_LIKES] where user_id=" + userID + " and page_id = " + pageID  ;
            return dBManager.ExecuteReader(sql);
        }
        public DataTable getEventsUserGoingTo(int userID)
        {
            string sql = "select * from dbo.[GOING_TO_EVENT] as g join EVENT as e on e.event_id = g.event_id left join EVENT_PHOTO as p on p.event_id = g.event_id where g.user_id=" + userID + " ";
            return dBManager.ExecuteReader(sql);
        }
    }
}
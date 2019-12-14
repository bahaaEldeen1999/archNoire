using System;
using System.Collections.Generic;
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


        public int insertUser(int user_id, string name, string bio, string gender, int phone_no, string location, string birth_date, string personal_email, string password)
        {
            string sql = String.Format("insert into [USER] values({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}')", user_id, name, bio, gender, phone_no, location, birth_date, personal_email, password);
            return dBManager.ExecuteNonQuery(sql);
        }

    

        public int insertGoingToEvent(int user_id, int page_id, int event_id)
        {
            string sql = String.Format("insert into [GOING_TO_EVENT] values({0},{1},{2})", user_id, page_id, event_id);
            return dBManager.ExecuteNonQuery(sql);
        }



        public int insertUserPost(int user_id, int post_id, string text, string date, string location, int no_of_likes)
        {
            string sql = String.Format("insert into [USER_POST] values({0},{1},'{2}','{3}','{4}',{5})", user_id, post_id, text, date, location, no_of_likes);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostComment(int user_posted_id,int post_id,int comment_id,int user_commented,string text,int no_of_likes)
        {
            string sql = String.Format("insert into [USER_POST_COMMENT] values({0},{1},{2},{3},'{4}',{5})", user_posted_id, post_id, comment_id, user_commented, text, no_of_likes);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostCommentLike(int user_commented_id,int user_posted_id,int post_id,int comment_id,int user_liked_id)
        {
            string sql = String.Format("insert into [USER_POST_COMMENT_LIKES] values({0},{1},{2},{3},{4})", user_commented_id, user_posted_id, post_id, comment_id, user_liked_id);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostLike(int user_posted_id,int post_id,int user_liked_id)
        {
            string sql = String.Format("insert into [USER_POST_LIKES] values({0},{1},{2})", user_posted_id, post_id, user_liked_id);
            return dBManager.ExecuteNonQuery(sql);
        }


        public int insertUserPhoto(int user_id, int user_photo_id, string source)
        {
            string sql = String.Format("insert into [USER_PHOTO] values({0},{1},'{2}')", user_id, user_photo_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }

        public int insertUserPostPhoto(int user_id, int post_id, int user_post_photo_id, string source)
        {
            string sql = String.Format("insert into [USER_POST_PHOTO] values({0},{1},{2},'{3}')", user_id, post_id, user_post_photo_id, source);
            return dBManager.ExecuteNonQuery(sql);
        }




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
            string sql = "DELETE FROM FRIENDS WHERE user1_id = '" + user1_id + "' AND user2_id = '" + user2_id + "'";
            return dBManager.ExecuteNonQuery(sql);
        }
    }
}
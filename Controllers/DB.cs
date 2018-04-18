using MVCADO.NET.Joke.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCADO.NET.Joke.Controllers
{
    public class DB
    {
        protected SqlConnection conn;
        //打开连接
        public bool OpenConnection()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Joke"].ConnectionString);
            try
            {
                bool result = true;
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                return result;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        //关闭连接
        public bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        //显示
        public List<Joke1> Select()
        {
            SqlDataReader sdr;
            List<Joke1> list = new List<Joke1>();
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand("Select * from Jokes", conn);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Joke1 j = new Joke1();
                        System.Diagnostics.Debug.WriteLine("ID:{0}\tcontent:{1}\tbelong:{2}\tstate:{3}\ttime:{4}", sdr["ID"], sdr["Content"], sdr["Belong"], sdr["State"], sdr["AddDate"]);
                        j.ID = Convert.ToInt32(sdr["ID"]);
                        j.Content = sdr["Content"].ToString();
                        j.Belong = sdr["Belong"].ToString();
                        j.State = Convert.ToInt32(sdr["State"]);
                        j.AddDate = Convert.ToDateTime(sdr["AddDate"]);

                        list.Add(j);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Joke details wrong:{0}", e);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        //Detail
        public Joke1 Detail(int? id)
        {
            SqlDataReader sdr;
            Joke1 j = new Joke1();
            System.Diagnostics.Debug.WriteLine("编号：{0}", id);
            string sql = "Select * from Jokes where ID = @ID";
            SqlParameter[] paras = new SqlParameter[]{//参数数组
                  new SqlParameter("@ID",System.Data.SqlDbType.Int)};
            paras[0].Value = id;//绑定ID
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(paras);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        System.Diagnostics.Debug.WriteLine("ID:{0}\tcontent:{1}\tbelong:{2}\tstate:{3}\ttime:{4}", sdr["ID"], sdr["Content"], sdr["Belong"], sdr["State"], sdr["AddDate"]);
                        j.ID = Convert.ToInt32(sdr["ID"]);
                        j.Content = sdr["Content"].ToString();
                        j.Belong = sdr["Belong"].ToString();
                        j.State = Convert.ToInt32(sdr["State"]);
                        j.AddDate = Convert.ToDateTime(sdr["AddDate"]);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Joke select wrong:{0}", e);
            }
            finally
            {
                conn.Close();
            }
            return j;

        }
        //Create
        public void Create(Joke1 joke)
        {
            //SqlDataReader sdr;
            //string sql = "insert into Jokes(Content,Belong,State,AddDate)values('" + joke.Content + "','" + joke.Belong + "','" + joke.State + "','" + joke.AddDate + "')";
            string sql = "insert into Jokes(Content,Belong,State,AddDate)values(@content,@belong,@state,@time)";
            SqlParameter[] paras = new SqlParameter[]{//参数数组
                  new SqlParameter("@content",System.Data.SqlDbType.VarChar),
                   new SqlParameter("@belong",System.Data.SqlDbType.VarChar),
                   new SqlParameter("@state",System.Data.SqlDbType.Int),
                    new SqlParameter("@time",System.Data.SqlDbType.DateTime)};
            paras[0].Value = joke.Content;//绑定内容
            paras[1].Value = joke.Belong;//绑定署名
            paras[2].Value = joke.State;//绑定状态
            paras[3].Value = joke.AddDate;//绑定时间
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(paras);
                    cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("插入成功！");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Joke create wrong:{0}", e);
            }
            finally
            {
                conn.Close();
            }
        }
        //Delete
        public int Delete(int? id)
        {
            string sql = "delete from Jokes where ID= @ID";
            SqlParameter[] paras = new SqlParameter[]{//参数数组
                  new SqlParameter("@ID",System.Data.SqlDbType.Int)};
            paras[0].Value = id;//绑定ID
            int i = 0;
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(paras);
                    i = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("插入成功！");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Joke create wrong:{0}", e);
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
        //update
        public int Update(Joke1 joke)
        {
            string sql = "update Jokes set Content = @content,Belong = @belong where ID= @ID";
            SqlParameter[] paras = new SqlParameter[]{//参数数组
                  new SqlParameter("@content",System.Data.SqlDbType.VarChar),
                   new SqlParameter("@belong",System.Data.SqlDbType.VarChar),
                   new SqlParameter("@ID",System.Data.SqlDbType.Int)};
            paras[0].Value = joke.Content;//绑定内容
            paras[1].Value = joke.Belong;//绑定署名
            paras[2].Value = joke.ID;//绑定ID
            System.Diagnostics.Debug.WriteLine("SQL语句：{0}", sql);
            int i = 0;
            try
            {
                if (conn.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(paras);
                    i = cmd.ExecuteNonQuery();
                    System.Diagnostics.Debug.WriteLine("更新成功！");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Joke update wrong:{0}", e);
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
    }
}
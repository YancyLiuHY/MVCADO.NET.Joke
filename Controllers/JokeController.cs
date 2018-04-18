using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using MVCADO.NET.Joke.Models;
using System.Collections;
using System.Net;

namespace MVCADO.NET.Joke.Controllers
{
    public class JokeController : Controller
    {
        private DB _db = new DB();
        // GET: Joke
        public ActionResult Index()
        {
            //_db.OpenConnection();

            List<Joke1> list = new List<Joke1>();
            //{
            //    new Joke1 { ID = 1, Content = "笑哈哈", Belong = "yancy", State = 1 },
            //    new Joke1 { ID = 1, Content = "笑哈哈", Belong = "yancy", State = 1 },
            //    new Joke1 { ID = 1, Content = "笑哈哈", Belong = "yancy", State = 1 },
            //};

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Joke"].ConnectionString);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("Select * from Jokes", conn);
            //SqlDataReader sdr = cmd.ExecuteReader();
            //while (sdr.Read()) {
            //    Joke1 j = new Joke1();
            //    System.Diagnostics.Debug.WriteLine("ID:{0}\tcontent:{1}\tbelong:{2}\tstate:{3}\ttime:{4}",sdr["ID"], sdr["Content"], sdr["Belong"], sdr["State"], sdr["AddDate"]);
            //    j.ID = Convert.ToInt32(sdr["ID"]);
            //    j.Content = sdr["Content"].ToString();
            //    j.Belong = sdr["Belong"].ToString();
            //    j.State = Convert.ToInt32(sdr["State"]);
            //    j.AddDate = Convert.ToDateTime(sdr["AddDate"]);

            //    list.Add(j);
            //}
            //conn.Close();
            _db.OpenConnection();
            return View(_db.Select());
        }

        // GET: Joke/Details/5
        public ActionResult Details(int? id)
        {
            _db.OpenConnection();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joke1 joke = _db.Detail(id);
            if (joke == null)
            {
                return HttpNotFound();
            }
            
            return View(joke);
        }

        // GET: Joke/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Joke/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Content,State,Belong,AddDate")] Joke1 joke)
        {
            try
            {
                // TODO: Add insert logic here
                joke.State = 1;
                joke.AddDate = System.DateTime.Now;
                _db.OpenConnection();
                _db.Create(joke);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Joke/Edit/5
        public ActionResult Edit(int? id)
        {
            _db.OpenConnection();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joke1 joke = _db.Detail(id);
            if (joke == null)
            {
                return HttpNotFound();
            }

            return View(joke);
        }

        // POST: Joke/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Content,State,Belong,AddDate")] Joke1 joke,int id)
        {
            try
            {
                // TODO: Add update logic here
                joke.ID = id;
                _db.OpenConnection();
                _db.Update(joke);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Joke/Delete/5
        public ActionResult Delete(int? id)
        {
            _db.OpenConnection();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joke1 joke = _db.Detail(id);
            if (joke == null)
            {
                return HttpNotFound();
            }

            return View(joke);
        }

        // POST: Joke/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            int i = 0;
            try
            {
                // TODO: Add delete logic here
                _db.OpenConnection();
                i=_db.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(i);
            }
        }
    }
    //sql帮助类
    //public class SqlDB {
    //    protected SqlConnection conn;
    //    //打开连接
    //    public bool OpenConnection() {
    //        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Joke"].ConnectionString);
    //        try {
    //            bool result = true;
    //            if (conn.State.ToString()!="Open") {
    //                conn.Open();
    //            }
    //            return result;
    //        }
    //        catch(SqlException ex){
    //            return false;
    //        }
    //    }
    //    //关闭连接
    //    public bool CloseConnection() {
    //        try {
    //            conn.Close();
    //            return true;
    //        }
    //        catch (SqlException ex) {
    //            return false;
    //        }
    //    }
    //    //显示
    //    public List<Joke1> Select()
    //    {
    //        SqlDataReader sdr;
    //        List<Joke1> list = new List<Joke1>();
    //        try
    //        {
    //            if (conn.State.ToString() == "Open")
    //            {
    //                SqlCommand cmd = new SqlCommand("Select * from Jokes", conn);
    //                sdr = cmd.ExecuteReader();
    //                while (sdr.Read())
    //                {
    //                    Joke1 j = new Joke1();
    //                    System.Diagnostics.Debug.WriteLine("ID:{0}\tcontent:{1}\tbelong:{2}\tstate:{3}\ttime:{4}", sdr["ID"], sdr["Content"], sdr["Belong"], sdr["State"], sdr["AddDate"]);
    //                    j.ID = Convert.ToInt32(sdr["ID"]);
    //                    j.Content = sdr["Content"].ToString();
    //                    j.Belong = sdr["Belong"].ToString();
    //                    j.State = Convert.ToInt32(sdr["State"]);
    //                    j.AddDate = Convert.ToDateTime(sdr["AddDate"]);

    //                    list.Add(j);
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine("Joke details wrong:{0}", e);
    //        }
    //        finally {
    //           conn.Close();
    //        }
    //        return list;
    //    }
    //    //Detail
    //    public Joke1 Detail(int? id) {
    //        SqlDataReader sdr;
    //        Joke1 j = new Joke1();
    //        System.Diagnostics.Debug.WriteLine("编号：{0}",id);
    //        SqlParameter[] paras = new SqlParameter[]{//参数数组
    //              new SqlParameter("@id",System.Data.SqlDbType.Int,50)};
    //        paras[0].Value = id;//绑定ID
    //        try
    //        {
    //            if (conn.State.ToString() == "Open")
    //            {
    //                SqlCommand cmd = new SqlCommand("Select * from Jokes where ID ="+id, conn);
    //                sdr = cmd.ExecuteReader();
    //                while (sdr.Read())
    //                {
                        
    //                    System.Diagnostics.Debug.WriteLine("ID:{0}\tcontent:{1}\tbelong:{2}\tstate:{3}\ttime:{4}", sdr["ID"], sdr["Content"], sdr["Belong"], sdr["State"], sdr["AddDate"]);
    //                    j.ID = Convert.ToInt32(sdr["ID"]);
    //                    j.Content = sdr["Content"].ToString();
    //                    j.Belong = sdr["Belong"].ToString();
    //                    j.State = Convert.ToInt32(sdr["State"]);
    //                    j.AddDate = Convert.ToDateTime(sdr["AddDate"]);
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            System.Diagnostics.Debug.WriteLine("Joke select wrong:{0}", e);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //        return j;

    //    }
    //    //Create
    //    public void Create(Joke1 joke) {
    //        //SqlDataReader sdr;
    //        string sql = "insert into Jokes(Content,Belong,State,AddDate)values('"+joke.Content+ "','" + joke.Belong + "','" + joke.State + "','" + joke.AddDate + "')";
    //        try
    //        {
    //            if (conn.State.ToString() == "Open")
    //            {
    //                SqlCommand cmd = new SqlCommand(sql, conn);
    //                cmd.ExecuteNonQuery();
    //                System.Diagnostics.Debug.WriteLine("插入成功！");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine("Joke create wrong:{0}", e);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }
    //    //Delete
    //    public int Delete(int? id) {
    //        string sql = "delete from Jokes where ID="+id;
    //        int i=0;
    //        try
    //        {
    //            if (conn.State.ToString() == "Open")
    //            {
    //                SqlCommand cmd = new SqlCommand(sql, conn);
    //                i=cmd.ExecuteNonQuery();
    //                System.Diagnostics.Debug.WriteLine("插入成功！");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine("Joke create wrong:{0}", e);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //        return i;
    //    }
    //    //update
    //    public int Update(Joke1 joke) {
    //        string sql = "update Jokes set Content='"+joke.Content+"',Belong='"+joke.Belong+"' where ID=" + joke.ID;
    //        System.Diagnostics.Debug.WriteLine("SQL语句：{0}",sql);
    //        int i = 0;
    //        try
    //        {
    //            if (conn.State.ToString() == "Open")
    //            {
    //                SqlCommand cmd = new SqlCommand(sql, conn);
    //                i = cmd.ExecuteNonQuery();
    //                System.Diagnostics.Debug.WriteLine("更新成功！");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine("Joke update wrong:{0}", e);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //        return i;
    //    }
    //}
}

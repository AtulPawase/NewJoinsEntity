using NewJoinsEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewJoinsEntity.Controllers
{
    public class HomeController : Controller
    {

        AtulDatabaseEntities db = new AtulDatabaseEntities();
        // GET: Home
        public ActionResult Index()
        {
            var InnerJoin = (from p in db.Student1
                             join f in db.Departments on p.Department equals f.DepartmentName
                             select p).ToList();

            return View(InnerJoin);
        }

        public ActionResult OuterJoin()
        {
            List<Outerjoin> OuterJoin = new List<Outerjoin>();
            var data = (from p in db.Student1
                        join f in db.Departments on p.Department equals f.DepartmentName
                        into pf
                        from subStudDept in pf.DefaultIfEmpty()
                            // group subStudentDept by p.Department into grouped
                        select new Outerjoin
                        {
                            ID = p.ID,
                            Name = p.Name,
                            Email = p.Email,
                            Phone = p.Phone,
                            Department = subStudDept.DepartmentName

                            //Department = subStudentDept != null ? subStudentDept : null
                        }).ToList();

            //var data = (from p in db.Student1
            //            join f in db.Departments on p.Department equals f.DepartmentName
            //            into pf
            //            from subStudDept in pf.DefaultIfEmpty()
            //                // group subStudentDept by p.Department into grouped
            //            select new
            //            {

            //                p.Name,
            //                p.Email,
            //                p.Phone,
            //                Department


            //                //Department = subStudentDept != null ? subStudentDept : null
            //            }).ToList();

            return View(data);
        }
    }
}
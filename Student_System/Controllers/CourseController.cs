using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Student_System.Controllers
{
    public class CourseController : Controller
    {
        StudentDBEntities DB = new StudentDBEntities();


        public ActionResult Index(String Searching)
        {
            var model = DB.Courses.AsQueryable();
            if(!String.IsNullOrEmpty(Searching))
            {
                model = model.Where(s => (s.Cname.Contains(Searching) || s.Code.ToString().Contains(Searching)));
            }
            return View("All",model);
        }
        // GET: Student
        public ActionResult Edit(int id)
        {
            var course = DB.Courses.Where(r => r.Code == id).FirstOrDefault();
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Cours course)
        {
            var c = DB.Courses.Where(r => r.Code== course.Code).FirstOrDefault();
            c.Cname = course.Cname;
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cours course)
        {
            DB.Courses.Add(course);
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult Delete(int id)
        {
            var course = DB.Courses.Where(r => r.Code== id).FirstOrDefault();
            return View(course);
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCourse(int id)
        {
            var course = DB.Courses.Where(r => r.Code == id).FirstOrDefault();
            DB.Courses.Remove(course);
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult All()
        {
            List<Cours> model = DB.Courses.ToList();
            return View(model);

        }
    }
}
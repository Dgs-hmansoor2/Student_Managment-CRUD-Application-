using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_System.Controllers
{
    public class StudentCourseController : Controller
    {
        StudentDBEntities DB = new StudentDBEntities();
        // GET: StudentCourse

        public ActionResult Index(String Searching)
        {
            var model = DB.StudentCourses.AsQueryable();
            if (!String.IsNullOrEmpty(Searching))
            {
                model = model.Where(s => (s.Cours.Cname.Contains(Searching)
                || s.Grade.Contains(Searching)
                || s.Cours.Code.ToString().Contains(Searching)
                || s.Student.Fname.Contains(Searching)
                || s.Student.Lname.Contains(Searching)
                || s.Student.Id.ToString().Contains(Searching)
                ));
            }

            foreach(var item in model)
            {
                if (item.Grade == null)
                    item.Grade = "-";
            }

            DB.SaveChanges();
            return View("All", model);
        }

        public ActionResult All()
        {
            var model = DB.StudentCourses.ToList();
            foreach (var item in model)
            {
                if (item.Grade == null)
                    item.Grade = "-";
            }

            DB.SaveChanges();
            return View(model);
        }

        public ActionResult Edit(int id,int code)
        {
            var model = DB.StudentCourses.Where(r => (r.Cours.Code == code)).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StudentCourse sc)
        {
            var c = DB.StudentCourses.Where(r => (r.CourseCode == sc.CourseCode) && (r.StudentId==sc.StudentId)).FirstOrDefault();
            c.Grade = sc.Grade;
            //DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult Create()
        {
            var model = DB.StudentCourses.ToList();
            List<Student> studentList = new List<Student>();
            List<Cours> courseList = new List<Cours>();

            studentList = DB.Students.ToList();
            courseList = DB.Courses.ToList();

            ViewData["studentList"] = studentList;
            ViewData["courseList"] = courseList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentCourse sc)
        {

            int code = int.Parse(Request.Form["Code"]);
            int StudentId = int.Parse(Request.Form["StudentId"]);
            String Grade = sc.Grade;

            var c = DB.StudentCourses.Where(r => (r.CourseCode == code) && (r.StudentId == StudentId)).FirstOrDefault();
            
            if (c==null)
                DB.StudentCourses.Add(new StudentCourse { CourseCode=code,StudentId=StudentId,Grade=Grade});
            
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult Delete(int id, int code)
        {
            var model = DB.StudentCourses.Where(r => (r.Cours.Code == code) && (r.Student.Id == id)).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCourse(int id,int code)
        {
            var model = DB.StudentCourses.AsQueryable();
            var enrollment = model.Where(r => (r.CourseCode == code && r.StudentId == id    )).FirstOrDefault();

            if(enrollment!=null)
                DB.StudentCourses.Remove(enrollment);
            DB.SaveChanges();
            return RedirectToAction("All");
        }


    }
}
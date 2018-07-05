using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_System.Controllers
{
    public class HomeController : Controller
    {
        StudentDBEntities DB = new StudentDBEntities();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult AllStudents()
        {
            List<Student> model = DB.Students.ToList();
            return PartialView("_Student", model);

        }

        public PartialViewResult AllCourses()
        {
            List<Cours> model = DB.Courses.ToList();
            return PartialView("_Course", model);
        }

        
            public PartialViewResult CourseWiseStudent()
        {
            List<StudentCourse> model = DB.StudentCourses.ToList();
            return PartialView("_StudentCourse", model);
        }


    }
}
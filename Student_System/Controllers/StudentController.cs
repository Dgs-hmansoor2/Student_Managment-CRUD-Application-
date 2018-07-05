using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_System.Controllers
{
    public class StudentController : Controller
    {
        StudentDBEntities DB = new StudentDBEntities();

        public ActionResult Index(String Searching)
        {
            var model = DB.Students.AsQueryable();
            if (!String.IsNullOrEmpty(Searching))
            {
                model = model.Where(s => 
                (s.Id.ToString().Contains(Searching)||
                s.Age.ToString().Contains(Searching) ||
                s.Email.Contains(Searching) ||
                s.Fname.Contains(Searching) ||
                s.Lname.Contains(Searching)));
            }
            return View("All", model);
        }
        // GET: Student
        public ActionResult Edit(int Id)
        {
            var student = DB.Students.Where(r => r.Id == Id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            var s = DB.Students.Where(r => r.Id == student.Id).FirstOrDefault();
            s.Id = student.Id;
            s.Email = student.Email;
            s.Lname = student.Lname;
            s.Fname = student.Fname;
            s.Age = student.Age;
            DB.SaveChanges();
            return RedirectToAction("All"); 
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Student s)
        {
            DB.Students.Add(s);
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult Delete(int id)
        {
            var student = DB.Students.Where(r => r.Id == id).FirstOrDefault();
            return View(student);
        }

        
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteStudent(int id)
        {
            var student = DB.Students.Where(r => r.Id == id).FirstOrDefault();
            DB.Students.Remove(student);
            DB.SaveChanges();
            return RedirectToAction("All");
        }

        public ActionResult All()
        {
            List<Student> model = DB.Students.ToList();
            return View( model);

        }
    }
}
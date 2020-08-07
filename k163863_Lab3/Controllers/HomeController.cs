using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Linq;
using k163863_Lab3.Models;

namespace k163863_Lab3.Controllers
{
    public class HomeController : ApiController
    {
        string GlobalDataFile = HttpContext.Current.Server.MapPath("~/courses.xml");

        public HttpResponseMessage GetCourses()
        {
            IEnumerable<XElement> allCourses = from courses in XDocument.Load(GlobalDataFile)
                                            .Element("Courses").Elements("Course")
                                               orderby (int)courses.Attribute("code") descending
                                            select courses;
            List<Course> response = new List<Course>();
            foreach (XElement res in allCourses)
            {
                Course c = new Course();
                c.code = res.Attribute("code").Value;
                c.name = res.Attribute("name").Value;
                c.desc = res.Element("description").Value;
                response.Add(c);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        public HttpResponseMessage GetCoursesDescription()
        {
            IEnumerable<XElement> allCourses = from courses in XDocument.Load(GlobalDataFile)
                                            .Element("Courses").Elements("Course")
                                               select courses;

            List<Course> response = new List<Course>();
            foreach (XElement res in allCourses)
            {
                Course c = new Course();
                c.code = res.Attribute("code").Value;
                c.name = res.Attribute("name").Value;
                c.desc = res.Element("description").Value;
                response.Add(c);
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [System.Web.Http.AcceptVerbs("Delete", "GET")]
        public HttpResponseMessage DeleteCourse(int id)
        {
            XDocument document = XDocument.Load(GlobalDataFile);
            var deleteQuery = from r in document.Descendants("Course")
                              where (string)r.Attribute("code") == Convert.ToString(id)
                              select r;
            deleteQuery.Remove();
            document.Save(GlobalDataFile);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage GetCoursesWithThreshold(int id)
        {
            IEnumerable<XElement> response = from courses in XDocument.Load(GlobalDataFile)
                                        .Descendants("Course")
                                             where (int)courses.Element("StudentsEnrolled") <= id
                                             select courses;

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
    }
}
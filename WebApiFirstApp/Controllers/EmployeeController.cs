using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiFirstApp.Models;

namespace WebApiFirstApp.Controllers
{
    public class EmployeeController : ApiController
    {
        //public HttpResponseMessage Get()
        //{
        //    using (EmployeeDBContext dbcontext = new EmployeeDBContext())
        //    {
        //        var Employees = dbcontext.EMPLOYEES.ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, Employees);
        //    }
        //}
       // [HttpGet]
        public IEnumerable<EMPLOYEE> GetEmployees()
        {
            EmployeeDBContext dBContext = new EmployeeDBContext();
            return dBContext.EMPLOYEES.ToList();
        }

        //    public HttpResponseMessage Get(string gender = "All")
        //{
        //    using(EmployeeDBContext dBContext = new EmployeeDBContext())
        //    {
        //        switch(gender.ToLower())
        //        {
        //            case "all":
        //                return Request.CreateResponse(HttpStatusCode.OK, dBContext.EMPLOYEES.ToList());

        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK, dBContext.EMPLOYEES.Where(e => e.Gender.ToLower() == "male").ToList());
        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK, dBContext.EMPLOYEES.Where(e => e.Gender.ToLower() == "female").ToList());

        //            default:
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, "Value for gender must be Male , Female or All. "+ gender+ " is invalid");


        //        }
        //    }
        //}

        //[HttpGet]
        //public HttpResponseMessage LoadAllEmployees()
        //{
        //    using(EmployeeDBContext dbcontext = new EmployeeDBContext())
        //    {
        //        var Employees = dbcontext.EMPLOYEES.ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, Employees);
        //    }
        //}

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBContext dbcontext = new EmployeeDBContext())
            {
                var employee = dbcontext.EMPLOYEES.FirstOrDefault(e => e.ID == id);

                if(employee!= null )
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "not Found in the Database");
                }
            }
        }

        //public void Post([FromBody] EMPLOYEE employee)  //basic Post requiest using Fiddler
        //{
        //    using (EmployeeDBContext dBContext = new EmployeeDBContext())
        //    {
        //        dBContext.EMPLOYEES.Add(employee);
        //        dBContext.SaveChanges();
        //    }
        //}

        public HttpResponseMessage Post([FromBody] EMPLOYEE employee) 
        {
            try { 
            using (EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                dBContext.EMPLOYEES.Add(employee);
                dBContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
            }

            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public void Put(int id, [FromBody]EMPLOYEE employee)
        {
            using(EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                var entity = dBContext.EMPLOYEES.FirstOrDefault(e => e.ID == id);
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Gender = employee.Gender;
                entity.Salary = employee.Salary;
                dBContext.SaveChanges();
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBContext dBContext = new EmployeeDBContext())
                {
                    var employee = dBContext.EMPLOYEES.FirstOrDefault(e => e.ID == id);
                    if (employee == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " is Not Found to Delete ");
                    }

                    else
                    {
                        dBContext.EMPLOYEES.Remove(employee);
                        dBContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }

            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
           
        }
    }
}

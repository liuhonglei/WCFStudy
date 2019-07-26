using Service.Interface1001;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Service1001
{
    public class EmployeesService : IEmployees
    {
        private static List<Employee> employees = new List<Employee>() {
            new Employee {   Id = "001", Name = "张三",  Department = "部门1", Grade = "100" },
            new Employee {   Id = "002", Name = "张四",  Department = "部门2", Grade = "96" },
        };


        public void Create(Employee employee)
        {
            employees.Add(employee);
        }

        public void Delete(string id)
        {
            Employee employee = Get(id);
            if (employee != null)
            {
                employees.Remove(employee);    
            }
        }

        public Employee Get(string id)
        {
           Employee employee =  employees.FirstOrDefault(p => p.Id == id);
            if (null == employee)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            WebOperationContext.Current.OutgoingResponse.SetETag(employee.GetHashCode());
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            int hasCode = employees.GetHashCode();
            WebOperationContext.Current.IncomingRequest.CheckConditionalRetrieve(hasCode);
            WebOperationContext.Current.OutgoingResponse.SetETag(hasCode);
            return employees;
        }

        public void Op1(Stream stream)
        {
            
        }

        public void Op2(Message stream)
        {
           
        }

        public void Op3(string[] stream)
        {
            
        }

        public void Op4(string[] args)
        {
            
        }

        public void Op5(string[] args)
        {
            
        }

        public void Op6()
        {
            
        }

        public void Op7(string arg)
        {
            
        }

        public void Update(Employee employee)
        {
            var existing = employees.FirstOrDefault(  p =>p.Id == employee.Id );
            if (null == existing)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }
            //并发修改
            employee.Name += Guid.NewGuid().ToString();

            WebOperationContext.Current.IncomingRequest.CheckConditionalUpdate(employee.GetHashCode());

            employees.Remove(existing);
            this.Delete(employee.Id);
            employees.Add(employee);
            WebOperationContext.Current.OutgoingResponse.SetETag(employee.GetHashCode());
        }
    }
}

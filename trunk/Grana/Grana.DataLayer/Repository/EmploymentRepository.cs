using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;

namespace Grana.DataLayer.Repository
{
    public class EmploymentRepository : BaseRepository, IEmploymentRepository
    {
        public void UpdateEmployment(Employment employment)
        {
            Employment originalEmp = GetEmployment(employment.EmploymentId);

            originalEmp.DirectDebit = employment.DirectDebit;
            originalEmp.EmployersName = employment.EmployersName;
            originalEmp.EmploymentStatus = employment.EmploymentStatus;
            originalEmp.IncomeFrequency = employment.IncomeFrequency;
            originalEmp.Industry = employment.Industry;
            originalEmp.MonthlyIncome = employment.MonthlyIncome;
            originalEmp.PayDate = employment.PayDate;
            originalEmp.Position = employment.Position;
            originalEmp.TimeAtEmployer = employment.TimeAtEmployer;
            originalEmp.WorkPhone = employment.WorkPhone;
            
            context.SubmitChanges();
        }

        public Employment GetEmployment(int employmentId)
        {
            Employment emp = (from employment in context.Employments
                                         where employment.EmploymentId == employmentId
                                         select employment).FirstOrDefault();

            return emp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FactoryModel
{
    public class users
    {
        public string OFFLINE { get; set; }
        public string USERID { get; set; }
        public string IUSERID { get; set; }
        public string REPORTTOROLEID { get; set; }
        public string CODE { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string PASSWORD { get; set; }
        public string USERNAME { get; set; }

        public string FNAME { get; set; }
        public string MNAME { get; set; }
        public string LNAME { get; set; }

        public string EMAIL { get; set; }
        public string MOBILE { get; set; }
        public string TELEPHONE { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string PIN { get; set; }
        public string USERTYPE { get; set; }

        public string DEPARTMENTID { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string ISACTIVE { get; set; }

        public string UTNAME { get; set; }
        public string REPORTINGTOID { get; set; }
        public string REPORTINGTONAME { get; set; }

        public string TPU { get; set; }
        public string APPLICABLETO { get; set; }
        public string REFERENCEID { get; set; }
        public string HQID { get; set; }
        public string HQNAME { get; set; }
        public string USERTAG { get; set; }
        public string DOB { get; set; }
        public string ANVDATE { get; set; }
        public string FULLDESCRIPTION { get; set; }
        public string FINYEAR { get; set; }
    }
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

    }

    public class Finyear
    {
        public int id { get; set; }
        public string FinYear { get; set; }
    }
    public class Finyearrange
    {
        public string frmyr { get; set; }
        public string toyr { get; set; }
        public string currentdt { get; set; }
        public string currentdt1 { get; set; }
        public string frmdate { get; set; }
        public string todate { get; set; }
        public string today { get; set; }
        public string TPU { get; set; }
        public string HO { get; set; }
    }



}

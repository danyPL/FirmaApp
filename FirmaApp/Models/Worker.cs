using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaApp.Models
{
    internal class Worker
    {
        public int id_worker { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string role_name { get; set; }
        public int id_role {  get; set; }
        public int age { get; set; }
        public DateTime hire_date { get; set; }
        public bool is_working { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaApp.Models
{
    internal class Note
    {
        public int id_note { get; set; }
        public int id_worker { get; set; }
        public string content { get; set; }
        public DateTime added_at { get; set; }
    }
}

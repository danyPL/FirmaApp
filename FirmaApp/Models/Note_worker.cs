using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaApp.Models
{
    internal class Note_worker
    {
        public int id_note { get; set; }
        public string WorkerName { get; set; }
        public string Content { get; set; }
        public DateTime AddedAt { get; set; }
    }
}

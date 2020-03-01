using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Deposita
    {
        [Key]
        public string UserName { get; set; }
        public int Deposit { get; set; }
         
        public int withdrow { get; set; }
        public int current_balance { get; set; }
        public DateTime DateTime { get; set; }
    }
}

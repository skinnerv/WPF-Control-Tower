using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower01.Models
{
    class LoginUser
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginUserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}

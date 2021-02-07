using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSystem.Models
{
    public class Detail
    {

        #region Properties

        [Key]
        public int ID { get; set; }
        public String Fname { get; set; }
        public String Lname { get; set; }
        public int Age { get; set; }

        public String Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        #endregion


    }
}

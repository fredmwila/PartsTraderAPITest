using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartsTraderApi
{
    public class Part
    {

        [Key]
        public string partNumber { get; set; }

        public string description{ get; set; }
    }
}

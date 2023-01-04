using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWordService
{
    public class Text
    {
        [Key]
        public int TextId { get; set; }

        public string Word { get; set; }
    }
}

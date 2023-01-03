using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public class TextContext: DbContext
    {

        public DbSet<Text> Texts { get; set; }

      
        public TextContext(DbContextOptions<TextContext> options) : base(options)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService;

namespace RandomWordService
{

    public interface ITextService
    {
         void CreateWord();
    }
    public class TextService : ITextService
    {
        private readonly TextContext _context;
        private readonly ILogger<TextService> _logger;


        public TextService( ILogger<TextService> logger , TextContext context ) {
        
           _logger= logger;
           _context = context;
        }


        public void  CreateWord()
        {
            var text = new Text();

            var LowerAllowedChars = "abcçdefgğhiıjklmnoöprsştuüvyz";
            var UpperAllowedChars = "ABCÇDEFGĞHIJKLMNOÖPRSTUÜVXYZ";

            Random random = new Random();

            char[] chars = new char[10];

            for (int i = 0; i < 10; i += 2)
            {
                chars[i] = LowerAllowedChars[random.Next(0, LowerAllowedChars.Length)];
                chars[i += 1] = UpperAllowedChars[random.Next(0, UpperAllowedChars.Length)];

                i = i - 1;
            }

            string charToString = new string(chars);
            text.Word = charToString;

            _context.Add(text);
            _context.SaveChanges();

            _logger.LogInformation("Word created and saved db.");

        }
    }
}

using BackEnd.DAL;
using BackEnd.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL
{
    public class BALEmail
    {
        private readonly DALEmail _dalEmail;

        public BALEmail(DALEmail dalEmail)
        {
            _dalEmail = dalEmail;
        }

        public void SendEmail(EmailDto request)
        {
            _dalEmail.SendEmail(request);
        }

    }
}

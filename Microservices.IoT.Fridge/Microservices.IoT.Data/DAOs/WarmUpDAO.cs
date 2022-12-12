using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs
{
    public class WarmUpDAO : DAOBase
    {
        public WarmUpDAO(IDbContextFactory<MicroservicesContext> factory) : base(factory)
        {
        }

        public void WarmUp()
        {
            using (var db = DB)
            {
                var query =
                    (from item in db.FRIDGE
                     select 1);
                    var result = query.ToList();
            }
        }
    }
}

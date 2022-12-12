using Microservices.IoT.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Microservices.IoT.Data.DAOs
{
    public abstract class DAOBase
    {
        private readonly IDbContextFactory<MicroservicesContext> factory;

        protected DAOBase(IDbContextFactory<MicroservicesContext> factory)
        {
            this.factory = factory;
        }

        protected MicroservicesContext DB => factory.CreateDbContext();
    }
}
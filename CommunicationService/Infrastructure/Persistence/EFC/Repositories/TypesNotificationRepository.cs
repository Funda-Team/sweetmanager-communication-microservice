using CommunicationService.Domain.Model.Entities;
using CommunicationService.Domain.Repositories;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Configuration;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CommunicationService.Infrastructure.Persistence.EFC.Repositories
{
    public class TypesNotificationRepository(CommunicationContext context) : BaseRepository<TypesNotification>(context), ITypesNotificationRepository
    {
        public async Task<bool> FindByNameAsync(string name)
            => await Context.Set<TypesNotification>().AnyAsync(t => t.Name.Equals(name));

    }
}

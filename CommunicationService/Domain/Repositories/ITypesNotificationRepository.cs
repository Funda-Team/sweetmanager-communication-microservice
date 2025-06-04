using CommunicationService.Domain.Model.Entities;
using IamService.Shared.Domain.Repositories;

namespace CommunicationService.Domain.Repositories
{
    public interface ITypesNotificationRepository: IBaseRepository<TypesNotification>
    {
        Task<bool> FindByNameAsync(string name);

    }
}

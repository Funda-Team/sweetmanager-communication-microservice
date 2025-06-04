
using CommunicationService.Domain.Model.Queries.TypesNotification;

namespace CommunicationService.Domain.Services.TypesNotification
{
    public interface ITypesNotificationQueryService
    {
        Task<IEnumerable<Model.Entities.TypesNotification>> Handle(GetAllTypesNotificationsQuery query);
        Task<Model.Entities.TypesNotification?> Handle(GetTypesNotificationByIdQuery query);
    }
}

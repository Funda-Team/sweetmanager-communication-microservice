using CommunicationService.Domain.Model.Queries.Notification;

namespace CommunicationService.Domain.Services.Notification
{
    public interface INotificationQueryService
    {
        Task<IEnumerable<Model.Aggregates.Notification>> Handle(GetAllNotificationsQuery query);

        Task<Model.Aggregates.Notification?> Handle(GetNotificationByIdQuery query);

        Task<IEnumerable<Model.Aggregates.Notification>> Handle(GetAllNotificationsBySenderIdQuery query);

    }
}

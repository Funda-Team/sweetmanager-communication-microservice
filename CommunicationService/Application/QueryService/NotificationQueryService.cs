using CommunicationService.Domain.Model.Aggregates;
using CommunicationService.Domain.Model.Queries.Notification;
using CommunicationService.Domain.Repositories;
using CommunicationService.Domain.Services.Notification;

namespace CommunicationService.Application.QueryService
{
    public class NotificationQueryService(INotificationRepository notificationRepository) : INotificationQueryService
    {
        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsQuery query)
        {
            return await notificationRepository.FindAllByHotelIdAsync(query.HotelId);
        }

        public async Task<Notification?> Handle(GetNotificationByIdQuery query)
        {
            return await notificationRepository.FindByIdAsync(query.Id);
        }

        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsBySenderIdQuery query)
        {
            return await notificationRepository.FindAllBySenderIdAsync(query.SenderId);
        }

    }
}

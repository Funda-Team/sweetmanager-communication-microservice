using CommunicationService.Domain.Model.Aggregates;
using IamService.Shared.Domain.Repositories;

namespace CommunicationService.Domain.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> FindByTypeNotificationIdAsync(int typeNotificationId);

        Task<IEnumerable<Notification>> FindAllByHotelIdAsync(int hotelId);

        Task<IEnumerable<Notification>> FindAllBySenderIdAsync(int senderId);

    }
}

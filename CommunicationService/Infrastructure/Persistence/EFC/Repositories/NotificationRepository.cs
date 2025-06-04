using CommunicationService.Domain.Model.Aggregates;
using CommunicationService.Domain.Repositories;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Configuration;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Repositories;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;

namespace CommunicationService.Infrastructure.Persistence.EFC.Repositories
{
    public class NotificationRepository(CommunicationContext context) : BaseRepository<Notification>(context), INotificationRepository
    {
        public async Task<IEnumerable<Notification>> FindByTypeNotificationIdAsync(int typeNotificationId)
            => await Task.Run(() => (
                from ntf in Context.Set<Notification>().ToList()
                where ntf.TypesNotifications.Equals(typeNotificationId)
                select ntf
            ).ToList());

        public async Task<IEnumerable<Notification>> FindAllByHotelIdAsync(int hotelId)
        => await Task.Run(() => (
            from ntf in Context.Set<Notification>().ToList()
            where ntf.HotelsId.Equals(hotelId)
            select ntf
        ).ToList());

        public async Task<IEnumerable<Notification>> FindAllBySenderIdAsync(int senderId)
        =>
           await Task.Run(() => (
                from noti in Context.Set<Notification>().ToList()
                where noti.SenderId.Equals(senderId)
                select noti
           ).ToList());
    }
}

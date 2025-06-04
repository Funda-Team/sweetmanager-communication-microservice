using CommunicationService.Interfaces.REST.Resources.Notification;

namespace CommunicationService.Interfaces.REST.Transform.Notification
{
    public class NotificationResourceFromEntityAssembler
    {
        public static NotificationResource ToResourceFromEntity(Domain.Model.Aggregates.Notification entity) =>
            new(entity.Id, entity.Title, entity.Content, entity.SenderType, entity.SenderId, entity.RecieverId, entity.HotelsId, entity.TypesNotifications, entity.Description);

    }
}

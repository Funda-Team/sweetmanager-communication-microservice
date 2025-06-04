using CommunicationService.Domain.Model.Commands;
using CommunicationService.Interfaces.REST.Resources.Notification;

namespace CommunicationService.Interfaces.REST.Transform.Notification
{
    public class CreateNotificationCommandFromResourceAssembler
    {
        public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
        {
            return new CreateNotificationCommand(
                resource.Title,
                resource.Content,
                resource.SenderType,
                resource.SenderId,
                resource.RecieverId,
                resource.HotelsId,
                resource.TypesNotifications,
                resource.Description
                );
        }
    }
}

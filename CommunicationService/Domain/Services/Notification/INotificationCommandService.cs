using CommunicationService.Domain.Model.Commands;

namespace CommunicationService.Domain.Services.Notification
{
    public interface INotificationCommandService
    {
        Task<bool> Handle(CreateNotificationCommand command);
    }
}

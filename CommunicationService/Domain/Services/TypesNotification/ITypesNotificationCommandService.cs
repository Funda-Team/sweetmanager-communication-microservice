using CommunicationService.Domain.Model.Commands;

namespace CommunicationService.Domain.Services.TypesNotification
{
    public interface ITypesNotificationCommandService
    {
        Task<bool> Handle(SeedTypesNotificationsCommand command);

    }
}

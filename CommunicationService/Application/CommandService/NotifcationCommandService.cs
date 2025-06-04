using CommunicationService.Domain.Model.Aggregates;
using CommunicationService.Domain.Model.Commands;
using CommunicationService.Domain.Repositories;
using CommunicationService.Domain.Services.Notification;
using IamService.Shared.Domain.Repositories;

namespace CommunicationService.Application.CommandService
{
    public class NotificationCommandService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork) : INotificationCommandService
    {
        public async Task<bool> Handle(CreateNotificationCommand command)
        {
            try
            {

                await notificationRepository.AddAsync(new Notification
                {
                    TypesNotifications = command.TypesNotifications,
                    SenderId = command.SenderId,
                    Title = command.Title,
                    Description = command.Description,
                    RecieverId = command.RecieverId,
                    HotelsId = command.HotelsId,
                    Content = command.Content,
                    SenderType = command.SenderType,
                });

                await unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

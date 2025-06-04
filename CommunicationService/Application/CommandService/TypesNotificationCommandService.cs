using CommunicationService.Domain.Model.Commands;
using CommunicationService.Domain.Model.Entities;
using CommunicationService.Domain.Model.ValueObjects;
using CommunicationService.Domain.Repositories;
using CommunicationService.Domain.Services.TypesNotification;
using IamService.Shared.Domain.Repositories;

namespace CommunicationService.Application.CommandService
{
    public class TypesNotificationCommandService(ITypesNotificationRepository typesNotificationRepository,
    IUnitOfWork unitOfWork) : ITypesNotificationCommandService
    {
        public async Task<bool> Handle(SeedTypesNotificationsCommand command)
        {
            foreach (var typesNotification in Enum.GetValues(typeof(ETypesNotification)))
            {
                if (await typesNotificationRepository.FindByNameAsync(typesNotification.ToString()!)) continue;

                await typesNotificationRepository.AddAsync(new TypesNotification(typesNotification.ToString()!));

                await unitOfWork.CommitAsync();
            }

            return true;
        }
    }
}

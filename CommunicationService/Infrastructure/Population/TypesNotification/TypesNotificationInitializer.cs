using CommunicationService.Domain.Model.Commands;
using CommunicationService.Domain.Model.Queries.TypesNotification;
using CommunicationService.Domain.Services.TypesNotification;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace CommunicationService.Infrastructure.Popularity.TypesNotification
{
    public class TypesNotificationsInitializer(ITypesNotificationCommandService typeNotificationCommandService,
        ITypesNotificationQueryService typeNotificationQueryService, CommunicationContext context)
    {
        public async Task InitializeAsync()
        {
            // Check if the role table is empty

            var result = await typeNotificationQueryService.Handle(new GetAllTypesNotificationsQuery());

            if (!result.Any())
            {
                // Prepopulate the empty table

                await typeNotificationCommandService.Handle(new SeedTypesNotificationsCommand());
            }
        }
    }
}

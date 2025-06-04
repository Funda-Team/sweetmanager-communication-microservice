using CommunicationService.Domain.Model.Entities;
using CommunicationService.Domain.Model.Queries.TypesNotification;
using CommunicationService.Domain.Repositories;
using CommunicationService.Domain.Services.TypesNotification;

namespace CommunicationService.Application.QueryService
{
    public class TypesNotificationQueryService : ITypesNotificationQueryService
    {
        private readonly ITypesNotificationRepository _typesNotificationRepository;

        public TypesNotificationQueryService(ITypesNotificationRepository typesNotificationRepository)
        {
            _typesNotificationRepository = typesNotificationRepository;
        }

        public async Task<IEnumerable<TypesNotification>> Handle(GetAllTypesNotificationsQuery query)
        {
            return await _typesNotificationRepository.ListAsync();
        }

        public async Task<TypesNotification?> Handle(GetTypesNotificationByIdQuery query)
        {
            return await _typesNotificationRepository.FindByIdAsync(query.Id);
        }
    }
}

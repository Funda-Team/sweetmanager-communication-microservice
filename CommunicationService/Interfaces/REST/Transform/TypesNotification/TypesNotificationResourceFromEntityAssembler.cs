using CommunicationService.Interfaces.REST.Resources.TypesNotification;

namespace CommunicationService.Interfaces.REST.Transform.TypesNotification
{
    public class TypesNotificationResourceFromEntityAssembler
    {
        public static TypesNotificationResource ToResourceFromEntity(Domain.Model.Entities.TypesNotification entity) =>
            new(entity.Id, entity.Name);
    }
}

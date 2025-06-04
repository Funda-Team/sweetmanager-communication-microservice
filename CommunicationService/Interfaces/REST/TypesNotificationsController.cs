using CommunicationService.Domain.Model.Queries.TypesNotification;
using CommunicationService.Domain.Services.TypesNotification;
using CommunicationService.Interfaces.REST.Transform.TypesNotification;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationService.Interfaces.REST
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesNotificationsController(ITypesNotificationQueryService typesNotificationQueryService) :
        ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> AllTypesNotifications()
        {
            var typesNotifications = await typesNotificationQueryService
                .Handle(new GetAllTypesNotificationsQuery());

            var typesNotificationsResource = typesNotifications.Select
            (TypesNotificationResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(typesNotificationsResource);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> TypesNotificationById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            try
            {
                var typesNotification = await typesNotificationQueryService.Handle(new GetTypesNotificationByIdQuery(id));

                if (typesNotification is null)
                {
                    throw new Exception("TypesNotification not found");
                }

                var typesNotificationResource = TypesNotificationResourceFromEntityAssembler.ToResourceFromEntity(typesNotification);

                return Ok(typesNotificationResource);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

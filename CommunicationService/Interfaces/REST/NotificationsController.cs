using CommunicationService.Domain.Model.Queries.Notification;
using CommunicationService.Domain.Services.Notification;
using CommunicationService.Interfaces.REST.Resources.Notification;
using CommunicationService.Interfaces.REST.Transform.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CommunicationService.Interfaces.REST
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class NotificationsController(
        INotificationCommandService notificationCommandService,
        INotificationQueryService notificationQueryService) : ControllerBase
    {
        [HttpPost("notifications")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await notificationCommandService.Handle(CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(resource));

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("hotelid")]
        public async Task<IActionResult> AllNotifications([FromQuery] int hotelId)
        {
            var notifications = await notificationQueryService.Handle(new GetAllNotificationsQuery(hotelId));

            var notificationsResource = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

            return Ok(notificationsResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> NotificationById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }

            try
            {
                var notification = await notificationQueryService.Handle(new GetNotificationByIdQuery(id));

                if (notification is null)
                {
                    throw new Exception("Notification not found");
                }

                var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);

                return Ok(notificationResource);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificationsBySenderId([FromQuery] int senderId)
        {
            try
            {
                var notifications =
                    await notificationQueryService.Handle(new GetAllNotificationsBySenderIdQuery(senderId));

                var notificationResources =
                    notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);

                return Ok(notificationResources);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CommunicationService.Interfaces.REST.Resources.Notification
{
    public record CreateNotificationResource(
        [Required][StringLength(100)] string? Title,
        string? Content,
        string? SenderType,
        int? SenderId, 
        int? RecieverId, 
        int? HotelsId,
        int? TypesNotifications,
        [Required][StringLength(500)] string? Description);
}

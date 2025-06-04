namespace CommunicationService.Domain.Model.Commands
{
    public record CreateNotificationCommand(string? Title, string? Content, string? SenderType, int? SenderId, int? RecieverId, int? HotelsId, int? TypesNotifications, string? Description);
}

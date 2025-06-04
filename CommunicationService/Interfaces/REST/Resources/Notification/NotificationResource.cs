namespace CommunicationService.Interfaces.REST.Resources.Notification
{
    public record NotificationResource(int Id, string? Title, string? Content, string? SenderType, int? SenderId, int? RecieverId, int? HotelsId, int? TypesNotifications, string? Description);
}

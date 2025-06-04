using CommunicationService.Domain.Model.Entities;

namespace CommunicationService.Domain.Model.Aggregates;

public partial class Notification
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? SenderType { get; set; }

    public int? SenderId { get; set; }

    public int? RecieverId { get; set; }

    public int? HotelsId { get; set; }

    public int? TypesNotifications { get; set; }

    public string? Description { get; set; }

    public virtual TypesNotification? TypesNotificationsNavigation { get; set; }

    public Notification() { }


}

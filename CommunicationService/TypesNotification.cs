using System;
using System.Collections.Generic;

namespace CommunicationService;

public partial class TypesNotification
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Domain.Model.Aggregates
{
    public partial class Notification : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("UpdateAt")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}

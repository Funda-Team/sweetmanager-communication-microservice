using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunicationService.Domain.Model.Entities
{
    public partial class TypesNotificationAudit : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

        [Column("UpdateAt")] public DateTimeOffset? UpdatedDate { get; set; }
    }
}

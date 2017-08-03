using Abp.Domain.Entities.Auditing;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Holds the selected ansers when an user selects multiple answers (when allowed)
    /// </summary>
    public class SelectedAnswer : FullAuditedEntity
    {
        public int SelectedAnswerId { get; set; }
        public int AnswerId { get; set; }
    }
}
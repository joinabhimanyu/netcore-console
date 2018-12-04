using System;

namespace Scrambler.Model.BusinessEntities
{
    public partial class DocumentRuleSettingEntity
    {
        public int DocumentRuleSettingId { get; set; }
        public int DocumentRuleId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public byte[] Stamp { get; set; }

        public virtual DocumentRuleEntity DocumentRule { get; set; }
    }
}

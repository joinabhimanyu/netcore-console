using System;
using System.Collections.Generic;
namespace  Scrambler.Model.BusinessEntities
{
    public class DocumentRuleEntity
    {
        public DocumentRuleEntity()
        {
            //DocumentRuleSetting = new HashSet<DocumentRuleSettingEntity>();
            //ProcessQueue = new HashSet<ProcessQueueEntity>();
        }
        public int DocumentRuleId { get; set; }
        public Guid DocumentRuleGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DocumentId { get; set; }
        public int ProcessSchemaId { get; set; }
        public int RuleOrder { get; set; }
        public string FieldQueryRule { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public byte[] Stamp { get; set; }

        //public virtual ICollection<DocumentRuleSettingEntity> DocumentRuleSetting { get; set; }
        //public virtual ICollection<ProcessQueueEntity> ProcessQueue { get; set; }
        //public virtual Data.BusinessEntities.DocumentEntity Document { get; set; }
        //public virtual ProcessSchemaEntity ProcessSchema { get; set; }
    }
}

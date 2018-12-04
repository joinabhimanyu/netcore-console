using System;
using System.Collections.Generic;

namespace Scrambler.Model.BusinessEntities
{
    public partial class ProcessSchemaEntity
    {
        public ProcessSchemaEntity()
        {
            //DocumentRule = new HashSet<DocumentRuleEntity>();
            //ImportMapping = new HashSet<ImportMappingEntity>();
            //ProcessHandler = new HashSet<Data.BusinessEntities.ProcessHandlerEntity>();
            //ProcessQueue = new HashSet<ProcessQueueEntity>();
            //ProcessSchemaStep = new HashSet<ProcessSchemaStepEntity>();
        }

        public int ProcessSchemaId { get; set; }
        public Guid ProcessSchemaGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public byte[] Stamp { get; set; }

        //public virtual ICollection<DocumentRuleEntity> DocumentRule { get; set; }
        //public virtual ICollection<ImportMappingEntity> ImportMapping { get; set; }
        //public virtual ICollection<Data.BusinessEntities.ProcessHandlerEntity> ProcessHandler { get; set; }
        //public virtual ICollection<ProcessQueueEntity> ProcessQueue { get; set; }
        //public virtual ICollection<ProcessSchemaStepEntity> ProcessSchemaStep { get; set; }
    }
}

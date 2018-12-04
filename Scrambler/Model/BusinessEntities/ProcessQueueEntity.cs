using System;
using System.Collections.Generic;

namespace Scrambler.Model.BusinessEntities
{
    public partial class ProcessQueueEntity
    {
        public ProcessQueueEntity()
        {
            //ProcessLog = new HashSet<ProcessLogEntity>();
            //ProcessQueueField = new HashSet<Data.BusinessEntities.ProcessQueueFieldEntity>();
            //ProcessQueueFile = new HashSet<ProcessQueueFileEntity>();
        }

        public int ProcessQueueId { get; set; }
        public int ImportHandlerId { get; set; }
        public int ProcessSchemaId { get; set; }
        public int? DocumentRuleId { get; set; }
        public int? NextProcessSchemaStepId { get; set; }
        public int ProcessQueueStateId { get; set; }
        public int ProcessErrorCount { get; set; }
        public double? ProcessTotaltime { get; set; }
        public DateTime? LastProcessDate { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentKey { get; set; }
        public string DocumentIdentity { get; set; }
        public string DocumentName { get; set; }
        public string DocumentCategory { get; set; }
        public DateTime? DocumentStamp { get; set; }
        public int? DocumentPages { get; set; }
        public string DocumentArchiveKey { get; set; }
        public DateTime? Created { get; set; }
        public byte[] Stamp { get; set; }

        //public virtual ICollection<ProcessLogEntity> ProcessLog { get; set; }
        //public virtual ICollection<Data.BusinessEntities.ProcessQueueFieldEntity> ProcessQueueField { get; set; }
        //public virtual ICollection<ProcessQueueFileEntity> ProcessQueueFile { get; set; }
        //public virtual Data.BusinessEntities.DocumentEntity Document { get; set; }
        //public virtual DocumentRuleEntity DocumentRule { get; set; }
        //public virtual Data.BusinessEntities.ProcessHandlerEntity ImportHandler { get; set; }
        //public virtual ProcessSchemaStepEntity NextProcessSchemaStep { get; set; }
        //public virtual ProcessQueueStateEntity ProcessQueueState { get; set; }
        //public virtual ProcessSchemaEntity ProcessSchema { get; set; }
    }
}

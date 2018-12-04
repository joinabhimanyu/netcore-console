using System;
using System.Collections.Generic;

namespace Scrambler.Model.BusinessEntities
{
    public partial class DocumentEntity
    {
        public DocumentEntity()
        {
            //DocumentField = new HashSet<DocumentFieldEntity>();
            //DocumentFieldSetting = new HashSet<DocumentFieldSettingEntity>();
            //DocumentRule = new HashSet<DocumentRuleEntity>();
            //ProcessQueue = new HashSet<ProcessQueueEntity>();
        }

        public int DocumentId { get; set; }
        public string DocumentIdentity { get; set; }
        public string Number { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Pages { get; set; }
        public string CompanyCode { get; set; }
        public string BatchClass { get; set; }
        public string ExportFileType { get; set; }
        public string Link { get; set; }
        public int? DocumentTemplateId { get; set; }
        public int DocumentCategoryId { get; set; }
        public int DocumentSourceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public byte[] Stamp { get; set; }

        //public virtual ICollection<DocumentFieldEntity> DocumentField { get; set; }
        //public virtual ICollection<DocumentFieldSettingEntity> DocumentFieldSetting { get; set; }
        //public virtual ICollection<DocumentRuleEntity> DocumentRule { get; set; }
        //public virtual ICollection<ProcessQueueEntity> ProcessQueue { get; set; }
        //public virtual Data.BusinessEntities.DocumentCategoryEntity DocumentCategory { get; set; }
        //public virtual DocumentSourceEntity DocumentSource { get; set; }
    }
}

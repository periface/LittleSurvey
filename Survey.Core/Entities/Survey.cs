using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Survey.Core.Entities
{
    /// <summary>
    /// Holds the survey info
    /// </summary>
    public class Survey : FullAuditedEntity
    {
        public Survey()
        {
            
        }

        /// <summary>
        /// Creates a new survey instance (only for convenience)
        /// </summary>
        /// <param name="description"></param>
        /// <param name="daysFromNow"></param>
        /// <param name="url"></param>
        public Survey(string description,int daysFromNow,string url)
        {
            Description = description;
            StartDateTime = DateTime.Now;
            EndDateTime = StartDateTime.AddDays(daysFromNow);
        }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SurveyUrl { get; set; }
    }
}

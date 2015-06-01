using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Serler.Models
{
    public class PaperViewModel
    {
        public int PaperId { get; set; }
        public string PaperTitle { get; set; }
        public int Rating { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string JournalName { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string Methodology { get; set; }
        public string Practice { get; set; }
        public string OutcomeBeingTested { get; set; }
        public string ContextWho { get; set; }
        public string ContextWhat { get; set; }
        public string ContextWhere { get; set; }
        public string StudyResult { get; set; }
        public string ImplementationIntegrity { get; set; }
        public string ConfidenceRating { get; set; }
        public string ConfidenceRatingReason { get; set; }
        public string WhoRated { get; set; }
        public string ResearchLevel { get; set; }
        public string ResearchQuestion { get; set; }
        public string ResearchMethod { get; set; }
        public string ResearchMetrics { get; set; }
        public string ParticipantsNature { get; set; }
        public int NoOfPeopleRated { get; set; }
        public bool IsActive { get; set; }
        public bool IsAnalyzed { get; set; }
        public bool isRejected { get; set; }
    }

}
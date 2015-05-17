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
        public string Publisher { get; set; }
        public string Abstract { get; set; }
        public string Reference { get; set; }
        public string Category { get; set; }
        public string Methodology { get; set; }
        public string MethodologyDescription { get; set; }
        public string Practice { get; set; }
        public string PracticeDescription { get; set; }
        public string OutcomeBeingTested { get; set; }
        public string StudyContext { get; set; }
        public string StudyResult { get; set; }
        public string ImplementationIntegrity { get; set; }
        public string ConfidenceRating { get; set; }
        public string WhoRated { get; set; }
        public string ResearchQuestion { get; set; }
        public string ResearchMethod { get; set; }
        public string ResearchMetrcis { get; set; }
        public string ParticipantsNature { get; set; }
        public string PaperLink { get; set; }
        public int NoOfPeopleRated { get; set; }
        public bool IsActive { get; set; }
    }

}
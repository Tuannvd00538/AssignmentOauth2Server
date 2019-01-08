using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Mark
    {
        private static readonly int MaxTheory = 10; // 100%.
        private static readonly int MaxPratice = 15;
        private static readonly int MaxAssignment = 10;
        private static readonly int PercentToPass = 40;

        public Mark() { }

        //public Mark(MarkType type, int value)
        //{
        //    this.MarkType = type;
        //    this.Value = value;
        //    this.GenerateStatus();
        //    this.CreatedAt = DateTime.Now;
        //    this.UpdatedAt = DateTime.Now;
        //}

        public Mark(int Theory, int Practice, int Assignment , int subjectClassId, long accountId)
        {
            this.SubjectClassId = subjectClassId;
            this.OwnerId = accountId;
            this.Theory = Theory;
            this.Practice = Practice;
            this.Assignment = Assignment;
            this.GenerateStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        private void GenerateStatus()
        {
            double percentTheory = (this.Theory / MaxTheory) * 100;
            double percentPractice = (this.Practice / MaxPratice) * 100;
            double percentAssignment = (this.Assignment / MaxAssignment) * 100;

            this.Status = percentTheory >= PercentToPass && percentPractice >= PercentToPass && percentAssignment >= PercentToPass ? MarkStatus.Pass : MarkStatus.Fail;
        }

        [Key]
        public long Id { get; set; }

        public float Theory { get; set; }

        public float Practice { get; set; }

        public float Assignment { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MarkStatus Status { get; set; }
        
        [ForeignKey("Account")]
        public long OwnerId { get; set; }
        
        [ForeignKey("SubjectClass")]
        public int SubjectClassId { get; set; }
    }

    //public enum MarkType
    //{
    //    Theory = 0,
    //    Practice = 1,
    //    Assignment = 3
    //}

    public enum MarkStatus
    {
        Pass = 1,
        Fail = 0
    }
}

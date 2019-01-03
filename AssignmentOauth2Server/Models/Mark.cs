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

        public Mark(MarkType type, int value)
        {
            this.MarkType = type;
            this.Value = value;
            this.GenerateStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Mark(MarkType type, int value, int subjectId, long accountId)
        {
            this.SubjectId = subjectId;
            this.OwnerId = accountId;
            this.MarkType = type;
            this.Value = value;
            this.GenerateStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        private void GenerateStatus()
        {
            int max = 0;
            switch (this.MarkType)
            {
                case MarkType.Theory:
                    max = MaxTheory;
                    break;
                case MarkType.Practice:
                    max = MaxPratice;
                    break;
                case MarkType.Assignment:
                    max = MaxAssignment;
                    break;
            }
            double percent = (this.Value / max) * 100;
            this.Status = percent >= PercentToPass ? MarkStatus.Pass : MarkStatus.Fail;
        }

        [Key]
        public long Id { get; set; }

        public float Value { get; set; }

        public MarkType MarkType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MarkStatus Status { get; set; }
        
        [ForeignKey("Account")]
        public long OwnerId { get; set; }
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
    }

    public enum MarkType
    {
        Theory = 0,
        Practice = 1,
        Assignment = 3
    }

    public enum MarkStatus
    {
        Pass = 1,
        Fail = 0
    }
}

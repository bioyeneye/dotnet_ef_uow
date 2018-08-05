using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Data
{
    [Table("GroupMeeting")]
    public partial class GroupMeeting
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ProjectName { get; set; }
        [StringLength(50)]
        public string GroupMeetingLeadName { get; set; }
        [StringLength(50)]
        public string TeamLeadName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? GroupMeetingDate { get; set; }
    }
}

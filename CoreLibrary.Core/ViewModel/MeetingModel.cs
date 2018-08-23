using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoreLibrary.Core.ViewModel
{
    public class MeetingModel
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

    public class MeetingItem : MeetingModel
    {
         

    }
    public class MeetingFilter : MeetingItem
    {
        public static MeetingFilter Deserilize(string whereCondition)
        {
            MeetingFilter filter = null;
            if (whereCondition != null)
            {
                filter = JsonConvert.DeserializeObject<MeetingFilter>(whereCondition);
            }
            return filter;
        }

        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
    }
}

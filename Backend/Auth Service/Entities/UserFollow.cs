using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace User_Service.Entities
{
    [Table("userfollow")]
    public class UserFollow
    {
        [Key]
        public long UserFollowId { get; set; }

        [IgnoreDataMember]
        public virtual User Follower { get; set; }
        public long FollowerId { get; set; }

        [IgnoreDataMember]
        public virtual User Followee { get; set; }
        public long FolloweeId { get; set; }
    }
}

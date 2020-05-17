using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Core.Model;
using Newtonsoft.Json;

namespace Auth_Service.Entities
{
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

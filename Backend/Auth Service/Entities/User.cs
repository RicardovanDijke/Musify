using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Service.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }

        public virtual List<UserFollow> Following { get; } = new List<UserFollow>();
        public virtual List<UserFollow> Followers { get; } = new List<UserFollow>();

        /// <summary>
        /// 'this' follows user followee
        /// </summary>
        /// <param name="followee"></param>
        public void AddFollowing(User followee)
        {
            Following.Add(new UserFollow() { Followee = followee, Follower = this });
            //followee
        }

        /// <summary>
        /// 'this' is followed by user follower
        /// </summary>
        /// <param name="follower"></param>
        public void AddFollower(User follower)
        {
            Followers.Add(new UserFollow() { Followee = this, Follower = follower });
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pegasus.Database.Model
{
    [Table("account")]
    public partial class Account
    {
        public Account()
        {
            FriendFriend1Navigation = new HashSet<Friend>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("privileges")]
        public short Privileges { get; set; }
        [Column("createip")]
        public string CreateIp { get; set; }
        [Column("createtime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
        [Column("lastip")]
        public string LastIp { get; set; }
        [Column("lasttime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastTime { get; set; }

        public virtual Friend FriendIdNavigation { get; set; }
        public virtual ICollection<Friend> FriendFriend1Navigation { get; set; }
    }
}

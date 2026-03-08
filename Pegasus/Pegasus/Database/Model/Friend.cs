using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pegasus.Database.Model
{
    public partial class Friend
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("friend")]
        public int Friend1 { get; set; }
        [Column("addtime")]
        public DateTime AddTime { get; set; }

        public virtual Account Friend1Navigation { get; set; }
        public virtual Account IdNavigation { get; set; }
    }
}

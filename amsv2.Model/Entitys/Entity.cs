using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace amsv2.Model.Entitys
{
    public abstract class Entity<TPrimaryKey>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TPrimaryKey Id { get; set; }

    }
    public class Entity : Entity<Int64>
    {
        protected Entity()
        {
            down = new Dictionary<string, object>();
            up = new Dictionary<string, object>();
        }
        [NonSerialized]
        private Dictionary<string, object> down = null;
        [NotMapped]
        public Dictionary<string, object> Down
        {
            get
            {
                return down;
            }
        }
        [NonSerialized]
        private Dictionary<string, object> up = null;
        [NotMapped]
        public Dictionary<string, object> Up
        {
            get
            {
                return up;
            }
        }
    }
}

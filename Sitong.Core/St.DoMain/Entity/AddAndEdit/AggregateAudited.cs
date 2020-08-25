using St.DoMain.Entity.AggregateRoot;
using System;

namespace St.DoMain.Entity.AddAndEdit
{
    public class AggregateAudited<TPrimaryKey> : AggregateRoot<TPrimaryKey>, IAggregateAudited<TPrimaryKey>
        where TPrimaryKey : struct
    {
        public AggregateAudited()
        {

        }

        public AggregateAudited(TPrimaryKey id) : base(id)
        {
        }

        public TPrimaryKey? CreatorId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public TPrimaryKey? LastModifierId { get; set; }
        public DateTime? LastModifierTime { get; set; }
    }
}

using St.DoMain.Entity.AggregateRoot;
using System;

namespace St.DoMain.Entity.Full
{
    public class FullAudited<TPrimaryKey> : AggregateRoot<TPrimaryKey>, IFullAudited<TPrimaryKey>
        where TPrimaryKey : struct
    {
        public FullAudited()
        {

        }

        public FullAudited(TPrimaryKey Id) : base(Id)
        {
        }

        public TPrimaryKey? CreatorId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public TPrimaryKey? LastModifierId { get; set; }
        public DateTime? LastModifierTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}


namespace St.DoMain.Entity.Audited
{
    public interface ISoftDelete 
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}

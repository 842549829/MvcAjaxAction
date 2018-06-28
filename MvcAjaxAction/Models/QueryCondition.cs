namespace MvcAjaxAction.Models
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryCondition
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Person Person { get; set; }
    }
}
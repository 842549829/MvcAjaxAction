namespace MvcAjaxAction.Models
{
    /// <summary>
    /// 分页操作类
    /// </summary>
    public class Pagination
    {
        /// <summary>
        ///  空构造函数
        /// </summary>
        public Pagination()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="getRowCount">是否或者总条数</param>
        public Pagination(int pageSize, int pageIndex, bool getRowCount)
        {
            if (pageSize > 0)
            {
                PageSize = pageSize;
            }
            if (pageIndex > 0)
            {
                PageIndex = pageIndex;
            }
            GetRowCount = getRowCount;
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 总条数
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (RowCount + PageSize - 1) / PageSize;
            }
        }

        /// <summary>
        /// 是否获取总页数
        /// </summary>
        public bool GetRowCount { get; set; } = true;

        /// <summary>
        /// 默认属性
        /// </summary>
        public static Pagination Default
        {
            get
            {
                return new Pagination(1, 10, true);
            }
        }
    }
}
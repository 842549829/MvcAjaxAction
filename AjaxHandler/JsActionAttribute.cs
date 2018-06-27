using System;
namespace AjaxHandler
{
    /// <summary>
    /// jsaction
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JsActionAttribute : Attribute
    {
        /// <summary>
        /// 默认特性
        /// </summary>
        internal static JsActionAttribute Default = new JsActionAttribute();

        /// <summary>
        /// 是否创建Js访问函数
        /// </summary>
        public bool IsCreateJs { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public JsActionAttribute()
        {
            this.IsCreateJs = true;
        }
    }
}
using AjaxHandler;
using MvcAjaxAction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MvcAjaxAction.Controllers
{
    /// <summary>
    /// 测试分页查询
    /// </summary>
    public class DefaultController : WebAjax
    {
        /// <summary>
        /// 默认测试视图
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 测试查询
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>json</returns>
        [JsAction]
        public ActionResult GetParameters(QueryCondition condition)
        {
            Pagination pagination = condition.Pagination;
            Person model = condition.Person;
            var list = QueryData();
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Address))
                {
                    list = list.Where(item => item.Address == model.Address).ToList();
                }
                if (!string.IsNullOrWhiteSpace(model.Name))
                {
                    list = list.Where(item => item.Name == model.Name).ToList();
                }
                if (!string.IsNullOrWhiteSpace(model.Mobile))
                {
                    list = list.Where(item => item.Mobile == model.Mobile).ToList();
                }
            }
            var startRow = (pagination.PageIndex - 1) * pagination.PageSize;
            var endRow = pagination.PageIndex * pagination.PageSize;
            pagination.RowCount = list.Count;
            var data = new
            {
                Person = list.Take(endRow).Skip(startRow),
                Pagination = pagination
            };
            return Json(data);
        }

        /// <summary>
        /// 生成测试数据
        /// </summary>
        /// <returns>测试数据</returns>
        private static List<Person> QueryData()
        {
            string[] names = new[] { "张三", "李四", "王五", "许昌", "何达" };
            var list = new List<Person>();
            for (int i = 0; i < 235; i++)
            {
                Random random = new Random();
                Person person = new Person { Id = i, Age = i, Address = "address" + i, Mobile = "1355115457" + i, Height = i, Weight = i, Remark = "格斯达克沙地上多空双方的伤口附近的客服电话开机" + i };
                if (i % 1 == 1)
                {
                    person.Name = names[0];
                }
                else if (i % 2 == 0)
                {
                    person.Name = names[1];
                }
                else if (i % 3 == 0)
                {
                    person.Name = names[2];
                }
                else if (i % 4 == 0)
                {
                    person.Name = names[3];
                }
                else
                {
                    person.Name = names[4];
                }
                list.Add(person);
            }

            return list;
        }
    }
}
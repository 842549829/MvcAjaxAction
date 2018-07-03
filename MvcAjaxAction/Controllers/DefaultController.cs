using AjaxHandler;
using MvcAjaxAction.Models;
using System.Collections.Generic;
using System.Linq;
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
            string[] names = { "张三", "李四", "王五", "许昌", "何达" };
            int[] ages = { 14, 56, 43, 21, 54 };
            string[] addrses = { "上海市长宁区天山路8号5楼", "浙江金华市李渔路1118号创新大厦", "长宁区福泉路99号携程网络技术大楼B1楼(近天山西路)", "深圳市南山区科技园飞亚达大厦3-10楼", "浙江省杭州市西湖区天目山路266号黄龙时代广场支付宝大楼3楼" };
            var list = new List<Person>();
            for (int i = 0; i < 235; i++)
            {
                Person person = new Person { Id = i, Address = "address" + i, Mobile = "1355115457" + i, Height = i, Weight = i, Remark = "" + i };
                if (i % 1 == 1)
                {
                    person.Name = names[0];
                    person.Age = ages[0];
                    person.Address = addrses[0];
                }
                else if (i % 2 == 0)
                {
                    person.Name = names[1];
                    person.Age = ages[1];
                    person.Address = addrses[1];
                }
                else if (i % 3 == 0)
                {
                    person.Name = names[2];
                    person.Age = ages[2];
                    person.Address = addrses[2];
                }
                else if (i % 4 == 0)
                {
                    person.Name = names[3];
                    person.Age = ages[3];
                    person.Address = addrses[3];
                }
                else
                {
                    person.Name = names[4];
                    person.Age = ages[4];
                    person.Address = addrses[4];
                }
                list.Add(person);
            }

            return list;
        }

        /// <summary>
        /// 菜单测试页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// Vue
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult Vue() {
            return View();
        }
    }
}
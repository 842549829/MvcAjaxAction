using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace AjaxHandler
{
    /// <summary>
    /// WebAjax处理类
    /// </summary>
    public class WebAjax : Controller
    {
        /// <summary>
        /// 获取Javascript
        /// </summary>
        /// <returns>视图结果</returns>
        public ActionResult GetJavascript()
        {
            //var macth = System.Text.RegularExpressions.Regex.Match(url, @"(\w+:\/\/)([^/:]+)(:\d*)?");
            //if (macth.Success)
            //{
            //    url = macth.Value;
            //}

            Type type = this.GetType();
            Uri uri = Request.Url;
            string url = uri.ToString();
            url = url.Substring(0, url.LastIndexOf("/"));
            url = url.Substring(0, url.LastIndexOf("/"));

            string text = GetScriptTemplete();
            text = text.Replace("$H_DES$", "通过jQuery.ajax完成服务端函数调用");
            text = text.Replace("$H_DATE$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            text = text.Replace("$URL$", url);
            text = text.Replace("$CLS$", GetControllerName(type.Name));
            StringBuilder stringBuilder = new StringBuilder(text);
            MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] array = methods;
            for (int i = 0; i < array.Length; i++)
            {
                MethodInfo methodInfo = array[i];
                JsActionAttribute methodAnnotation = this.GetMethodAnnotation(methodInfo.Name);
                if (methodAnnotation != null && methodAnnotation.IsCreateJs)
                {
                    string functionTemplete = GetFunctionTemplete(methodInfo);
                    stringBuilder.AppendLine(functionTemplete);
                }
            }
            return JavaScript(stringBuilder.ToString());
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <returns>模板</returns>
        private static string GetScriptTemplete()
        {
            Type typeFromHandle = typeof(WebAjax);
            AssemblyName assemblyName = new AssemblyName(typeFromHandle.Assembly.FullName);
            Stream manifestResourceStream = typeFromHandle.Assembly.GetManifestResourceStream(assemblyName.Name + ".net.js");
            if (manifestResourceStream == null)
            {
                throw new ApplicationException("模板未找到");
            }
            return GetMessage(manifestResourceStream, "utf-8");
        }

        /// <summary>
        /// 获取当前请求流
        /// </summary>
        /// <param name="inputStream">请求流</param>
        /// <param name="charset">编码</param>
        /// <returns>结果</returns>
        public static string GetMessage(Stream inputStream, string charset)
        {
            Encoding encoding = Encoding.GetEncoding(charset);
            StreamReader streamReader = new StreamReader(inputStream, encoding);
            StringBuilder stringBuilder = new StringBuilder();
            string arg = string.Empty;
            while ((arg = streamReader.ReadLine()) != null)
            {
                stringBuilder.AppendFormat("{0}\n", arg);
            }
            streamReader.Close();
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取函数模板
        /// </summary>
        /// <param name="method">method</param>
        /// <returns>结果</returns>
        private static string GetFunctionTemplete(MethodInfo method)
        {
            var controller = GetControllerName(method.DeclaringType.Name);
            StringBuilder stringBuilder = new StringBuilder(controller);
            stringBuilder.AppendFormat(".prototype.{0} = function(", method.Name);
            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];
                stringBuilder.AppendFormat("{0},", parameterInfo.Name);
            }
            stringBuilder.Append("obj)");
            stringBuilder.AppendLine("{");
            parameters = method.GetParameters();
            if (parameters.Length > 0)
            {
                stringBuilder.Append("\tvar args = {");
                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo parameterInfo = parameters[i];
                    stringBuilder.AppendFormat("{0}:{1},", parameterInfo.Name, parameterInfo.Name);
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.AppendLine("};");
            }
            else
            {
                stringBuilder.Append("\tvar args = null;");
            }
            stringBuilder.AppendLine("\tvar options={dataType:'json'};");
            stringBuilder.AppendLine("\t$.extend(true,options,{},this.Options);");
            stringBuilder.AppendFormat("\t$.net.CallWebMethod(options,'{0}',args, obj);", method.Name);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("}\t\t");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取控制器名称
        /// </summary>
        /// <param name="controllerName">控制器</param>
        /// <returns>结果</returns>
        private static string GetControllerName(string controllerName)
        {
            return controllerName.Replace("Controller", string.Empty);
        }

        /// <summary>
        /// 获取请求方法特性
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <returns>特性对象</returns>
        private JsActionAttribute GetMethodAnnotation(string methodName)
        {
            MethodInfo method = base.GetType().GetMethod(methodName);
            JsActionAttribute[] array = (JsActionAttribute[])method.GetCustomAttributes(typeof(JsActionAttribute), false);
            JsActionAttribute result = null;
            if (array.Length > 0)
            {
                result = array[0];
            }
            return result;
        }
    }
}
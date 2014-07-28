using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models
{
    /// <summary>
    /// 数据结构基类
    /// </summary>
    [DataContract]
    public class Base
    {
        /// <summary>
        /// 是否第一个元素
        /// </summary>
        public bool IsFirst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Maps { get; set; }

        public Base()
        {
            this.Maps = new Dictionary<string, string>();
        }
        protected void doAddMap(string key, string value)
        {
            if (this.Maps == null) return;

            if (this.Maps.ContainsKey(key))
            {
                this.Maps[key] = value;
            }
            else
            {
                this.Maps.Add(key, value);
            }
        }
        protected void doAddMap<T>(Expression<Func<T>> propertyExpression, string value)
        {
            if (this.Maps == null) return;
            var body = propertyExpression.Body as MemberExpression;

            this.doAddMap(body.Member.Name, value);
        }
    }
}

﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 FeedResultConfigurationAppend 分部方法。
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Models;
using XIMALAYA.PCDesktop.Core.Models.Feed;

namespace XIMALAYA.PCDesktop.Core.Data
{
 public partial class FeedResultConfig<T> : IJsonConfig
        where T : FeedResult
    {

        /// <summary>
        /// 添加配置
        /// </summary>
        partial void AppendConfig();
        /// <summary>
        /// 配置类
        /// </summary>
        public JsonConfiguration<FeedResult> Config { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public FeedResultConfig()
        {
            this.Config = Json.ConfigurationFor< FeedResult>();
            this.AddConfig();
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        public void AddConfig()
        {
            this.Config.MapType<T>(map => map.AllFields());
            this.AppendConfig();
        }
    }
}

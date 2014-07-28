using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AlbumData : BaseData
    {
        /// <summary>
        /// 专辑ID
        /// id
        /// </summary>
        [DataMember]
        public long AlbumID { get; set; }
        /// <summary>
        /// 专辑标题
        /// title
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// 专辑图片 290*290
        /// albumCoverUrl290
        /// </summary>
        [DataMember]
        public string AlbumCoverUrl290 { get; set; }
        /// <summary>
        /// 专辑图片86*86
        /// </summary>
        [DataMember]
        public string AlbumCoverUrl86 { get; set; }
        /// <summary>
        /// 专辑图片140*140
        /// </summary>
        [DataMember]
        public string AlbumCoverUrl140 { get; set; }
        /// <summary>
        /// 专辑图片640*640
        /// </summary>
        [DataMember]
        public string AlbumCoverUrl640 { get; set; }
        /// <summary>
        /// 专辑所属人昵称
        /// </summary>
        [DataMember]
        public string AlbumNickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [DataMember]
        public string AvatarPath { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [DataMember]
        public string AvatarPath86 { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        [DataMember]
        public long CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CoverPath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public long CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DigStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HumanCategoryID { get; set; }
        /// <summary>
        /// 专辑简介
        /// </summary>
        [DataMember]
        public string Intro { get; set; }
        /// <summary>
        /// 是否加V
        /// </summary>
        [DataMember]
        public bool IsVerified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsCrawler { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        [DataMember]
        public bool IsPublic { get; set; }
        /// <summary>
        /// 是否连载
        /// </summary>
        [DataMember]
        public bool IsPublish { get; set; }
        /// <summary>
        /// 是否喜欢
        /// </summary>
        [DataMember]
        public bool IsFavorite { get; set; }
        /// <summary>
        /// 是否倒叙排列
        /// </summary>
        [DataMember]
        public bool IsRecordsDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsV { get; set; }
        /// <summary>
        /// 最新声音更新时间
        /// </summary>
        [DataMember]
        public long LastUptrackDate { get; set; }
        /// <summary>
        /// 最新声音图片
        /// </summary>
        [DataMember]
        public string LastUptrackCoverPath { get; set; }
        /// <summary>
        /// 最新声音ID
        /// </summary>
        [DataMember]
        public long LastUptrackID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MusicCategory { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OpType { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        [DataMember]
        public long PlayCount { get; set; }
        /// <summary>
        /// 富文本简介
        /// </summary>
        [DataMember]
        public string RichIntro { get; set; }
        /// <summary>
        /// 短简介
        /// </summary>
        [DataMember]
        public string ShortIntro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string SourceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 所属标签
        /// </summary>
        [DataMember]
        public string Tags { get; set; }
        /// <summary>
        /// 声音数量
        /// </summary>
        [DataMember]
        public int TrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int TrackOrder { get; set; }
        /// <summary>
        /// UID
        /// </summary>
        [DataMember]
        public long Uid { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [DataMember]
        public long UpdateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int UserSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int SerialState { get; set; }
    }
}

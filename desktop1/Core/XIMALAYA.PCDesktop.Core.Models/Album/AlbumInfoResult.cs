using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 专辑下的声音
    /// </summary>
    [DataContract]
    public class AlbumInfoResult : BaseResult
    {
        /// <summary>
        /// 专辑数据
        /// </summary>
        [DataMember]
        public AlbumData Album { get; set; }
        /// <summary>
        /// 声音数据
        /// </summary>
         [DataMember]
        public SoundsResult SoundsResult { get; set; }
    }
}

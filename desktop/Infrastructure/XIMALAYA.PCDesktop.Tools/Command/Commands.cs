using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Events;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局命令
    /// </summary>
    public class CommandBaseSingleton : NotificationObject
    {
        #region 字段

        private long _TrackID;

        #endregion

        #region 属性

        /// <summary>
        /// 事件管理器
        /// </summary>
        private IEventAggregator EventAggregator { get; set; }
        /// <summary>
        /// 当前播放的声音
        /// </summary>
        public long TrackID
        {
            get
            {
                return _TrackID;
            }
            set
            {
                if (value != _TrackID)
                {
                    _TrackID = value;
                    this.RaisePropertyChanged(() => this.TrackID);
                    this.PlaySoundCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 命令

        /// <summary>
        /// 播放命令
        /// </summary>
        public DelegateCommand<long?> PlaySoundCommand { get; set; }
        /// <summary>
        /// 专辑详情页命令
        /// </summary>
        public DelegateCommand<long?> AlbumDetailCommand { get; set; }

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            this.PlaySoundCommand = new DelegateCommand<long?>(trackID =>
            {
                if (trackID == null) return;

                this.EventAggregator.GetEvent<ModulesManagerEvent>().Publish(new ModuleInfoArgument()
                {
                    ModuleName = WellKnownModuleNames.MusicPlayerModule,
                    Action = new Action(() =>
                    {
                        this.EventAggregator.GetEvent<PlayerEvent>().Publish((long)trackID);
                    })
                });
            }, (trackID) =>
            {
                return trackID != this.TrackID;
            });

            this.AlbumDetailCommand = new DelegateCommand<long?>(albumID =>
            {
                if (albumID == null) return;

                this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Publish((long)albumID);
            });
        }

        #endregion
    }
    /// <summary>
    /// 按钮
    /// </summary>
    public class CommandSingleton : Singleton<CommandBaseSingleton> { }
}

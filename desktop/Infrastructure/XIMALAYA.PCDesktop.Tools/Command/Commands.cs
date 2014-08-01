﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools.Extension;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 全局命令
    /// </summary>
    public class CommandBaseSingleton : NotificationObject
    {
        #region 字段

        private long _TrackID;
        private ItemCollection _SoundCollection;
        private string _TrackTitle;

        #endregion

        #region 属性

        /// <summary>
        /// 事件管理器
        /// </summary>
        private IEventAggregator EventAggregator { get; set; }
        /// <summary>
        /// 当前播放的声音ID
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
        /// <summary>
        /// 当前播放的声音Title
        /// </summary>
        public string TrackTitle
        {
            get
            {
                return _TrackTitle;
            }
            set
            {
                if (value != _TrackTitle)
                {
                    _TrackTitle = value;
                    this.RaisePropertyChanged(() => this.TrackTitle);
                }
            }
        }
        /// <summary>
        /// 全局播放
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }
        /// <summary>
        /// 当前点击播放的声音行
        /// </summary>
        public Control CurrentPlayControl { get; set; }
        /// <summary>
        /// 佔位服务
        /// </summary>
        public ItemCollection SoundCollection
        {
            get
            {
                return _SoundCollection;
            }
            set
            {
                if (value != _SoundCollection)
                {
                    _SoundCollection = value;
                    this.RaisePropertyChanged(() => this.SoundCollection);
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
        /// 播放命令
        /// </summary>
        public DelegateCommand<Control> PlaySound1Command { get; set; }
        /// <summary>
        /// 专辑详情页命令
        /// </summary>
        public DelegateCommand<long?> AlbumDetailCommand { get; set; }
        /// <summary>
        /// 上一首命令
        /// </summary>
        public DelegateCommand PrevCommand { get; set; }
        /// <summary>
        /// 下一首命令
        /// </summary>
        public DelegateCommand NextCommand { get; set; }
        /// <summary>
        /// 热门专辑命令
        /// </summary>
        public DelegateCommand HotAlbumListCommand { get; set; }
        /// <summary>
        /// 声音详情页命令
        /// </summary>
        public DelegateCommand ShowSoundDetailCommand { get; set; }

        #endregion

        #region 构造

        private CommandBaseSingleton()
        {
            this.EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            this.PlaySound1Command = new DelegateCommand<Control>(con =>
            {
                var soundData = con.DataContext as SoundData;

                if (soundData == null) return;

                var dataGrid = VisualTreeHelperExtensions.FindAncestor<DataGrid>(con);
                this.SoundCollection = dataGrid.Items;
                if (this.SoundCollection.MoveCurrentTo(soundData))
                {
                    this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                }
            });

            this.PrevCommand = new DelegateCommand(() =>
            {
                if (this.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.SoundCollection.MoveCurrentToPrevious())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (this.SoundCollection == null) return false;

                return this.SoundCollection.CurrentPosition > 0;
            });
            this.NextCommand = new DelegateCommand(() =>
            {
                if (this.SoundCollection == null) return;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.SoundCollection.MoveCurrentToNext())
                    {
                        this.PlaySoundCommand.Execute(((SoundData)this.SoundCollection.CurrentItem).TrackId);
                    }
                }), null);
            }, () =>
            {
                if (this.SoundCollection == null) return false;

                return this.SoundCollection.CurrentPosition < this.SoundCollection.Count - 1;
            });

            this.PlaySoundCommand = new DelegateCommand<long?>(trackID =>
            {
                if (trackID == null) return;

                this.PrevCommand.RaiseCanExecuteChanged();
                this.NextCommand.RaiseCanExecuteChanged();
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
                return true;
            });

            this.AlbumDetailCommand = new DelegateCommand<long?>(albumID =>
            {
                if (albumID == null) return;

                this.EventAggregator.GetEvent<AlbumDetailEvent<long>>().Publish((long)albumID);
            });

            this.HotAlbumListCommand = new DelegateCommand(() =>
            {
                this.EventAggregator.GetEvent<AlbumListEvent<TagEventArgument>>().Publish(new TagEventArgument()
                {
                    Category = "all",
                    Title = "热门",
                    TagName = " ",
                });
            });

            this.ShowSoundDetailCommand = new DelegateCommand(() => {
                this.EventAggregator.GetEvent<SoundDetailEvent<long>>().Publish(this.TrackID);
            });
        }

        #endregion
    }
    /// <summary>
    /// 按钮
    /// </summary>
    public class CommandSingleton : Singleton<CommandBaseSingleton> { }
}

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using XIMALAYA.PCDesktop.Core.Models.Category;
using XIMALAYA.PCDesktop.Core.Models.Discover;
using XIMALAYA.PCDesktop.Core.Models.FocusImage;
using XIMALAYA.PCDesktop.Core.Models.Subject;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools.Untils;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using XIMALAYA.PCDesktop.Core.Models.Album;
using XIMALAYA.PCDesktop.Cache;
using XIMALAYA.PCDesktop.Controls;

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage
{
    [Export]
    public sealed class DiscoverViewModel : BaseViewModel
    {
        #region fields

        private string _SubjectModuleTitle;

        #endregion

        #region Properties

        /// <summary>
        /// 焦点图服务
        /// </summary>
        [Import]
        private IFocusImageService FocusImageService { get; set; }
        /// <summary>
        /// 发现也整合接口，没用
        /// </summary>
        [Import]
        private ISuperExploreIndex SuperExploreIndex { get; set; }
        /// <summary>
        /// 分类接口服务
        /// </summary>
        [Import]
        private ICategoryService CategoryService { get; set; }
        /// <summary>
        /// 热门声音服务
        /// </summary>
        [Import]
        private IHotSoundsService HotSoundsService { get; set; }
        /// <summary>
        /// 今日焦点的title
        /// </summary>
        public string SubjectModuleTitle
        {
            get { return _SubjectModuleTitle; }
            set
            {
                if (value != _SubjectModuleTitle)
                {
                    _SubjectModuleTitle = value;
                    this.RaisePropertyChanged(() => this.SubjectModuleTitle);
                }
            }
        }
        /// <summary>
        /// 焦点图列表
        /// </summary>
        public ObservableCollection<FocusImageData> FocusImageList { get; set; }
        /// <summary>
        /// 分类列表
        /// </summary>
        public static ObservableCollection<CategoryData> CategoryList { get; set; }
        /// <summary>
        /// 今日热点
        /// </summary>
        public ObservableCollection<SubjectData> SubjectList { get; set; }
        /// <summary>
        /// 热门声音所在的分类
        /// </summary>
        public ObservableCollection<CategoryData> HotSoundsCategories { get; set; }
        /// <summary>
        /// 推荐专辑
        /// </summary>
        public ObservableCollection<AlbumData> AlbumList { get; set; }

        #endregion

        #region commands

        /// <summary>
        /// 查看分类的详情
        /// </summary>
        public DelegateCommand<string> ShowCategoryDetailCommand { get; private set; }

        #endregion

        #region actions

        private void GetFocusImageDataAction()
        {
            if (SuperExploreIndex == null) throw new ArgumentNullException();

            this.SuperExploreIndex.GetData(this.GetExporeIndexData, new SuperExploreParam
            {
                Device = DeviceType.pc,
                PicVersion = 5,
                Scale = 2
            });
        }
        private void GetCategoryListAction()
        {

            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = true, Name = "all", Title = "热门", Path = PathData.Instance.all });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "book", Title = "有声小说", Path = PathData.Instance.book });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "music", Title = "音乐", Path = PathData.Instance.music });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "entertainment", Title = "综艺娱乐", Path = PathData.Instance.entertainment });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "comic", Title = "相声评书", Path = PathData.Instance.comic });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "news", Title = "最新资讯", Path = PathData.Instance.news });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "emotion", Title = "情感生活", Path = PathData.Instance.emotion });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "culture", Title = "历史人文", Path = PathData.Instance.culture });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "train", Title = "外语", Path = PathData.Instance.train });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "baijia", Title = "百家讲坛", Path = PathData.Instance.baijia });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "chair", Title = "培训讲座", Path = PathData.Instance.chair });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "radioplay", Title = "广播剧", Path = PathData.Instance.radioplay });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "opera", Title = "戏剧", Path = PathData.Instance.opera });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "kid", Title = "儿童", Path = PathData.Instance.kid });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "radio", Title = "电台", Path = PathData.Instance.radio });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "finance", Title = "商业财经", Path = PathData.Instance.finance });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "it", Title = "IT科技", Path = PathData.Instance.it });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "health", Title = "健康养生", Path = PathData.Instance.health });
            //DiscoverViewModel.CategoryList.Add(new CategoryData { IsFirst = false, Name = "other", Title = "其他", Path = PathData.Instance.other });

            //return;

            this.CategoryService.GetData(categories =>
            {
                CategoryResult categoryResult = categories as CategoryResult;
                int index = 0;
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    foreach (CategoryData cd in categoryResult.List)
                    {
                        cd.IsFirst = index == 0;
                        index++;
                        DiscoverViewModel.CategoryList.Add(cd);
                    }
                });
            }, new CategoryParam
            {
                Device = DeviceType.pc,
                PicVersion = 5,
                Scale = 2
            });
        }
        private void GetExporeIndexData(object result)
        {
            var superData = result as SuperExploreIndexResult;

            if (superData == null) throw new ArgumentNullException();
            if (superData.Ret == 0)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    int index = 0;
                    this.FocusImageList.Clear();
                    foreach (FocusImageData fi in superData.FocusImages.List)
                    {
                        fi.IsFirst = index == 0;
                        index++;
                        this.FocusImageList.Add(fi);
                    }
                    //this.SubjectModuleTitle = superData.Subjects.ModuleTitle;
                    //foreach (var sd in superData.Subjects.List)
                    //{
                    //    this.SubjectList.Add(sd);
                    //}

                    foreach (var album in superData.Albums.List)
                    {
                        this.AlbumList.Add(album);
                    }
                    index = 0;
                    foreach (var category in superData.Categories.List)
                    {
                        category.IsFirst = index == 0;
                        index++;
                        DiscoverViewModel.CategoryList.Add(category);
                    }
                }, System.Windows.Threading.DispatcherPriority.Background);
            }
            else
            {
                DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", superData.Message);
            }
        }
        private void GetHotSoundsAction()
        {
            if (this.HotSoundsService != null)
            {
                this.HotSoundsService.GetData(result =>
                {
                    var res = result as HotSoundsResult;

                    if (res.Ret == 0)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            foreach (var cate in res.Categories)
                            {
                                SoundCache.Instance.SetData(cate.Sounds);
                                this.HotSoundsCategories.Add(cate);
                            }
                        });

                    }
                }, new BaseParam
                {
                    Device = DeviceType.pc
                });
            }
        }
        public void Initialize()
        {
            this.GetFocusImageDataAction();
            //this.GetHotSoundsAction();
            //this.GetCategoryListAction();
        }

        #endregion

        #region construction

        public DiscoverViewModel()
        {
            DiscoverViewModel.CategoryList = new ObservableCollection<CategoryData>();
            this.FocusImageList = new ObservableCollection<FocusImageData>();
            this.SubjectList = new ObservableCollection<SubjectData>();
            this.HotSoundsCategories = new ObservableCollection<CategoryData>();
            this.AlbumList = new ObservableCollection<AlbumData>();

            for (int i = 0; i < 6; i++)
            {
                this.FocusImageList.Add(new FocusImageData { IsFirst = i == 0 });
            }

            //点击分类命令
            this.ShowCategoryDetailCommand = new DelegateCommand<string>(cateName =>
            {
                //发送事件
                this.EventAggregator.GetEvent<TagEvent>().Publish(new TagEventArgument
                {
                    TagKey = cateName
                });
            });
        }

        #endregion
    }
}

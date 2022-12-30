using SkillProfiCompanyWpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SkillProfiCompanyWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ViewModel viewModel;
        private static double angleRotation = 0;
        private readonly static double serviceUpdateView = 600;
        private readonly static double applicattionViewStage1 = 0;
        private readonly static double applicattionViewStage2 = 10;
        private readonly static double applicattionViewStage3 = 810;
        private readonly static double applicattionViewStage4 = 1210;
        private readonly static double controlView = 560;
        private bool checkOpenCurtain = false;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();

            NameScope.SetNameScope(contextMenu, NameScope.GetNameScope(this));
            NameScope.SetNameScope(MenuForServiceList, NameScope.GetNameScope(this));
            NameScope.SetNameScope(ContextForListBlogs, NameScope.GetNameScope(this));
            NameScope.SetNameScope(ContextForListProject, NameScope.GetNameScope(this));
            NameScope.SetNameScope(MenuForOpenApplication, NameScope.GetNameScope(this));
            NameScope.SetNameScope(MenuForContactList1, NameScope.GetNameScope(this));
            NameScope.SetNameScope(MenuForContactList2, NameScope.GetNameScope(this));
            NameScope.SetNameScope(MenuForContactList3, NameScope.GetNameScope(this));

            DataContext = viewModel;
            HomeView();
        }

        #region ServiceAnimation
        private bool checkSeкvice = true;
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(serviceUpdateView, 0, TimeSpan.FromSeconds(0.5));
            ServiceUpdateView.BeginAnimation(HeightProperty, animation);
            checkSeкvice = true;
        }

        private void SaveService_Click(object sender, RoutedEventArgs e)
        {
            HideServiseUpdate();
        }

        private void ServiceUpdate_Click(object sender, RoutedEventArgs e)
        {
            ViewServiceUpdate();
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            if (checkSeкvice)
            {
                ViewServiceUpdate();
            }
            else
            {
                HideServiseUpdate();
            }
        }

        private void ViewServiceUpdate()
        {
            DoubleAnimation animation = new DoubleAnimation(0, serviceUpdateView, TimeSpan.FromSeconds(0.5));
            ServiceUpdateView.BeginAnimation(HeightProperty, animation);
            checkSeкvice = false;
        }

        private void HideServiseUpdate()
        {
            if (ServiceHeader.Text != String.Empty && ServiceDescription.Text != String.Empty)
            {
                viewModel.ServiceUpdate.Execute(new object[] { ServiceId.Text, ServiceHeader.Text, ServiceDescription.Text });

                DoubleAnimation animation = new DoubleAnimation(serviceUpdateView, 0, TimeSpan.FromSeconds(0.5));
                ServiceUpdateView.BeginAnimation(HeightProperty, animation);
                checkSeкvice = true;
            }
            else
            {
                viewModel.ServiceError.Execute("Заполните все поля!");
            }
        }
        #endregion

        #region ApplicationAnimation
        private void OpenApplication_Click(object sender, RoutedEventArgs e)
        {
            AppClient application = (AppClient)ApplicationTable.SelectedItem;
            SelectStatus.SelectedItem = viewModel.StatusClient[Convert.ToInt32(application.ApplicationStatus.Id) - 1];

            DoubleAnimation animationWidthOpen = new DoubleAnimation(applicattionViewStage1, applicattionViewStage4, TimeSpan.FromSeconds(0.5));
            DoubleAnimation animationHeightOpen = new DoubleAnimation(applicattionViewStage1, applicattionViewStage2, TimeSpan.FromSeconds(0.5));
            animationWidthOpen.Completed += AnimationWidthOpen_Completed;
            GreyBackground.Visibility = Visibility.Visible;
            AppClienView.BeginAnimation(HeightProperty, animationHeightOpen);
            AppClienView.BeginAnimation(WidthProperty, animationWidthOpen);
        }

        private void AnimationWidthOpen_Completed(object sender, EventArgs e)
        {
            DoubleAnimation animationHeightOpen = new DoubleAnimation(applicattionViewStage2, applicattionViewStage3, TimeSpan.FromSeconds(0.5))
            {
                EasingFunction = new QuadraticEase()
            };
            AppClienView.BeginAnimation(HeightProperty, animationHeightOpen);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animationHeightClose = new DoubleAnimation(applicattionViewStage3, applicattionViewStage2, TimeSpan.FromSeconds(0.5));
            animationHeightClose.Completed += Animation1_Completed;
            AppClienView.BeginAnimation(HeightProperty, animationHeightClose);
        }

        private void Animation1_Completed(object sender, EventArgs e)
        {
            DoubleAnimation animationWidthClose = new DoubleAnimation(applicattionViewStage4, applicattionViewStage1, TimeSpan.FromSeconds(0.5));
            DoubleAnimation animationHeightClose = new DoubleAnimation(applicattionViewStage2, applicattionViewStage1, TimeSpan.FromSeconds(0.5));
            animationWidthClose.EasingFunction = new QuadraticEase();
            animationHeightClose.Completed += AnimationHeightClose_Completed;
            AppClienView.BeginAnimation(WidthProperty, animationWidthClose);
            AppClienView.BeginAnimation(HeightProperty, animationHeightClose);
        }

        private void AnimationHeightClose_Completed(object sender, EventArgs e)
        {
            GreyBackground.Visibility = Visibility.Hidden;
        }

        private void GreyBackground_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(applicattionViewStage3, applicattionViewStage2, TimeSpan.FromSeconds(0.5));
            animation.Completed += Animation1_Completed;
            AppClienView.BeginAnimation(HeightProperty, animation);
        }

        private void MoveNextApplication_Click(object sender, RoutedEventArgs e)
        {
            AppClient application = (AppClient)ApplicationList.SelectedItem;
            SelectStatus.SelectedItem = viewModel.StatusClient[Convert.ToInt32(application.ApplicationStatus.Id) - 1];
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            AppClient application = (AppClient)ApplicationTable.SelectedItem;
            viewModel.UpdateStatusCommand.Execute(new object[] { application.Id, textBlock.Text });
        }

        #endregion

        #region Control

        private void TabControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (checkOpenCurtain)
            {
                ArrowAnimation();
                CurtainAnimation(checkOpenCurtain);
            }
        }

        private void ArrowAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation(0 + angleRotation, 180 + angleRotation, TimeSpan.FromSeconds(0.25));
            RotateTransform rotateTransform = new RotateTransform()
            {
                CenterX = Arrow.Width / 4,
                CenterY = Arrow.Height / 2
            };
            ArrowButton.RenderTransform = rotateTransform;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
            angleRotation += 180;
        }

        private void CurtainAnimation(bool checkOpenCurtain)
        {
            double startPosition = 0;
            double finishPosition = 470;

            if (checkOpenCurtain)
            {
                TranslateTransform translate = new TranslateTransform();
                Settings.RenderTransform = translate;
                DoubleAnimation doubleAnimation = new DoubleAnimation(finishPosition, startPosition, TimeSpan.FromSeconds(0.25));
                translate.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
                this.checkOpenCurtain = false;
            }
            else
            {
                TranslateTransform translate = new TranslateTransform();
                Settings.RenderTransform = translate;
                DoubleAnimation doubleAnimation = new DoubleAnimation(startPosition, finishPosition, TimeSpan.FromSeconds(0.25));
                translate.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);
                this.checkOpenCurtain = true;
            }
        }

        private void HomeView()
        {
            TabControlView.SelectedItem = HomePageView;
        }

        private void ArrowButton_Click(object sender, RoutedEventArgs e)
        {
            ArrowAnimation();
            CurtainAnimation(checkOpenCurtain);
        }

        private void LoginButton_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordUser.Password = String.Empty;
            PasswordRepeat.Password = String.Empty;
        }

        private bool checkControlView = false;
        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkControlView)
            {
                DoubleAnimation animation = new DoubleAnimation(controlView, 0, TimeSpan.FromSeconds(0.5));
                ControlView.BeginAnimation(HeightProperty, animation);
                checkControlView = false;
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation(0, controlView, TimeSpan.FromSeconds(0.5));
                ControlView.BeginAnimation(HeightProperty, animation);
                checkControlView = true;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            HomeView();
            if (checkControlView)
            {
                ControlButton_Click(sender, e);
            }
        }

        private void gridForOtv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                viewModel.UpdateDatebase.Execute();
            }
        }

        #endregion
    }
}

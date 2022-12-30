using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SkillProfiCompanyWpf.Models;
using SkillProfiCompanyWpf.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace SkillProfiCompanyWpf.ViewModels
{
    public class ViewModel : BindableBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Model _model;
        private View.BlogView BlogView;
        private View.ProjectView ProjectView;
        private enum DateFilter
        {
            Today,
            Yesterday,
            Week,
            Month,
        }

        public ViewModel()
        {
            _model = new Model(_httpClient);
            _model.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            #region Login

            Login = new DelegateCommand<object[]>(obj => {

                string login = obj[0].ToString();
                var password = (System.Windows.Controls.PasswordBox)obj[1];

                if (login != String.Empty && password.Password != String.Empty)
                {
                    _model.Login(login, password.Password);
                }
                else
                {
                    _model.LoginError("Заполните все поля");
                }
            });

            Registration = new DelegateCommand<object[]>(obj => {

                string login = obj[0].ToString();
                var password = (System.Windows.Controls.PasswordBox)obj[1];
                var passwordRepeat = (System.Windows.Controls.PasswordBox)obj[1];
                string email = obj[3].ToString();

                if (ValidRegistration(login, password.Password, passwordRepeat.Password, email))
                {
                    _model.Registration(login, password.Password, email);
                }
            });

            AbortRegistration = new DelegateCommand(() => {

                _model.AbortRegistration();
            });

            RegistrationView = new DelegateCommand(() => {

                _model.RegistrationView();
            });

            Logout = new DelegateCommand(() => {

                _model.Logout();
            });

            ControlSave = new DelegateCommand<object[]>(obj =>
            {
                _model.ControlView(obj);
            });

            RepeatRequest = new DelegateCommand(() => {

                _model.RepMethod();
            });

            CloseViewGlogalError = new DelegateCommand(() => {

                _model.GlobalErrorClose();
            });

            UpdateDatebase = new DelegateCommand(() => {

                _model.UpdateCollection();
            });

            SaveMainUrl = new DelegateCommand<string>(url => {

                _model.ServerAndPort(url);
            });



            #endregion

            #region Application

            FilterApplicationByDate = new DelegateCommand<string>(buttonName => {

                DateTime date = DateTime.Today;

                if (buttonName == DateFilter.Today.ToString())
                {
                    date = DateTime.Today;
                }
                else if(buttonName == DateFilter.Yesterday.ToString())
                {
                    date = DateTime.Today.AddDays(-1);
                }
                else if (buttonName == DateFilter.Week.ToString())
                {
                    date = DateTime.Today.AddDays(-7);
                }
                else if (buttonName == DateFilter.Month.ToString())
                {
                    date = DateTime.Today.AddDays(-31);
                }
                else
                {
                    date = new DateTime(1900, 1, 1);
                }

                _model.FilterApplicationByDate(date, DateTime.Today.AddDays(1).AddSeconds(-1));
            });

            FilterApplicationByDate_2 = new DelegateCommand<object[]>(obj => {

                if (obj[0] != null && obj[1] != null)
                {
                    DateTime startDate = Convert.ToDateTime(obj[0]);
                    DateTime finishDate = Convert.ToDateTime(obj[1]);

                    _model.FilterApplicationByDate(startDate, finishDate);
                }
            });

            FilterApplicationByName = new DelegateCommand<AppClient>(SelectedValue => {

                _model.FilterApplicationByName(SelectedValue.UserFullName);
            });

            OpenUserApplication = new DelegateCommand<AppClient>(SelectedValue => {

                _model.ViewApplication(SelectedValue);
                _model.ApplicationByUserName(SelectedValue.UserFullName);
            });

            AddApplication = new DelegateCommand<object[]>(obj => {

                string name = obj[0].ToString();
                string email = obj[1].ToString();
                string message = obj[2].ToString();

                if (name != String.Empty && email != String.Empty && message != String.Empty)
                {
                    _model.AddApplication(name, email, message);
                }
                else
                {
                    _model.ApplicationError("Заполните все поля!");
                }
            });

            UpdateStatusCommand = new DelegateCommand<object[]>(i => {

                StatusClient status = (StatusClient)i[0];
                _model.UpdateStatusClient(status.Id, i[1].ToString());
            });

            MoveNextApplication = new DelegateCommand<AppClient>(application => {

                _model.ViewApplication(application);
            });
            #endregion

            #region Project

            AddFileProject = new DelegateCommand(() => {

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                if (fileDialog.ShowDialog() == true)
                {
                    _model.SaveImageProject(fileDialog.FileName);
                }

            });

            DeleteProject = new DelegateCommand<string>(itemId => {

                _model.DeleteProject(itemId);
            });

            AddProject = new DelegateCommand(() => {

                OpenWindowProject();
                _model.ViewProject(null, true);
            });

            ViewProject = new DelegateCommand<ProjectClient>(project => {

                if (project != null)
                {
                    OpenWindowProject();
                    _model.ViewProject(project);
                }
            });

            UpdateProject = new DelegateCommand<ProjectClient>(project => {

                if (project != null)
                {
                    OpenWindowProject();
                    _model.ViewProject(project, true);
                }
            });

            SaveProject = new DelegateCommand<ProjectClient>(project => {

                _model.UpdateProject(project);
            });

            ProjectError = new DelegateCommand<string>(error => {

                _model.ErrorProject(error);
            });

            #endregion

            #region Service

            ServiceDelete = new DelegateCommand<string>(id =>
            {
                _model.DeleteService(id);
            });

            ServiceViewClose = new DelegateCommand(() =>
            {
                _model.UpdateViewClose();
            });

            ServiceUpdateView = new DelegateCommand<string>(id =>
            {
                _model.UpdateServiceView(id);
            });

            ServiceUpdate = new DelegateCommand<object[]>(obj =>
            {
                _model.UpdateService(obj[0].ToString(), obj[1].ToString(), obj[2].ToString());
            });

            ServiceError = new DelegateCommand<string>(error =>
            {
                _model.ServiceError(error);
            });

            #endregion

            #region Blog

            AddFileBlog = new DelegateCommand(() => {

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                if (fileDialog.ShowDialog() == true)
                {
                    _model.SaveImageBlog(fileDialog.FileName);
                }

            });

            DeleteBlog = new DelegateCommand<string>(itemId => {

                _model.DeleteBlog(itemId);
            });

            AddBlog = new DelegateCommand(() => {

                OpenWindowBlog();
                _model.ViewBlog(null, true);
            });

            ViewBlog = new DelegateCommand<BlogClient>(blog => {

                if (blog != null)
                {
                    OpenWindowBlog();
                    _model.ViewBlog(blog);
                }
            });

            UpdateBlog = new DelegateCommand<BlogClient>(blog => {

                if (blog != null)
                {
                    OpenWindowBlog();
                    _model.ViewBlog(blog, true);
                }
            });

            SaveBlog = new DelegateCommand<BlogClient>(blog => {

                _model.UpdateBlog(blog);
            });

            BlogError = new DelegateCommand<string>(error => {

                _model.ErrorBlog(error);
            });

            #endregion

            #region Contact

            AddContact = new DelegateCommand(() => {

                _model.ViewContact(null);
            });       
            
            UpdateContact = new DelegateCommand<ContactClient>(contact => {

                if (contact != null)
                {
                    _model.ViewContact(contact);
                }
            });

            SaveContact = new DelegateCommand<object[]>(obj => {

                string id = obj[0].ToString();
                string header = obj[1].ToString();
                string value = obj[2].ToString();

                if (header != String.Empty && value != String.Empty)
                {
                    _model.UpdateContact(id, header, value);
                }
                else
                {
                    _model.ContactError("Заполните все поля!");
                }
            });

            DeleteContact = new DelegateCommand<string>(id =>
            {

                _model.DeleteContect(id);
            });

            CreateContactClose = new DelegateCommand(() => {

                _model.CreateContactCloseView();
            });

            #endregion
        }

        #region Login

        public DelegateCommand Start { get; }
        public DelegateCommand<object[]> Login { get; }
        public DelegateCommand<object[]> Registration { get; }
        public DelegateCommand RegistrationView { get; }
        public DelegateCommand AbortRegistration { get; }
        public DelegateCommand Logout { get; }
        public DelegateCommand<object> PlusOrMinus { get; }
        public DelegateCommand<object[]> ControlSave { get; }
        public DelegateCommand RepeatRequest { get; }
        public DelegateCommand CloseViewGlogalError { get; }
        public DelegateCommand UpdateDatebase { get; }
        public DelegateCommand<string> SaveMainUrl { get; }

        #endregion

        #region Application
        public DelegateCommand<string> FilterApplicationByDate { get; }
        public DelegateCommand<object[]> FilterApplicationByDate_2 { get; }
        public DelegateCommand<AppClient> FilterApplicationByName { get; }
        public DelegateCommand<AppClient> OpenUserApplication { get; }
        public DelegateCommand<object[]> AddApplication { get; }
        public DelegateCommand<object[]> UpdateStatusCommand { get; }
        public DelegateCommand<AppClient> MoveNextApplication { get; }
        #endregion

        #region Project
        public DelegateCommand AddFileProject { get; }
        public DelegateCommand<string> DeleteProject { get; }
        public DelegateCommand AddProject { get; }
        public DelegateCommand<ProjectClient> ViewProject { get; }
        public DelegateCommand<ProjectClient> UpdateProject { get; }
        public DelegateCommand<ProjectClient> SaveProject { get; }
        public DelegateCommand<string> ProjectError { get; }

        #endregion

        #region Service
        public DelegateCommand<string> ServiceDelete { get; }
        public DelegateCommand<string> ServiceUpdateView { get; }
        public DelegateCommand ServiceViewClose { get; }
        public DelegateCommand<object[]> ServiceUpdate { get; }
        public DelegateCommand<string> ServiceError { get; }
        #endregion

        #region Blog

        public DelegateCommand AddFileBlog { get; }
        public DelegateCommand<string> DeleteBlog { get; }
        public DelegateCommand AddBlog { get; }
        public DelegateCommand<BlogClient> ViewBlog { get; }
        public DelegateCommand<BlogClient> UpdateBlog { get; }
        public DelegateCommand<BlogClient> SaveBlog { get; }
        public DelegateCommand<string> BlogError { get; }

        #endregion

        #region Contact

        public DelegateCommand AddContact { get; }
        public DelegateCommand<ContactClient> UpdateContact { get; }
        public DelegateCommand<object[]> SaveContact { get; }
        public DelegateCommand<string> DeleteContact { get; }
        public DelegateCommand CreateContactClose { get; }


        #endregion

        public ObservableCollection<AppClient> Clients => _model.Clients;
        public ObservableCollection<AppClient> TempClients => _model.TempClients;
        public ObservableCollection<AppClient> TempClientsUserName => _model.TempClientsUserName;
        public ObservableCollection<StatusClient> StatusClient => _model.StatusClient;
        public ObservableCollection<ServiceClient> ServiceClients => _model.ServiceClients;
        public ObservableCollection<BlogClient> BlogClients => _model.BlogClients;
        public ObservableCollection<ProjectClient> ProjectClients => _model.ProjectClients;
        public ObservableCollection<ContactClient> ContactClientsAddress => _model.ContactClientsAddress;
        public ObservableCollection<ContactClient> ContactClientsTel => _model.ContactClientsTel;
        public ObservableCollection<ContactClient> ContactClientsEmail => _model.ContactClientsEmail;
        public Dictionary<string, string> UIElements => _model.UIElements;
        public ClientViewModel ClientView => _model.ClientView;

        private void OpenWindowProject()
        {
            ProjectView = new View.ProjectView();
            ProjectView.Show();
        }

        private void OpenWindowBlog()
        {
            BlogView = new View.BlogView();
            BlogView.Show();
        }

        private bool ValidRegistration(string login, string password, string passwordRepeat, string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                _model.LoginError("Почта указана не верно");
            }

            if (password == passwordRepeat)
            {
                if (login.Length > 4 && password.Length > 4 && email.Length > 4)
                {
                    return true;
                }
                else
                {
                    _model.LoginError("Минимальная длина пароля - 5 символов");
                    return false;
                }
            }
            else
            {
                _model.LoginError("Пароли не совпадают");
                return false;
            }
        }
    }
}

using Newtonsoft.Json;
using Prism.Mvvm;
using SkillProfiCompanyWpf.Interface;
using SkillProfiCompanyWpf.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SkillProfiCompanyWpf.Models
{
    public class Model : BindableBase
    {
        private readonly HttpClient _httpClient;
        private Action RepeatMethod;
        private User _user;
        private string MainUrl = "https://localhost:5001";

        public ClientViewModel ClientView { get; set; }
   

        public ObservableCollection<AppClient> Clients;
        public ObservableCollection<AppClient> TempClients;
        public ObservableCollection<AppClient> TempClientsUserName;
        public ObservableCollection<StatusClient> StatusClient;
        public ObservableCollection<ServiceClient> ServiceClients;
        public ObservableCollection<BlogClient> BlogClients;
        public ObservableCollection<ProjectClient> ProjectClients;
        public ObservableCollection<ContactClient> ContactClientsAddress;
        public ObservableCollection<ContactClient> ContactClientsTel;
        public ObservableCollection<ContactClient> ContactClientsEmail;
        public Dictionary<string, string> UIElements;

        public Model(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _user = new User(new List<string>(){ "Anonymous" }, String.Empty);
            _user.Update(new Dictionary<string, object>() { { "LoginServer", System.IO.File.ReadAllText("../../MainUrl.txt") } });

            ClientView = _user.ClientViewModel;
            RaisePropertyChanged("ClientView");
            UpdateDatabase();
        }

        #region Load

        private string DownloadDatabase(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync($"{MainUrl}/{url}/items").Result;
            }
        }

        private void UpdateDatabase()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                { 
                    try
                    {
                        UpdateCollection();
                    }
                    catch
                    {
                        GlobalError("При попытке обновить базу данных произошла ошибка. Проверьте соединение с сервером");
                    }
                    finally
                    {
                        Task.Delay(new TimeSpan(0, 1, 0)).Wait();
                    }
                }
            });
        }

        public void UpdateCollection()
        {
            try
            {
                RepeatMethod = UpdateCollection;
                ServiceClients = JsonConvert.DeserializeObject<ObservableCollection<ServiceClient>>(DownloadDatabase("api/service"));
                BlogClients = JsonConvert.DeserializeObject<ObservableCollection<BlogClient>>(DownloadDatabase("api/blog"));
                ProjectClients = JsonConvert.DeserializeObject<ObservableCollection<ProjectClient>>(DownloadDatabase("api/project"));
                UIElements = GetUI();
                GetContacts();
                GlobalErrorClose();

                RaisePropertyChanged("ServiceClients");
                RaisePropertyChanged("BlogClients");
                RaisePropertyChanged("ProjectClients");
                RaisePropertyChanged("UIElements");
                RepeatMethod = null;
            }
            catch (Exception ex)
            {
                GlobalError(ex.InnerException.Message);
            }
        }

        private Dictionary<string, string> GetUI()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            var uIElements = JsonConvert.DeserializeObject<ObservableCollection<UIElement>>(_httpClient.GetStringAsync($"{MainUrl}/api/ui/items/home-page").Result);
            var home = JsonConvert.DeserializeObject<ObservableCollection<UIElement>>(_httpClient.GetStringAsync($"{MainUrl}/api/ui/items/home-header").Result);
            var contact = JsonConvert.DeserializeObject<ObservableCollection<UIElement>>(_httpClient.GetStringAsync($"{MainUrl}/api/ui/items/contact-page").Result);

            uIElements.AddRange(home).AddRange(contact).Remove(uIElements.FirstOrDefault(e => e.Key.Contains("control")));

            for (int i = 0; i < uIElements.Count; i++)
            {
                map.Add(uIElements[i].Key, uIElements[i].Value);
            }
            return map;
        }

        public void RepMethod()
        {
            RepeatMethod?.Invoke();
            RepeatMethod = null;
        }

        public void GlobalError(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "GlobalErrorVisibility", Visibility.Visible.ToString() }, { "GlobalError", error } });
            RaisePropertyChanged("ClientView");
        }

        public void GlobalErrorClose()
        {
            _user.Update(new Dictionary<string, object>() { { "GlobalErrorVisibility", Visibility.Collapsed.ToString() }, { "GlobalError", String.Empty } });
            RaisePropertyChanged("ClientView");
        }

        #endregion

        #region Login

        public void AbortRegistration()
        {
            _user.Update(Parameter.ViewLogin());
            RaisePropertyChanged("ClientView");
        }

        public async void Login(string login, string password)
        {
            try
            {
                JsonContent userLogin = JsonContent.Create(new { LoginProp = login, Password = password });
                string result = await _httpClient.PostAsync($"{MainUrl}/Account/LoginWpf", userLogin).Result.Content.ReadAsStringAsync();

                if (result != String.Empty)
                {
                    string roles = await _httpClient.GetStringAsync($"{MainUrl}/Roles/GetUserRoles/{result}");
                    List<string> allRoles = JsonConvert.DeserializeObject<List<string>>(roles);

                    _user = new User(allRoles, login);
                    ClientView = _user.ClientViewModel;

                    if (_user.GetPrivilege(10))
                    {
                        _user.Update(Parameter.ViewAuthorized(login, "Admin"));

                        Clients = JsonConvert.DeserializeObject<ObservableCollection<AppClient>>(await _httpClient.GetStringAsync($"{MainUrl}/api/application/items"));
                        StatusClient = JsonConvert.DeserializeObject<ObservableCollection<StatusClient>>(await _httpClient.GetStringAsync($"{MainUrl}/api/application/items/allStatuses"));
                        ViewApplications(e => e.Id < 0);    

                        RaisePropertyChanged("Clients");
                        RaisePropertyChanged("StatusClient");
                    }
                    else if (_user.GetPrivilege(1))
                    {
                        _user.Update(Parameter.ViewAuthorized(login));
                        _user.Update(new Dictionary<string, object>() { { "ApplicationVisibilityDesktop", Visibility.Collapsed.ToString() }, { "ApplicationEmailRead", false }, { "ApplicationMessageRead", false } });
                    }
                    else
                    {
                        _user.Update(Parameter.ViewAuthorized(login));
                        _user.Update(new Dictionary<string, object>(){ { "ApplicationVisibilityDesktop", Visibility.Collapsed.ToString() } });
                    }
                }
                else
                {
                    LoginError("Проверьте введенные данные!");
                }

                ServerAndPort(String.Empty);
                RaisePropertyChanged("ClientView");
            }
            catch
            {
                GlobalError("При попытке обновить базу данных произошла ошибка. Проверьте соединение с сервером");
            }
        }

        public async void Registration(string login, string password, string email)
        {
            JsonContent userRegistration = JsonContent.Create(new { LoginProp = login, Password = password, Email = email });
            var result = await _httpClient.PostAsync($"{MainUrl}/Account/RegistrationWpf", userRegistration);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _user.Update(Parameter.ViewLogin());
            }

            RaisePropertyChanged("ClientView");
        }

        public async void Logout()
        {
            var result = await _httpClient.GetAsync($"{MainUrl}/Account/Logout");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _user = new User(new List<string>() { "Anonymous" }, String.Empty);
                ClientView = _user.ClientViewModel;
                _user.Update(Parameter.ViewLogin());
                ServerAndPort(String.Empty);
            }

            RaisePropertyChanged("ClientView");
        }

        public void RegistrationView()
        {
            _user.Update(Parameter.ViewRegistration());
            RaisePropertyChanged("ClientView");
        }

        public void LoginError(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "LoginErrorContent", error } });
            RaisePropertyChanged("ClientView");
        }

        public async void ControlView(object[] obj)
        {
            ServerAndPort(obj.Last().ToString());

            List<dynamic> ui = new List<dynamic>();
            var keys = UIElements.Keys.ToList();

            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i].ToString() == String.Empty)
                {
                    ControlError("Заполните все поля!");
                    return;
                }

                ui.Add(new { key = keys[i], value = obj[i].ToString() });
            }

            JsonContent uiUpdate = JsonContent.Create(new { newElements = ui });
            var result = await _httpClient.PutAsync($"{MainUrl}/api/ui/items/", uiUpdate);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UIElements = GetUI();
                ControlError(String.Empty);
                RaisePropertyChanged("UIElements");
            }

            RaisePropertyChanged("ClientView");
        }

        private void ControlError(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "LoginControlError", error } });
            RaisePropertyChanged("ClientView");
        }

        public void ServerAndPort(string url)
        {
            if (url == String.Empty)
            {
                url = "https://localhost:5001";
            }

            _user.Update(new Dictionary<string, object>() { { "LoginServer", url } });
            MainUrl = url;

            RaisePropertyChanged("ClientView");
            System.IO.File.WriteAllText("../../MainUrl.txt", url);
        }

        #endregion

        #region Application

        private Func<AppClient, bool> filter;
        public void ViewApplications(Func<AppClient, bool> func, bool checkFilter = false)
        {
            int num = 0;

            if (checkFilter)
            {
                var list = Clients.Where(filter).ToList();

                list.ForEach(i =>
                {
                    i.Number = ++num;
                });

                TempClients = new ObservableCollection<AppClient>(Clients.Where(filter).ToList());
            }
            else
            {
                var list = Clients.Where(func).ToList();

                list.ForEach(i =>
                {
                    i.Number = ++num;
                });

                TempClients = new ObservableCollection<AppClient>(Clients.Where(func).ToList());
                filter = func;
            }

            _user.Update(new Dictionary<string, object>(){

                { "ApplicationAllApplication", $"Всего заявок: {Clients.Count}" },
                { "ApplicationViewApplication", $"Показано заявок: {num}"},            
            });

            RaisePropertyChanged("TempClients");
            RaisePropertyChanged("ClientView");
        }

        public void FilterApplicationByDate(DateTime startDate, DateTime finishDate)
        {
            ViewApplications(e => e.Date >= startDate && e.Date <= finishDate);
        }

        public void FilterApplicationByName(string name)
        {
            ViewApplications(e => e.UserFullName.Contains(name));
        }

        public void ApplicationByUserName(string name)
        {
            TempClientsUserName = new ObservableCollection<AppClient>(Clients.ToList().FindAll(e => e.UserFullName == name));
            RaisePropertyChanged("TempClientsUserName");
        }

        public void ViewApplication(AppClient appClient)
        {
            _user.Update(new Dictionary<string, object>(){

                { "ApplicationId", appClient.Id },
                { "ApplicationUserFullName", appClient.UserFullName },
                { "ApplicationUserMessage", appClient.UserMessage },
            });

            RaisePropertyChanged("ClientView");
        }

        public async void AddApplication(string login, string email, string message)
        {
            JsonContent application = JsonContent.Create(new { userFullName = login, userEmail = email, userMessage = message });
            var result = await _httpClient.PostAsync($"{MainUrl}/api/application/items", application);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _user.Update(new Dictionary<string, object>() { { "ApplicationEmailText", String.Empty }, { "ApplicationMessageText", String.Empty }, { "ApplicationError", String.Empty } });
            }

            RaisePropertyChanged("ClientView");
        }

        public async void UpdateStatusClient(int newStatusId, string applicationId)
        {
            JsonContent app = JsonContent.Create(new { NewStatusId = newStatusId, ApplicationId = applicationId });
            var result = await _httpClient.PutAsync($"{MainUrl}/api/application/items/", app);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Clients = JsonConvert.DeserializeObject<ObservableCollection<AppClient>>(await _httpClient.GetStringAsync($"{MainUrl}/api/application/items"));
                ViewApplications(e => e.Id < 0, true);
                RaisePropertyChanged("Clients");
            }

            RaisePropertyChanged("ClientView");
        }

        public void ApplicationError(string error)
        {
            _user.Update(new Dictionary<string, object>(){ { "ApplicationError", error } });
            RaisePropertyChanged("ClientView");
        }

        #endregion

        #region Project

        private string image;
        public void ViewProject(ProjectClient project, bool edit = false)
        {
            if (project == null)
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "ProjectTitle", "Создание" },
                    { "ProjectId", 0 },
                    { "ProjectHeader", String.Empty },
                    { "ProjectDescription", String.Empty },
                    { "ProjectImage", null },
                });
            }
            else
            {
                image = project.Img;
                _user.Update(new Dictionary<string, object>()
                {
                    { "ProjectId", int.Parse(project.Id) },
                    { "ProjectHeader", project.Header },
                    { "ProjectDescription", project.Description },
                    { "ProjectImage", project.Image },
                    { "ProjectFileName", String.Empty },
                });
            }

            if (edit)
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "ProjectIsReadOnly", false },
                    { "ProjectVisibility", Visibility.Visible.ToString() },
                });
            }
            else
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "ProjectId", -1 },
                    { "ProjectIsReadOnly", true },
                    { "ProjectVisibility", Visibility.Collapsed.ToString() },
                });
            }

            RaisePropertyChanged("ClientView");
        }

        public async void DeleteProject(string id)
        {
            var result = await _httpClient.DeleteAsync($"{MainUrl}/api/project/items/{id}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ProjectClients = JsonConvert.DeserializeObject<ObservableCollection<ProjectClient>>(DownloadDatabase("api/project"));
                RaisePropertyChanged("ProjectClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public async void UpdateProject(ProjectClient project)
        {
            if (project.Image == null)
            {
                project.Img = image;
            }

            JsonContent proj = JsonContent.Create(new { Id = int.Parse(project.Id), Header = project.Header, Description = project.Description, ImageProject = project.Img});
            var result = await _httpClient.PutAsync($"{MainUrl}/api/project/items/", proj);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ProjectClients = JsonConvert.DeserializeObject<ObservableCollection<ProjectClient>>(DownloadDatabase("api/project"));
                _user.Update(new Dictionary<string, object>() { { "ProjectError", String.Empty } });
                RaisePropertyChanged("ProjectClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public void SaveImageProject(string fileName)
        {
            ClientView.ProjectImage = new BitmapImage(new Uri(fileName));
            ClientView.ProjectFileName = fileName.Split('\\').Last();
            RaisePropertyChanged("ClientView");
        }

        public void ErrorProject(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "ProjectError", error } });
            RaisePropertyChanged("ClientView");
        }

        #endregion

        #region Service

        public async void DeleteService(string id)
        {
            var result = await _httpClient.DeleteAsync($"{MainUrl}/api/service/items/{id}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ServiceClients = JsonConvert.DeserializeObject<ObservableCollection<ServiceClient>>(DownloadDatabase("api/service"));
                RaisePropertyChanged("ServiceClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public void UpdateViewClose()
        {
            _user.Update(new Dictionary<string, object>() { { "ServiceId", 0 }, { "ServiceHeader", String.Empty }, { "ServiceDescription", String.Empty } });
            RaisePropertyChanged("ClientView");
        }

        public async void UpdateServiceView(string id)
        {
            if (int.TryParse(id, out int serviceId))
            {
                string result = await _httpClient.GetStringAsync($"{MainUrl}/api/service/items/");
                var sercises = JsonConvert.DeserializeObject<ObservableCollection<ServiceClient>>(result);

                ServiceClient service = sercises.FirstOrDefault(e => e.Id == id);
                _user.Update(new Dictionary<string, object>() { { "ServiceId", serviceId }, { "ServiceHeader", service.Header }, { "ServiceDescription", service.Description } });
            }
            else
            {
                _user.Update(new Dictionary<string, object>() { { "ServiceId", 0 }, { "ServiceHeader", String.Empty }, { "ServiceDescription", String.Empty } });
            }

            RaisePropertyChanged("ClientView");
        }

        public async void UpdateService(string id, string header, string description)
        {
            JsonContent service = JsonContent.Create(new { Id = int.Parse(id), Header = header, Description = description });
            var result = await _httpClient.PutAsync($"{MainUrl}/api/service/items/", service);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ServiceClients = JsonConvert.DeserializeObject<ObservableCollection<ServiceClient>>(DownloadDatabase("api/service"));
                _user.Update(new Dictionary<string, object>() { { "ServiceError", String.Empty } });
                RaisePropertyChanged("ServiceClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public void ServiceError(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "ServiceError", error } });
            RaisePropertyChanged("ClientView");
        }

        #endregion

        #region Blog

        public void ViewBlog(BlogClient blog, bool edit = false)
        {
            if (blog == null)
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "BlogTitle", "Создание" },
                    { "BlogId", 0 },
                    { "BlogHeader", String.Empty },
                    { "BlogDescription", String.Empty },
                    { "BlogImage", null },
                });
            }
            else
            {
                image = blog.Img;
                _user.Update(new Dictionary<string, object>()
                {
                    { "BlogTitle", "Блог" },
                    { "BlogId", int.Parse(blog.Id) },
                    { "BlogHeader", blog.Header },
                    { "BlogDescription", blog.Description },
                    { "BlogImage", blog.Image },
                    { "BlogFileName", String.Empty },
                });
            }

            if (edit)
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "BlogIsReadOnly", false },
                    { "BlogVisibility", Visibility.Visible.ToString() },
                });
            }
            else
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "BlogId", -1 },
                    { "BlogIsReadOnly", true },
                    { "BlogVisibility", Visibility.Collapsed.ToString() },
                });
            }

            RaisePropertyChanged("ClientView");
        }

        public async void DeleteBlog(string id)
        {
            var result = await _httpClient.DeleteAsync($"{MainUrl}/api/blog/items/{id}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                BlogClients = JsonConvert.DeserializeObject<ObservableCollection<BlogClient>>(DownloadDatabase("api/blog"));
                RaisePropertyChanged("BlogClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public async void UpdateBlog(BlogClient blog)
        {
            if (blog.Image == null)
            {
                blog.Img = image;
            }

            JsonContent bl = JsonContent.Create(new { Id = int.Parse(blog.Id), Header = blog.Header, Description = blog.Description, ImageBlog = blog.Img });
            var result = await _httpClient.PutAsync($"{MainUrl}/api/blog/items/", bl);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                BlogClients = JsonConvert.DeserializeObject<ObservableCollection<BlogClient>>(DownloadDatabase("api/blog"));
                _user.Update(new Dictionary<string, object>() { { "BlogError", String.Empty } });
                RaisePropertyChanged("BlogClients");
            }

            RaisePropertyChanged("ClientView");
        }

        public void SaveImageBlog(string fileName)
        {
            ClientView.BlogImage = new BitmapImage(new Uri(fileName));
            ClientView.BlogFileName = fileName.Split('\\').Last();
            RaisePropertyChanged("ClientView");
        }

        public void ErrorBlog(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "BlogError", error } });
            RaisePropertyChanged("ClientView");
        }

        #endregion

        #region Contact

        private async void GetContacts()
        {
            string contacts = await _httpClient.GetStringAsync($"{MainUrl}/api/contact/items");
            var conts = JsonConvert.DeserializeObject<ObservableCollection<ContactClient>>(contacts);

            ContactClientsAddress = new ObservableCollection<ContactClient>(conts.ToList().Where(e => e.Tag == 0));
            ContactClientsTel = new ObservableCollection<ContactClient>(conts.ToList().Where(e => e.Tag == 1));
            ContactClientsEmail = new ObservableCollection<ContactClient>(conts.ToList().Where(e => e.Tag == 2));

            RaisePropertyChanged("ContactClientsAddress");
            RaisePropertyChanged("ContactClientsTel");
            RaisePropertyChanged("ContactClientsEmail");
        }

        public void ViewContact(ContactClient contact)
        {
            if (contact == null)
            {
                _user.Update(new Dictionary<string, object>()
                {
                    { "ContactError", String.Empty },
                    { "ContactVisibilityCreateMenu", Visibility.Visible.ToString() },
                    { "ContactCreateHeader", "Создание" },
                    { "ContactId", "0" },
                    { "ContactHeader", String.Empty },
                    { "ContactValue", String.Empty },
                    { "ContactVisibilityButtonDelete", Visibility.Collapsed.ToString() },
                });
            }
            else
            {
                _user.Update(new Dictionary<string, object>() 
                {
                    { "ContactError", String.Empty },
                    { "ContactVisibilityCreateMenu", Visibility.Visible.ToString() },
                    { "ContactCreateHeader", "Изменение" },
                    { "ContactId", contact.Id },
                    { "ContactHeader", contact.Header },
                    { "ContactValue", contact.Value },
                    { "ContactVisibilityButtonDelete", Visibility.Visible.ToString() },
                });
            }

            RaisePropertyChanged("ClientView");
        }

        public async void UpdateContact(string id, string header, string value)
        {
            int tag;

            if (value.Contains("@"))
            {
                tag = 2;
            }
            else if (value.StartsWith("89") || value.StartsWith("+79"))
            {
                tag = 1;
            }
            else
            {
                tag = 0;
            }

            JsonContent cont = JsonContent.Create(new { Id = int.Parse(id), Header = header, Value = value, Tag = tag });
            var result = await _httpClient.PutAsync($"{MainUrl}/api/contact/items/", cont);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GetContacts();
            }

            CreateContactCloseView();
        }

        public void CreateContactCloseView()
        {
            _user.Update(new Dictionary<string, object>() { { "ContactVisibilityCreateMenu", Visibility.Collapsed.ToString() } });
            RaisePropertyChanged("ClientView");
        }        

        public async void DeleteContect(string id)
        {
            var result = await _httpClient.DeleteAsync($"{MainUrl}/api/contact/items/{id}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GetContacts();
            }

            CreateContactCloseView();
        }

        public void ContactError(string error)
        {
            _user.Update(new Dictionary<string, object>() { { "ContactError", error } });
            RaisePropertyChanged("ClientView");
        
        }

        #endregion
    }
}

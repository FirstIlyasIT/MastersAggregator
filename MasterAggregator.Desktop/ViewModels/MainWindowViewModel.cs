using Avalonia.Controls;
using MasterAggregator.Desktop.Commands;
using MasterAggregator.Desktop.Models;
using MasterAggregator.Desktop.Services;
using MasterAggregator.Desktop.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MasterAggregator.Desktop.ViewModels;

 
public class MainWindowViewModel : ViewModelBase
{
    private UserSevic _userSevic;
    public MainWindowViewModel(UserSevic userSevic)
    {
        _userSevic = userSevic;
    }

    /// <summary>
    /// устанавлмвает показывать коллекцию заказчиков UserItems
    /// </summary>
    public bool boolIsVisibleUserItems = false;

    /// <summary>
    /// устанавлмвает показывать коллекцию ЗАКАЗОВ OrderItems
    /// </summary>
    public bool boolIsVisibleOrderItems = false;

    /// <summary>
    /// устанавлмвает показывать коллекцию МАСТЕРОВ MasterItems
    /// </summary>
    public bool boolIsVisibleMasterItems = false;

    /// <summary>
    /// вывод названия коллекции с которой работаем на MainWindow
    /// </summary>
    public string TitleList { get; set; }


    #region работаетм с обектом ЗАКАЗЧИКИ (User)  
    private ObservableCollection<User>? _UserItems;
    /// <summary>
    /// получить всех User по API
    /// </summary> 
    public ObservableCollection<User>? UserItems { get => _UserItems; set => Set(ref _UserItems, value); }

    /// <summary>
    /// получить список User в UserItems по клику кнопки заказчики
    /// </summary>
    public void GetAllUser()
    {
        UserItems = new ObservableCollection<User>(_userSevic.GetAll());
        boolIsVisibleUserItems = true;
        TitleList = "СПИСОК ВСЕХ ЗАКАЗЧИКОВ";
        OnPropertyChanged("TitleList");//обновляем данные на странице 
    }

    /// <summary>
    /// Выбранный User из формы
    /// </summary>
    private User _SelectedUser;
    public User SelectedUser { get => _SelectedUser; set => Set(ref _SelectedUser, value); }


    /// <summary>
    /// Удаление ЗАКАЗЧИКА (User)
    /// </summary> 
    private void RemoveUser()
    {
        if (SelectedUser != null)
            UserItems = _userSevic.Delete(SelectedUser);
    }


    /// <summary>
    /// Создание нового ЗАКАЗЧИКА (User)
    /// </summary> 
    private void CreateUser()
    {
        if (UserItems != null)
        {
            User NewUser = new User() { Id = 0, UserFirstName = "", UserName = "", UserPfone = "" };
            var view_model = new UserCreateViewModel(NewUser);
            var view = new UserCreateWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += (_, e) =>
            {
                view.Close();
                //пререзаписываем в бд если корректно заполнили данные для User (передали в e true)
                if (e == true)
                {
                    //добавляем в бд NewUser
                    ObservableCollection<User> updateUsers = _userSevic.Save(NewUser);

                    //Если удачно сохранили в БД то обновляем список UserItems
                    if (updateUsers != null)
                        UserItems = updateUsers;

                    //обновляем список на странице MainWindow
                    OnPropertyChanged("UserItems");
                }
            };

            view.Show();
        }
    }


    /// <summary>
    /// Редактирование ЗАКАЗЧИКА (User)
    /// </summary> 
    private void EditUser()
    {
        if (SelectedUser != null)
        {
            var view_model = new UserEditorViewModel(SelectedUser);
            var view = new UserEditorWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += (_, e) =>
            {
                view.DialogResult = e;
                view.Close();
                //пререзаписываем в бд если есть изменеия в User (передали в e true)
                if (e == true)
                {
                    //пререзаписываем в бд
                    _userSevic.Edit(SelectedUser);
                    //TODO проверить на опубликованном приложение если будет обновление списка при редактирование то убрать
                    UserItems.Insert(0, SelectedUser);
                    UserItems.RemoveAt(0);
                    //обновляем список на странице MainWindow
                    OnPropertyChanged("UserItems");
                }
            };

            view.Show();
        }
    }
    #endregion




}
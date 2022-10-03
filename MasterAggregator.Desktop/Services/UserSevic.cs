using MasterAggregator.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Services;

public class UserSevic
{
    static ObservableCollection<User> Users { get; set; }
    //Все тестовые данные в классе TestData для получения тестовых данных  
    public UserSevic()
    {
        Users = TestData.Users;
    }

    public ObservableCollection<User> GetAll()
    {
        return Users;
    }

    public ObservableCollection<User>? Edit(User user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users.ElementAt(i) == user)
            {
                Users.ElementAt(i).Id = user.Id;
                Users.ElementAt(i).UserName = user.UserName;
                Users.ElementAt(i).UserFirstName = user.UserFirstName;
                Users.ElementAt(i).UserPfone = user.UserPfone;
                return Users;
            }
        }

        return null;
    }

    public ObservableCollection<User>? Save(User user)
    {
        user.Id = Users.Count;
        Users.Insert(0, user);
        // Users.Add(user);
        return Users;
    }

    public ObservableCollection<User>? Delete(User user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users.ElementAt(i) == user)
            {
                Users.RemoveAt(i);
                return Users;
            }
        }

        return null;
    }
}



//Класс в котором храним входные тестовые данные для проекта 
public static class TestData
{
    public static ObservableCollection<User> Users = new ObservableCollection<User>
    {
        new User { Id = 0,UserName = "Brock", UserFirstName ="Avye", UserPfone ="(745)-582-8364"},
        new User { Id = 1, UserName = "Hashim", UserFirstName ="Willa", UserPfone ="(745)-273-5007"},
        new User { Id = 2,UserName = "Harper", UserFirstName ="Tamekah", UserPfone ="(895)-542-7744"},
        new User { Id = 3,UserName = "Solomon", UserFirstName ="Jael", UserPfone ="(415)-317-3189"},
        new User { Id = 4,UserName = "Kennedy", UserFirstName ="Ava", UserPfone ="(883)-730-2898"},
        new User { Id = 5,UserName = "Oscar", UserFirstName ="Bryar", UserPfone ="(745)-981-7472"},
        new User { Id = 6,UserName = "Troy", UserFirstName ="Briar", UserPfone ="(745)-304-7368"},
        new User { Id = 7,UserName = "Aladdin", UserFirstName ="Rhonda", UserPfone ="(982)-766-8454"},
        new User { Id = 8,UserName = "Ryder", UserFirstName ="Brittany", UserPfone ="(629)-392-2081"},
        new User { Id = 9,UserName = "Paki", UserFirstName ="Yvonne", UserPfone ="(745)-367-5834"},
        new User { Id = 10,UserName = "Kibo", UserFirstName ="Meghan", UserPfone ="(745)-844-9846"},
        new User { Id = 11,UserName = "Barrett", UserFirstName ="Guinevere", UserPfone ="(745)-445-8239"},
        new User { Id = 12,UserName = "Brett", UserFirstName ="Jessamine", UserPfone ="(745)-963-4862"},
        new User { Id = 13,UserName = "Neville", UserFirstName ="Katelyn", UserPfone ="(745)-549-3802"},
        new User { Id = 14,UserName = "Kaseem", UserFirstName ="Brooke", UserPfone ="(524)-641-0603"},
        new User { Id = 15,UserName = "Fuller", UserFirstName ="Wyoming", UserPfone ="(745)-515-8546"},
        new User { Id = 16,UserName = "Thane", UserFirstName ="Ignacia", UserPfone ="(255)-834-8247"},
        new User { Id = 17,UserName = "Norman", UserFirstName ="Margaret", UserPfone ="(745)-484-1842"},
        new User { Id = 18,UserName = "Micah", UserFirstName ="Pascale", UserPfone ="(349)-955-4354"},
        new User { Id = 19,UserName = "Gabriel", UserFirstName ="Brenna", UserPfone ="(967)-632-7815"},
        new User { Id = 20,UserName = "Rahim", UserFirstName ="Joelle", UserPfone ="(745)-365-3310"},
        new User { Id = 21,UserName = "Harper", UserFirstName ="Courtney", UserPfone ="(582)-202-5582"},
        new User { Id = 22,UserName = "Anthony", UserFirstName ="Ella", UserPfone ="(745)-868-6336"}
    };
     
}

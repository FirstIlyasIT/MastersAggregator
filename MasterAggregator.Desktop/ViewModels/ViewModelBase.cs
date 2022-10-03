using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MasterAggregator.Desktop.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged //INotifyPropertyChanged - интерфейс используетс¤ дл¤ отслеживани¤ изменений в Property, определенных в ViewModel. 
{
    //через наследованный интерфейс INotifyPropertyChanged реализуем событие PropertyChanged помещаем PropertyChanged в сеттер (set) и сеттер передаст нашу переменную через PropertyChanged 
    //ƒелегат PropertyChangedEventHandler ассоциирован с классом PropertyChangedEventArgs, определ¤ющим всего одно свойство:
    //PropertyName типа string. ≈сли класс реализует INotifyPropertyChanged, то при каждом изменении одного из его свойств инициируетс¤ событие PropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;

    //јтрибут [CallerMemberName] позвол¤ет не указывать им¤ свойства, если вызов происходит из Set метода этого свойства.
    //тоесть наличие этого атрибута присваивает значение параметру, к которому он применен, равное имени вызывающего метода.¬ случае геттеров и сеттеров свойств Ч это им¤ свойства.
    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }

}
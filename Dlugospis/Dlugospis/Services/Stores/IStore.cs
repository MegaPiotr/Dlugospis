using Nito.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Dlugospis.Services.Stores
{
    public interface IStore<T>: IInitializable
    {
        ObservableCollection<T> Content { get; }
        Task<T> GetAsync(int id);
        Task<int> SeveAsync(T data);
        Task<int> DeleteAsync(T data);
    }
}

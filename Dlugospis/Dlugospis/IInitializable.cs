using Nito.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dlugospis
{
    public interface IInitializable
    {
        NotifyTask InitializeTask { get; }
    }
}

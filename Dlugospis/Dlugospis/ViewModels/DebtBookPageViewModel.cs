using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dlugospis.ViewModels
{
	public class DebtBookPageViewModel : BindableBase
	{
        public string Title { get; set; }
        public DebtBookPageViewModel()
        {
            Title = "kupa2";
        }
	}
}

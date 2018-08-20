using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;
using Validation.Rules;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamlElements.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidableEntry : ContentView
    {
        public ValidableEntry()
        {
            InitializeComponent();

            _validableObject = new ValidableObject<string>();
        }
        private ValidableObject<string> _validableObject;

        #region Header
        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
            propertyName: nameof(Header),
            returnType: typeof(string),
            declaringType: typeof(ValidableEntry),
            defaultValue: default(string),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: HeaderChanged);

        private static void HeaderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sender = (ValidableEntry)bindable;
            sender.HeaderLabel.Text = (string)newValue;
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        #endregion

        #region Text
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(ValidableEntry),
            defaultValue: default(string),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextChanged);

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sender = (ValidableEntry)bindable;
            string newValueString = (string)newValue;
            sender.TextEntry.Text = newValueString;
            sender._validableObject.Value = newValueString;
            sender._validableObject.Validate();
            sender.IsValid = sender._validableObject.IsValid;
            sender.Error = sender._validableObject.Errors.FirstOrDefault();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        #region IsValid
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            propertyName: nameof(IsValid),
            returnType: typeof(bool),
            declaringType: typeof(ValidableEntry),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWayToSource,
            propertyChanged: IsValidChanged);

        private static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //var sender = (ValidableEntry)bindable;
            //if((bool)oldValue==false&&(bool)newValue==true)
            //    sender.ErrorLabel.Text = "";
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidProperty, value);
        }
        #endregion

        #region Error
        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
            propertyName: nameof(Error),
            returnType: typeof(string),
            declaringType: typeof(ValidableEntry),
            defaultValue: default(string),
            defaultBindingMode: BindingMode.OneWayToSource,
            propertyChanged: ErrorChanged);

        private static void ErrorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sender = (ValidableEntry)bindable;
            sender.ErrorLabel.Text = (string)newValue;
        }

        public string Error
        {
            get => (string)GetValue(ErrorProperty);
            private set => SetValue(ErrorProperty, value);
        }
        #endregion

        #region Rules
        public static readonly BindableProperty RulesProperty = BindableProperty.Create(
            propertyName: "Rules",
            returnType: typeof(IEnumerable<IValidationRule<string>>),
            declaringType: typeof(ValidableEntry),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneTime,
            propertyChanged: RulesChanged);

        private static void RulesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sender = (ValidableEntry)bindable;
            var rules = (IEnumerable<IValidationRule<string>>)newValue;
            sender._validableObject.Validations.AddRange(rules);
        }

        #endregion

        #region Keyboard
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            propertyName: nameof(Keyboard),
            returnType: typeof(Keyboard),
            declaringType: typeof(ValidableEntry),
            defaultValue: default(Keyboard),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: KeyboardChanged);

        private static void KeyboardChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sender = (ValidableEntry)bindable;
            sender.TextEntry.Keyboard = (Keyboard)newValue;
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
        #endregion

        private void Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }
    }
}
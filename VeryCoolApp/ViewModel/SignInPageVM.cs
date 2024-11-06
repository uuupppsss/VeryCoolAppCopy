using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class SignInPageVM:BaseVM
    {
        private DialogServise dialogServise;
        private CookingServise servise;
        private string _login;

        public string Login
        {
            get => _login;
            set 
            {
                _login = value;
                Signal();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set 
            {
                _password = value;
                Signal();
            }
        }


        public CommandVM SignInCommand { get; set; }
        public CommandVM EnterAsAGuestCommand { get; set; }

        public SignInPageVM()
        {
            servise= CookingServise.Instance;
            dialogServise = DialogServise.Instance;
            SignInCommand = new CommandVM(async () =>
            {
                if (Login == null || Password == null)
                {
                    await ShowWarning("Заполните все поля", "Кажется, вы что то забыли");
                }
                bool result = await servise.SignUserIn(new User { Login = Login, Password = Password });
                if (result)
                {
                    await ShowWarning("Всё чики пуки", "Добро пожаловать, приятного пользования");
                    Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                    await Shell.Current.GoToAsync("//RecipesPage");
                    //Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                }
                else
                {
                    await ShowWarning("Ошибочка", "Таких не держим");
                }
            });

            EnterAsAGuestCommand = new CommandVM(async () =>
            {
                await Shell.Current.GoToAsync("//RecipesPage");
            });
        }
        private async Task ShowWarning(string title, string content)
        {
            await dialogServise.ShowWarning(title, content);
        }
    }
}

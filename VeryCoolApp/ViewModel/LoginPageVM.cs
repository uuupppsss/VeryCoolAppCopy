using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class LoginPageVM:BaseVM
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
        public LoginPageVM()
        {
            servise= CookingServise.Instance;
            dialogServise = DialogServise.Instance;
            SignInCommand = new CommandVM(async () =>
            {
                if (Login == null || Password == null)
                {
                    await ShowWarning("Заполните все поля", "Кажется, вы что то забыли");
                }
                bool result = await servise.SignUserInAsync(new User { Login = Login, Password = Password });
                if (result)
                {
                    await ShowWarning("Всё чики пуки", "Добро пожаловать, приятного пользования");
                }
                else
                {
                    await ShowWarning("Ошибочка", "Таких не держим");
                }
            });
        }
        private async Task ShowWarning(string title, string content)
        {
            await dialogServise.ShowWarning(title, content);
        }
    }
}

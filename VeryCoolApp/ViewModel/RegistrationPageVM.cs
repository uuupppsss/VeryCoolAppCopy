using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class RegistrationPageVM : BaseVM
    {
        private DialogServise dialogServise;
        private CookingServise servise;
		private string _login;

		public string  Login
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

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set 
            { 
                _confirmPassword = value;
                Signal();
            }
        }

        public CommandVM SignUpCommand { get; set; }

        public RegistrationPageVM()
        {
            servise=CookingServise.Instance;
            dialogServise=DialogServise.Instance;

            SignUpCommand = new CommandVM(async () =>
            {
                if (Password != ConfirmPassword)
                {
                    await ShowWarning("Попробуйте еще разок", "Пароли не совпадают");
                }
                if (Password ==null||ConfirmPassword==null||Login==null)
                {
                    await ShowWarning("Заполните все поля", "Кажется, вы что то забыли");
                }
                else
                {
                    bool result = await servise.CreateNewUserAsync(new User { Login = Login, Password = Password });
                    if (!result)
                    {
                        await ShowWarning("Ошибочка", "Такой пользователь уже существует");
                    }
                    else
                    {
                        await ShowWarning("Всё чики пуки", "Добро пожаловать, приятного пользования");
                    }
                }
            });
        }

        private async Task ShowWarning(string title, string content)
        {
            await dialogServise.ShowWarning(title, content);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeryCoolApp.Model
{
    public class DialogServise:IDialogService
    {
        static DialogServise instance;

        public static DialogServise Instance
        {
            get
            {
                if (instance == null)
                    instance = new DialogServise();
                return instance;
            }
        }
        public async Task ShowWarning(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<string> ShowInputDialog(string title, string message)
        {
            string result = await Application.Current.MainPage.DisplayPromptAsync(title, message);
            return result;
        }
    }
    public interface IDialogService
    {
        Task ShowWarning(string title, string message);
        Task<string> ShowInputDialog(string title, string message);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeryCoolApp.ViewModel
{
    public class RegistrationPageVM : BaseVM
    {
		private string _login;

		public string  Login
		{
			get { return _login; }
			set { _login = value; }
		}

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; }
        }

        public CommandVM SignUpCommand { get; set; }

        public RegistrationPageVM()
        {
            
        }
    }
}
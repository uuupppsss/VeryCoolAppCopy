﻿using VeryCoolApp.Pages;
namespace VeryCoolApp

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

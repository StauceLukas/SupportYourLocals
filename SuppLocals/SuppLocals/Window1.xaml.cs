﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuppLocals
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void LogInBtnClick(object sender, RoutedEventArgs e)
        {

            string username = First.Text;
            string password = PasswordBox.Password.ToString();

            string path = @"..\LoginInfo.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            else if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(@"..\LoginInfo.txt");

                if (username != "" && password != "")
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] line = lines[i].Split('`');
                        if (line[0] == username && line[1] == password)
                        {
                            MainWindow map = new MainWindow();
                            map.Show();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void SignUpBtnClick(object sender, RoutedEventArgs e)
        {
            SignUp signUpForm = new SignUp();
            signUpForm.Show();

        }
    }
}

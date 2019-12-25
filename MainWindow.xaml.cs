using Microsoft.Win32;
using System.Windows;
using UIHelper;

using System.Text.RegularExpressions;

namespace TextViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _Model = new Model();
            this.DataContext = _Model;
        }

        private Model _Model;

        private void OnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnOpen_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenTextFileDialog aDlg = new OpenTextFileDialog();
            if (aDlg.ShowDialog() != true) return;
            try
            {
                _Model.Load(aDlg.FileName, aDlg.CurrentEncoding);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnStartFilte_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            try
            {
                _Model.StartFilte();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnStartFilte_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model != null && _Model.CanStartFilte;
        }

        private void OnOpenPictureFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFileDialog aDlg = new OpenFileDialog()
            {
                Title = "选择图片",
                Filter = "图片文件 (*.jpg)|*.jpg|图片文件 (*.*)|*.*",
            };
            if (aDlg.ShowDialog() != true) return;
            _Model.PictureFileName = aDlg.FileName;
        }

        private void OnOpenPictureFile_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnSendEmail_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("发送成功！");
        }

        private void OnSendEmail_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model != null
                && IsEmailVaild(_Model.EmailAddress)
                && !string.IsNullOrEmpty(_Model.EmailAddress)
                && !string.IsNullOrEmpty(_Model.FiltedText)
                && !string.IsNullOrEmpty(_Model.PictureFileName);
        }

        public static bool IsEmailVaild(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            email = email.Trim();
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

            Regex aRegex = new Regex(pattern);
            return aRegex.IsMatch(email);
        }
    }
}

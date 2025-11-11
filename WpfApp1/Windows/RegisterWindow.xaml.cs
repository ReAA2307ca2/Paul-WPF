using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private Context _context;
        public RegisterWindow(Context context)
        {
            _context = context;
            InitializeComponent();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_register_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbox_name.Text.Trim())
                && !string.IsNullOrEmpty(tbox_login.Text.Trim())
                && !string.IsNullOrEmpty(tbox_pass.Text.Trim()))
            {
                User newUser = new User()
                {
                    FullName = tbox_name.Text.Trim(),
                    Login = tbox_login.Text.Trim(),
                    Password = tbox_pass.Text.Trim(),
                    RoleId = 1,
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                this.Close();
            }
        }
    }
}

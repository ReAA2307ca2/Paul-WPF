using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Context _context;
        public LoginWindow(Context context)
        {
            _context = context;
            InitializeComponent();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }

        private void bt_login_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbox_login.Text.Trim()) 
                && !string.IsNullOrEmpty(tbox_pass.Text.Trim()))
            {
                User? loggedUser = _context.Users
                    .Include(u => u.Role)
                    .Where(
                    u => u.Login == tbox_login.Text.Trim()
                    && u.Password == tbox_pass.Text.Trim()).FirstOrDefault();
                if (loggedUser != null)
                {
                    LoginCookies.loggedUser = loggedUser;
                    DialogResult = true;
                    return;
                }
            }
        }

        private void bt_register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(_context);
            registerWindow.ShowDialog();
        }

        private void bt_guest_Click(object sender, RoutedEventArgs e)
        {
            LoginCookies.loggedUser = null;
            DialogResult = true;
            return;
        }
    }
}

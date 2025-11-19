using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Data;
using WpfApp1.Models;
using WpfApp1.Windows;
using WpfApp1.Windows.UserControls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Context _context = new();
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow loginWindow = new(_context);
            loginWindow.ShowDialog();
            if(loginWindow.DialogResult == true)
            {
                List<Supplier> suppliers = new();
                suppliers.Add(new Supplier() { SupplierName = "No filter" });
                foreach(Supplier sup in _context.Suppliers.ToList())
                {
                    cb_filter.Items.Add(sup);
                }

                if(LoginCookies.loggedUser == null)
                {
                    tb_userName.Text = "Гость";
                    tb_userRole.Text = "Гость";
                    stp_options.Visibility = Visibility.Hidden;
                    tbox_search.Visibility = Visibility.Hidden;
                    bt_seeOrders.Visibility = Visibility.Hidden;
                } else
                {
                    tb_userName.Text = LoginCookies.loggedUser!.FullName;
                    tb_userRole.Text = LoginCookies.loggedUser!.Role.RoleName;
                    if (LoginCookies.loggedUser!.Role.RoleName == "Авторизированный клиент")
                    {
                        stp_options.Visibility = Visibility.Hidden;
                        tbox_search.Visibility = Visibility.Hidden;
                        bt_seeOrders.Visibility = Visibility.Hidden;
                    }
                    if (LoginCookies.loggedUser!.Role.RoleName == "Менеджер")
                    {
                        stp_crud.Visibility = Visibility.Hidden;
                    }
                }
            }
            else { Application.Current.Shutdown(); }

            UpdateList();
            cb_sort.SelectedIndex = 0;
            cb_filter.SelectedIndex = 0;
        }

        private void UpdateList(List<Equipment> equipment = null)
        {
            lv_orders.Items.Clear();
            if(equipment == null)
            {
                foreach(Equipment eq in _context.Equipment.ToList())
                {
                    eq.Manufacturer = _context.Manufacturers.Find(eq.ManufacturerId)!;
                    eq.Supplier = _context.Suppliers.Find(eq.SupplierId)!;
                    eq.Type = _context.EquipmentTypes.Find(eq.TypeId)!;
                    lv_orders.Items.Add(new ListElement(eq));
                }
            } else
            {
                foreach(Equipment eq in equipment)
                {
                    lv_orders.Items.Add(new ListElement(eq));
                }
            }
        }

        private void bt_create_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new(_context);
            createWindow.ShowDialog();
            if (createWindow.DialogResult == true) UpdateList();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            if(lv_orders.SelectedItem != null)
            {
                Equipment equipment = ((ListElement)lv_orders.SelectedItem).Equipment;
                _context.Equipment.Remove(equipment);
                _context.SaveChanges();
                UpdateList();
            }
        }

        private void bt_edit_Click(object sender, RoutedEventArgs e)
        {
            if (lv_orders.SelectedItem != null)
            {
                Equipment equipment = ((ListElement)lv_orders.SelectedItem).Equipment;
                EditWindow editWindow = new(_context, equipment);
                editWindow.ShowDialog();
                if(editWindow.DialogResult == true) UpdateList();
            }
        }

        private void bt_seeOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Unimplemented pdoferpokmefodefrk");
        }

        private void bt_quit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            LoginCookies.loggedUser = null;
            this.Close();
            mainWindow.Show();
        }

        private void cb_sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cb_sort.SelectedIndex) 
            {
                case 0:
                    UpdateList();
                    break;
                case 1:
                    UpdateList(_context.Equipment
                        .Include(e => e.Manufacturer)
                        .Include(e => e.Supplier)
                        .Include(e => e.Type)
                        .OrderBy(e => e.AvailableQuantity)
                        .ToList());
                    break;
                case 2:
                    UpdateList(_context.Equipment
                        .Include(e => e.Manufacturer)
                        .Include(e => e.Supplier)
                        .Include(e => e.Type)
                        .OrderByDescending(e => e.AvailableQuantity)
                        .ToList());
                    break;
            }
        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cb_filter.SelectedIndex)
            {
                case 0:
                    UpdateList();
                    break;
                case 1:
                    UpdateList(
                        _context.Equipment
                        .Include(e => e.Supplier)
                        .Where(e => e.Supplier.SupplierName == "Музторг")
                        .ToList());
                    break;
                case 2:
                    UpdateList(
                        _context.Equipment
                        .Include(e => e.Supplier)
                        .Where(e => e.Supplier.SupplierName == "Седьмой ряд")
                        .ToList());
                    break;
                case 3:
                    UpdateList(
                        _context.Equipment
                        .Include(e => e.Supplier)
                        .Where(e => e.Supplier.SupplierName == "audiomania")
                        .ToList());
                    break;
            }
        }
    }
}
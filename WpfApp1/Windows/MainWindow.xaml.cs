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
            if(LoginCookies.loggedUser == null)
            {
                LoginWindow loginWindow = new LoginWindow(_context);
                loginWindow.ShowDialog();
                if(loginWindow.DialogResult == false) { Application.Current.Shutdown(); }
            }
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            lv_orders.Items.Clear();
            foreach(Equipment eq in _context.Equipment.ToList())
            {
                eq.Manufacturer = _context.Manufacturers.Find(eq.ManufacturerId)!;
                eq.Supplier = _context.Suppliers.Find(eq.SupplierId)!;
                eq.Type = _context.EquipmentTypes.Find(eq.TypeId)!;
                lv_orders.Items.Add(new ListElement(eq));
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
    }
}
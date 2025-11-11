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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Context _context;
        private Equipment _equipment;
        public EditWindow(Context context, Equipment equipment)
        {
            _context = context;
            _equipment = equipment;
            DataContext = equipment;
            InitializeComponent();

            cb_man.ItemsSource = _context.Manufacturers.ToList();
            cb_supplier.ItemsSource = _context.Suppliers.ToList();
            cb_type.ItemsSource = _context.EquipmentTypes.ToList();

            tbox_article.Text = equipment.Article;
            tbox_name.Text = equipment.Name;
            tbox_amount.Text = equipment.AvailableQuantity.ToString();

            tbox_unit.Text = equipment.RentalUnit;
            tbox_cost.Text = equipment.RentalCost.ToString();
            tbox_discount.Text = equipment.Discount.ToString();

            cb_supplier.SelectedItem = equipment.Supplier;
            cb_man.SelectedItem = equipment.Manufacturer;
            cb_type.SelectedItem = equipment.Type;

            tbox_description.Text = equipment.Description;
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }

        private void bt_create_Click(object sender, RoutedEventArgs e)
        {
            _equipment.Article = tbox_article.Text;
            _equipment.Name = tbox_name.Text;
            _equipment.AvailableQuantity = int.Parse(tbox_amount.Text);
            _equipment.Description = tbox_description.Text;
            _equipment.Discount = decimal.Parse(tbox_discount.Text);
            _equipment.Manufacturer = cb_man.SelectedItem as Manufacturer;
            _equipment.RentalCost = decimal.Parse(tbox_cost.Text);
            _equipment.RentalUnit = tbox_unit.Text;
            _equipment.Supplier = cb_supplier.SelectedItem as Supplier;
            _equipment.Type = cb_type.SelectedItem as EquipmentType;
            _context.SaveChanges();

            DialogResult = true;
            return;
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaOrder
{

    public partial class MainWindow : Window
    {
        public string product_tag;

        public string sauce_name;
        public string ingredient_name;
        public string other_product_name;

        public string pizza_type;
        public string pizza_size;
        public string pizza_sharpness_level;

        public float pizza_cost = 0;
        public float pizza_size_cost = 0;
        public float pizza_additionals_cost = 0;
        public string pizza_delivery = "5 zl";

        List<string> sauces_list = new List<string>();
        public string sauces;
        List<string> ingredients_list = new List<string>();
        public string ingredients;
        List<string> other_products_list = new List<string>();
        public string other_products;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void OrderSumarry()
        {
            other_products = string.Join(", ", other_products_list);
            ingredients = string.Join(", ", ingredients_list);
            sauces = string.Join(", ", sauces_list);

            info_order.Text = pizza_type + " " + pizza_size + pizza_sharpness_level + "\r\n" +
                sauces + "\r\n" +
                ingredients + "\r\n" +
                other_products;

            pizza_cost = pizza_size_cost + pizza_additionals_cost;

            if(pizza_cost >= 30)
            {
                pizza_delivery = "gratis";
            }
            else
            {
                pizza_delivery = "5 zł";
            }

            confirm_order.Content = "ZŁÓŻ ZAMÓWIENIE \r\n" + pizza_cost + " zl + (dostawa " + pizza_delivery + ")";

            if ( (sizeButtonS.IsChecked == true || sizeButtonL.IsChecked == true || 
                sizeButtonXL.IsChecked == true) && pizza_sharpness_level != null && pizza_type != null)
            {
                confirm_button.IsEnabled = true;
            }
            
        }

        private void SelectPizza(object sender, RoutedEventArgs e)
        {
            string type = (string)((sender as ComboBoxItem).Tag);

            if (type == "Havana")
            {
                consistOf.Text = "Składniki: \r\n ser, szynka, ananas, kukurydza, oregano";
                pizza_type = "Havana";
            }
            else if (type == "Vege")
            {
                consistOf.Text = "Składniki: \r\n ser, pieczarki, papryka, kukurydza, oliwki czarne, cebula, oregano";
                pizza_type = "Vege";
            }
            else if (type == "NY")
            {
                consistOf.Text = "Składniki: \r\n podwójny ser, salami na całej pizzy, oregano";
                pizza_type = "NY Style";
            }
            else
            {
                consistOf.Text = "Składniki: \r\n ser, ser pleśniowy, ser favita, ser parmezan, ser mozarella, oregano";
                pizza_type = "Serio";
            }

            OrderSumarry();
        }

        private void size_checked(object sender, RoutedEventArgs e)
        {
            string size = (string)((sender as RadioButton).Tag);

            if (size == "S")
            {
                pizza_size_cost = 15;
            }
            else if (size == "L")
            {
                pizza_size_cost = 30;
            }
            else 
            {
                pizza_size_cost = 45;
            }

            pizza_size = size;

            OrderSumarry();

        }


        private void sharpness_changed(object sender, MouseButtonEventArgs e)
        {
            var slider = sender as Slider;
            double value = slider.Value;

            if (value == 1)
            {
                pizza_sharpness_level = " mało ostra";
            }
            else if (value == 2)
            {
                pizza_sharpness_level = " średnio ostra";
            }
            else
            {
                pizza_sharpness_level = " mocno ostra";
            }

            OrderSumarry();
        }

        private void additionals_checked(object sender, RoutedEventArgs e)
        {
            product_tag = (string)((sender as CheckBox).Tag);

            if (product_tag == "group1")
            {
                pizza_additionals_cost = pizza_additionals_cost + (float)2.50;
                sauce_name = (string)((sender as CheckBox).Content);
                sauces_list.Add(sauce_name);
            }
            else if (product_tag == "group2")
            {
                pizza_additionals_cost = pizza_additionals_cost + (float)4.50;
                ingredient_name = (string)((sender as CheckBox).Content);
                ingredients_list.Add(ingredient_name);
            }
            else if (product_tag == "group3")
            {
                pizza_additionals_cost = pizza_additionals_cost + (float)10;
                other_product_name = (string)((sender as CheckBox).Content);
                other_products_list.Add(other_product_name);
            }
            else 
            {
                pizza_additionals_cost = pizza_additionals_cost + (float)5.50;
                other_product_name = (string)((sender as CheckBox).Content);
                other_products_list.Add(other_product_name);
            }

            OrderSumarry();
        }

        private void additionals_unchecked(object sender, RoutedEventArgs e)
        {
            product_tag = (string)((sender as CheckBox).Tag);


            if (product_tag == "group1")
            {
                pizza_additionals_cost = pizza_additionals_cost - (float)2.50;
                sauce_name = (string)((sender as CheckBox).Content);
                sauces_list.Remove(sauce_name);
            }
            else if (product_tag == "group2")
            {
                pizza_additionals_cost = pizza_additionals_cost - (float)4.50;
                ingredient_name = (string)((sender as CheckBox).Content);
                ingredients_list.Remove(ingredient_name);
            }
            else if (product_tag == "group3")
            {
                pizza_additionals_cost = pizza_additionals_cost - (float)10;
                other_product_name = (string)((sender as CheckBox).Content);
                other_products_list.Remove(other_product_name);
            }
            else
            {
                pizza_additionals_cost = pizza_additionals_cost - (float)5.50;
                other_product_name = (string)((sender as CheckBox).Content);
                other_products_list.Remove(other_product_name);
            }


            OrderSumarry();
        }

        private void order_click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Podsumowanie zamówienia \r\n ================== \r\n" + info_order.Text + "\r\n" + Uwagi.Text );
        }
    }
}

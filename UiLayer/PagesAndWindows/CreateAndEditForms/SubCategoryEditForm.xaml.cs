using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UiLayer
{
    /// <summary>
    /// Interaction logic for SubCategoryEditForm.xaml
    /// </summary>
    public partial class SubCategoryEditForm : Window
    {
        private ObservableCollection<CCategoryNotifyObject> _categories;

        public String CategoryTitle { get => CategoryBox.Text; }
        public String SubCategoryTitle { get => SubCategoryTitleBox.Text; }
        public String Description { get => DescriptionBox.Text; }

        internal SubCategoryEditForm(ObservableCollection<CCategoryNotifyObject> categories)
        {
            _categories = categories;
            InitializeComponent();

            DataContext = _categories;
        }

        internal SubCategoryEditForm(ObservableCollection<CCategoryNotifyObject> categories, CCategoryNotifyObject selectedCategory, CSubCategoryNotufyObject selectedSubCategory)
        {
            _categories = categories;
            InitializeComponent();

            DataContext = _categories;
            CategoryBox.SelectedItem = selectedCategory;
            SubCategoryTitleBox.Text = selectedSubCategory.Title;
            DescriptionBox.Text = selectedSubCategory.Description;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

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
using Modeling.Constructors;
using Modeling.generationFunctions;

namespace Modeling.Windows
{
    /// <summary>
    /// Логика взаимодействия для searchMaterial.xaml
    /// </summary>
    public partial class searchMaterial : Window
    {
        allConstruct allConstruct;
        private generateFunc generateFunc;
        string[] searchedItems;
        string[] nameMaterial = new string[95];
        string[] densityMaterial = new string[95];

        public searchMaterial()
        {
            InitializeComponent();
            clearComboBox();
            
            allConstruct = MainWindow.allConstruct;
            generateFunc = MainWindow.generateFunc;

            for(int i = 0; i < nameMaterial.Length; i++)
            {
                nameMaterial[i] = allConstruct.material[i, 0];
                densityMaterial[i] = allConstruct.material[i, 1];
            }

            cmb_material.ItemsSource = nameMaterial;
        }

        private void cmb_material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        public void clearComboBox()
        {
            cmb_material.ItemsSource = null;
            cmb_material.Items.Clear();
        }


        private void tb_searchMaterial_TextChanged(object sender, TextChangedEventArgs e)
        {

            if ((tb_searchMaterial.Text).Trim().Length <= 1)
            {
                clearComboBox();
                cmb_material.ItemsSource = nameMaterial;
            }
            else
            {
                searchedItems = nameMaterial.Where(material => material.Contains(tb_searchMaterial.Text)).ToArray();
                clearComboBox();
                cmb_material.ItemsSource = searchedItems;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cmb_material.SelectedItem != null) {
                generateFunc.selectedMaterial = cmb_material.SelectedItem as string;
                generateSparWindow generateSparWindow = new generateSparWindow();
                generateSparWindow.selectedMaterial = cmb_material.SelectedItem as string;
                string selectedMaterial = cmb_material.SelectedItem as string;

                for (int i = 0; i < nameMaterial.Length; i++)
                {
                    if (nameMaterial[i] == selectedMaterial)
                    {
                        generateFunc.selectedMaterialDensity = double.Parse(densityMaterial[i].Replace(".", ","));
                        break;
                    }
                }
                generateSparWindow.changeMaterial();
                generateSparWindow.Show();
                this.Close();
            }
        }
    }
}

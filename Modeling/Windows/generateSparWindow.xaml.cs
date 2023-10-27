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
using Modeling.Pages;
using Modeling.generationFunctions;
using System.Windows.Navigation;
using Kompas6API5;
using KompasAPI7;
using Modeling.Class;
using Modeling.Constructors;
using System.ComponentModel;
using Kompas6Constants3D;

namespace Modeling.Windows
{
    /// <summary>
    /// Логика взаимодействия для generateSparWindow.xaml
    /// </summary>
    public partial class generateSparWindow : Window
    {

        public string selectedMaterial = string.Empty;
        public bool isAssembly = false;

        private allConstruct allConstruct;
        private generateFunc generateFunc;
        public generateSparWindow()
        {
            InitializeComponent();
            FrmMain.Navigate(new generateWholeSpar());
            allConstruct = MainWindow.allConstruct;
            generateFunc = MainWindow.generateFunc;

            if (generateFunc.isAssembly && generateFunc.selectedMaterial != null)
                cb_assembly_Click(cb_assembly, null);

        }

        private void FrmMain_ContentRendered(object sender, EventArgs e)
        {

        }

        public void changeMaterial()
        {
            tb_selectedMaterial.Text = selectedMaterial;
            this.UpdateLayout();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            searchMaterial searchMaterial = new searchMaterial();
            searchMaterial.Show();
            this.Close();
        }

  

       /* private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (kompas == null)
                {
                    kompas = generateFunc.kompas;
                }
                if (kompas != null)
                {
                    kompas.Visible = false;
                    kompas.ActivateControllerAPI();

                    doc3D = generateFunc.createDocuments(kompas, true);

                    ksPart part = (ksPart)doc3D.GetPart((short)Part_Type.pTop_Part);

                    part.SetMaterial(generateFunc.selectedMaterial, generateFunc.selectedMaterialDensity);

                    kompas.Visible = true;

                    doc3D.SetPartFromFile(@"G:\Modeling\Modeling\Modeling\bin\Debug\NoEditor\Детал2ыь.m3d", part, true);

                    doc3D.ComponentPositioner().SetDragPoint(0, 0, 0);
                    doc3D.ComponentPositioner().SetPlane(part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
                    if (doc3D.ComponentPositioner().Prepare((part)part, (short)Positioner_Type.pnMove) == 0)
                    {
                        if (doc3D.ComponentPositioner().MoveComponent(1000, 0, 0) == true)
                            MessageBox.Show("Подвинул00");
                        doc3D.ComponentPositioner().Finish();
                    }



                    *//*
                     doc3D.ComponentPositioner().Prepare(part, (short)Positioner_Type.pnMove);
                     doc3D.ComponentPositioner().SetPlane((short)Obj3dType.o3d_planeXOZ);                
                     doc3D.ComponentPositioner().SetDragPoint(0, 0, 0);
                     doc3D.ComponentPositioner().MoveComponent(100, 0, 0);
                     doc3D.ComponentPositioner().Finish();*//*


                }
            }
            catch (Exception ex)
            {
                loggingActions.loggingFunction(ex.Message, true);
            }
        }*/


    
        private void tb_stepMissing_TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] enteredSymbols = tb_stepMissing.Text.ToCharArray();
            string temp = string.Empty;

            for(int i = 0; i < enteredSymbols.Length; i++)
            {
                if (Convert.ToInt32(enteredSymbols[i]) >= 48 && Convert.ToInt32(enteredSymbols[i]) <= 57)
                {
                    temp += enteredSymbols[i];
                }
            }
            tb_stepMissing.Text = temp;

            if (temp == string.Empty)
                return;

            generateFunc.stepMissing = double.Parse(temp);
        }

        private void tb_countSpar_TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] enteredSymbols = tb_countSpar.Text.ToCharArray();
            string temp = string.Empty;

            for (int i = 0; i < enteredSymbols.Length; i++)
            {
                if (Convert.ToInt32(enteredSymbols[i]) >= 48 && Convert.ToInt32(enteredSymbols[i]) <= 57)
                {
                    temp += enteredSymbols[i];
                }
            }
            tb_countSpar.Text = temp;

            if (temp == string.Empty)
                return;

            generateFunc.countSpar = int.Parse(temp);
        }

        private void cb_assembly_Click(object sender, RoutedEventArgs e)
        {
            if (generateFunc.isAssembly)
            {
                tb_countSpar.Visibility = Visibility.Collapsed;
                lb_countSpar.Visibility = Visibility.Collapsed;
                tb_stepMissing.Visibility = Visibility.Collapsed;
                lb_stepMissing.Visibility = Visibility.Collapsed;
                generateFunc.isAssembly = false;
            }
           else
            {
                tb_countSpar.Visibility = Visibility.Visible;
                lb_countSpar.Visibility = Visibility.Visible;
                tb_stepMissing.Visibility = Visibility.Visible;
                lb_stepMissing.Visibility = Visibility.Visible;
                generateFunc.isAssembly = true;
            }
        }
    }
}

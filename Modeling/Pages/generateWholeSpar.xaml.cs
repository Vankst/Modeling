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
using Modeling.Constructors;
using Modeling.Windows;
using Kompas6API5;
using Kompas6Constants3D;
using Modeling.generationFunctions;
using Modeling.Class;

namespace Modeling.Pages
{
    /// <summary>
    /// Логика взаимодействия для generateWholeSpar.xaml
    /// </summary>
    public partial class generateWholeSpar : Page
    {

        private KompasObject kompas;
        private ksDocument3D doc3D;

        private allConstruct allConstruct;
        private generateFunc generateFunc;
        loggingActions loggingActions = new loggingActions();

        private double extraction;
        private double sparHeight; //h
        private double sparUpperParallel;//h1
        private double sparLowerParallel; //h2
        private double sparInnerWidth;
        private double sparLowerHeight;
        private double sparUpperHeight;

        public generateWholeSpar()
        {
            InitializeComponent();
            allConstruct =  MainWindow.allConstruct;
            generateFunc = MainWindow.generateFunc;
        }

        private void btn_show_Click(object sender, RoutedEventArgs e)
        {
            if (lbl_sparHeight.Visibility == Visibility.Collapsed)
            {
                lbl_sparUpperHeight.Visibility = Visibility.Visible;
                lbl_sparLowerParallel.Visibility = Visibility.Visible;
                lbl_sparHeight.Visibility = Visibility.Visible;
                lbl_sparInnerWidth.Visibility = Visibility.Visible;
                lbl_sparLowerHeight.Visibility = Visibility.Visible;
                lbl_sparUpperHeight.Visibility = Visibility.Visible;
                lbl_sparUpperParallel.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_sparUpperHeight.Visibility = Visibility.Collapsed;
                lbl_sparLowerParallel.Visibility = Visibility.Collapsed;
                lbl_sparHeight.Visibility = Visibility.Collapsed;
                lbl_sparInnerWidth.Visibility = Visibility.Collapsed;
                lbl_sparLowerHeight.Visibility = Visibility.Collapsed;
                lbl_sparUpperHeight.Visibility = Visibility.Collapsed;
                lbl_sparUpperParallel.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            Validation();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            searchMaterial searchMaterial = new searchMaterial();
            searchMaterial.ShowDialog();
        }

        StringBuilder errorValidationString = new StringBuilder();

        public void Validation()
        {
            errorValidationString.Clear();
            if (double.TryParse(tb_extrusion.Text, out double ex) && ex >= 0)
                extraction = ex;
            else
                errorValidationString.Append("Неверно заполнено поле выдавливания \n");

            if (double.TryParse(tb_sparUpperParallel.Text, out double sUP) && sUP >= 0)
                sparUpperParallel = sUP;
            else
                errorValidationString.Append("Неверно заполнена ширина верхней параллели лонжерона \n");
            if (double.TryParse(tb_sparLowerParallel.Text, out double sUL) && sUL >= 0)
                sparLowerParallel = sUL;
            else
                errorValidationString.Append("Неверно заполнена ширина нижней параллели лонжерона \n");

            if (double.TryParse(tb_sparInnerWidth.Text, out double sIW) && sIW >= 0)
                sparInnerWidth = sIW;
            else
                errorValidationString.Append("Неверно заполнена внутренняя ширина лонжерона \n");

            if (double.TryParse(tb_sparLowerHeight.Text, out double sLH) && sLH >= 0)
                sparLowerHeight = sLH;
            else
                errorValidationString.Append("Неверно заполнена высота нижнего пояса лонжерона \n");

            if (double.TryParse(tb_sparUpperHeight.Text, out double sUH) && sUH >= 0)
                sparUpperHeight = sUH;
            else
                errorValidationString.Append("Неверно заполнена высота верхнего пояса лонжерона \n");

            if (double.TryParse(tb_sparHeight.Text, out double sH) && sH >= 0 && sH - sUH - sLH > 0)
                sparHeight = sH;
            else
                errorValidationString.Append("Неверно заполнена высота лонжерона \n");
            
            if(generateFunc.selectedMaterial == null)
                errorValidationString.Append("Не выбран материал \n");

            if (generateFunc.isAssembly && generateFunc.countSpar <= 0)
                errorValidationString.Append("Не введено количество лонжернов \n");

            if (generateFunc.isAssembly && generateFunc.stepMissing <= 0)
                errorValidationString.Append("Не указан шаг \n");

            if (errorValidationString.Length == 0)
                generateSpar();
            else
            {
                MessageBox.Show(Convert.ToString(errorValidationString));
                loggingActions.loggingFunction($"Ошибка валидации - {errorValidationString}", false);
            }
        }


        public void generateSpar()
        {
            try
            {
                if(kompas == null)
                {
                    kompas = generateFunc.kompas;
                }
                if(kompas != null)
                {
                    kompas.Visible = false;
                    kompas.ActivateControllerAPI();

                    doc3D = generateFunc.createDocuments(kompas, false);

                    ksPart part = (ksPart)doc3D.GetPart((short)Part_Type.pTop_Part);

                    part.SetMaterial(generateFunc.selectedMaterial, generateFunc.selectedMaterialDensity);
                    ksEntity sketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                    ksSketchDefinition sketchDef = (ksSketchDefinition)sketch.GetDefinition();
                    sketchDef.SetPlane(part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY)); // Устанавливаем плоскость XOY для эскиза
                    sketch.Create();

                    ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit(); // Получаем доступ к редактированию эскиза


                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, 0, (sparHeight - sparUpperHeight - sparLowerHeight) / 2, 0, 1); //Высота
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, sparInnerWidth, (sparHeight - sparUpperHeight - sparLowerHeight) / 2, sparInnerWidth, 1); //Высота

                    //Верхний пояс
                    sketchEdit.ksLineSeg((sparHeight - sparUpperHeight - sparLowerHeight) / 2, 0, (sparHeight - sparUpperHeight - sparLowerHeight) / 2, -((sparUpperParallel - sparInnerWidth) / 2), 1);
                    sketchEdit.ksLineSeg((sparHeight - sparUpperHeight - sparLowerHeight) / 2, sparInnerWidth, (sparHeight - sparUpperHeight - sparLowerHeight) / 2, (sparUpperParallel + sparInnerWidth) / 2, 1);
                    sketchEdit.ksLineSeg((sparHeight - sparUpperHeight - sparLowerHeight) / 2, -((sparUpperParallel - sparInnerWidth) / 2), (sparHeight - sparUpperHeight - sparLowerHeight) / 2 + sparUpperHeight, -((sparUpperParallel - sparInnerWidth) / 2), 1);
                    sketchEdit.ksLineSeg((sparHeight - sparUpperHeight - sparLowerHeight) / 2, ((sparUpperParallel + sparInnerWidth) / 2), (sparHeight - sparUpperHeight - sparLowerHeight) / 2 + sparUpperHeight, (sparUpperParallel + sparInnerWidth) / 2, 1);
                    sketchEdit.ksLineSeg((sparHeight - sparUpperHeight - sparLowerHeight) / 2 + sparUpperHeight, (sparUpperParallel + sparInnerWidth) / 2, (sparHeight - sparUpperHeight - sparLowerHeight) / 2 + sparUpperHeight, -((sparUpperParallel - sparInnerWidth) / 2), 1);


                    //Нижний пояс
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, 0, -(sparHeight - sparUpperHeight - sparLowerHeight) / 2, -((sparLowerParallel - sparInnerWidth) / 2), 1);
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, sparInnerWidth, -(sparHeight - sparUpperHeight - sparLowerHeight) / 2, (sparLowerParallel + sparInnerWidth) / 2, 1);
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, -((sparLowerParallel - sparInnerWidth) / 2), -(sparHeight - sparUpperHeight - sparLowerHeight) / 2 - sparLowerHeight, -((sparLowerParallel - sparInnerWidth) / 2), 1);
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2, ((sparLowerParallel + sparInnerWidth) / 2), -(sparHeight - sparUpperHeight - sparLowerHeight) / 2 - sparLowerHeight, (sparLowerParallel + sparInnerWidth) / 2, 1);
                    sketchEdit.ksLineSeg(-(sparHeight - sparUpperHeight - sparLowerHeight) / 2 - sparLowerHeight, -((sparLowerParallel - sparInnerWidth) / 2), -(sparHeight - sparUpperHeight - sparLowerHeight) / 2 - sparLowerHeight, (sparLowerParallel + sparInnerWidth) / 2, 1);

                    sketchDef.EndEdit(); // Завершаем редактирование эскиза

                    sketch.Update();

                    ksEntity extrusion = (ksEntity)part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)extrusion.GetDefinition();
                    extrusionDef.SetSideParam(true, (short)End_Type.etBlind, extraction); // Устанавливаем параметры выдавливания
                    extrusionDef.SetSketch(sketch); // Устанавливаем эскиз для выдавливания 

                    extrusion.Create();

                    sketch.Update();
                    part.Update();

                    loggingActions.loggingFunction($"Успешная генерация Н-образного цельного лонжерона с параметрами, материал = {generateFunc.selectedMaterial}; выдавливание = {extraction}; ширина верхней паралелли = {sparUpperParallel}; " +
                        $"ширина нижней паралелли = {sparLowerParallel}; внутренняя ширина = {sparInnerWidth}; высота верхнего пояса = {sparUpperHeight}; высота нижнего пояса = {sparLowerHeight}; высота лонжерона = {sparHeight}", false);
                    
                    doc3D.SaveAs(generateFunc.returnPath(generateFunc.isAssembly ? 0 : 1));

                    generateFunc.buildMode(sparInnerWidth);
                }

            } catch (Exception e)
            {
                loggingActions.loggingFunction($"Ошибка генерации детали {e.Message} параметры: материал = {generateFunc.selectedMaterial}; выдавливание = {extraction}; ширина верхней паралелли = {sparUpperParallel}; " +
                        $"ширина нижней паралелли = {sparLowerParallel}; внутренняя ширина = {sparInnerWidth}; высота верхнего пояса = {sparUpperHeight}; высота нижнего пояса = {sparLowerHeight}; высота лонжерона = {sparHeight}", true);
            }
        }
    }
}

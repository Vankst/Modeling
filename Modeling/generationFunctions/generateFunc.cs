﻿using Kompas6API5;
using Kompas6Constants3D;
using Modeling.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Modeling.generationFunctions
{
    internal class generateFunc
    {
        public KompasObject kompas { get; set; }
        public string selectedMaterial { get; set; }
        public double selectedMaterialDensity { get; set; }

        public bool isAssembly { get; set; }

        public int countSpar { get; set; }
        public double stepMissing { get; set; }


        public static string[] nameFiles;


        public ksDocument3D createDocuments(KompasObject kompas, bool isAssembly)
        {
            ksDocument3D doc3D = kompas.Document3D();
            if (isAssembly)
                doc3D.Create(false, false);
            else
                doc3D.Create();
            return doc3D;
        }

        public string returnPath(bool isTemp)
        {
            string directoryPath;
            string fileName;
            int countFiles = 0;

            if (isTemp)
            {
                directoryPath = "NoEditor/3dDocuments/Temp/";
                fileName = "ВременныйЛонжерон№";
            }
            else
            {
                directoryPath = "NoEditor/3dDocuments/";
                fileName = "Лонжерон№";
                if (Directory.Exists("NoEditor/3dDocuments/Temp/"))
                    countFiles -= 1;
            }

            checkDirectory(directoryPath);

            string resultPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int ind = resultPath.Length - 12;
            resultPath = resultPath.Remove(ind);
            resultPath += directoryPath;
            countFiles += new DirectoryInfo(resultPath).GetFiles().Length + 1;
            resultPath += $"{fileName}{countFiles}.m3d";

            return resultPath;
        }

        public void clearDirectory()
        {
            string command = "taskkill /im KOMPAS.exe";

            var proc = new ProcessStartInfo()
            {
                UseShellExecute = true,
                WorkingDirectory = @"C:\Windows\System32",
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = "/c" +  command,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(proc);

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.Length - 12) + @"NoEditor\3dDocuments\Temp\";

            command = "rd /s /q " + path;
            proc = new ProcessStartInfo() {
                UseShellExecute = false,
                WorkingDirectory = @"C:\Windows\System32",
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = "/c " + command,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(proc);


        }

        private void checkDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void assemblyGetNameFiles()
        {
            if (!isAssembly)
                kompas.Visible = true;
            else
            {
                try
                {
                    nameFiles = Directory.GetFiles("NoEditor/3dDocuments/Temp/");

                    kompas.ActivateControllerAPI();

                    ksDocument3D doc3D = createDocuments(kompas, true);

                    for (int i = 0; i < countSpar; i++)
                    {
                        ksPart part = (ksPart)doc3D.GetPart((short)Part_Type.pTop_Part);

                        part.SetMaterial(selectedMaterial, selectedMaterialDensity);

                        doc3D.SetPartFromFile(nameFiles[0], part, true);

                        doc3D.ComponentPositioner().SetDragPoint(0, 0, 0);
                        doc3D.ComponentPositioner().SetPlane(part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
                        if (doc3D.ComponentPositioner().Prepare((part)part, (short)Positioner_Type.pnMove) == 0)
                        {
                            if (doc3D.ComponentPositioner().MoveComponent(1000, 0, 0) == true)
                                MessageBox.Show("Подвинул00");
                            doc3D.ComponentPositioner().Finish();
                        }
                    }

                    kompas.Visible = true;

                }
                catch(Exception ex)
                {
                   loggingActions loggingActions = new loggingActions();
                    loggingActions.loggingFunction(ex.Message, true);
                }
               
            }

        }
    }
}
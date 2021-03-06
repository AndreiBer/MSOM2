﻿using System;
using System.IO;

using iScan;
using iScan_trig;
using iScan_mock;
using iScan_dual;
using iScan_Alazar;
using MathWorks.MATLAB.NET.Arrays;
using System.Windows.Controls;
using System.Xml.Linq;

namespace ViewRSOM.Acquisition
{
    class initFullScan
    {
        // changes 
        // new changes
        // new changes 2

        public void start(string comment, Hardware.LaserSW.ViewModelBrightSolutions ondaLaser, Button cancelAcq_Button)
        {            
            
            // Define file parameters
            MWArray acqFolder = fileParameters.studyFolder + "\\" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00")
                + System.DateTime.Now.Day.ToString("00") +"\\";
            MWArray acqQuickScanFolder = acqFolder + "QuickScan\\";
            MWArray acqLogFolder = acqFolder + "Log\\";
            MWArray acqPreviewFolder = fileParameters.acqPreviewFolder;
            MWArray acqThumbnailFolder = acqFolder + "Thumbnail\\";
            MWArray dataName;
            if (fileParameters.dataName != null)
                dataName = System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + "_" + fileParameters.dataName;
            else
                dataName = System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");


            // create recon folder
            if (!Directory.Exists(acqFolder.ToString()))
            {
                Directory.CreateDirectory(acqFolder.ToString());
            }
            

            if (!Directory.Exists(acqQuickScanFolder.ToString()))
            {
                Directory.CreateDirectory(acqQuickScanFolder.ToString());
            }
            if (!Directory.Exists(acqLogFolder.ToString()))
            {
                Directory.CreateDirectory(acqLogFolder.ToString());
            }
            if (!Directory.Exists(acqPreviewFolder.ToString()))
            {
                Directory.CreateDirectory(acqPreviewFolder.ToString());
            }
            if (!Directory.Exists(acqThumbnailFolder.ToString()))
            {
                Directory.CreateDirectory(acqThumbnailFolder.ToString());
            }

            // copy file parameters to structure
            string[] fieldNames = { "acqFolder", "acqQuickScanFolder", "acqLogFolder", "acqPreviewFolder", "acqThumbnailFolder", "dataName" };
            MWStructArray fP = new MWStructArray(1, 1, fieldNames);
            fP.SetField("acqFolder", acqFolder);
            fP.SetField("acqQuickScanFolder", acqQuickScanFolder);
            fP.SetField("acqLogFolder", acqLogFolder);
            fP.SetField("acqPreviewFolder", acqPreviewFolder);
            fP.SetField("acqThumbnailFolder", acqThumbnailFolder);
            fP.SetField("dataName", dataName);

            // Define acquisition parameters
            MWNumericArray y_0 = acquisitionParameters.y_0;
            MWNumericArray x_0 = acquisitionParameters.x_0;
            MWNumericArray l_y = acquisitionParameters.l_y;
            MWNumericArray l_x = acquisitionParameters.l_x;
            MWNumericArray ds = acquisitionParameters.ds;
            MWNumericArray PRR = acquisitionParameters.PRR;
            MWNumericArray triggerWidth;
            if (systemState.LASERconnected == 1) // Wedge
                triggerWidth = 130; // [us]
            else
                triggerWidth = 5;
            MWNumericArray f_s = acquisitionParameters.f_s;
            MWNumericArray inputRange = acquisitionParameters.inputRange_list[acquisitionParameters.inputRange_listIndex];
            MWNumericArray z_low = acquisitionParameters.z_low;
            MWNumericArray z_target = acquisitionParameters.z_target;
            MWNumericArray z_high = acquisitionParameters.z_high;
            MWNumericArray v_x = acquisitionParameters.v_x;
            MWNumericArray acc = acquisitionParameters.acc;
            MWNumericArray acqDelay = acquisitionParameters.acqDelay;
            MWNumericArray d_acc = acquisitionParameters.d_acc;
            MWNumericArray acqRes = acquisitionParameters.acqRes;
            MWArray acqMode = acquisitionParameters.acqMode;
            MWNumericArray triggerLevel = acquisitionParameters.triggerLevel;
            MWNumericArray numberOfWavelength = acquisitionParameters.numberOfWavelength;
            
               
            MWNumericArray BscanUpdate = acquisitionParameters.BscanUpdate;
            MWArray controllerSerialNumber = acquisitionParameters.controllerSerialNumber;
            MWNumericArray quickScan = 0;

            // copy file parameters to structure
            string[] fieldNames2 = { "y_0", "x_0", "l_y", "l_x", "ds",
                                    "PRR", "triggerWidth", "f_s", "inputRange", "z_low", "z_target", "z_high",
                                    "v_x", "acc", "acqDelay", "d_acc", "acqRes", "acqMode", "triggerLevel", "numberOfWavelength",
                                    "BscanUpdate", "controllerSerialNumber", "quickScan" };
            MWStructArray aP = new MWStructArray(1, 1, fieldNames2);
            aP.SetField("y_0", y_0);
            aP.SetField("x_0", x_0);
            aP.SetField("l_y", l_y);
            aP.SetField("l_x", l_x);
            aP.SetField("ds", ds);
            aP.SetField("PRR", PRR);
            aP.SetField("triggerWidth", triggerWidth);
            aP.SetField("f_s", f_s);
            aP.SetField("inputRange", inputRange);
            aP.SetField("z_low", z_low);
            aP.SetField("z_target", z_target);
            aP.SetField("z_high", z_high);
            aP.SetField("v_x", v_x);
            aP.SetField("acc", acc);
            aP.SetField("acqDelay", acqDelay);
            aP.SetField("d_acc", d_acc);
            aP.SetField("acqRes", acqRes);
            aP.SetField("acqMode", acqMode);
            aP.SetField("triggerLevel", triggerLevel);
            aP.SetField("numberOfWavelength", numberOfWavelength);
            aP.SetField("BscanUpdate", BscanUpdate);
            aP.SetField("controllerSerialNumber", controllerSerialNumber);
            aP.SetField("quickScan", quickScan);

            // Define reconstruction parameters
            MWNumericArray t_focus = detectorParameters.t_focus;

            // copy file parameters to structure
            string[] fieldNames3 = { "t_focus"};
            MWStructArray dP = new MWStructArray(1, 1, fieldNames3);
            dP.SetField("t_focus", t_focus);

            // Define reconstruction parameters
            MWNumericArray v_s = reconstructionParameters.v_s;

            // copy file parameters to structure
            string[] fieldNames4 = { "v_s" };
            MWStructArray rP = new MWStructArray(1, 1, fieldNames4);
            rP.SetField("v_s", v_s);
            
            try
            {

                if (systemState.LASERconnected == 0 || systemState.DAQconnected == 0 || systemState.STAGESconnected == 0)
                {
                    iScanClass_mock obj_mock = null;
                    obj_mock = new iScanClass_mock();
                    obj_mock.iScan_mock(fP, aP, dP, rP);
                }
                else
                {
                   
                    // choose right kernel for stages
                    switch (systemState.DAQconnected)
                    {
                        case 1: // Gage
                            if (acquisitionParameters.triggerMode == 0)
                            {
                                iScanClass obj = null;
                                obj = new iScanClass();
                                obj.iScan(fP, aP, dP, rP);
                            }
                            else
                            {
                                iScanClass_trig obj_trig = null;
                                obj_trig = new iScanClass_trig();
                                obj_trig.iScan_trig(fP, aP, dP, rP);
                            }
                            break;
                        case 2: // Alazar
                            if (acquisitionParameters.triggerMode == 0)
                            {
                                iScanClass_Alazar obj_Alazar = null;
                                obj_Alazar = new iScanClass_Alazar();
                                obj_Alazar.iScan_Alazar(fP, aP, dP, rP);
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Alazar external trigger not yet implmented.");
                            }
                            break;
                        default:
                            Console.WriteLine("ERROR: Settings say there is no DAQ connected.");
                            break;
                    }

                    // switch off laser and close connection
                    if (systemState.LASERconnected == 1 || systemState.LASERconnected == 2)
                    {
                        // switch laser off and close connection
                        ondaLaser.EmissionOFF();
                        ondaLaser.closeConnection();
                        cancelAcq_Button.Click -= ondaLaser.laserHandle;
                    }

                }
            }
            catch (Exception e)
            {
                // check if laser emission can be stopped
                if (ondaLaser != null)
                {
                    try
                    {
                        ondaLaser.EmissionOFF();
                        ondaLaser.closeConnection();
                        cancelAcq_Button.Click -= ondaLaser.laserHandle;
                    }
                    catch
                    {
                        Console.WriteLine("WARNING: switch off laser EMISSION if necessary.");
                    };
                }

                if (!e.Message.Contains("ERROR:"))
                {
                    Console.WriteLine("ERROR:" + e.Message);
                }
                Console.WriteLine("\n");
                Console.WriteLine("Status-Acq: 1.00");
            }

            // write xml-file
            XDocument acqXML = new XDocument(
                new XElement("acquisitionParameters",
                    new XElement("l_y", l_y.ToString()),
                    new XElement("l_x", l_x.ToString()),
                    new XElement("ds", ds.ToString()),
                    new XElement("PRR", PRR.ToString()),
                    new XElement("laserEnergy", acquisitionParameters.laserPower.ToString()),
                    new XElement("f_s", f_s.ToString()),
                    new XElement("inputRange", inputRange.ToString()),
                    new XElement("z_low", z_low.ToString()),
                    new XElement("z_high", z_high.ToString()),
                    new XElement("numberOfWavelength", numberOfWavelength.ToString()),
                    new XElement("comment", comment)
                    )
               );

            acqXML.Declaration = new XDeclaration("1.0", "utf-8", "true");

            string filename = acqFolder.ToString() + dataName.ToString() + ".xml";
            acqXML.Save(@filename);

            //// write RSOMpreset xml-file
            //XDocument presetXML = new XDocument(
            //    new XElement("RSOMpreset",
            //        new XElement("laserSettings",
            //            new XElement("energy", acquisitionParameters.laserPower.ToString())
            //            )
            //        )
            //   );

            //presetXML.Declaration = new XDeclaration("1.0", "utf-8", "true");

            //string filenamePreset = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\iThera\\ViewRSOM\\RSOMpreset.xml";
            //presetXML.Save(@filenamePreset);

        }


        public void startMulti(string comment, ViewRSOM.MSOT.Hardware.ViewModels.Laser.ViewModelLaserInnolas multiLaser, Button cancelAcq_Button)
        {

            // Define file parameters
            MWArray acqFolder = fileParameters.studyFolder + "\\" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00")
                + System.DateTime.Now.Day.ToString("00") + "\\";
            MWArray acqQuickScanFolder = acqFolder + "QuickScan\\";
            MWArray acqLogFolder = acqFolder + "Log\\";
            MWArray acqPreviewFolder = fileParameters.acqPreviewFolder;
            MWArray acqThumbnailFolder = acqFolder + "Thumbnail\\";
            MWArray dataName;
            if (fileParameters.dataName != null)
                dataName = System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + "_" + fileParameters.dataName;
            else
                dataName = System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00");


            // create recon folder
            if (!Directory.Exists(acqFolder.ToString()))
            {
                Directory.CreateDirectory(acqFolder.ToString());
            }
            //else
            //{
            //    Console.Write("WARNING: acquisition folder exists already. Proceed with reconstruction. Previous reconstructions may be overwritten.");
            //}
            if (!Directory.Exists(acqQuickScanFolder.ToString()))
            {
                Directory.CreateDirectory(acqQuickScanFolder.ToString());
            }
            if (!Directory.Exists(acqLogFolder.ToString()))
            {
                Directory.CreateDirectory(acqLogFolder.ToString());
            }
            if (!Directory.Exists(acqPreviewFolder.ToString()))
            {
                Directory.CreateDirectory(acqPreviewFolder.ToString());
            }
            if (!Directory.Exists(acqThumbnailFolder.ToString()))
            {
                Directory.CreateDirectory(acqThumbnailFolder.ToString());
            }

            // copy file parameters to structure
            string[] fieldNames = { "acqFolder", "acqQuickScanFolder", "acqLogFolder", "acqPreviewFolder", "acqThumbnailFolder", "dataName" };
            MWStructArray fP = new MWStructArray(1, 1, fieldNames);
            fP.SetField("acqFolder", acqFolder);
            fP.SetField("acqQuickScanFolder", acqQuickScanFolder);
            fP.SetField("acqLogFolder", acqLogFolder);
            fP.SetField("acqPreviewFolder", acqPreviewFolder);
            fP.SetField("acqThumbnailFolder", acqThumbnailFolder);
            fP.SetField("dataName", dataName);

            // Define acquisition parameters
            MWNumericArray y_0 = acquisitionParameters.y_0;
            MWNumericArray x_0 = acquisitionParameters.x_0;
            MWNumericArray l_y = acquisitionParameters.l_y;
            MWNumericArray l_x = acquisitionParameters.l_x;
            MWNumericArray ds = acquisitionParameters.ds;
            MWNumericArray PRR = acquisitionParameters.PRR;
            MWNumericArray triggerWidth;
            if (systemState.LASERconnected == 1) // Wedge
                triggerWidth = 130; // [us]
            else
                triggerWidth = 5;
            MWNumericArray f_s = acquisitionParameters.f_s;
            MWNumericArray inputRange = acquisitionParameters.inputRange_list[acquisitionParameters.inputRange_listIndex];
            MWNumericArray z_low = acquisitionParameters.z_low;
            MWNumericArray z_target = acquisitionParameters.z_target;
            MWNumericArray z_high = acquisitionParameters.z_high;
            MWNumericArray v_x = acquisitionParameters.v_x;
            MWNumericArray acc = acquisitionParameters.acc;
            MWNumericArray acqDelay = acquisitionParameters.acqDelay;
            MWNumericArray d_acc = acquisitionParameters.d_acc;
            MWNumericArray acqRes = acquisitionParameters.acqRes;
            MWArray acqMode = acquisitionParameters.acqMode;
            MWNumericArray triggerLevel = acquisitionParameters.triggerLevel;
            MWNumericArray numberOfWavelength = acquisitionParameters.numberOfWavelength;
            MWNumericArray BscanUpdate = acquisitionParameters.BscanUpdate;
            MWArray controllerSerialNumber = acquisitionParameters.controllerSerialNumber;
            MWNumericArray quickScan = 0;

            //get number of wavelength from comment box
            if (!string.IsNullOrEmpty(comment)) 
            {
                char[] delimiterChars = { ' ', ',', '.', ':', ';', '\t' };
                string[] tokens = comment.Split(delimiterChars);
                int[] convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse);
                numberOfWavelength = convertedItems.Length;
                //Console.WriteLine(convertedItems);
            }
            // copy file parameters to structure
            string[] fieldNames2 = { "y_0", "x_0", "l_y", "l_x", "ds",
                                    "PRR", "triggerWidth", "f_s", "inputRange", "z_low", "z_target", "z_high",
                                    "v_x", "acc", "acqDelay", "d_acc", "acqRes", "acqMode", "triggerLevel", "numberOfWavelength",
                                    "BscanUpdate", "controllerSerialNumber", "quickScan" };
            MWStructArray aP = new MWStructArray(1, 1, fieldNames2);
            aP.SetField("y_0", y_0);
            aP.SetField("x_0", x_0);
            aP.SetField("l_y", l_y);
            aP.SetField("l_x", l_x);
            aP.SetField("ds", ds);
            aP.SetField("PRR", PRR);
            aP.SetField("triggerWidth", triggerWidth);
            aP.SetField("f_s", f_s);
            aP.SetField("inputRange", inputRange);
            aP.SetField("z_low", z_low);
            aP.SetField("z_target", z_target);
            aP.SetField("z_high", z_high);
            aP.SetField("v_x", v_x);
            aP.SetField("acc", acc);
            aP.SetField("acqDelay", acqDelay);
            aP.SetField("d_acc", d_acc);
            aP.SetField("acqRes", acqRes);
            aP.SetField("acqMode", acqMode);
            aP.SetField("triggerLevel", triggerLevel);
            aP.SetField("numberOfWavelength", numberOfWavelength);
            aP.SetField("BscanUpdate", BscanUpdate);
            aP.SetField("controllerSerialNumber", controllerSerialNumber);
            aP.SetField("quickScan", quickScan);

            // Define reconstruction parameters
            MWNumericArray t_focus = detectorParameters.t_focus;

            // copy file parameters to structure
            string[] fieldNames3 = { "t_focus" };
            MWStructArray dP = new MWStructArray(1, 1, fieldNames3);
            dP.SetField("t_focus", t_focus);

            // Define reconstruction parameters
            MWNumericArray v_s = reconstructionParameters.v_s;

            // copy file parameters to structure
            string[] fieldNames4 = { "v_s" };
            MWStructArray rP = new MWStructArray(1, 1, fieldNames4);
            rP.SetField("v_s", v_s);
            
            try
            {

                //if (systemState.LASERconnected == 0 || systemState.DAQconnected == 0 || systemState.STAGESconnected == 0)
                if (systemState.DAQconnected == 0 || systemState.STAGESconnected == 0)
                {
                    iScanClass_mock obj_mock = null;
                    obj_mock = new iScanClass_mock();
                    obj_mock.iScan_mock(fP, aP, dP, rP);
                }
                else
                {                   

                    // choose right kernel for stages
                    switch (systemState.DAQconnected)
                    {
                        case 1: // Gage                            

                            if (acquisitionParameters.triggerMode == 0)
                            {
                                
                                iScanClass_OPO obj = null;
                                obj = new iScanClass_OPO();
                                obj.iScan_dual(fP, aP, dP, rP);         

                            }
                            else
                            {
                                iScanClass_trig obj_trig = null;
                                obj_trig = new iScanClass_trig();
                                obj_trig.iScan_trig(fP, aP, dP, rP);
                            }
                            break;
                        case 2: // Alazar
                            if (acquisitionParameters.triggerMode == 0)
                            {
                                iScanClass_Alazar obj_Alazar = null;
                                obj_Alazar = new iScanClass_Alazar();
                                obj_Alazar.iScan_Alazar(fP, aP, dP, rP);
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Alazar external trigger not yet implmented.");
                            }
                            break;
                        default:
                            Console.WriteLine("ERROR: Settings say there is no DAQ connected.");
                            break;
                    }

                    // switch off laser and close connection
                    //if (systemState.LASERconnected == 1 || systemState.LASERconnected == 2)
                    {
                        // switch laser off and close connection
                        multiLaser.q_switch(false);

                        multiLaser.lamp(false);
                        //cancelAcq_Button.Click -= multiLaser.laserHandle;
                    }

                }
            }
            catch (Exception e)
            {
                // check if laser emission can be stopped
                if (multiLaser != null)
                {
                    try
                    {

                        multiLaser.q_switch(false);
                        multiLaser.lamp(false);
                        //cancelAcq_Button.Click -= multiLaser.laserHandle;
                    }
                    catch
                    {
                        Console.WriteLine("WARNING: switch off laser EMISSION if necessary.");
                    };
                }

                if (!e.Message.Contains("ERROR:"))
                {
                    Console.WriteLine("ERROR:" + e.Message);
                }
                Console.WriteLine("\n");
                Console.WriteLine("Status-Acq: 1.00");
            }

            // write xml-file
            XDocument acqXML = new XDocument(
                new XElement("acquisitionParameters",
                    new XElement("l_y", l_y.ToString()),
                    new XElement("l_x", l_x.ToString()),
                    new XElement("ds", ds.ToString()),
                    new XElement("PRR", PRR.ToString()),
                    new XElement("laserEnergy", acquisitionParameters.laserPower.ToString()),
                    new XElement("f_s", f_s.ToString()),
                    new XElement("inputRange", inputRange.ToString()),
                    new XElement("z_low", z_low.ToString()),
                    new XElement("z_high", z_high.ToString()),
                    new XElement("numberOfWavelength", numberOfWavelength.ToString()),
                    new XElement("comment", comment)
                    )
               );

            acqXML.Declaration = new XDeclaration("1.0", "utf-8", "true");

            string filename = acqFolder.ToString() + dataName.ToString() + ".xml";
            acqXML.Save(@filename);


        }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Net;
using System.Diagnostics;

namespace UpdateSystemDateTime
{
    class Program
    {
        [DllImport("kernel32.dll")]
        private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("kernel32.dll")]
        private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        const string DirExe = @"C:\Windows\System32\UpdateSystemDateTime.exe";

        static void Main(string[] args)
        {
            if (!File.Exists(DirExe))
            {
                RegistryKey IniKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run\", true);
                IniKey.SetValue("UpdateSystemDateTime", DirExe, RegistryValueKind.String);

                File.Copy("UpdateSystemDateTime.exe", DirExe, true);
            }
            else
            {
            }

            try
            {
                SYSTEMTIME systime = new SYSTEMTIME();

                var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
                var response = myHttpWebRequest.GetResponse();

                string[] dt = response.Headers.GetValues("Date");
                DateTime DtTime = Convert.ToDateTime(dt[0]);

                systime.wDay = (ushort)Convert.ToInt16((DtTime.ToString().Substring(0, 2)));
                systime.wMonth = (ushort)Convert.ToInt16((DtTime.ToString().Substring(3, 2)));
                systime.wYear = (ushort)Convert.ToInt16((DtTime.ToString().Substring(6, 4)));

                //Arreglo para restarle a la hora el UTC+1 que añade por la ubicación.
                var inTRepair = (-1 + (Convert.ToInt16((DtTime.ToString().Substring(11, 2)))));

                systime.wHour = (ushort)inTRepair;
                systime.wMinute = (ushort)Convert.ToInt16((DtTime.ToString().Substring(14, 2)));
                systime.wSecond = (ushort)Convert.ToInt16((DtTime.ToString().Substring(17, 2)));

                SetSystemTime(ref systime);
            }
            catch (Exception E)
            {
                String R;
                bool I = true; bool Z = true;

                while (I == true)
                {
                    Console.WriteLine("No se ha podido actualizar la fecha y hora del sistema. Compruebe su conexión a internet e intentelo de nuevo." + "\r\n\r\n" + "¿Desea intentarlo de nuevo? Pulse Y para (Si) o pulse N(No)");
                    I = false;
                    R = Console.ReadLine();

                    while (Z == true)
                    {
                        if (R.Equals("Y"))
                        {
                            Process RepeatProcess = new Process();
                            RepeatProcess.StartInfo.FileName = (DirExe);
                            RepeatProcess.Start();
                            Console.WriteLine("Si ha conectado el equipo a internet, la hora ya debería de haber cambiado con este proceso... Si aún no lo ha hecho, conectelo y vuelva a intentarlo.\r\n¿Desea volver a intentarlo?\r\nResponda con la nomenclatura anterior ante el si usando (Y) o el no (N)");
                            R = Console.ReadLine();

                            if (R.Equals("Y"))
                            {
                                I = true;
                                Z = true;
                                break;
                            }
                            else if (R.Equals("N"))
                            {
                                Z = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("No ha marcado la tecla correcta. Es necesario que sea en letras mayúsculas. Pulse cualquier tecla para finalizar...");
                                Console.ReadLine();
                            }
                        }
                        else if (R.Equals("N"))
                        {
                            Z = false;
                            break;
                        }
                        else
                        {
                            I = true;
                            Z = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}

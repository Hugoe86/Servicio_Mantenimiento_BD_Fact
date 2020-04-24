using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace SW_Mantenimiento_BD_Facturacion
{
    public partial class Servicio : ServiceBase
    {


        #region Variables
        public Timer Tmr_Intervalor;
        //public Int32 intervalo = 5000;//    prueba 5 seg
        public Int32 intervalo = 1800000; //    30 minutos
        public Int32 dias = 30;
        public string ruta = @"C:\\Respaldos_Bds\\Diarios";
        #endregion


        public Servicio()
        {
            InitializeComponent();

            Tmr_Intervalor = new System.Timers.Timer();
            Tmr_Intervalor.Interval = intervalo;
            Tmr_Intervalor.Elapsed += new ElapsedEventHandler(Tmr_Intervalor_Acciones);
        }

        protected override void OnStart(string[] args)
        {
            Tmr_Intervalor.Enabled = true;
        }

        protected override void OnStop()
        {
        }


        #region Metodos_Operacion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmr_Intervalor_Acciones(object sender, EventArgs e)
        {
            StreamWriter SW = new StreamWriter("C:\\Respaldos_Bds\\Historial.txt", true);

            DateTime fecha_actual = DateTime.Now;
            DateTime fecha_respaldo = new DateTime();

            int año = 0;
            int mes = 0;
            int dia = 0;
            int posicion_recorrida = 0;
            int dias_transcurridos = 0;

            try
            {
                DirectoryInfo Directorios = new DirectoryInfo(ruta);


                //  se recorren las carpetas
                foreach (var carpeta in Directorios.GetDirectories())
                {
                    //Console.WriteLine(carpeta.Name);

                    //SW.WriteLine(fecha_actual.ToString("dd/MM/yyyy"));
                    //SW.WriteLine(carpeta.Name);


                    //  se obtienen los archivos
                    DirectoryInfo Archivos = new DirectoryInfo(ruta + "\\" + carpeta.Name);

                    //  se recorren los archivos
                    foreach (var archivo in Archivos.GetFiles())
                    {
                        //Console.WriteLine(archivo.Name);

                        //SW.WriteLine(archivo.Name);


                        //  se inicializan las variables

                        año = 0;
                        mes = 0;
                        dia = 0;
                        posicion_recorrida = 0;


                        String[] Matriz_Archivo = archivo.Name.Split('_');
                        String[] Matriz_Empresa = carpeta.Name.Split('_');
                        int Posiciones = Matriz_Empresa.Length + 1;//   por la palabra backup

                        for (int Cont_For = Posiciones; Cont_For <= Posiciones + 2; Cont_For++)
                        {
                            if (posicion_recorrida == 0)
                            {
                                año = Convert.ToInt32(Matriz_Archivo[Cont_For]);
                            }
                            else if (posicion_recorrida == 1)
                            {
                                mes = Convert.ToInt32(Matriz_Archivo[Cont_For]);
                            }
                            else if (posicion_recorrida == 2)
                            {
                                dia = Convert.ToInt32(Matriz_Archivo[Cont_For]);
                            }


                            posicion_recorrida++;
                        }


                        //Console.WriteLine(año);
                        //Console.WriteLine(mes);
                        //Console.WriteLine(dia);

                        //  se carga la fecha
                        fecha_respaldo = new DateTime(año, mes, dia);

                        //  se comparan las fechas
                        TimeSpan ts_dias_transcurridos = new TimeSpan();

                        ts_dias_transcurridos = fecha_actual - fecha_respaldo;

                        dias_transcurridos = ts_dias_transcurridos.Days;
                        //Console.WriteLine(dias_transcurridos);


                        //SW.WriteLine("dias transucrridos: " + dias_transcurridos);

                        //  validamos la cantidad de dias
                        if (dias_transcurridos >= dias)
                        {
                            File.Delete(ruta + "\\" + carpeta.Name + "\\" + archivo.Name);
                            //Console.WriteLine("Borrado ****" + archivo.Name);
                            SW.WriteLine("Borrado ****" + archivo.Name);
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                //SW.WriteLine(ex.Message);
                throw new Exception(ex.Message);
                SW.WriteLine(ex.Message);
            }
            finally
            {
                SW.Close();
            }
        }

        #endregion
    }
}

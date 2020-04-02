using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicio_Windows_MantenimientoBD
{
    public partial class Servicio_Mantenimiento : ServiceBase
    {

        #region Variables
        private Timer Tmr_Intervalor = new Timer();
        #endregion



        #region Metodo_Inicial
        public Servicio_Mantenimiento()
        {
            InitializeComponent();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Inicializar_();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
        }


        #region Metodos_Operacion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Inicializar_()
        {

            try
            {
                Tmr_Intervalor.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Intervalo"]);

                Tmr_Intervalor.Tick += new EventHandler(Tmr_Intervalor_Acciones);
                Tmr_Intervalor.Enabled = true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmr_Intervalor_Acciones(object sender, EventArgs e)
        {
            String ruta = "";
            Int32 dias = 0;
            DateTime fecha_actual = DateTime.Now;
            DateTime fecha_respaldo = new DateTime();

            int año = 0;
            int mes = 0;
            int dia = 0;
            int posicion_recorrida = 0;
            int dias_transcurridos = 0;

            try
            {
                ruta = ConfigurationManager.AppSettings["Ruta"].ToString();
                DirectoryInfo Directorios = new DirectoryInfo(ruta);

                dias = Convert.ToInt32(ConfigurationManager.AppSettings["Dias"]);


                //  se recorren las carpetas
                foreach (var carpeta in Directorios.GetDirectories())
                {
                    Console.WriteLine(carpeta.Name);

                    //  se obtienen los archivos
                    DirectoryInfo Archivos = new DirectoryInfo(ruta + "\\" + carpeta.Name);

                    //  se recorren los archivos
                    foreach (var archivo in Archivos.GetFiles())
                    {
                        Console.WriteLine(archivo.Name);

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


                        Console.WriteLine(año);
                        Console.WriteLine(mes);
                        Console.WriteLine(dia);

                        //  se carga la fecha
                        fecha_respaldo = new DateTime(año, mes, dia);

                        //  se comparan las fechas
                        TimeSpan ts_dias_transcurridos = new TimeSpan();

                        ts_dias_transcurridos = fecha_actual - fecha_respaldo;

                        dias_transcurridos = ts_dias_transcurridos.Days;
                        Console.WriteLine(dias_transcurridos);

                        //  validamos la cantidad de dias
                        if (dias_transcurridos >= dias)
                        {
                            File.Delete(ruta + "\\" + carpeta.Name + "\\" + archivo.Name);
                            Console.WriteLine("Borrado ****" + archivo.Name);
                        }


                    }

                }


                Tmr_Intervalor.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        #endregion

    }
}

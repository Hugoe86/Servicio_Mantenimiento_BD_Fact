using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicio_Windows_MantenimientoBD
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Mantenimiento());


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Servicio_Mantenimiento()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

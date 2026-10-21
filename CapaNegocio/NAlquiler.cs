using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NAlquiler
    {
        private DAlquiler dalquiler = new DAlquiler();

        public List<Cliente> ListarClientes()
        {
            return dalquiler.ListarClientes();
        }

        public List<Equipo> ListarEquipos()
        {
            return dalquiler.ListarEquipos();
        }

        public List<Alquiler> ListarAlquileres()
        {
            return dalquiler.ListarAlquileres();
        }

        public string Registrar(Alquiler alquiler)
        {
            return dalquiler.Registrar(alquiler);
        }

        public string Modificar(Alquiler alquiler)
        {
            return dalquiler.Modificar(alquiler);
        }

        public string EliminarLogico(int idAlquiler)
        {
            return dalquiler.EliminarLogico(idAlquiler);
        }

        public List<Alquiler> ReporteAlquileresActivos()
        {
            return dalquiler.ReporteAlquileresActivos();
        }

        public List<Alquiler> ReporteIngresosPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            return dalquiler.ReporteIngresosPorRango(fechaInicio, fechaFin);
        }

        public decimal TotalIngresosPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            return dalquiler.TotalIngresosPorRango(fechaInicio, fechaFin);
        }
    }
}

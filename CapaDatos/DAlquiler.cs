using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos
{
    public class DAlquiler
    {
        public List<Cliente> ListarClientes()
        {
            using (var context = new BD_EFEntities())
            {
                return context.Cliente
                    .Where(c => !c.Eliminado)
                    .OrderBy(c => c.RazonSocial)
                    .ToList();
            }
        }

        public List<Equipo> ListarEquipos()
        {
            using (var context = new BD_EFEntities())
            {
                return context.Equipo
                    .Where(e => !e.Eliminado)
                    .OrderBy(e => e.Nombre)
                    .ToList();
            }
        }

        public List<Alquiler> ListarAlquileres()
        {
            using (var context = new BD_EFEntities())
            {
                return context.Alquiler
                    .Include(a => a.Cliente)
                    .Include(a => a.Equipo)
                    .Where(a => !a.Eliminado && !a.Cliente.Eliminado && !a.Equipo.Eliminado)
                    .OrderByDescending(a => a.IdAlquiler)
                    .ToList();
            }
        }

        public string Registrar(Alquiler alquiler)
        {
            try
            {
                using (var context = new BD_EFEntities())
                {
                    var cliente = context.Cliente.FirstOrDefault(c => c.IdCliente == alquiler.IdCliente && !c.Eliminado);
                    if (cliente == null)
                    {
                        return "Seleccione un cliente válido";
                    }

                    var equipo = context.Equipo.FirstOrDefault(e => e.IdEquipo == alquiler.IdEquipo && !e.Eliminado);
                    if (equipo == null)
                    {
                        return "Seleccione un equipo válido";
                    }

                    if (!equipo.Disponible)
                    {
                        return "No se puede registrar: el equipo no está disponible";
                    }

                    // Cualquier alquiler abierto impide asignar el equipo simultáneamente.
                    bool existeConflicto = context.Alquiler.Any(a =>
                        !a.Eliminado &&
                        a.IdEquipo == alquiler.IdEquipo &&
                        !a.FechaDevolucion.HasValue);

                    if (existeConflicto)
                    {
                        return "El equipo ya tiene un alquiler en esa fecha";
                    }

                    alquiler.FechaDevolucion = null;
                    alquiler.CostoTotal = 0;
                    alquiler.Eliminado = false;

                    equipo.Disponible = false;
                    context.Alquiler.Add(alquiler);
                    context.SaveChanges();
                }

                return "Registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar: " + ex.Message;
            }
        }

        public string Modificar(Alquiler alquiler)
        {
            try
            {
                using (var context = new BD_EFEntities())
                {
                    var temp = context.Alquiler
                        .Include(a => a.Equipo)
                        .FirstOrDefault(a => a.IdAlquiler == alquiler.IdAlquiler && !a.Eliminado);

                    if (temp == null)
                    {
                        return "Alquiler no encontrado";
                    }

                    var cliente = context.Cliente.FirstOrDefault(c => c.IdCliente == alquiler.IdCliente && !c.Eliminado);
                    if (cliente == null)
                    {
                        return "Seleccione un cliente válido";
                    }

                    var equipoNuevo = context.Equipo.FirstOrDefault(e => e.IdEquipo == alquiler.IdEquipo && !e.Eliminado);
                    if (equipoNuevo == null)
                    {
                        return "Seleccione un equipo válido";
                    }

                    // Evitar asignar o reprogramar sobre un alquiler activo diferente.
                    // El control también considera fechas anteriores al alquiler actual.
                    bool otroAlquilerActivo = context.Alquiler.Any(a =>
                        !a.Eliminado &&
                        a.IdAlquiler != alquiler.IdAlquiler &&
                        a.IdEquipo == alquiler.IdEquipo &&
                        !a.FechaDevolucion.HasValue);
                    if (otroAlquilerActivo)
                    {
                        return "No se puede modificar: el equipo tiene otro alquiler activo";
                    }

                    DateTime? fechaDevolucionNueva = alquiler.FechaDevolucion;
                    if (fechaDevolucionNueva.HasValue && fechaDevolucionNueva.Value.Date < alquiler.FechaAlquiler.Date)
                    {
                        return "La fecha de devolución no puede ser menor a la fecha de alquiler";
                    }

                    int equipoAnteriorId = temp.IdEquipo;
                    bool estabaActivo = !temp.FechaDevolucion.HasValue;
                    bool quedaraActivo = !fechaDevolucionNueva.HasValue;

                    if (equipoAnteriorId != alquiler.IdEquipo)
                    {
                        var equipoAnterior = context.Equipo.FirstOrDefault(e => e.IdEquipo == equipoAnteriorId);

                        if (estabaActivo && equipoAnterior != null)
                        {
                            equipoAnterior.Disponible = true;
                        }

                        if (quedaraActivo)
                        {
                            if (!equipoNuevo.Disponible)
                            {
                                return "No se puede modificar: el equipo no está disponible";
                            }
                            equipoNuevo.Disponible = false;
                        }
                    }
                    else
                    {
                        if (estabaActivo && !quedaraActivo)
                        {
                            equipoNuevo.Disponible = true;
                        }
                        else if (!estabaActivo && quedaraActivo)
                        {
                            if (!equipoNuevo.Disponible)
                            {
                                return "No se puede reactivar el alquiler: el equipo no está disponible";
                            }
                            equipoNuevo.Disponible = false;
                        }
                    }

                    temp.IdCliente = alquiler.IdCliente;
                    temp.IdEquipo = alquiler.IdEquipo;
                    temp.FechaAlquiler = alquiler.FechaAlquiler;
                    temp.FechaDevolucion = alquiler.FechaDevolucion;

                    if (fechaDevolucionNueva.HasValue)
                    {
                        int dias = (fechaDevolucionNueva.Value.Date - alquiler.FechaAlquiler.Date).Days;
                        if (dias <= 0)
                        {
                            dias = 1;
                        }
                        temp.CostoTotal = dias * equipoNuevo.CostoDiario;
                    }
                    else
                    {
                        temp.CostoTotal = 0;
                    }

                    context.SaveChanges();
                }

                return "Modificado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al modificar: " + ex.Message;
            }
        }

        public string EliminarLogico(int idAlquiler)
        {
            try
            {
                using (var context = new BD_EFEntities())
                {
                    var alquiler = context.Alquiler.FirstOrDefault(a => a.IdAlquiler == idAlquiler && !a.Eliminado);
                    if (alquiler == null)
                    {
                        return "Alquiler no encontrado";
                    }

                    if (!alquiler.FechaDevolucion.HasValue)
                    {
                        return "No se puede eliminar un alquiler sin fecha de devolución";
                    }

                    alquiler.Eliminado = true;
                    context.SaveChanges();
                }

                return "Eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar: " + ex.Message;
            }
        }

        public List<Alquiler> ReporteAlquileresActivos()
        {
            using (var context = new BD_EFEntities())
            {
                return context.Alquiler
                    .Include(a => a.Cliente)
                    .Include(a => a.Equipo)
                    .Where(a => !a.Eliminado && !a.Cliente.Eliminado && !a.Equipo.Eliminado && !a.FechaDevolucion.HasValue)
                    .OrderByDescending(a => a.FechaAlquiler)
                    .ToList();
            }
        }

        public List<Alquiler> ReporteIngresosPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            DateTime inicio = fechaInicio.Date;
            DateTime finExclusivo = fechaFin.Date.AddDays(1);

            using (var context = new BD_EFEntities())
            {
                return context.Alquiler
                    .Include(a => a.Cliente)
                    .Include(a => a.Equipo)
                    .Where(a => !a.Eliminado && !a.Cliente.Eliminado && !a.Equipo.Eliminado &&
                                a.FechaDevolucion.HasValue &&
                                a.FechaDevolucion >= inicio &&
                                a.FechaDevolucion < finExclusivo)
                    .OrderByDescending(a => a.FechaDevolucion)
                    .ToList();
            }
        }

        public decimal TotalIngresosPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            DateTime inicio = fechaInicio.Date;
            DateTime finExclusivo = fechaFin.Date.AddDays(1);

            using (var context = new BD_EFEntities())
            {
                var lista = context.Alquiler
                    .Where(a => !a.Eliminado &&
                                a.FechaDevolucion.HasValue &&
                                a.FechaDevolucion >= inicio &&
                                a.FechaDevolucion < finExclusivo)
                    .Select(a => a.CostoTotal)
                    .ToList();

                return lista.Sum(x => x ?? 0);
            }
        }
    }
}

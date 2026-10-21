using CapaDatos;
using CapaNegocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CapaAplicacion
{
    public partial class FrmAlquiler : Form
    {
        private NAlquiler nAlquiler = new NAlquiler();
        private int idAlquilerSeleccionado = 0;

        public FrmAlquiler()
        {
            InitializeComponent();
            dtpInicio.ValueChanged += DtpInicio_ValueChanged;
            dtpFin.ValueChanged += DtpFin_ValueChanged;
            lbEquipos.SelectedIndexChanged += LbEquipos_SelectedIndexChanged;
        }

        private void FrmAlquiler_Load(object sender, EventArgs e)
        {
            Text = "Mantenimiento de Alquileres";
            dtpFin.ShowCheckBox = true;
            dtpFin.Checked = false;
            tbCostoTotal.ReadOnly = true;
            dgvAlquileres.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlquileres.MultiSelect = false;
            dgvAlquileres.ReadOnly = true;
            dgvAlquileres.AllowUserToAddRows = false;
            dgvAlquileres.AllowUserToDeleteRows = false;

            CargarClientes();
            CargarEquipos();
            CargarAlquileres();
            LimpiarFormulario();
        }

        private void CargarClientes()
        {
            lbClientes.DataSource = null;
            lbClientes.DataSource = nAlquiler.ListarClientes();
            lbClientes.DisplayMember = "RazonSocial";
            lbClientes.ValueMember = "IdCliente";
        }

        private void CargarEquipos()
        {
            lbEquipos.DataSource = null;
            lbEquipos.DataSource = nAlquiler.ListarEquipos();
            lbEquipos.DisplayMember = "Nombre";
            lbEquipos.ValueMember = "IdEquipo";
        }

        private void CargarAlquileres()
        {
            var lista = nAlquiler.ListarAlquileres();
            var datos = lista.Select(a => new
            {
                a.IdAlquiler,
                a.IdCliente,
                a.IdEquipo,
                Cliente = a.Cliente.RazonSocial,
                Equipo = a.Equipo.Nombre,
                FechaAlquiler = a.FechaAlquiler.ToShortDateString(),
                FechaDevolucion = a.FechaDevolucion.HasValue ? a.FechaDevolucion.Value.ToShortDateString() : "",
                CostoTotal = (a.CostoTotal ?? 0).ToString("0.00")
            }).ToList();

            dgvAlquileres.DataSource = datos;

            if (dgvAlquileres.Columns["IdCliente"] != null)
            {
                dgvAlquileres.Columns["IdCliente"].Visible = false;
            }
            if (dgvAlquileres.Columns["IdEquipo"] != null)
            {
                dgvAlquileres.Columns["IdEquipo"].Visible = false;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (lbClientes.SelectedItem == null || lbEquipos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un cliente y un equipo");
                return;
            }

            Alquiler alquiler = new Alquiler
            {
                IdCliente = ((Cliente)lbClientes.SelectedItem).IdCliente,
                IdEquipo = ((Equipo)lbEquipos.SelectedItem).IdEquipo,
                FechaAlquiler = dtpInicio.Value.Date,
                FechaDevolucion = null,
                CostoTotal = 0,
                Eliminado = false
            };

            string mensaje = nAlquiler.Registrar(alquiler);
            MessageBox.Show(mensaje);

            if (mensaje == "Registrado correctamente")
            {
                CargarAlquileres();
                CargarEquipos();
                LimpiarFormulario();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idAlquilerSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un alquiler de la grilla");
                return;
            }

            if (lbClientes.SelectedItem == null || lbEquipos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un cliente y un equipo");
                return;
            }

            DateTime? fechaFin = null;
            if (dtpFin.Checked)
            {
                fechaFin = dtpFin.Value.Date;
            }

            Alquiler alquiler = new Alquiler
            {
                IdAlquiler = idAlquilerSeleccionado,
                IdCliente = ((Cliente)lbClientes.SelectedItem).IdCliente,
                IdEquipo = ((Equipo)lbEquipos.SelectedItem).IdEquipo,
                FechaAlquiler = dtpInicio.Value.Date,
                FechaDevolucion = fechaFin
            };

            string mensaje = nAlquiler.Modificar(alquiler);
            MessageBox.Show(mensaje);

            if (mensaje == "Modificado correctamente")
            {
                CargarAlquileres();
                CargarEquipos();
                LimpiarFormulario();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idAlquilerSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un alquiler de la grilla");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar lógicamente el alquiler seleccionado?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            string mensaje = nAlquiler.EliminarLogico(idAlquilerSeleccionado);
            MessageBox.Show(mensaje);

            if (mensaje == "Eliminado correctamente")
            {
                CargarAlquileres();
                LimpiarFormulario();
            }
        }

        private void LimpiarFormulario()
        {
            idAlquilerSeleccionado = 0;

            if (lbClientes.Items.Count > 0)
            {
                lbClientes.SelectedIndex = 0;
            }
            if (lbEquipos.Items.Count > 0)
            {
                lbEquipos.SelectedIndex = 0;
            }

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            dtpFin.Checked = false;
            tbCostoTotal.Text = "0.00";
            ActualizarCostoEstimado();
            dgvAlquileres.ClearSelection();
        }

        private void dgvAlquileres_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlquileres.SelectedRows.Count == 0)
            {
                return;
            }

            if (!dgvAlquileres.Columns.Contains("IdCliente"))
            {
                return;
            }

            DataGridViewRow fila = dgvAlquileres.SelectedRows[0];
            idAlquilerSeleccionado = Convert.ToInt32(fila.Cells["IdAlquiler"].Value);
            int idCliente = Convert.ToInt32(fila.Cells["IdCliente"].Value);
            int idEquipo = Convert.ToInt32(fila.Cells["IdEquipo"].Value);

            SeleccionarCliente(idCliente);
            SeleccionarEquipo(idEquipo);

            DateTime fechaAlquiler;
            if (DateTime.TryParse(fila.Cells["FechaAlquiler"].Value.ToString(), out fechaAlquiler))
            {
                dtpInicio.Value = fechaAlquiler;
            }

            string devolucionTexto = fila.Cells["FechaDevolucion"].Value == null ? "" : fila.Cells["FechaDevolucion"].Value.ToString();
            if (string.IsNullOrWhiteSpace(devolucionTexto))
            {
                dtpFin.Checked = false;
            }
            else
            {
                DateTime fechaDevolucion;
                if (DateTime.TryParse(devolucionTexto, out fechaDevolucion))
                {
                    dtpFin.Value = fechaDevolucion;
                    dtpFin.Checked = true;
                }
            }

            tbCostoTotal.Text = fila.Cells["CostoTotal"].Value.ToString();
        }

        private void SeleccionarCliente(int idCliente)
        {
            for (int i = 0; i < lbClientes.Items.Count; i++)
            {
                Cliente cliente = lbClientes.Items[i] as Cliente;
                if (cliente != null && cliente.IdCliente == idCliente)
                {
                    lbClientes.SelectedIndex = i;
                    break;
                }
            }
        }

        private void SeleccionarEquipo(int idEquipo)
        {
            for (int i = 0; i < lbEquipos.Items.Count; i++)
            {
                Equipo equipo = lbEquipos.Items[i] as Equipo;
                if (equipo != null && equipo.IdEquipo == idEquipo)
                {
                    lbEquipos.SelectedIndex = i;
                    break;
                }
            }
        }

        private void DtpInicio_ValueChanged(object sender, EventArgs e)
        {
            ActualizarCostoEstimado();
        }

        private void DtpFin_ValueChanged(object sender, EventArgs e)
        {
            ActualizarCostoEstimado();
        }

        private void LbEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCostoEstimado();
        }

        private void ActualizarCostoEstimado()
        {
            if (lbEquipos.SelectedItem == null)
            {
                tbCostoTotal.Text = "0.00";
                return;
            }

            if (!dtpFin.Checked)
            {
                tbCostoTotal.Text = "0.00";
                return;
            }

            if (dtpFin.Value.Date < dtpInicio.Value.Date)
            {
                tbCostoTotal.Text = "0.00";
                return;
            }

            Equipo equipo = (Equipo)lbEquipos.SelectedItem;
            int dias = (dtpFin.Value.Date - dtpInicio.Value.Date).Days;
            if (dias <= 0)
            {
                dias = 1;
            }

            decimal total = dias * equipo.CostoDiario;
            tbCostoTotal.Text = total.ToString("0.00");
        }

        private void btnReporteActivos_Click(object sender, EventArgs e)
        {
            var lista = nAlquiler.ReporteAlquileresActivos();
            var datos = lista.Select(a => new
            {
                a.IdAlquiler,
                Cliente = a.Cliente.RazonSocial,
                Equipo = a.Equipo.Nombre,
                FechaAlquiler = a.FechaAlquiler.ToShortDateString(),
                DiasTranscurridos = (DateTime.Today - a.FechaAlquiler.Date).Days
            }).ToList();

            dgvAlquileres.DataSource = datos;
            tbCostoTotal.Text = "No aplica";
        }

        private void btnReporteIngresos_Click(object sender, EventArgs e)
        {
            if (dtpFin.Value.Date < dtpInicio.Value.Date)
            {
                MessageBox.Show("La fecha fin no puede ser menor a la fecha inicio");
                return;
            }

            var lista = nAlquiler.ReporteIngresosPorRango(dtpInicio.Value.Date, dtpFin.Value.Date);
            var datos = lista
                .GroupBy(a => a.Cliente.RazonSocial)
                .Select(g => new
                {
                    Cliente = g.Key,
                    TotalIngresos = g.Sum(a => a.CostoTotal ?? 0)
                })
                .OrderBy(x => x.Cliente)
                .ToList();

            dgvAlquileres.DataSource = datos;
            tbCostoTotal.Text = nAlquiler.TotalIngresosPorRango(dtpInicio.Value.Date, dtpFin.Value.Date).ToString("0.00");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CargarAlquileres();
            tbCostoTotal.Text = "0.00";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

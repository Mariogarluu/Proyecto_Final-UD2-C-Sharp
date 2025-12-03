using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using CatalogoSeries.Data;
using CatalogoSeries.Models;

namespace CatalogoSeries
{
    /// <summary>
    /// Lógica de interacción para la ventana principal. Gestiona CRUD y eventos.
    /// </summary>
    public partial class MainWindow : Window
    {
        private int? serieSeleccionadaId = null;

        /// <summary>
        /// Constructor: Inicializa la BD, carga categorías y todas las series.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            CargarCategorias();
            CargarTodasLasSeries();
        }

        /// <summary>
        /// Obtiene las categorías de la BD y rellena la lista lateral y el ComboBox.
        /// </summary>
        private void CargarCategorias()
        {
            var lista = new List<Categoria>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Categoria";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            lbCategorias.ItemsSource = lista;
            cbCategoria.ItemsSource = lista;
        }

        /// <summary>
        /// Reinicia la vista mostrando todas las series sin filtrar por categoría.
        /// </summary>
        private void CargarTodasLasSeries()
        {
            CargarSeries(null, txtBuscar.Text);
            lbCategorias.SelectedItem = null;
        }

        /// <summary>
        /// Consulta SQL dinámica que filtra por ID de Categoría y/o Texto de búsqueda.
        /// </summary>
        private void CargarSeries(int? categoriaId, string filtro = "")
        {
            var lista = new List<Serie>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Serie WHERE 1=1";
                if (categoriaId != null) sql += " AND CategoriaId = @catId";
                if (!string.IsNullOrEmpty(filtro)) sql += " AND Titulo LIKE @filtro";
                sql += " ORDER BY Titulo ASC";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (categoriaId != null) cmd.Parameters.AddWithValue("@catId", categoriaId);
                    if (!string.IsNullOrEmpty(filtro)) cmd.Parameters.AddWithValue("@filtro", $"%{filtro}%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Serie
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Plataforma = reader.GetString(2),
                                Anio = reader.GetInt32(3),
                                Nota = reader.GetDouble(4),
                                Visto = reader.GetInt32(5),
                                CategoriaId = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            dgSeries.ItemsSource = lista;
            txtStats.Text = $"Total Series: {lista.Count}";
        }

        /// <summary>
        /// Evento al hacer clic en una categoría lateral: Filtra la tabla central.
        /// </summary>
        private void LbCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategorias.SelectedValue != null)
            {
                CargarSeries((int)lbCategorias.SelectedValue, txtBuscar.Text);
            }
        }

        /// <summary>
        /// Evento de búsqueda en tiempo real: Filtra por texto manteniendo la categoría seleccionada.
        /// </summary>
        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            int? catId = lbCategorias.SelectedValue as int?;
            CargarSeries(catId, txtBuscar.Text);
        }

        /// <summary>
        /// Evento al seleccionar una fila de la tabla: Rellena el formulario para editar.
        /// </summary>
        private void DgSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSeries.SelectedItem is Serie serie)
            {
                serieSeleccionadaId = serie.Id;
                txtTitulo.Text = serie.Titulo;
                cbPlataforma.Text = serie.Plataforma;
                txtAnio.Text = serie.Anio.ToString();
                txtNota.Text = serie.Nota.ToString();
                cbCategoria.SelectedValue = serie.CategoriaId;
                chkVisto.IsChecked = serie.Visto == 1;
            }
        }

        /// <summary>
        /// Botón Limpiar: Resetea el formulario para insertar una nueva serie.
        /// </summary>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormulario();
        }

        /// <summary>
        /// Botón Guardar: Realiza INSERT o UPDATE según si hay una serie seleccionada.
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || cbCategoria.SelectedValue == null)
            {
                MessageBox.Show("Título y Categoría son obligatorios.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtAnio.Text, out int anio) || !double.TryParse(txtNota.Text, out double nota))
            {
                MessageBox.Show("Año y Nota deben ser numéricos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql;

                if (serieSeleccionadaId == null)
                    sql = "INSERT INTO Serie (Titulo, Plataforma, Anio, Nota, Visto, CategoriaId) VALUES (@tit, @plat, @anio, @nota, @visto, @cat)";
                else
                    sql = "UPDATE Serie SET Titulo=@tit, Plataforma=@plat, Anio=@anio, Nota=@nota, Visto=@visto, CategoriaId=@cat WHERE Id=@id";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tit", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@plat", cbPlataforma.Text);
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.Parameters.AddWithValue("@visto", chkVisto.IsChecked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@cat", (int)cbCategoria.SelectedValue);

                    if (serieSeleccionadaId != null)
                        cmd.Parameters.AddWithValue("@id", serieSeleccionadaId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Guardado con éxito.", "Info");
            LimpiarFormulario();
            int? catId = lbCategorias.SelectedValue as int?;
            CargarSeries(catId, txtBuscar.Text);
        }

        /// <summary>
        /// Botón Eliminar: Borra la serie seleccionada tras confirmación.
        /// </summary>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (serieSeleccionadaId == null) return;

            if (MessageBox.Show($"¿Eliminar '{txtTitulo.Text}'?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Serie WHERE Id = @id";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", serieSeleccionadaId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                int? catId = lbCategorias.SelectedValue as int?;
                CargarSeries(catId, txtBuscar.Text);
            }
        }

        /// <summary>
        /// Helper para vaciar los controles visuales.
        /// </summary>
        private void LimpiarFormulario()
        {
            txtTitulo.Clear();
            txtAnio.Clear();
            txtNota.Clear();
            cbPlataforma.Text = "";
            cbCategoria.SelectedIndex = -1;
            chkVisto.IsChecked = false;
            serieSeleccionadaId = null;
            dgSeries.SelectedItem = null;
        }
    }
}
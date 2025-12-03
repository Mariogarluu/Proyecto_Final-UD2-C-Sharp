namespace CatalogoSeries.Models
{
    /// <summary>
    /// Representa una categoría o género dentro del catálogo (Ej: Anime, Live Action).
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        /// <summary>
        /// Sobrescribe ToString para que los controles WPF muestren el nombre y no el tipo de objeto.
        /// </summary>
        public override string ToString() => Nombre;
    }

    /// <summary>
    /// Representa una serie individual con sus detalles y relación con una categoría.
    /// </summary>
    public class Serie
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Plataforma { get; set; }
        public int Anio { get; set; }
        public double Nota { get; set; }
        public int Visto { get; set; }
        public int CategoriaId { get; set; }

        /// <summary>
        /// Propiedad auxiliar para convertir el entero de la BD en booleano para la interfaz.
        /// </summary>
        public bool EstaVisto
        {
            get { return Visto == 1; }
            set { Visto = value ? 1 : 0; }
        }
    }
}
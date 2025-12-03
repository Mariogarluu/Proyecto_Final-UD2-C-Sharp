using System.IO;
using System.Data.SQLite;

namespace CatalogoSeries.Data
{
    /// <summary>
    /// Clase estática encargada de la gestión de la base de datos SQLite.
    /// Maneja la creación del archivo, tablas y la conexión.
    /// </summary>
    public static class DatabaseHelper
    {
        private static string dbName = "Catalogo.db";
        private static string connectionString = $"Data Source={dbName};Version=3;Foreign Keys=True;";

        /// <summary>
        /// Verifica si la BD existe. Si no, la crea, genera tablas e inserta datos semilla (Animes, Series famosas).
        /// </summary>
        public static void InitializeDatabase()
        {
            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
            }

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 1. Crear Tablas
                string sqlTablas = @"
                    CREATE TABLE IF NOT EXISTS Categoria (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Serie (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Titulo TEXT NOT NULL,
                        Plataforma TEXT NOT NULL,
                        Anio INTEGER NOT NULL,
                        Nota REAL NOT NULL,
                        Visto INTEGER DEFAULT 0,
                        CategoriaId INTEGER NOT NULL,
                        FOREIGN KEY(CategoriaId) REFERENCES Categoria(Id) ON DELETE CASCADE
                    );";

                using (var cmd = new SQLiteCommand(sqlTablas, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2. Insertar Datos de Prueba (SOLO SI ESTÁ VACÍA)
                string check = "SELECT COUNT(*) FROM Categoria";
                using (var cmdCheck = new SQLiteCommand(check, connection))
                {
                    long count = (long)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        // Categorías
                        string sqlCats = @"
                            INSERT INTO Categoria (Nombre) VALUES 
                            ('Series Live Action'), 
                            ('Anime Japonés'), 
                            ('Animación Occidental');";
                        using (var cmd = new SQLiteCommand(sqlCats, connection)) { cmd.ExecuteNonQuery(); }

                        // Series Famosas y Animes
                        string sqlSeries = @"
                            -- Live Action
                            INSERT INTO Serie (Titulo, Plataforma, Anio, Nota, CategoriaId, Visto) VALUES 
                            ('Breaking Bad', 'Netflix', 2008, 9.5, 1, 1),
                            ('Game of Thrones', 'HBO', 2011, 9.2, 1, 1),
                            ('Stranger Things', 'Netflix', 2016, 8.7, 1, 0),
                            ('The Last of Us', 'HBO', 2023, 9.0, 1, 1);

                            -- Anime
                            INSERT INTO Serie (Titulo, Plataforma, Anio, Nota, CategoriaId, Visto) VALUES 
                            ('One Piece', 'Crunchyroll', 1999, 9.0, 2, 1),
                            ('Attack on Titan', 'Crunchyroll', 2013, 9.1, 2, 1),
                            ('Naruto Shippuden', 'Netflix', 2007, 8.5, 2, 1),
                            ('Death Note', 'Netflix', 2006, 9.0, 2, 1),
                            ('Demon Slayer', 'Crunchyroll', 2019, 8.9, 2, 0);

                            -- Animación
                            INSERT INTO Serie (Titulo, Plataforma, Anio, Nota, CategoriaId, Visto) VALUES 
                            ('Arcane', 'Netflix', 2021, 9.3, 3, 1),
                            ('Rick and Morty', 'HBO', 2013, 9.1, 3, 1),
                            ('Avatar: La Leyenda de Aang', 'Netflix', 2005, 9.3, 3, 1);
                        ";
                        using (var cmd = new SQLiteCommand(sqlSeries, connection)) { cmd.ExecuteNonQuery(); }
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve una nueva conexión a la base de datos lista para usar.
        /// </summary>
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
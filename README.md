# 📺 Catálogo de Series - MySeries List

Una aplicación de escritorio moderna desarrollada con WPF y .NET 8.0 para gestionar tu colección personal de series de televisión, anime y animación.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![WPF](https://img.shields.io/badge/WPF-Windows-0078D6?style=flat-square&logo=windows)
![SQLite](https://img.shields.io/badge/SQLite-3-003B57?style=flat-square&logo=sqlite)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

## 📋 Descripción

**MySeries List** es una aplicación de catálogo de series que te permite organizar, categorizar y llevar un seguimiento de todas las series que has visto o planeas ver. Con una interfaz inspirada en plataformas de streaming como Netflix, ofrece una experiencia de usuario moderna y atractiva.

## ✨ Características

- 🎬 **Gestión completa de series**: Operaciones CRUD (Crear, Leer, Actualizar, Eliminar)
- 📂 **Categorización**: Organiza tus series por categorías (Live Action, Anime, Animación)
- 🔍 **Búsqueda en tiempo real**: Filtra series por título mientras escribes
- 📊 **Puntuación**: Asigna notas del 0 al 10 a cada serie
- ✅ **Estado de visualización**: Marca las series como vistas o pendientes
- 🎯 **Filtrado por categoría**: Visualiza series de una categoría específica
- 📺 **Plataformas de streaming**: Registra en qué plataforma está disponible cada serie
- 💾 **Persistencia de datos**: Base de datos SQLite local
- 🎨 **Interfaz moderna**: Diseño oscuro inspirado en Netflix

## 🛠️ Tecnologías Utilizadas

| Tecnología | Versión | Descripción |
|------------|---------|-------------|
| .NET | 8.0 | Framework principal |
| WPF | - | Framework de interfaz gráfica |
| C# | 12 | Lenguaje de programación |
| SQLite | 2.0.2 | Base de datos local |
| System.Data.SQLite | 2.0.2 | Proveedor ADO.NET para SQLite |

## 📁 Estructura del Proyecto

```
Proyecto_Series/
├── 📄 App.xaml                    # Configuración de la aplicación
├── 📄 App.xaml.cs                 # Código de inicio de la aplicación
├── 📄 MainWindow.xaml             # Interfaz principal (XAML)
├── 📄 MainWindow.xaml.cs          # Lógica de la ventana principal
├── 📄 AssemblyInfo.cs             # Información del ensamblado
├── 📄 Proyecto_Series.csproj      # Archivo de proyecto
├── 🗃️ Catalogo.db                 # Base de datos SQLite (generada en ejecución)
├── 📁 Models/
│   └── 📄 Clases.cs               # Modelos de datos (Serie, Categoria)
└── 📁 Data/
    └── 📄 DatabaseHelper.cs       # Gestión de base de datos
```

## 🚀 Requisitos Previos

- **Sistema Operativo**: Windows 10/11
- **.NET 8.0 SDK**: [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (recomendado) con carga de trabajo "Desarrollo de escritorio de .NET"

## 📥 Instalación

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Mariogarluu/Proyecto_Final-UD2-C-Sharp.git
   cd Proyecto_Final-UD2-C-Sharp
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Compilar el proyecto**
   ```bash
   dotnet build
   ```

4. **Ejecutar la aplicación**
   ```bash
   dotnet run --project Proyecto_Series
   ```

## 💡 Uso de la Aplicación

### Pantalla Principal

La interfaz se divide en tres secciones:

| Sección | Descripción |
|---------|-------------|
| **Panel Izquierdo** | Lista de categorías para filtrar series |
| **Panel Central** | Tabla con todas las series (búsqueda incluida) |
| **Panel Derecho** | Formulario para añadir/editar series |

### Operaciones Disponibles

#### ➕ Añadir una Serie
1. Rellena los campos del formulario derecho
2. Selecciona una categoría y plataforma
3. Haz clic en **GUARDAR**

#### ✏️ Editar una Serie
1. Haz clic en una serie de la tabla central
2. Modifica los campos deseados
3. Haz clic en **GUARDAR**

#### 🗑️ Eliminar una Serie
1. Selecciona la serie en la tabla
2. Haz clic en **Eliminar**
3. Confirma la acción

#### 🔍 Buscar Series
- Escribe en el campo de búsqueda para filtrar en tiempo real

#### 📂 Filtrar por Categoría
- Haz clic en una categoría del panel izquierdo

## 🗄️ Base de Datos

La aplicación utiliza SQLite con las siguientes tablas:

### Tabla `Categoria`
| Campo | Tipo | Descripción |
|-------|------|-------------|
| Id | INTEGER | Clave primaria autoincremental |
| Nombre | TEXT | Nombre de la categoría |

### Tabla `Serie`
| Campo | Tipo | Descripción |
|-------|------|-------------|
| Id | INTEGER | Clave primaria autoincremental |
| Titulo | TEXT | Título de la serie |
| Plataforma | TEXT | Plataforma de streaming |
| Anio | INTEGER | Año de estreno |
| Nota | REAL | Puntuación (0-10) |
| Visto | INTEGER | Estado de visualización (0/1) |
| CategoriaId | INTEGER | FK a la categoría |

### Datos de Prueba Incluidos

La aplicación incluye datos de ejemplo:
- **Series Live Action**: Breaking Bad, Game of Thrones, Stranger Things, The Last of Us
- **Anime**: One Piece, Attack on Titan, Naruto Shippuden, Death Note, Demon Slayer
- **Animación**: Arcane, Rick and Morty, Avatar: La Leyenda de Aang

## 📸 Capturas de Pantalla

> *La aplicación presenta un diseño moderno con tema oscuro inspirado en Netflix, con colores principales en rojo (#E50914) y fondos en tonos de gris oscuro (#141414, #202020).*

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Para contribuir:

1. Haz un Fork del proyecto
2. Crea una rama para tu feature (`git checkout -b feature/NuevaCaracteristica`)
3. Realiza tus cambios y haz commit (`git commit -m 'Añadir nueva característica'`)
4. Sube los cambios (`git push origin feature/NuevaCaracteristica`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👤 Autor

**Mario García** - [@Mariogarluu](https://github.com/Mariogarluu)

---

⭐ Si este proyecto te ha sido útil, ¡no olvides darle una estrella!

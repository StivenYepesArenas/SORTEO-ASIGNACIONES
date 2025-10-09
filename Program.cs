using System;
using System.Collections.Generic;
using System.IO;

namespace SorteoDeHermanos
{
    /// <summary>
    /// Clase principal del programa que gestiona la lectura, ordenamiento
    /// y reescritura de un archivo de texto con una lista de nombres.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método principal del programa.
        /// Busca la ruta del archivo "Hermanos.txt", obtiene los nombres,
        /// los imprime, los reordena y sobrescribe el archivo.
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados).</param>
        static void Main(string[] args)
        {
            string rutaArchivo = BuscarRutaArchivo("Hermanos.txt");

            List<string> nombres = ObtenerNombres(rutaArchivo);
            ImprimirNombres(nombres);

            List<string> nombresSorteados = SortearNombres(nombres);
            ReescribirArchivo(nombresSorteados, rutaArchivo);
            ImprimirNombres(nombresSorteados);
        }

        /// <summary>
        /// Busca la ruta absoluta del archivo indicado dentro de la carpeta "Data"
        /// del proyecto, partiendo desde el directorio base de ejecución.
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo que se desea ubicar.</param>
        /// <returns>Ruta completa del archivo dentro de la carpeta "Data".</returns>
        /// <exception cref="DirectoryNotFoundException">
        /// Se lanza si no se encuentra el archivo o la carpeta del proyecto.
        /// </exception>
        static string BuscarRutaArchivo(string nombreArchivo)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            while (!string.IsNullOrEmpty(dir))
            {
                // Si existe un archivo .csproj en este nivel, se considera raíz del proyecto.
                var csprojFiles = Directory.GetFiles(dir, "*.csproj");
                if (csprojFiles.Length > 0)
                    break;

                // Verifica si el archivo existe dentro de una carpeta "Data".
                string posible = Path.Combine(dir, "Data", nombreArchivo);
                if (File.Exists(posible))
                    return posible;

                // Retrocede un nivel en el árbol de directorios.
                dir = Directory.GetParent(dir)?.FullName ?? "";
            }

            if (string.IsNullOrEmpty(dir))
                throw new DirectoryNotFoundException("No se encontró la carpeta del proyecto ni el archivo.");

            return Path.Combine(dir, "Data", nombreArchivo);
        }

        /// <summary>
        /// Lee las líneas de un archivo de texto y devuelve una lista con los nombres encontrados.
        /// </summary>
        /// <param name="rutaArchivo">Ruta completa del archivo a leer.</param>
        /// <returns>Lista de nombres leídos del archivo.</returns>
        static List<string> ObtenerNombres(string rutaArchivo)
        {
            List<string> listaNombres = new List<string>();

            try
            {
                if (File.Exists(rutaArchivo))
                {
                    foreach (var linea in File.ReadAllLines(rutaArchivo))
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            listaNombres.Add(linea.Trim());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El archivo no existe en la ruta especificada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al leer el archivo: {ex.Message}");
            }

            return listaNombres;
        }

        /// <summary>
        /// Reordena una lista de nombres moviendo los elementos posteriores al inicio de la lista.
        /// </summary>
        /// <param name="listaDeNombres">Lista original de nombres.</param>
        /// <returns>Lista reordenada de nombres.</returns>
        static List<string> SortearNombres(List<string> listaDeNombres)
        {
            List<string> copiaNombres = new List<string>();

            try
            {
                for (int i = 0; i < listaDeNombres.Count; i++)
                {
                    if (i < 3)
                        copiaNombres.Add(listaDeNombres[i]);
                    else
                        copiaNombres.Insert(0, listaDeNombres[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al sortear los nombres: {ex.Message}");
            }

            return copiaNombres;
        }

        /// <summary>
        /// Imprime en consola todos los nombres contenidos en una lista.
        /// </summary>
        /// <param name="listaNombres">Lista de nombres a mostrar.</param>
        static void ImprimirNombres(List<string> listaNombres)
        {
            try
            {
                if (listaNombres == null || listaNombres.Count == 0)
                {
                    Console.WriteLine("No hay nombres para mostrar.");
                    return;
                }

                foreach (var nombre in listaNombres)
                {
                    Console.WriteLine(nombre);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al imprimir los nombres: {ex.Message}");
            }
        }

        /// <summary>
        /// Sobrescribe el archivo de texto con una nueva lista de nombres.
        /// </summary>
        /// <param name="listaNombres">Lista de nombres a escribir en el archivo.</param>
        /// <param name="rutaArchivo">Ruta completa del archivo a sobrescribir.</param>
        /// <returns>La misma lista de nombres reescrita en el archivo.</returns>
        static List<string> ReescribirArchivo(List<string> listaNombres, string rutaArchivo)
        {
            try
            {
                File.WriteAllLines(rutaArchivo, listaNombres);
                Console.WriteLine($"Archivo reescrito exitosamente con {listaNombres.Count} nombres.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al escribir el archivo: {ex.Message}");
            }

            return listaNombres;
        }
    }
}

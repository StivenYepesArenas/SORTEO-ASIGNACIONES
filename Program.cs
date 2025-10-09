using System;
using System.Collections.Generic;
using System.IO;

namespace REUNIONES
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivo = BuscarRutaArchivo("Hermanos.txt");

            List<string> nombres = ObtenerNombres(rutaArchivo);
            ImprimirNombres(nombres);

            List<string> nombresSorteados = SortearNombres(nombres);
            ReescribirArchivo(nombresSorteados, rutaArchivo);
            ImprimirNombres(nombres);
            

        }

        static string BuscarRutaArchivo(string nombreArchivo)
        {
            string directorioActual = AppDomain.CurrentDomain.BaseDirectory;

            // Buscamos hacia arriba hasta encontrar la carpeta del proyecto
            while (!string.IsNullOrEmpty(directorioActual) &&
                   !File.Exists(Path.Combine(directorioActual, "SORTEO HERMANOS.csproj")))
            {
                directorioActual = Directory.GetParent(directorioActual)?.FullName ?? "";
            }

            if (string.IsNullOrEmpty(directorioActual))
            {
                throw new DirectoryNotFoundException("No se encontró la carpeta del proyecto.");
            }

            string rutaArchivo = Path.Combine(directorioActual, "Data", nombreArchivo);
            return rutaArchivo;
        }



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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace dummyData
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cargar datos de origen
            DatosAlumno datosAlumno = GeneradorDatos.CargarDatosOrigen();

            // Solicitar cantidad de alumnos
            int cantidadAlumnos = GeneradorDatos.SolicitarCantidadAlumnos();

            // Generar lista de alumnos
            List<Alumno> listaAlumnos = GeneradorDatos.GenerarListaAlumnos(datosAlumno, cantidadAlumnos);

            // Mostrar la lista de alumnos en la consola
            GeneradorDatos.MostrarListaAlumnos(listaAlumnos);

            // Permitir exportar la lista (opcional)
            GeneradorDatos.ExportarListaAlumnos(listaAlumnos);
        }
    }

    public class Alumno
    {
        public int Expediente { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Alumno(int expediente, string nombreCompleto, string correo, DateTime fechaNacimiento)
        {
            Expediente = expediente;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            FechaNacimiento = fechaNacimiento;
        }
    }

    public class DatosAlumno
    {
        public string[] NombresRusos { get; set; }
        public string[] ApellidosRusos { get; set; }
        public string[] NombresEspanol { get; set; }
        public string[] ApellidosEspanol { get; set; }
        public string[] NombresChino { get; set; }
        public string[] ApellidosChino { get; set; }
        public string[] NombresFrances { get; set; }
        public string[] ApellidosFrances { get; set; }
    }

    public static class GeneradorDatos
    {
        private static Random random = new Random();

        public static string GenerarNombreAleatorio(DatosAlumno datosAlumno, string origen)
        {
            string[] nombres;
            string[] apellidos;

            switch (origen)
            {
                case "Ruso":
                    nombres = datosAlumno.NombresRusos;
                    apellidos = datosAlumno.ApellidosRusos;
                    break;
                case "Español":
                    nombres = datosAlumno.NombresEspanol;
                    apellidos = datosAlumno.ApellidosEspanol;
                    break;
                case "Chino":
                    nombres = datosAlumno.NombresChino;
                    apellidos = datosAlumno.ApellidosChino;
                    break;
                case "Francés":
                    nombres = datosAlumno.NombresFrances;
                    apellidos = datosAlumno.ApellidosFrances;
                    break;
                default:
                    throw new Exception("Origen no válido: " + origen);
            }

            int indiceNombreAleatorio = random.Next(nombres.Length);
            int indiceApellidoAleatorio = random.Next(apellidos.Length);

            return nombres[indiceNombreAleatorio] + " " + apellidos[indiceApellidoAleatorio];
        }

        public static string GenerarCorreoElectronico(int expediente)
        {
            return expediente + "@unison.mx";
        }

        public static DateTime GenerarFechaNacimientoAleatoria()
        {
            DateTime fechaMinima = new DateTime(1998, 1, 1);
            DateTime fechaMaxima = new DateTime(2004, 12, 31);

            int diasAleatorios = random.Next((int)(fechaMaxima - fechaMinima).TotalDays);
            DateTime fechaNacimiento = fechaMinima.AddDays(diasAleatorios);

            return fechaNacimiento;
        }

        public static List<Alumno> GenerarListaAlumnos(DatosAlumno datosAlumno, int cantidadAlumnos)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            for (int i = 0; i < cantidadAlumnos; i++)
            {
                string origen = SeleccionarOrigenAleatorio();
                string nombreCompleto = GenerarNombreAleatorio(datosAlumno, origen);
                string correo = GenerarCorreoElectronico(i + 1);
                DateTime fechaNacimiento = GenerarFechaNacimientoAleatoria();

                Alumno nuevoAlumno = new Alumno(i + 1, nombreCompleto, correo, fechaNacimiento);
                listaAlumnos.Add(nuevoAlumno);
            }

            return listaAlumnos;
        }

        private static string SeleccionarOrigenAleatorio()
        {
            string[] posiblesOrigenes = { "Ruso", "Español", "Chino", "Francés" };
            int indiceOrigenAleatorio = random.Next(posiblesOrigenes.Length);
            return posiblesOrigenes[indiceOrigenAleatorio];
        }

        public static DatosAlumno CargarDatosOrigen()
        {
            DatosAlumno datosAlumno = new DatosAlumno();

            datosAlumno.NombresRusos = new string[] { "Ivan", "Dmitri", "Alexei", "Natalia", "Irina" };
            datosAlumno.ApellidosRusos = new string[] { "Ivanov", "Petrov", "Smirnov", "Volkova", "Kuznetsova" };

            datosAlumno.NombresEspanol = new string[] { "José", "María", "Antonio", "Ana", "Carlos" };
            datosAlumno.ApellidosEspanol = new string[] { "García", "López", "Martínez", "González", "Rodríguez" };

            datosAlumno.NombresChino = new string[] { "李", "王", "张", "刘" };
            datosAlumno.ApellidosChino = new string[] { "陈", "李", "王", "张", "赵" };

            datosAlumno.NombresFrances = new string[] { "Jean", "Marie", "Pierre", "Anne", "Nicolas" };
            datosAlumno.ApellidosFrances = new string[] { "Martin", "Bernard", "Dubois", "Durand", "Leroy" };

            return datosAlumno;
        }

        public static int SolicitarCantidadAlumnos()
        {
            Console.WriteLine("¿Cuántos alumnos desea generar?");
            int cantidadAlumnos;

            while (!int.TryParse(Console.ReadLine(), out cantidadAlumnos) || cantidadAlumnos <= 0)
            {
                Console.WriteLine("Ingrese un número positivo de alumnos:");
            }

            return cantidadAlumnos;
        }

        public static void MostrarListaAlumnos(List<Alumno> listaAlumnos)
        {
            Console.WriteLine("\nLista de Alumnos Generados:");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("{0,-15} {1,-25} {2,-30} {3}", "Expediente", "Nombre Completo", "Correo", "Fecha Nac.");
            Console.WriteLine("------------------------------------");

            foreach (Alumno alumno in listaAlumnos)
            {
                Console.WriteLine("{0,-15} {1,-25} {2,-30} {3}", alumno.Expediente, alumno.NombreCompleto, alumno.Correo, alumno.FechaNacimiento.ToString("yyyy/MM/dd"));
            }

            Console.WriteLine("\n------------------------------------");
        }

        public static void ExportarListaAlumnos(List<Alumno> listaAlumnos)
        {
            Console.WriteLine("\nExportación a CSV no implementada aún.");
        }
    }
}


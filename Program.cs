namespace AdministracionVM
{
    abstract class MaquinaVirtual
    {
        public string Nombre { get; set; }
        public string Version { get; set; }
        public string SistemaOperativo { get; set; }
        public int Estado { get; protected set; } // 0 = bajamos y 1 = levantamos

        public MaquinaVirtual(string nombre, string version, string so)
        {
            Nombre = nombre;
            Version = version;
            SistemaOperativo = so;
            Estado = 0; // por defecto apagada
        }

        // le mandamos un método abstracto para que cada vm pueda levantar
        public abstract void Levantar();

        public void Bajar()
        {
            Estado = 0;
            Console.WriteLine($"La máquina {Nombre} está ahora DOWN.");
        }

        public void MostrarEstado()
        {
            string estadoStr;

            if (Estado == 1)
            {
                estadoStr = "UP";
            }
            else
            {
                estadoStr = "DOWN";
            }

            Console.WriteLine("Máquina: " + Nombre + ", Estado: " + estadoStr);
        }
    }

    class MaquinaProcesoDatos : MaquinaVirtual
    {
        public string DatosOrigen { get; set; }
        public string DatosFin { get; set; }

        public MaquinaProcesoDatos(string nombre, string version, string so,
                                   string datosOrigen, string datosFin)
            : base(nombre, version, so)
        {
            DatosOrigen = datosOrigen;
            DatosFin = datosFin;
        }

        public override void Levantar()
        {
            Estado = 1;
            Console.WriteLine($"La máquina de proceso {Nombre} está UP.");
            Console.WriteLine($"Acceso a datos de origen: {DatosOrigen} almacenados.");
            Console.WriteLine($"Acceso a datos de fin: {DatosFin} almacenados.");
        }
    }

    class MaquinaAplicacion : MaquinaVirtual
    {
        public string Lenguaje { get; set; }
        public string VersionLenguaje { get; set; }
        public string BaseDeDatos { get; set; }

        public MaquinaAplicacion(string nombre, string version, string so,
                                 string lenguaje, string versionLenguaje, string baseDeDatos)
            : base(nombre, version, so)
        {
            Lenguaje = lenguaje;
            VersionLenguaje = versionLenguaje;
            BaseDeDatos = baseDeDatos;
        }

        public override void Levantar()
        {
            Estado = 1;
            Console.WriteLine($"La máquina de aplicación {Nombre} está UP.");
            Console.WriteLine($"Lenguaje {Lenguaje} instalado en versión {VersionLenguaje}.");
            Console.WriteLine($"Acceso a base de datos: {BaseDeDatos} correcto.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MaquinaVirtual[] maquinas = new MaquinaVirtual[]
            {
                new MaquinaProcesoDatos("VMProceso1", "1.0", "Linux", "OrigenDB1", "FinDB1"),
                new MaquinaProcesoDatos("VMProceso2", "1.1", "Linux", "OrigenDB2", "FinDB2"),
                new MaquinaAplicacion("VMApp1", "2.0", "Windows", "C#", "9.0", "DBApp1"),
                new MaquinaAplicacion("VMApp2", "2.1", "Windows", "Python", "3.11", "DBApp2")
            };

            Console.WriteLine("Levantando todas las máquinas virtuales");
            foreach (var vm in maquinas)
            {
                vm.Levantar();
                Console.WriteLine();
            }

            Console.WriteLine("Mostrando estados de todas las máquinas");
            foreach (var vm in maquinas)
            {
                vm.MostrarEstado();
            }

            Console.WriteLine("\nBajando todas las máquinas virtuales");
            foreach (var vm in maquinas)
            {
                vm.Bajar();
            }

            Console.WriteLine("\nEstados finales");
            foreach (var vm in maquinas)
            {
                vm.MostrarEstado();
            }
        }
    }
}


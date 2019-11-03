namespace ServidorChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor servidor = new Servidor("192.168.0.35", 1234);
            servidor.AceptarConexiones();
        }
    }
}

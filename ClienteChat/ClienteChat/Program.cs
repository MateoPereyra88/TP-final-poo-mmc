using System;

namespace ClienteChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("ingrese su nick: ");
            Cliente cliente = new Cliente("192.168.0.35", 1234, Console.ReadLine());
            cliente.Interactuar();
        }
    }
}
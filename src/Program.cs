using CsBindgen;

class Program
{
    static void Main(string[] args)
    {
        if (NativeMethods.initialize() != TrembleCError.Success)
        {
            Console.WriteLine("A fatal error occured when initializing the TrembleLib");
            return;
        }

        Log.Info("Hello, world!");

        NativeMethods.create_window(out ulong id);
        Log.Info($"{id}");

        while (NativeMethods.tick()) { }

        Log.Info("Goodbye, world!");
        NativeMethods.shutdown();
    }
}

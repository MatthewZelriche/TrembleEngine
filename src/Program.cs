using CsBindgen;

class Program
{
    static void Main(string[] args)
    {
        if (NativeMethods.tr_initialize() != TrembleCError.Success)
        {
            Console.WriteLine("A fatal error occured when initializing the TrembleLib");
            return;
        }

        NativeMethods.tr_test();
        Log.Info("Hello, world!");

        NativeMethods.tr_shutdown();
    }
}

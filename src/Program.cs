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

        Log.Info("Hello, world!");
        // Test creating a second window
        ulong id = 0;
        unsafe
        {
            NativeMethods.tr_create_window(&id);
        }

        while (NativeMethods.tr_tick()) { }

        Log.Info("Goodbye, world!");
        NativeMethods.tr_shutdown();
    }
}

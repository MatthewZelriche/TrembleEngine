using CsBindgen;

class Program
{
    static void Main(string[] args)
    {
        var res = NativeMethods.tr_initialize();
        Console.WriteLine("Library initialized with: " + res);
        NativeMethods.tr_test();
        NativeMethods.tr_shutdown();
    }
}

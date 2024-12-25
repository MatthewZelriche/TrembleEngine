using System.Runtime.CompilerServices;
using System.Text;
using CsBindgen;

// TODO: Investigate performance
public class Log
{

    public static void Info(string msg, [CallerFilePath] string filepath = "", [CallerLineNumber] uint line = 0)
    {
        unsafe { Print(msg, filepath, line, &NativeMethods.tr_info); }
    }

    public static void Warn(string msg, [CallerFilePath] string filepath = "", [CallerLineNumber] uint line = 0)
    {
        unsafe { Print(msg, filepath, line, &NativeMethods.tr_warn); }
    }

    public static void Error(string msg, [CallerFilePath] string filepath = "", [CallerLineNumber] uint line = 0)
    {
        unsafe { Print(msg, filepath, line, &NativeMethods.tr_error); }
    }

    private static unsafe void Print(string msg, string filename, uint line, delegate*<byte*, uint, byte*, void> printMethod)
    {
        {
            var filenameutf8 = Encoding.UTF8.GetBytes(Path.GetFileName(filename + "\0"));
            var msgutf8 = Encoding.UTF8.GetBytes(msg + "\0");

            unsafe
            {
                fixed (byte* msg_ptr = msgutf8, filename_ptr = filenameutf8)
                {
                    printMethod(filename_ptr, line, msg_ptr);
                }
            }
        }
    }
}
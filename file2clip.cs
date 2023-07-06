/*
	this simple tool will copy file passed as argument into windows clipboard

	http://stackoverflow.com/questions/17189010/how-to-copy-cut-a-file-not-the-contents-to-the-clipboard-in-windows-on-the-com
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Windows;

[assembly: AssemblyTitle("File2Clip")]
[assembly: AssemblyDescription("copies file to clipboard (as a file)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("rostok - https://github.com/rostok/")]
[assembly: AssemblyProduct("File2Clip")]
[assembly: AssemblyCopyright("Copyright � 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]

namespace File2Clip
{
    public class App
    {
	    [STAThread]
        static void Main(string[] args)
        {
            List<string> files = new List<string>();

	        string line;

	        if (App.IsRedirected())
	        {
		        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
		        {
			        files.Add(line);
		        }
	        }

	        // args
            foreach (string arg in args) {
            	files.Add(arg);
            }

            StringCollection paths = new StringCollection();
            foreach (string file in files) 
            {
                string t = file.Trim('"');
                string path = Path.GetFullPath(t);
                
                paths.Add(path);
            }
            
            if (paths.Count < 1)
            {
				Console.WriteLine("Input is Empty! Provide file paths via STDIN or command line arguments.");
				Environment.Exit(87); // 87 is INVALID_PARAMETER, which fits this pretty well. https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-
            }

            Clipboard.SetFileDropList(paths);

            Console.WriteLine("Files copied to the clipboard");
        }

        private static bool IsRedirected()
        {
	        try{
		        bool ka = Console.KeyAvailable;
		        // if this should raise an error we are dealing with redirection
		        // http://stackoverflow.com/questions/12911833/how-to-catch-all-exceptions-in-c-sharp-using-try-and-catch
		        // http://stackoverflow.com/questions/3453220/how-to-detect-if-console-in-stdin-has-been-redirected/3453272#3453272
		        // http://stackoverflow.com/questions/7302544/flushing-system-console-read
		        return false;
	        }
	        catch (Exception e)
	        {
		        return true;
	        }

        }
    }
}
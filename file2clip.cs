/*
	this simple tool will copy file passed as argument into windows clipboard

	http://stackoverflow.com/questions/17189010/how-to-copy-cut-a-file-not-the-contents-to-the-clipboard-in-windows-on-the-com
*/
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("File2Clip")]
[assembly: AssemblyDescription("copies file to clipboard (as a file)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("rostok - https://github.com/rostok/")]
[assembly: AssemblyProduct("File2Clip")]
[assembly: AssemblyCopyright("Copyright © 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]

namespace File2Clip
{
    public class App
    {
    
    private static bool isConsoleSizeZero { 
        get{  return 0 == (Console.WindowHeight + Console.WindowWidth); }
    }
    public static bool IsOutputRedirected {
        get { return isConsoleSizeZero && !Console.KeyAvailable; }
    }
    public static bool IsInputRedirected {
        get { return isConsoleSizeZero && Console.KeyAvailable; }
    }

        [STAThread]
        static void Main(string[] args)
        {
			//Console.WriteLine("File2Clip");
            List<string> list = new List<string>();

	        string line;

			try{
				bool ka = Console.KeyAvailable;
				// if this should raise an error we are dealing with redirection
				// http://stackoverflow.com/questions/12911833/how-to-catch-all-exceptions-in-c-sharp-using-try-and-catch
				// http://stackoverflow.com/questions/3453220/how-to-detect-if-console-in-stdin-has-been-redirected/3453272#3453272
				// http://stackoverflow.com/questions/7302544/flushing-system-console-read
			}
			catch (Exception e)
			{
				//Console.WriteLine("Console.KeyAvailable	PROBLEM");
    			// stdin 
                //if (!IsInputRedirected)
                {
                	while(!string.IsNullOrEmpty(line = Console.ReadLine())) list.Add(line);
                }
    			//Console.WriteLine("Console.In.Peek()	"+ Console.In.Peek());
			}
			

            // args
            foreach (string s in args) {
            	list.Add(s);
            }

            StringCollection paths = new StringCollection();
            foreach (string s in list) 
            {
                string t = s.Trim('"');
                paths.Add( 
                    System.IO.Path.IsPathRooted(t) ? 
                      t : 
                      System.IO.Directory.GetCurrentDirectory() + 
                        @"\" + t);
            }
            if (paths.Count>0)
            {
            	 Clipboard.SetFileDropList(paths);
            }
			else
			{
				Console.WriteLine("Input is Empty! Provide file paths via STDIN or command line arguments.");
			}
        }
    }
}
using CefSharp;
using CefSharp.Example;
using CefSharp.Example.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zyj.WebBrowser
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            const bool simpleSubProcess = false;

            Cef.EnableHighDPISupport();

            //NOTE: Using a simple sub processes uses your existing application executable to spawn instances of the sub process.
            //Features like JSB, EvaluateScriptAsync, custom schemes require the CefSharp.BrowserSubprocess to function
            //            if (simpleSubProcess)
            //            {
            //                var exitCode = Cef.ExecuteProcess();

            //                if (exitCode >= 0)
            //                {
            //                    return exitCode;
            //                }

            //#if DEBUG
            //                if (!System.Diagnostics.Debugger.IsAttached)
            //                {
            //                    MessageBox.Show("When running this Example outside of Visual Studio" +
            //                                    "please make sure you compile in `Release` mode.", "Warning");
            //                }
            //#endif

            //                var settings = new CefSettings();
            //                settings.BrowserSubprocessPath = "CefSharp.WinForms.Example.exe";

            //                Cef.Initialize(settings);

            //                var browser = new SimpleBrowserForm();
            //                Application.Run(browser);
            //            }
            //            else
            //            {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                MessageBox.Show("When running this Example outside of Visual Studio" +
                                "please make sure you compile in `Release` mode.", "Warning");
            }
#endif

            const bool multiThreadedMessageLoop = true;

            var browser = new WebBrowser(multiThreadedMessageLoop);
            //var browser = new SimpleBrowserForm();
            //var browser = new TabulationDemoForm();

            IBrowserProcessHandler browserProcessHandler;

            if (multiThreadedMessageLoop)
            {
                browserProcessHandler = new BrowserProcessHandler();
            }
            else
            {
                //Get the current taskScheduler (must be called after the form is created)
                var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

                browserProcessHandler = new WinFormsBrowserProcessHandler(scheduler);
            }

            CefExample.Init(osr: false, multiThreadedMessageLoop: multiThreadedMessageLoop, browserProcessHandler: browserProcessHandler);

            Application.Run(browser);
            return 0;
        }

    }
    //}
}

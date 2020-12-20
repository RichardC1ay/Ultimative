using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimative.MCL.Launch
{
    public class GameHandle
    {
        public string CommandLine { get; private set; }
        public Process RunningProcess { get; private set; }

        public GameHandle(string _commandline)
        {
            CommandLine = _commandline;
        }

        public void Run()
        {
            RunningProcess = Process.Start(CommandLine);
        }

        public async void Dispose()
        {
            if (RunningProcess != null)
            {
                RunningProcess.Kill();
            }
            RunningProcess.Close();

            await new ContentDialog()
            {
                Title = "Game Stopped",
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
        }
    }

    
}

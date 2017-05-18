using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;

namespace PowerShell
{
   class Program
   {
      static void Main(string[] args)
      {
         string scriptfile = @"C:\file.ps1";
         //using (System.Management.Automation.PowerShell powerShellInstance = System.Management.Automation.PowerShell.Create())
         //{
         //   // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
         //   // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.
         //   powerShellInstance.AddScript("param($param1) $d = get-date; $s = 'test string value'; " +
         //           "$d; $s; $param1; get-service");

         //   // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
         //   powerShellInstance.AddParameter("param1", "parameter 1 value!");
         //   // invoke execution on the pipeline (collecting output)
         //   Collection<PSObject> PSOutput = powerShellInstance.Invoke();

         //   // loop through each output object item
         //   foreach (PSObject outputItem in PSOutput)
         //   {
         //      // if null object was dumped to the pipeline during the script then a null
         //      // object may be present here. check for null to prevent potential NRE.
         //      if (outputItem != null)
         //      {
         //         //TODO: do something with the output item 
         //         // outputItem.BaseOBject
         //         Console.WriteLine(outputItem.BaseObject.GetType().FullName);
         //         Console.WriteLine(outputItem.BaseObject.ToString() + "\n");
         //      }
         //   }
         //}

         RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();

         Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
         runspace.Open();

         RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);

         Pipeline pipeline = runspace.CreatePipeline();

         //Here's how you add a new script with arguments
         Command myCommand = new Command(scriptfile);
         //   CommandParameter testParam = new CommandParameter("key", "value");
        
         myCommand.Parameters.Add(new CommandParameter("PathToSourceZipFile", @"C:\swat\Tes.zip"));
         myCommand.Parameters.Add(new CommandParameter("PathToPropertiesFile", @"C:\Test.properties"));
         myCommand.Parameters.Add(new CommandParameter("PathToOutput", @"C:\swat\PowerShell"));
         //myCommand.Parameters.Add(new CommandParameter("DbAction", "all"));
         //myCommand.Parameters.Add(new CommandParameter("Action", "publish"));
         pipeline.Commands.Add(myCommand);

         // Execute PowerShell script
         var results = pipeline.Invoke();
         pipeline.Dispose();
         runspace.Dispose();
      }
   }
}

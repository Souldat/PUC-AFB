using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Timers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PUC_AFB
{
    public partial class AFB : ServiceBase
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static System.Timers.Timer backupTimer = new System.Timers.Timer();
        public static System.Timers.Timer backupTimer2 = new System.Timers.Timer();
        PUC_AFB.Properties.Settings Settings = new PUC_AFB.Properties.Settings();        

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(
                string lpszUsername,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                out IntPtr phToken);

        List<string> storageDestinationList = new List<string>();
        List<string> netUserList = new List<string>();
        List<string> netPassList = new List<string>();
        string line;
        string destination;
        string netUser;
        string netPass;
        int counter = 0;

        public static string basePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public List<string> settingsList;

        public void writeLogInfo(string str)
        {
                log.Info(str);
        }

        public void writeLogError(string str)
        {

            log.Error(str);
        }
               
        BackgroundWorker bg1 = new BackgroundWorker();        

        protected override void OnStart(string[] args)
        {
             System.Diagnostics.Debugger.Launch();  

            writeLogInfo("---------------------STARTUP------------------------");

            //Start timers for initial file copy and the future recurring file copy
            backupTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            backupTimer2.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            writeLogInfo("Event timers created.");

            writeLogInfo("Starting timers.");
            bg1.RunWorkerAsync(startTimers());          
            writeLogInfo("Background workers Async timer start initiated.");

            
        }

        public void GetDestinationSettings()
        {
            //read settings file to find a storage count and start a file copy thread for each seperate storage destination
            //position 0 of each list are the settings for the first destination path, position 1 is the settings for the next destination path (network path and user credentials)

            storageDestinationList.Clear();
            counter = 0;

            writeLogInfo("Reading settings for destination path information.");

            try
            {
                // Read the file and find all instances of storage destination.
                System.IO.StreamReader file = new System.IO.StreamReader(basePath + "\\destinationConfiguration.ws");

                while ((line = file.ReadLine()) != null)
                {
                    //find instances of destination path
                    if (line.Contains("DestinationPath"))
                    {
                        //clean up strings 
                        //for some reason I was geting a bunch of empty characters at the beginning of each string so used the .Trim() method to ditch it
                        //ugly but works
                        destination = file.ReadLine().Replace("<value>", "");
                        destination = destination.Replace("</value>", "");
                        destination = destination.Trim();
                        netUser = file.ReadLine().Replace("<user>", "");
                        netUser = netUser.Replace("</user>", "");
                        netUser = netUser.Trim();
                        netPass = file.ReadLine().Replace("<password>", "");
                        netPass = netPass.Replace("</password>", "");
                        netPass = netPass.Trim();

                        //insert dest path net user and net pass into list to be used in their own threads later
                        storageDestinationList.Insert(counter, destination);
                        netUserList.Insert(counter, netUser);
                        netPassList.Insert(counter, netPass);
                        counter++;
                    }
                }

                file.Close();
                file.Dispose();
            }
            catch (Exception exc)
            {
                writeLogError(exc.InnerException.ToString());
            }
        }

        public int startTimers()
        {
            try
            {
                backupTimer.Interval = Settings.UpdateIntervalMinutes * 60000;
                backupTimer.AutoReset = true;
                backupTimer.Start();
                writeLogInfo("Update interval timer started with a value of: " + Settings.UpdateIntervalMinutes.ToString());

                backupTimer2.Interval = 1000;
                backupTimer2.AutoReset = false;
                backupTimer2.Start();
                writeLogInfo("Initial startup copy timer started.");
            }
            catch (Exception exc)
            {
                writeLogError(exc.InnerException.ToString());
            }         

            return 0;
        }

        public void fileCopy(string sourcePath, string destinationPath, string netUser, string netPassword)
        {  
            //set directory paths for copy
            DirectoryInfo dir = new DirectoryInfo(sourcePath);

            //set directory paths for copy
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            try
            {
                var impersonationContext = new WrappedImpersonationContext(destinationPath, netUser, netPassword);
                impersonationContext.Enter();
                writeLogInfo("Entered domain (success?)");                

                //prevent dummies from blowing up systems (as much as possible)
                if (destinationPath.Contains(Settings.SourcePath))
                {
                    writeLogInfo("Destination path is the same as the source path. Please verify source and destination path settings.");
                    Environment.Exit(1);
                }                              

                //set directory paths for delete
                DirectoryInfo dirDelete = new DirectoryInfo(destinationPath);
                
                try
                {  
                    //get current date time minus N days for retention period (adjust -N to number of days of retention required)
                    DateTime expiryDate = DateTime.Today.AddDays((Settings.RetentionPeriodDays + Settings.RetentionBufferDays) * -1);
                                    
                    //copy files from A to B 
                    try
                    {
                        writeLogInfo("Checking if new files need to be copied to: " + destinationPath);

                        int count = 0;

                        foreach (FileInfo fileSource in dir.GetFiles())
                        {
                            FileInfo fileDest = new FileInfo(destinationPath.ToString() + "\\" + fileSource.Name);

                            if (fileSource.LastWriteTime.Date > expiryDate.Date && fileDest.LastWriteTime != fileSource.LastWriteTime)
                            {
                                fileSource.CopyTo(Path.Combine(destinationPath.ToString(), fileSource.Name), true);
                                writeLogInfo("Copying:" + fileSource.Name);

                                count++;
                            }
                        }

                        if (count == 0)
                        {
                            writeLogInfo("No new files exist that need to be copied.");
                        }
                        else
                        {
                            writeLogInfo("Finished copying files.");
                        }

                    }
                    catch (Exception ex)
                    {
                        writeLogError(ex.ToString());
                    }

                    if (Settings.RetentionPeriodDays == 0)
                    {
                        writeLogInfo("No retention period set nothing to delete.");
                    }
                    else if (Settings.RetentionPeriodDays > 0)
                    {
                        try
                        {
                            writeLogInfo("Deleting any files older than: " + Settings.RetentionPeriodDays + " (plus buffer of " + Settings.RetentionBufferDays + ") days if they exist.");

                            int count = 0;

                            //delete any files older than N days from B
                            foreach (FileInfo file in dirDelete.GetFiles())
                            {
                                if (file.LastWriteTime.Date < expiryDate.Date)
                                {
                                    writeLogInfo("Deleting: " + file.Name);
                                    count++;
                                    File.Delete(Path.Combine(destinationPath.ToString(), file.Name));
                                }
                            }

                            if (count == 0)
                            {
                                writeLogInfo("No files outside of retention period.");
                            }
                        }
                        catch (Exception ex)
                        {
                            writeLogError(ex.ToString());
                        }
                    }
                    else
                    {
                        writeLogInfo("Please select a retention period of ether 0 (infinite) or an interger greater than 0.");
                    }
                }
                catch (Exception ex)
                {
                    writeLogError(ex.ToString());
                }
               
                impersonationContext.Leave();
                writeLogInfo("Reverted impersonation.");
                writeLogInfo("Current operations complete, waiting for next update interval.");
            }
            catch (Exception exc)
            {
                writeLogError(exc.InnerException.ToString());
                return;              
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destinationPath, subdir.Name);
               
                fileCopy(subdir.FullName, temppath, netUser, netPassword);
            }

            return;
        }
                  
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            writeLogInfo("TimedEvent fired.");

            //Get destination settings each time we attempt a file copy, this eliminates the need to restart the service to obtain new settings.
            GetDestinationSettings();
            Settings.Reload();  
            
            //start file copy threadzzz
            //reset counter to 0 to iterate through the settings
            counter = 0;
            try
            {
                foreach (string L in storageDestinationList)
                {
                    var count = counter;

                    //start a new thread file copy for each instance of a destination
                    new Thread(() =>
                    {
                        //writeLogInfo("Starting copy function to:" + storageDestinationList[count]);
                        fileCopy(Settings.SourcePath, storageDestinationList[count], netUserList[count], netPassList[count]);                        
                    }).Start();

                    counter++;
                } 
            }
            catch (Exception exc)
            {
                writeLogError(exc.InnerException.ToString());
            }
        }
               
        protected override void OnStop()
        {
           //System.Environment.Exit(0);
        }
    }
}


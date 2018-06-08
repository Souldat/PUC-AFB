using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;


namespace PUC_AFB_Config
{
    public partial class PUCAFBConfig : Form
    {
        string currentDir = Environment.CurrentDirectory;
        List<string> storageDestinationList = new List<string>();
        List<string> netUserList = new List<string>();
        List<string> netPassList = new List<string>();
        string line;
        string destination;
        string netUser;
        string netPass;
        string sourcePath;
        string retentionPeriod;
        string updateInterval;
        string retentionBuffer;
        int counter = 0;

        public PUCAFBConfig()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            statusLabel.Visible = false;

            GetDestinationSettings();
            GetRetentionSettings();
            PopulateInputs();
        }

        public void PopulateInputs()
        {
            destinationTxtBox.Text = destination;
            userNameTxtBox.Text = netUser;
            passwordTxtBox.Text = netPass;

            sourceTxtBox.Text = sourcePath;
            updateIntervalTxtBox.Text = updateInterval;
        }

        public void GetDestinationSettings()
        {
            //Get Destination Configuration Settings 

            try
            {
                // Read the file and find all instances of storage destination.
                System.IO.StreamReader file = new System.IO.StreamReader(currentDir + "\\destinationConfiguration.ws");

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
            }
            catch (Exception exc)
            {
                //probrems
            }

        }

        public void GetRetentionSettings()
        {
            XmlDocument doc = null;
            List<string> retentionSettings = new List<string>();

            try
            {
                doc = new XmlDocument();
                doc.Load(currentDir + "\\PUC_AFB.exe.config");

            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }

            XmlNodeList source = doc.GetElementsByTagName("value");

            for (int i = 0; i < source.Count; i++)
            {
                retentionSettings.Add(source[i].InnerXml);
            }

            sourcePath = retentionSettings[0];
            retentionPeriod = retentionSettings[1];
            updateInterval = retentionSettings[2];
            retentionBuffer = retentionSettings[3];

        }

        private void setDestinationBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                destinationTxtBox.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }

        public void SaveSettings()
        {
            destination = folderBrowserDialog1.SelectedPath.ToString();

            XmlDocument doc = null;
            List<string> retentionSettings = new List<string>();

            try
            {
                doc = new XmlDocument();
                doc.Load(currentDir + "\\PUC_AFB.exe.config");

            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }

            XmlNodeList source = doc.GetElementsByTagName("value");

            if (source.Count < 1)
            {
                MessageBox.Show("There appears to be an XML malformation, unable to load settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (sourceTxtBox.Text != sourcePath)
                {
                    source[0].InnerXml = sourceTxtBox.Text;
                }

                if (updateIntervalTxtBox.Text != updateInterval)
                {
                    source[2].InnerXml = updateIntervalTxtBox.Text;
                }

                try
                {
                    doc.Save(currentDir + "\\PUC_AFB.exe.config");
                }
                catch (Exception e)
                {
                    MessageBox.Show("There's been an issue saving the new settings, please verify the file exists and is accessable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            try
            {
                doc.Load(currentDir + "\\destinationConfiguration.ws");
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }

            try
            {
                doc.SelectSingleNode("//setting/value").InnerText = destinationTxtBox.Text;
                doc.SelectSingleNode("//setting/user").InnerText = userNameTxtBox.Text;
                doc.SelectSingleNode("//setting/password").InnerText = passwordTxtBox.Text;
            }
            catch (Exception e)
            {
                MessageBox.Show("There appears to be an XML malformation, unable to save settings file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            try
            {
                doc.Save(currentDir + "\\destinationConfiguration.ws");
            }
            catch (Exception e)
            {
                MessageBox.Show("There's been an issue saving the new settings, please verify the file exists and is accessable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            statusLabel.Visible = true;
            statusLabel.Text = "Save Success!";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void setSourceBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                sourceTxtBox.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }

    }
}

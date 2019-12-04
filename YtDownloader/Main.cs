using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YtDownloader
{
    public partial class Main : Form
    {
        public static Data.Client client;
        public Main()
        {
            InitializeComponent();
            client = new Data.Client();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog downloadFolder = new FolderBrowserDialog();
            downloadFolder.ShowNewFolderButton = true;
            DialogResult resultado = downloadFolder.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                string path = Directory.GetCurrentDirectory() + @"\downloadfolder.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(downloadFolder.SelectedPath);
                        client.hasPath = true;
                        client.path = downloadFolder.SelectedPath + @"\";
                        MessageBox.Show("Changes done succesfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else 
                {
                    File.WriteAllText(path, downloadFolder.SelectedPath);
                    client.hasPath = true;
                    client.path = downloadFolder.SelectedPath + @"\";
                    MessageBox.Show("Changes done succesfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else MessageBox.Show("You must choose a valid folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client.hasPath)
            {
                if (textBox1.Text != "")
                {
                    if (radioButton1.Checked || radioButton2.Checked)
                    {
                        if (radioButton1.Checked)
                        {
                            button2.Text = "Downloading...";
                            try
                            {
                                var source = client.path;
                                var youtube = YouTube.Default;
                                var vid = youtube.GetVideo(textBox1.Text);
                                File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

                                var inputFile = new MediaFile { Filename = source + vid.FullName };
                                var outputFile = new MediaFile { Filename =  $"{source + vid.FullName}.mp3" };

                                using (var engine = new Engine())
                                {
                                    engine.GetMetadata(inputFile);

                                    engine.Convert(inputFile, outputFile);

                                    File.Delete($"{source + vid.FullName}");

                                    MessageBox.Show($"Video downloaded successfull\n{source + vid.FullName}.mp3", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    button2.Text = "Download";
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("The link format was not correct or the video has comercial content\n" + client.path + "\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                button2.Text = "Download";
                            }
                        }

                        if (radioButton2.Checked)
                        {
                            button2.Text = "Downloading...";
                            try
                            {
                                var source = client.path;
                                var youtube = YouTube.Default;
                                var vid = youtube.GetVideo(textBox1.Text);
                                File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

                                MessageBox.Show($"Video downloaded successfull\n{source + vid.FullName}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                button2.Text = "Download";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("The link format was not correct or the video has comercial content\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                button2.Text = "Download";
                            }
                        }
                    }
                    else MessageBox.Show("You have to choose a format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("You have to enter a valid link", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("You have to choose a destination folder first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FolderBrowserDialog downloadFolder = new FolderBrowserDialog();
                downloadFolder.ShowNewFolderButton = true;
                DialogResult resultado = downloadFolder.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    string path = Directory.GetCurrentDirectory() + @"\downloadfolder.txt";
                    if (!File.Exists(path))
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(downloadFolder.SelectedPath);
                            client.hasPath = true;
                            client.path = downloadFolder.SelectedPath + @"\";
                            MessageBox.Show("Changes done succesfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        File.WriteAllText(path, downloadFolder.SelectedPath);
                        client.hasPath = true;
                        client.path = downloadFolder.SelectedPath + @"\";
                        MessageBox.Show("Changes done succesfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else MessageBox.Show("You have to choose a valid folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

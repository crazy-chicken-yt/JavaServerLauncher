using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace JavaServerLauncher
{
    public partial class Form1 : Form
    {
        JsonNode? manifest;
        JsonArray? versions;
        string javaPath = @"C:\Program Files\Eclipse Adoptium\jre-21.0.5.11-hotspot\bin\java.exe";

        string GetUrlFromID(string TargetID, JsonArray VersionList)
        {
            string URL = "";

            foreach (var version in VersionList)
            {
                if (version?["id"]?.ToString() == TargetID)
                {
                    URL = version["url"].ToString();
                }
            }
            return URL;
        }
        string GetVersionTypeFromID(string TargetID, JsonArray VersionList)
        {
            string type = "";

            foreach (var version in VersionList)
            {
                if (version?["id"]?.ToString() == TargetID)
                {
                    type = version["type"].ToString();

                    DateTime releaseDate = DateTime.Parse(version["releaseTime"].ToString());

                    if (releaseDate.Month == 4 && releaseDate.Day == 1)
                    {
                        type = "snapshot (april fools)";
                    }
                }
            }
            return type;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
        ErrorRetry:
            try
            {
                HttpClient client = new HttpClient();
                string manifestJSON = await client.GetStringAsync("https://piston-meta.mojang.com/mc/game/version_manifest_v2.json");

                manifest = JsonNode.Parse(manifestJSON);
                versions = manifest?["versions"]?.AsArray();

                foreach (var version in versions)
                {
                    string? id = version?["id"]?.ToString();
                    string? type = version?["type"]?.ToString();

                    comboBoxVersions.Items.Add($"{id}");
                }

                comboBoxVersions.SelectedIndex = 0; // Select latest version by default

                string SelectedVersion = comboBoxVersions.SelectedItem.ToString();
                string jarDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\JavaServerLauncher\\versions\\{SelectedVersion}";
                string jarPath = Path.Combine(jarDirectory, "server.jar");

                if (File.Exists(jarPath))
                {
                    button1.Enabled = false;
                    button2.Enabled = true;
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = false;
                }

                label1.Text = $"Type: {GetVersionTypeFromID(comboBoxVersions.SelectedItem.ToString(), versions)}";
            }
            catch (Exception ex)
            {
                DialogResult Response = MessageBox.Show(ex.Message, "Failed to fetch version manifest:", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                switch (Response)
                {
                    case DialogResult.Cancel:
                        this.Close();
                        break;
                    case DialogResult.Retry:
                        goto ErrorRetry;

                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string SelectedVersion = comboBoxVersions.SelectedItem.ToString();
            string VersionJsonUrl = GetUrlFromID(SelectedVersion, versions);


            string downloadDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\JavaServerLauncher\\versions\\{SelectedVersion}";
            string downloadPath = Path.Combine(downloadDirectory, "server.jar");
            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }
            if (File.Exists(downloadPath))
            {
                DialogResult result = MessageBox.Show("It appears this version is already installed. Overwrite the existing server.jar file?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            HttpClient client = new HttpClient();
            string VersionJSON = await client.GetStringAsync(VersionJsonUrl);
            JsonNode versionManifest = JsonNode.Parse(VersionJSON);

            string? JarDownloadUrl = versionManifest?["downloads"]?["server"]?["url"]?.ToString();

            async Task DownloadServerJarAsync(string jarUrl, string destinationPath)
            {
                MessageBox.Show(jarUrl, "debug");
                HttpClient client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync(jarUrl);
                response.EnsureSuccessStatusCode();

                await using FileStream fs = new FileStream(destinationPath, FileMode.Create);
                await response.Content.CopyToAsync(fs);
            }

            await DownloadServerJarAsync(JarDownloadUrl, downloadPath);

            string jarDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\JavaServerLauncher\\versions\\{SelectedVersion}";
            string jarPath = Path.Combine(jarDirectory, "server.jar");

            MessageBox.Show("Download Complete.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (File.Exists(jarPath))
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void comboBoxVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedVersion = comboBoxVersions.SelectedItem.ToString();
            string type = GetVersionTypeFromID(SelectedVersion, versions);
            label1.Text = $"Type: {type}";

            string jarDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\JavaServerLauncher\\versions\\{SelectedVersion}";
            string jarPath = Path.Combine(jarDirectory, "server.jar");

            if (File.Exists(jarPath))
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SelectedVersion = comboBoxVersions.SelectedItem.ToString();
            string jarDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\JavaServerLauncher\\versions\\{SelectedVersion}";
            string jarPath = Path.Combine(jarDirectory, "server.jar");
            string eulaPath = Path.Combine(jarDirectory, "eula.txt");
            string logPath = Path.Combine(jarDirectory, "server.log");

            if (!File.Exists(eulaPath))
            {
                DialogResult eulaDialog = MessageBox.Show("Do you agree to the Minecraft EULA?\n(this is a requirement to run a server)", "EULA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (eulaDialog == DialogResult.Yes)
                    File.WriteAllText(eulaPath, "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://aka.ms/MinecraftEULA).\r\n#Fri Apr 18 17:05:18 EDT 2025\r\neula=true\r\n");
                else
                    return;
            }

            int ramAmount = (int)numericUpDown1.Value;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = javaPath,
                Arguments = $"-Xmx{ramAmount}M -Xms{ramAmount}M -jar \"{jarPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = jarDirectory
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            StreamWriter logWriter = new StreamWriter(logPath, append: true);

            process.OutputDataReceived += (s, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    logWriter.WriteLine(args.Data);
                    logWriter.Flush();
                }
            };

            process.ErrorDataReceived += (s, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    logWriter.WriteLine("[ERROR] " + args.Data);
                    logWriter.Flush();
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            MessageBox.Show("Server has closed.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = @"C:\Program Files\Eclipse Adoptium\";
            openFileDialog.Filter = "jar files (*.jar)|*.jar";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                javaPath = openFileDialog.FileName;
            }

            label3.Text = $"Java Path: {javaPath}";
        }
    }
}

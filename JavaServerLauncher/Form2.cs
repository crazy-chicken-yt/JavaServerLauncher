using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaServerLauncher
{
    public partial class Form2 : Form
    {
        private string minecraftVersion;
        private string pluginDirectory;
        public Form2(string version, string pluginDir)
        {
            InitializeComponent();
            minecraftVersion = version;
            pluginDirectory = pluginDir;
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            await LoadPlugins("");
        }

        private async Task LoadPlugins(string query)
        {
            listBoxPlugins.Items.Clear();
            labelDescription.Text = "";
            buttonDownload.Enabled = false;

            string facet = $"[[\"project_type:plugin\"],[\"server_side:required\"],[\"versions:{minecraftVersion}\"]]";
            string url = $"https://api.modrinth.com/v2/search?query={Uri.EscapeDataString(query)}&facets={Uri.EscapeDataString(facet)}&limit=100";


            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);
            JsonNode root = JsonNode.Parse(json);
            JsonArray hits = root["hits"].AsArray();

            foreach (var plugin in hits)
            {
                listBoxPlugins.Items.Add(new PluginItem
                {
                    Title = plugin["title"]?.ToString(),
                    Description = plugin["description"]?.ToString(),
                    ProjectId = plugin["project_id"]?.ToString()
                });
            }
        }

        private async void listBoxPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPlugins.SelectedItem is PluginItem item)
            {
                labelDescription.Text = item.Description;
                buttonDownload.Tag = item;
                buttonDownload.Enabled = true;
            }
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            if (buttonDownload.Tag is PluginItem item)
            {
                string? jarUrl = await GetLatestJarUrl(item.ProjectId);
                if (jarUrl != null)
                {
                    Directory.CreateDirectory(pluginDirectory);

                    string filePath = Path.Combine(pluginDirectory, Path.GetFileName(jarUrl));

                    HttpClient client = new HttpClient();
                    byte[] jar = await client.GetByteArrayAsync(jarUrl);
                    await File.WriteAllBytesAsync(filePath, jar);

                    MessageBox.Show("Plugin downloaded successfully!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Could not find a .jar for this plugin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<string?> GetLatestJarUrl(string projectId)
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"https://api.modrinth.com/v2/project/{projectId}/version");

            JsonArray versions = JsonNode.Parse(json).AsArray();

            foreach (var version in versions)
            {
                JsonArray? files = version["files"]?.AsArray();
                foreach (var file in files)
                {
                    string? url = file["url"]?.ToString();
                    if (url?.EndsWith(".jar") == true)
                        return url;
                }
            }
            return null;
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            await LoadPlugins(textBoxSearch.Text);
        }

        private class PluginItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ProjectId { get; set; }

            public override string ToString() => Title;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DristLauncher4
{
    public class Manifest
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("files")]
        public List<string> Files { get; set; }

        [JsonPropertyName("files")]
        public List<string> folders { get; set; }

        [JsonPropertyName("files")]
        public Dictionary<string, string> file_hashes { get; set; }
    }
    public class JsonPortIpD
    {
        public string defaultIp { get; set; }
        public int defaultPort { get; set; }
        public string type { get; set; }
        public string serverListDir { get; set; }
        public string serverListFile { get; set; }
        public string manifestListDir { get; set; }
        public string manifestListFile { get; set; }


    }
    public class ServerInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("modloader")]
        public string Modloader { get; set; }

        [JsonPropertyName("modloaderVersion")]
        public string ModloaderVersion { get; set; }

        [JsonPropertyName("javaVersion")]
        public string JavaVersion { get; set; }

        [JsonPropertyName("serverversion")]
        public int Serverversion { get; set; }

        [JsonPropertyName("files_url")]
        public string FilesUrl { get; set; }
    }
}

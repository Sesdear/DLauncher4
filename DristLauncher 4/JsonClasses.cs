using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DristLauncher_4
{
    public class GithubInfo
    {
        public string DefaultIp { get; set; }
        public int DefaultPort { get; set; }
        public string TypeOfProtocol { get; set; }
        public string ServersListFile { get; set; }
    }

    public class ServerIndexes
    {
        public string Server1 { get; set; }
        public string Server2 { get; set; }
        public string Server3 { get; set; }
        public string Server4 { get; set; }
    }
    
    
    public class ServerInfo
    {
        public string ServerName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MVersion { get; set; }
        public string MModLoader { get; set; }
        public string MModLoaderVersion { get; set; }
        public string ModPackSize { get; set; }
        public string ModPackVersion { get; set; }
        public string ModPackFiles { get; set; }

    }

    public class ModsManifest
    {
        public int version { get; set; }
        public List<string> mods { get; set; }
    }

    public class FilesManifest
    {
        public int version { get; set; }
        public List<string> files_hashes { get; set; }
    }

}

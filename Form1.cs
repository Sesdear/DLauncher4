using DLauncher4Rebuild;
using System.Windows.Forms;

namespace DristLauncher4
{
    public partial class exectServers : Form
    {
        int PW;

        bool hide;

        public exectServers()
        {
            InitializeComponent();
            setCurrentNickname();


            PW = 320;
            hide = true;


        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void setCurrentNickname()
        {
            MinecraftUser minecraftUser = new MinecraftUser();
            string nickname = minecraftUser.username;
            usernameBox.Text = nickname;
        }
        //ItemCheck event handler for your checkedListBox1


        private void settingsButton_Click(object sender, EventArgs e)
        {
            tmrSettingsPanel.Start();
        }

        private void tmrSettingsPanel_Tick(object sender, EventArgs e)
        {
            if (hide)
            {
                settingsPanel.Width = settingsPanel.Width + 20;
                if (settingsPanel.Width >= PW)
                {
                    tmrSettingsPanel.Stop();
                    hide = false;
                    this.Refresh();
                }
            }
            else
            {
                settingsPanel.Width = settingsPanel.Width - 20;
                if (settingsPanel.Width <= 0)
                {
                    tmrSettingsPanel.Stop();
                    hide = true;
                    this.Refresh();
                }
            }
        }

        private void serverCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            /*
            MinecraftMethods minecraftMethods = new MinecraftMethods();
            
           
            string nickname = usernameBox.Text;
            if (minecraftMethods.checkNickname(nickname))
            {
                minecraftMethods.saveNickname(nickname);
                minecraftMethods.generateCrackedUUID();
                minecraftMethods.InstallAndLaunchMinecraft("test1", "1.20.4");
            }
            */
            
            
            
        }
    }
}

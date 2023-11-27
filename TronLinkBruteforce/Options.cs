using CommandLine;

namespace TronLinkBruteforce
{
    public class Options
    {
        [Option('c', "chrome-path", Required = true, HelpText = "Set path to chrome executable.")]
        public string ChromePath { get; set; }
        
        [Option('u', "user-data-dir", Required = true, HelpText = "Set path to chrome user data dir.")]
        public string UserDataDir { get; set; }

        [Option('p', "passwords-path", Required = true, HelpText = "Set path to file with passwords, delimited with new line.")]
        public string PasswordsPath { get; set; }

        [Option('d', "delay", Required = true, HelpText = "Set delay between actions (text input, click, etc).")]
        public int Delay { get; set; }

        [Option("backspace-delay", Required = true, HelpText = "Set delay between backspace clicks.")]
        public int BackspaceDelay { get; set; }
        
        [Option("use-ctrl-a", Required = false, HelpText = "Instructs to use Ctrl + A + Backspace to delete input content.")]
        public bool UseCtrlA { get; set; }
        
        [Option("kill", Required = false, HelpText = "Instructs to kill all chrome processes before start.")]
        public bool KillAllChromes { get; set; }

    }
}
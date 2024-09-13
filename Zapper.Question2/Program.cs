// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, Zapper! This is the answer to Question 2.1 and 2.2");

//allow the user to use settings untill they close the program
while (true)
{
    try
    {
        Console.WriteLine("Read settings from file?: Y/N");
        var response = Console.ReadLine();

        //settings as 00000001
        var settings = "";

        //path of file to write to disk
        var filePath = "../../../settings.txt";

        //response must be y or n
        if (string.IsNullOrWhiteSpace(response) || response.Length > 1
                        || (response.ToLowerInvariant() != "y" && response.ToLowerInvariant() != "n"))
            Console.WriteLine("Invalid Response: Y/N");
        else
        {
            //user selected yes, read from file
            if(response.ToLowerInvariant() == "y")
            {
                var settingsArr = File.ReadAllLines(filePath);

                //read settings from first line
                if(settingsArr.Length > 0)
                {
                    settings = settingsArr[0];
                    Console.WriteLine($"Settings are: {settings}");
                }
                //file is empty
                else
                {
                    Console.WriteLine("Settings file is empty");
                }
            }
        }
        //user selected not to read from file so ask them for settings
        if (string.IsNullOrEmpty(settings))
        {
            Console.WriteLine("Please enter user settings:");
            settings = Console.ReadLine();
        }
        //settings must be 8 characters
        if (string.IsNullOrWhiteSpace(settings) || settings.Length < 8 || settings.Length > 8)
            Console.WriteLine("Invalid settings...");
        else
        { 
            Console.WriteLine("Please enter setting position:");
            var setting = Console.ReadLine();

            //setting position must be 1-8
            if (!int.TryParse(setting, out var intSetting) || setting.Length > 1)
                Console.WriteLine("Setting must be a single number");
            else
            {
                if (intSetting > 8) Console.WriteLine("Settting must be between 1 and 8");
                else
                {
                    //get selectedc setting by index
                    var userSetting = settings[intSetting - 1];

                    //is it enabled?
                    var enabled = Convert.ToBoolean(int.Parse(userSetting.ToString()));
                    Console.WriteLine($"Enabled: {enabled.ToString()}");
                    Console.WriteLine("Do you want to write these settings to disk? Y/N");
                    response = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(response) || response.Length > 1
                        || (response.ToLowerInvariant() != "y" && response.ToLowerInvariant() != "n"))
                        Console.WriteLine("Invalid Response: Y/N");
                    //write settings to disk
                    else
                    {
                        if (response.ToLowerInvariant() == "y")
                        {
                            await File.AppendAllTextAsync(filePath, settings);
                            Console.WriteLine("Settings written to settings.txt in root rirectory of project");
                        }
                    }

                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error has occured, please try again...");
    }
}


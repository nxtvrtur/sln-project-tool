using System.Diagnostics;
using labCreator;

Console.Write("№ of lab: ");
var n = int.Parse(Console.ReadLine() ?? string.Empty);
Console.Write("count of projects: ");
var count = int.Parse(Console.ReadLine() ?? string.Empty);

var labFolderPath = $"{Secret.Path}{n}";

Process.Start(new ProcessStartInfo
{
    FileName = "cmd.exe", Arguments = $"/c mkdir {labFolderPath}", CreateNoWindow = true
});

Process.Start(new ProcessStartInfo
{
    FileName = "cmd.exe", Arguments = $"/C cd {labFolderPath} & dotnet new solution --name lab{n}"
    , UseShellExecute = false, CreateNoWindow = true
})?.WaitForExit();

Console.Write("loading...");
for (var i = 1; i < count + 1; i++)
{
    Process.Start(new ProcessStartInfo
    {
        FileName = "cmd.exe", Arguments = $"/C cd {labFolderPath} & dotnet new console --name task{i}"
        , UseShellExecute = false, CreateNoWindow = true
    })?.WaitForExit();
    Process.Start(new ProcessStartInfo
    {
        FileName = "cmd.exe"
        , Arguments = $"/C cd {labFolderPath} & dotnet sln add \"{labFolderPath}\\task{i}\\task{i}.csproj\""
        , UseShellExecute = false, CreateNoWindow = true
    })?.WaitForExit();

}

Console.WriteLine("done!");
await Task.Delay(TimeSpan.FromSeconds(1));
Console.WriteLine("rider is running...");
Process.Start(Secret.RiderPath
    , labFolderPath + @"\lab" + n + ".sln");
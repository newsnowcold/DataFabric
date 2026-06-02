using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var solutionRoot = FindSolutionRoot();
        if (solutionRoot == null)
        {
            Console.Error.WriteLine("Could not locate DataFabric.sln in parent directories.");
            return 1;
        }

        var apiProj = Path.Combine(solutionRoot, "DataFabric.Api", "DataFabric.Api.csproj");
        var clientDir = Path.Combine(solutionRoot, "datafabric.client");

        Console.WriteLine($"Solution root: {solutionRoot}");

        var apiProc = StartProcess("dotnet", $"run --project \"{apiProj}\"", solutionRoot, "API");

        var npmFile = Environment.OSVersion.Platform == PlatformID.Win32NT ? "npm.cmd" : "npm";
        var clientProc = StartProcess(npmFile, "start", clientDir, "Client");

        Console.CancelKeyPress += (s, e) => {
            try { if (!apiProc.HasExited) apiProc.Kill(true); } catch {}
            try { if (!clientProc.HasExited) clientProc.Kill(true); } catch {}
        };

        await Task.WhenAny(WaitForExitAsync(apiProc), WaitForExitAsync(clientProc));
        return 0;
    }

    static Process StartProcess(string fileName, string arguments, string workingDirectory, string name)
    {
        var psi = new ProcessStartInfo(fileName, arguments)
        {
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false,
        };

        var p = new Process { StartInfo = psi, EnableRaisingEvents = true };

        p.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine($"[{name}] {e.Data}"); };
        p.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.Error.WriteLine($"[{name} ERR] {e.Data}"); };

        try
        {
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start {name}: {ex.Message}");
        }

        return p;
    }

    static Task WaitForExitAsync(Process p)
    {
        var tcs = new TaskCompletionSource<object?>();
        p.Exited += (s, e) => tcs.TrySetResult(null);
        if (p.HasExited) tcs.TrySetResult(null);
        return tcs.Task;
    }

    static string? FindSolutionRoot()
    {
        var dir = Directory.GetCurrentDirectory();
        for (int i = 0; i < 10 && dir != null; i++)
        {
            if (File.Exists(Path.Combine(dir, "DataFabric.sln"))) return dir;
            var parent = Directory.GetParent(dir);
            dir = parent?.FullName;
        }
        return null;
    }
}

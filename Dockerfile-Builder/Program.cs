// See https://aka.ms/new-console-template for more information
class DockerfileBuilder
{
    private static readonly int[] JavaVersions = [8, 11, 17, 21, 23];

    private static readonly int[] NodeVersions = [18, 20, 22, 23];

    private static readonly string[] PythonVersions = ["2", "3", "3.9", "3.10", "3.11", "3.12", "3.13"];

    static void Main()
    {
        string lang;

        Console.WriteLine("Enter Application Language:");
        lang = Console.ReadLine().ToLower();

        switch (lang)
        {
            case "node":
            case "nodejs":
            case "node.js":
                int nodeVersion = GetNodeVersion();
                GenerateNodeDockerfile(nodeVersion);
                break;
            case "java":
                int javaVersion = GetJavaVersion();
                string javaAppName = GetJavaAppName();
                GenerateJavaDockerfile(javaVersion, javaAppName);
                break;
            case "python":
                string pythonVersion = GetPythonVersion();
                string pythonAppName = GetPythonAppName();
                GeneratePythonDockerfile(pythonVersion, pythonAppName);
                break;
            case "c#":
            case "csharp":
                string cSharpAppName = GetCSharpAppName();
                GenerateCSharpDockerfile(cSharpAppName);
                break;
            default:
                Console.WriteLine("Please enter a valid Application Language.");
                break;
        }
    }

    private static string GenerateVersionsString(int[] versions)
    {
        string versionsString = "";

        for (int i = 0; i < versions.Length - 1; i++)
        {
            versionsString  = versionsString + versions[i] + " / ";
        }
        versionsString += versions[^1];
        
        return versionsString;
    }

    private static string GenerateVersionsString(string[] versions)
    {
        string versionsString = "";

        for (int i = 0; i < versions.Length - 1; i++)
        {
            versionsString  = versionsString + versions[i] + " / ";
        }
        versionsString += versions[^1];

        return versionsString;
    }

    private static string GetCSharpAppName()
    {
        string appName = "";
        while (appName.Length < 1)
        {
            Console.WriteLine("Enter C# Application DLL File Name:");
            appName = Console.ReadLine();

            if (appName.EndsWith(".dll"))
            {
                appName = appName.Substring(0, appName.Length - 4);
            }
        }

        return appName;
    }

    private static string GetPythonVersion()
    {
        string version = "";
        bool validVersionInput = false;
        while (!validVersionInput)
        {
            Console.WriteLine($"Enter Java Version ({GenerateVersionsString(PythonVersions)}):");
            version = Console.ReadLine();

            if (PythonVersions.Contains(version))
            {
                validVersionInput = true;
            }
        }

        return version;
    }

    private static string GetPythonAppName()
    {
        string appName = "";
        while (appName.Length < 1)
        {
            Console.WriteLine("Enter Python Daemon or Script File Name:");
            appName = Console.ReadLine();

            if (appName.EndsWith(".py"))
            {
                appName = appName.Substring(0, appName.Length - 3);
            }
        }

        return appName;
    }

    private static int GetJavaVersion()
    {
        int version = 0;
        bool validVersionInput = false;
        while (!validVersionInput)
        {
            Console.WriteLine($"Enter Java Version ({GenerateVersionsString(JavaVersions)}):");
            version = int.Parse(Console.ReadLine());

            if (JavaVersions.Contains(version))
            {
                validVersionInput = true;
            }
        }

        return version;
    }

    private static string GetJavaAppName()
    {
        string appName = "";
        while (appName.Length < 1)
        {
            Console.WriteLine("Enter Application Jar File Name:");
            appName = Console.ReadLine();

            if (appName.EndsWith(".jar"))
            {
                appName = appName.Substring(0, appName.Length - 4);
            }
        }

        return appName;
    }

    private static int GetNodeVersion()
    {
        int version = 0;
        bool validVersionInput = false;
        while (!validVersionInput)
        {
            Console.WriteLine($"Enter Node Version ({GenerateVersionsString(NodeVersions)}):");
            version = int.Parse(Console.ReadLine());

            if (NodeVersions.Contains(version))
            {
                validVersionInput = true;
            }
        }

        return version;
    }

    private static void GenerateNodeDockerfile(int version)
    {
        Console.WriteLine("Creating Dockerfile...");
        Console.WriteLine("======================");

        Console.WriteLine($"FROM node:{version}");
        Console.WriteLine("RUN mkdir -p /opt/app");
        Console.WriteLine("WORKDIR /opt/app");
        Console.WriteLine("COPY src/package.json src/package-lock.json .");
        Console.WriteLine("RUN npm install");
        Console.WriteLine("COPY src/ .");
        Console.WriteLine("EXPOSE 3000");
        Console.WriteLine("CMD [ \"npm\", \"start\"]");

        Console.WriteLine("======================");
        Console.WriteLine("Finished creating Dockerfile.");
    }

    private static void GenerateJavaDockerfile(int version, string appName)
    {
        Console.WriteLine("Creating Dockerfile...");
        Console.WriteLine("======================");

        Console.WriteLine($"FROM amazoncorretto:{version}");
        Console.WriteLine($"COPY target/{appName}.jar app.jar");
        Console.WriteLine("EXPOSE 8080");
        Console.WriteLine("ENTRYPOINT [\"java\",\"-jar\",\"/app.jar\"]");

        Console.WriteLine("======================");
        Console.WriteLine("Finished creating Dockerfile.");
    }

    private static void GeneratePythonDockerfile(string version, string appName)
    {
        Console.WriteLine("Creating Dockerfile...");
        Console.WriteLine("======================");

        Console.WriteLine($"FROM python:{version}");
        Console.WriteLine($"WORKDIR /usr/src/app");
        Console.WriteLine("COPY requirements.txt ./");
        Console.WriteLine("RUN pip install --no-cache-dir -r requirements.txt");
        Console.WriteLine("COPY . .");
        Console.WriteLine($"ENTRYPOINT [\"python\",\"./{appName}.py\"]");

        Console.WriteLine("======================");
        Console.WriteLine("Finished creating Dockerfile.");
    }

    private static void GenerateCSharpDockerfile(string appName)
    {
        Console.WriteLine("Creating Dockerfile...");
        Console.WriteLine("======================");

        Console.WriteLine("FROM mcr.microsoft.com/dotnet/sdk:8.0");
        Console.WriteLine("WORKDIR /app");
        Console.WriteLine("COPY . ./");
        Console.WriteLine("RUN dotnet restore");
        Console.WriteLine("RUN dotnet publish -c Release -o release");
        Console.WriteLine("WORKDIR /app/release");
        Console.WriteLine($"ENTRYPOINT [\"dotnet\",\"{appName}.dll\"]");

        Console.WriteLine("======================");
        Console.WriteLine("Finished creating Dockerfile.");
    }
}

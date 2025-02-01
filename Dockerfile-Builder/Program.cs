﻿// See https://aka.ms/new-console-template for more information
class DockerfileBuilder
{
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
            default:
                Console.WriteLine("Please enter a valid Application Language.");
                break;
        }
    }

    private static int GetJavaVersion()
    {
        int version = 0;
        bool validVersionInput = false;
        while (!validVersionInput)
        {
            Console.WriteLine("Enter Java Version (8 / 11 / 17 / 21 / 23):");
            version = int.Parse(Console.ReadLine());

            if (version is 8 or 11 or 17 or 21 or 23)
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
            Console.WriteLine("Enter Node Version (18 / 20 / 22 / 23):");
            version = int.Parse(Console.ReadLine());

            if (version is 18 or 20 or 22 or 23)
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

    private static void GenerateJavaDockerfile(int version, string app_name)
    {
        Console.WriteLine("Creating Dockerfile...");
        Console.WriteLine("======================");

        Console.WriteLine($"FROM amazoncorretto:{version}");
        Console.WriteLine($"COPY target/{app_name}.jar app.jar");
        Console.WriteLine("EXPOSE 8080");
        Console.WriteLine("ENTRYPOINT [\"java\",\"-jar\",\"/app.jar\"]");

        Console.WriteLine("======================");
        Console.WriteLine("Finished creating Dockerfile.");
    }
}

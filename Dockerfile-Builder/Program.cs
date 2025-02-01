// See https://aka.ms/new-console-template for more information
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
                int version = GetNodeVersion();
                GenerateNodeDockerfile(version);
                break;
            default:
                Console.WriteLine("Please enter a valid Application Language.");
                break;
        }
    }

    private static int GetNodeVersion()
    {
        Console.WriteLine("Enter Node Version (18 / 20 / 22 / 23):");
        string version = Console.ReadLine();
        return int.Parse(version);
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
}

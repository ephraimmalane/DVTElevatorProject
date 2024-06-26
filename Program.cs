using ElevatorSystem.Core.Models;

class Program
{
    static void Main(string[] args)
    {
        var elevatorSystem = new ElevatorSystems(3, 20);
        var uiManager = new UserInterfaceManager(elevatorSystem);

        uiManager.RunAsync().GetAwaiter().GetResult();
    }
}

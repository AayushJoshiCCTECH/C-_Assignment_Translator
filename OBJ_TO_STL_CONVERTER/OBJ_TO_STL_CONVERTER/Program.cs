using OBJ_TO_STL_CONVERTER.Functions;
using OBJ_TO_STL_CONVERTER.Storage;

namespace OBJ_TO_STL_CONVERTER
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Clear();


            string inputFilePath = @"cube.obj";
            Triangulation triangulationObject = new();

            _ = new
            ObjReader();
            ObjReader.Read(inputFilePath, triangulationObject);

            string outputFilePath = @"cube.stl";

            _ = new
            StlWriter();
            StlWriter.Write(outputFilePath, triangulationObject);

            Console.WriteLine("-----------------------------------------");
        }
    }
}

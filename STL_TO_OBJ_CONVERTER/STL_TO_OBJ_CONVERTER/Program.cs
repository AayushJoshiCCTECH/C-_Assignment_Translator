using STL_TO_OBJ_CONVERTER.Functions;
using STL_TO_OBJ_CONVERTER.Storage;

namespace STL_TO_OBJ_CONVERTER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            // Path to the input STL file
            string filePath = "cube.stl";

            // Create triangulation object
            Triangulation triangulationObject = new();


            // Read STL file and populate triangulation data
            _ = new
            // Read STL file and populate triangulation data
            StlReader();
            StlReader.Read(filePath, triangulationObject);

            // Path to the output OBJ file
            string output = "OutputFile.obj";


            // Write triangulation data to OBJ file
            _ = new
            // Write triangulation data to OBJ file
            Obj_Writer();
            Obj_Writer.Write(output, triangulationObject);

            Console.WriteLine("-------------------------------------------------");
        }
    }
}

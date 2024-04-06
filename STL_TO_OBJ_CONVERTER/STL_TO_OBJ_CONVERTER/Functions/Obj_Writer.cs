using STL_TO_OBJ_CONVERTER.Storage;

namespace STL_TO_OBJ_CONVERTER.Functions
{
    internal class Obj_Writer
    {

        public Obj_Writer()
        {
        }

        public static void Write(string fileName, Triangulation triangulationObj)
        {
            // Define the output path
            string outputPath = Path.Combine("output", fileName);

            // Create the output directory if it doesn't exist
            string? outputDirectory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Open the output file for writing
            using StreamWriter outFile = new(outputPath);
            // Write each unique point
            foreach (var point in triangulationObj.UniquePoints)
            {
                outFile.WriteLine($"v {point.X} {point.Y} {point.Z}"); // Writing vertex data
            }

            outFile.WriteLine(); // Write an empty line for separation

            // Write each unique normal
            foreach (var normal in triangulationObj.UniqueNormals)
            {
                outFile.WriteLine($"vn {normal.X} {normal.Y} {normal.Z}"); // Writing normal data
            }

            outFile.WriteLine(); // Write an empty line for separation

            // Write each triangle with corresponding indices and normals
            foreach (var triangle in triangulationObj.Triangles)
            {
                outFile.WriteLine($"f {triangle.Index1 + 1}//{triangle.NormalIndex + 1} " +
                                  $"{triangle.Index2 + 1}//{triangle.NormalIndex + 1} " +
                                  $"{triangle.Index3 + 1}//{triangle.NormalIndex + 1}"); // Writing face data
            }

            // Notify completion
            Console.WriteLine("Data written successfully !!!!");
        }
    }

}

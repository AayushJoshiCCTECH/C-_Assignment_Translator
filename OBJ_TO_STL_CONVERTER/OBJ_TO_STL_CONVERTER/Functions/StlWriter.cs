using OBJ_TO_STL_CONVERTER.Storage;

namespace OBJ_TO_STL_CONVERTER.Functions
{
    internal class StlWriter
    {
        public StlWriter()
        {
        }

        public static void Write(string filePath, Triangulation triangulationObj)
        {
            using (StreamWriter outFile = new(filePath))
            {
                List<Point3D> points = triangulationObj.UniquePoints;
                List<Triangle> triangles = triangulationObj.Triangles;
                List<Point3D> normals = triangulationObj.UniqueNormals;

                outFile.WriteLine("solid");

                foreach (Triangle triangle in triangles)
                {
                    outFile.WriteLine($"  facet normal {normals[triangle.NormalIndex].X} {normals[triangle.NormalIndex].Y} {normals[triangle.NormalIndex].Z}");
                    outFile.WriteLine("    outer loop");
                    outFile.WriteLine($"      vertex {points[triangle.Index1].X} {points[triangle.Index1].Y} {points[triangle.Index1].Z}");
                    outFile.WriteLine($"      vertex {points[triangle.Index2].X} {points[triangle.Index2].Y} {points[triangle.Index2].Z}");
                    outFile.WriteLine($"      vertex {points[triangle.Index3].X} {points[triangle.Index3].Y} {points[triangle.Index3].Z}");
                    outFile.WriteLine("    endloop");
                    outFile.WriteLine("  endfacet");
                }

                outFile.WriteLine("endsolid");

                Console.WriteLine("Data Written successfully!!!!");
            }
        }
    }
}

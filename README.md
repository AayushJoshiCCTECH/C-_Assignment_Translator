# C#_Assignment_Translator

## 1. STL to OBJ Converter

This C# codebase provides functionality for converting 3D models from the STL (Stereolithography) format to the OBJ (Wavefront .obj) format. It consists of the following classes:

1. `Obj_Writer`: Responsible for writing OBJ files from a triangulated model.
2. `StlReader`: Responsible for reading STL files and generating triangulated models.
3. `Point3D`: Represents a point in 3D space.
4. `Triangle`: Represents a triangle defined by three indices.
5. `Triangulation`: Handles the storage of unique points, triangles, and normals in a triangulated model.

### Sample code
### Reader Class
```cpp

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
```
### Writer class
```cpp
using STL_TO_OBJ_CONVERTER.Storage;

namespace STL_TO_OBJ_CONVERTER.Functions
{
    internal class StlReader
    {
        public StlReader()
        {
        }
        public static void Read(string filePath, Triangulation triangulationObj)
        {
            using StreamReader file = new(filePath);
            int count = 0;
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            int normalIndex = 0;
            string inputLine;
            Dictionary<Point3D, int> pointIndexMap = new();
            Dictionary<Point3D, int> normalIndexMap = new();

            while ((inputLine = file.ReadLine()) != null)
            {
                if (inputLine.Contains("facet normal"))
                {
                    string[] tokens = inputLine.Split(' ');
                    double x = Convert.ToDouble(tokens[2]);
                    double y = Convert.ToDouble(tokens[3]);
                    double z = Convert.ToDouble(tokens[4]);
                    Point3D facetNormal = new(x, y, z);

                    if (!normalIndexMap.TryGetValue(facetNormal, out normalIndex))
                    {
                        normalIndex = triangulationObj.UniqueNormals.Count;
                        normalIndexMap.Add(facetNormal, normalIndex);
                        triangulationObj.AddUniqueNormal(facetNormal);
                    }
                }

                if (inputLine.Contains("vertex"))
                {
                    string[] tokens = inputLine.Split(' ');
                    double x = Convert.ToDouble(tokens[1]);
                    double y = Convert.ToDouble(tokens[2]);
                    double z = Convert.ToDouble(tokens[3]);
                    Point3D point = new(x, y, z);

                    if (!pointIndexMap.TryGetValue(point, out int index))
                    {
                        index = triangulationObj.UniquePoints.Count;
                        pointIndexMap.Add(point, index);
                        triangulationObj.AddUniquePoint(point);
                    }

                    switch (count)
                    {
                        case 0:
                            index1 = index;
                            count++;
                            break;
                        case 1:
                            index2 = index;
                            count++;
                            break;
                        case 2:
                            index3 = index;
                            count++;
                            break;
                    }

                    if (count == 3)
                    {
                        Triangle triangle = new(index1, index2, index3)
                        {
                            NormalIndex = normalIndex
                        };
                        triangulationObj.AddTriangleTo(triangle);
                        count = 0;
                    }
                }
            }
            Console.WriteLine("Data reading successfully !!!");
        }
    }
}
```

## 2. OBJ to STL Converter

This C# codebase provides functionality for converting 3D models from the STL (Stereolithography) format to the OBJ (Wavefront .obj) format. It consists of the following classes:

1. `Obj_Reader`:Responsible for reading OBJ files and generating triangulated models.
2. `StlWriter`:Responsible for writing STL files from a triangulated model.
3. `Point3D`: Represents a point in 3D space.
4. `Triangle`: Represents a triangle defined by three indices.
5. `Triangulation`: Handles the storage of unique points, triangles, and normals in a triangulated model.

### Sample code
### Reader Class
```cpp
using OBJ_TO_STL_CONVERTER.Storage;

namespace OBJ_TO_STL_CONVERTER.Functions
{
    internal class ObjReader
    {
        public ObjReader()
        {
        }


        public static void Read(string filePath, Triangulation triangulationObj)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] tokens = line.Split(' ');

                    if (tokens[0] == "v") // Vertex
                    {
                        double x = double.Parse(tokens[1]);
                        double y = double.Parse(tokens[2]);
                        double z = double.Parse(tokens[3]);
                        Point3D point = new(x, y, z);
                        triangulationObj.AddUniquePoint(point);
                    }
                    else if (tokens[0] == "vn") // Vertex Normal
                    {
                        double x = double.Parse(tokens[1]);
                        double y = double.Parse(tokens[2]);
                        double z = double.Parse(tokens[3]);
                        Point3D normal = new(x, y, z);
                        triangulationObj.AddUniqueNormal(normal);
                    }
                    else if (tokens[0] == "f") // Face
                    {
                        int vertex1 = int.Parse(tokens[1].Split('/')[0]) - 1;
                        int vertex2 = int.Parse(tokens[2].Split('/')[0]) - 1;
                        int vertex3 = int.Parse(tokens[3].Split('/')[0]) - 1;

                        int normalVertex1 = int.Parse(tokens[1].Split('/')[2]) - 1;

                        _ = int.Parse(tokens[2].Split('/')[2]) - 1;

                        _ = int.Parse(tokens[3].Split('/')[2]) - 1;

                        Triangle triangle = new(vertex1, vertex2, vertex3)
                        {
                            NormalIndex = normalVertex1 // Assuming normal index is per vertex in .obj file
                        };
                        triangulationObj.AddTriangle(triangle);
                    }
                }
            }
            Console.WriteLine("Data read successfully!!!!");
        }
    }
}
```
### Writer class
```cpp
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
```
Clone this repository to your local machine.
```
git clone https://github.com/AayushJoshiCCTECH/C-_Assignment_Translator.git
```

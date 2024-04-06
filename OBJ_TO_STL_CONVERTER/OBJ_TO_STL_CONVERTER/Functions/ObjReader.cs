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

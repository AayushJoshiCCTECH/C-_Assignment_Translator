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

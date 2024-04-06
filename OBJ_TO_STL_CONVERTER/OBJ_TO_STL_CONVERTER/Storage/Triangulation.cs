namespace OBJ_TO_STL_CONVERTER.Storage
{
    public class Triangulation
    {
        public Triangulation()
        {
            UniquePoints = [];
            Triangles = [];
            UniqueNormals = [];
        }

        public List<Point3D> UniquePoints
        {
            get;
        }
        public List<Triangle> Triangles
        {
            get;
        }
        public List<Point3D> UniqueNormals
        {
            get;
        }

        public void AddUniquePoint(Point3D inPoint)
        {
            UniquePoints.Add(inPoint);
        }

        public void AddTriangle(Triangle inTriangle)
        {
            Triangles.Add(inTriangle);
        }

        public void AddUniqueNormal(Point3D inNormal)
        {
            UniqueNormals.Add(inNormal);
        }
    }
}

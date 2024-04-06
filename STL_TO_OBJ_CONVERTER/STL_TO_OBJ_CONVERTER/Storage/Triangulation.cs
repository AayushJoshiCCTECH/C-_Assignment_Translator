
namespace STL_TO_OBJ_CONVERTER.Storage
{
    public class Triangulation
    {
        private List<Point3D> _UniquePoints;
        private List<Triangle> _Triangles;
        private List<Point3D> _UniqueNormals;

        public Triangulation()
        {
            _UniquePoints = [];
            _Triangles = [];
            _UniqueNormals = [];
        }

        public List<Point3D> UniquePoints
        {
            get
            {
                return _UniquePoints;
            }
        }
        public List<Triangle> Triangles
        {
            get
            {
                return _Triangles;
            }
        }
        public List<Point3D> UniqueNormals
        {
            get
            {
                return _UniqueNormals;
            }
        }

        public void AddUniquePoint(Point3D inPoint)
        {
            _UniquePoints.Add(inPoint);
        }

        public void AddTriangleTo(Triangle inTriangle)
        {
            _Triangles.Add(inTriangle);
        }

        public void AddUniqueNormal(Point3D inNormal)
        {
            _UniqueNormals.Add(inNormal);
        }
    }
}

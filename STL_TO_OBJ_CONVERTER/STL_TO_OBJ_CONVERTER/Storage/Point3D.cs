namespace STL_TO_OBJ_CONVERTER.Storage
{
    public class Point3D : IComparable<Point3D>
    {
        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Point3D(double inX, double inY, double inZ)
        {
            X = inX;
            Y = inY;
            Z = inZ;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public int CompareTo(Point3D other)
        {
            if (X != other.X)
                return X.CompareTo(other.X);
            if (Y != other.Y)
                return Y.CompareTo(other.Y);
            return Z.CompareTo(other.Z);
        }
    }
}

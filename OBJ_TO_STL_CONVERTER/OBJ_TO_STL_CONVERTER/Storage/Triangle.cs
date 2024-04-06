namespace OBJ_TO_STL_CONVERTER.Storage
{
    public class Triangle
    {
        private int _Index1;
        private int _Index2;
        private int _Index3;
        private int _NormalIndex;

        public Triangle()
        {
        }

        public Triangle(int inIndex1, int inIndex2, int inIndex3)
        {
            _Index1 = inIndex1;
            _Index2 = inIndex2;
            _Index3 = inIndex3;
        }

        public int Index1
        {
            get
            {
                return _Index1;
            }
            set
            {
                _Index1 = value;
            }
        }
        public int Index2
        {
            get
            {
                return _Index2;
            }
            set
            {
                _Index2 = value;
            }
        }
        public int Index3
        {
            get
            {
                return _Index3;
            }
            set
            {
                _Index3 = value;
            }
        }
        public int NormalIndex
        {
            get
            {
                return _NormalIndex;
            }
            set
            {
                _NormalIndex = value;
            }
        }
    }
}

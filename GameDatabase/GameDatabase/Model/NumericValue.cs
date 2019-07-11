using System;

namespace GameDatabase.Model
{
    public struct NumericValue<T> where T : struct, IComparable
    {
        public NumericValue(T value, T min, T max)
        {
            //This is made to prevent editor overriding values changed using text editors. StructDataEditor still cant go beyond bounds, but external text editors can.
            _value = value;
            //_value = value.CompareTo(max) > 0 ? max : value.CompareTo(min) < 0 ? min : value;
            Max = max;
            Min = min;

            if (Max.CompareTo(Min) < 0)
                throw new ArgumentException();
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value.CompareTo(Min) < 0)
                    _value = Min;
                else if (value.CompareTo(Max) > 0)
                    _value = Max;
                else
                    _value = value;
            }
        }

        public T Max { get; }
        public T Min { get; }

        private T _value;
    }
}

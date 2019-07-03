﻿using System;

namespace ODE.Astronomy
{
    public struct Angle
    {
        private const double degreesToRadians = Math.PI / 180;
        private const double radiansToDegrees = 180 / Math.PI;
        private double _radians;

        // Basic properties
        public double Radians
        {
            get
            {
                return _radians;
            }
            private set
            {
                _radians = value;
            }
        }

        public double Degrees
        {
            get
            {
                return radiansToDegrees * _radians;
            }
            private set
            {
                _radians = degreesToRadians * value;
            }
        }

        // Static properties of type Angle
        public static Angle Zero
        {
            get
            {
                return new Angle();
            }
        }

        public static Angle Right
        {
            get
            {
                return Angle.FromDegrees(90);
            }
        }

        public static Angle Straight
        {
            get
            {
                return Angle.FromDegrees(180);
            }
        }

        public static Angle Circle
        {
            get
            {
                return Angle.FromDegrees(360);
            }
        }

        // Other static creation properties
        public static Angle FromRadians(double radians)
        {
            Angle angle = new Angle();
            angle.Radians = radians;
            return angle;
        }

        public static Angle FromDegrees(double degrees)
        {
            Angle angle = new Angle();
            angle.Degrees = degrees;
            return angle;
        }

        public static Angle FromDegrees(int degrees, double minutes)
        {
            return FromDegrees(degrees + minutes / 60);
        }

        public static Angle FromDegrees(int degrees, int minutes, double seconds)
        {
            return FromDegrees(degrees, minutes + seconds / 60);
        }

        public static Angle FromHours(double hours)
        {
            Angle angle = new Angle();
            angle.Degrees = 15 * hours;
            return angle;
        }

        public static Angle FromHours(int hours, double minutes)
        {
            return FromHours(hours + minutes / 60);
        }

        public static Angle FromHours(int hours, int minutes, double seconds)
        {
            return FromHours(hours, minutes + seconds / 60);
        }

        // Trig functions
        public double Sine
        {
            get
            {
                return Math.Sin(_radians);
            }
            set
            {
                _radians = Math.Asin(value);
            }
        }

        public double Cosine
        {
            get
            {
                return Math.Cos(_radians);
            }
            set
            {
                _radians = Math.Acos(value);
            }
        }

        public double Tangent
        {
            get
            {
                return Math.Tan(_radians);
            }
            set
            {
                _radians = Math.Atan(value);
            }
        }

        public static Angle ArcTangent(double y, double x)
        {
            return Angle.FromRadians(Math.Atan2(y, x));
        }

        // Normalization
        public void NormalizeAroundZero()
        {
            Radians = Radians % 2 * Math.PI;

            while (Radians < -Math.PI)
                Radians += 2 * Math.PI;

            while (Radians > Math.PI)
                Radians -= 2 * Math.PI;
        }

        public void NormalizePositive()
        {
            Radians = Radians % 2 * Math.PI;

            while (Radians < 0)
                Radians += 2 * Math.PI;

            while (Radians > 2 * Math.PI)
                Radians -= 2 * Math.PI;
        }

        // Operators and overrides
        public static Angle operator -(Angle angle)
        {
            return Angle.FromRadians(-angle.Radians);
        }

        public static Angle operator +(Angle ang1, Angle ang2)
        {
            return Angle.FromRadians(ang1.Radians + ang2.Radians);
        }

        public static Angle operator -(Angle ang1, Angle ang2)
        {
            return Angle.FromRadians(ang1.Radians - ang2.Radians);
        }

        public static Angle operator *(Angle angle, double multiplier)
        {
            return Angle.FromRadians(angle.Radians * multiplier);
        }

        public static Angle operator *(double multiplier, Angle angle)
        {
            return Angle.FromRadians(multiplier * angle.Radians);
        }

        public static double operator /(Angle angle1, Angle angle2)
        {
            return angle1.Radians / angle2.Radians;
        }

        public static Angle operator /(Angle angle, double divisor)
        {
            return Angle.FromRadians(angle.Radians / divisor);
        }

        public static bool operator <(Angle angle1, Angle angle2)
        {
            return angle1.Radians < angle2.Radians;
        }

        public static bool operator >(Angle angle1, Angle angle2)
        {
            return angle1.Radians > angle2.Radians;
        }

        public static bool operator <=(Angle angle1, Angle angle2)
        {
            return angle1.Radians <= angle2.Radians;
        }

        public static bool operator >=(Angle angle1, Angle angle2)
        {
            return angle1.Radians >= angle2.Radians;
        }

        public static bool operator ==(Angle angle1, Angle angle2)
        {
            return angle1.Radians == angle2.Radians;
        }

        public static bool operator !=(Angle angle1, Angle angle2)
        {
            return angle1.Radians != angle2.Radians;
        }

        public static explicit operator int(Angle angle)
        {
            return System.Convert.ToInt32(angle.Radians);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Angle))
                return false;

            return Equals((Angle)obj);
        }

        public bool Equals(Angle that)
        {
            return this._radians.Equals(that._radians);
        }

        public override int GetHashCode()
        {
            return _radians.GetHashCode();
        }

        public override string ToString()
        {
            return Degrees.ToString() + Convert.ToChar(0xB0);
        }
    }
}

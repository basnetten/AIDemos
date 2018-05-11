using System.Drawing;
using GameMath.Matrices;

namespace GameMath.Vectors
{
	public class Vector3 : Vector
	{
		/// <summary>
		/// Creates a new Vector3, initialized with X, Y and Z = 0.
		/// </summary>
		public Vector3() : this(0d, 0d, 0d)
		{
		}

		/// <summary>
		/// Creates a new Vector3, initialized with an X, Y and Z value.
		/// </summary>
		public Vector3(double x, double y, double z) : base(new[] {x, y, z})
		{
		}

		/// <summary>
		/// Upscales a Vector2 to a Vector3. The Z value is added to the end of the new Vector3.
		/// </summary>
		public Vector3(Vector2 vector2, double z) : base(new[] {vector2.X, vector2.Y, z})
		{
		}

		/**
		 * Properties.
		 */
		public double X
		{
			get => base[0];
			set => base[0] = value;
		}

		public double Y
		{
			get => base[1];
			set => base[1] = value;
		}

		public double Z
		{
			get => base[2];
			set => base[2] = value;
		}

		public double W
		{
			get => Z;
			set => Z = value;
		}

		/**
		 * 
		 */
		public new Vector3 Normalize()
		{
			return (Vector3) base.Normalize();
		}

		/**
		 * Operators.
		 */
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return (Vector3) a.Addition(b);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return (Vector3) a.Negative();
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return (Vector3) a.Subtraction(b);
		}

		public static double operator *(Vector3 a, Vector3 b)
		{
			return a.DotProduct(b);
		}

		public static Vector3 operator *(Vector3 a, double b)
		{
			return (Vector3) a.Multiplication(b);
		}

		public static Vector3 operator *(double a, Vector3 b)
		{
			return (Vector3) b.Multiplication(a);
		}

		public static Vector3 operator /(Vector3 a, double b)
		{
			return (Vector3) a.Division(b);
		}

		public static Vector3 operator /(double a, Vector3 b)
		{
			return (Vector3) b.Division(a);
		}

		/**
		 * Casts.
		 */
		public static explicit operator Matrix3(Vector3 a)
		{
			return new Matrix3(new[,]
			{
				{a.X, 0, 0},
				{a.Y, 0, 0},
				{a.Z, 0, 0},
			});
		}

		public static explicit operator Point(Vector3 a)
		{
			return (Point) ToVector2(a);
		}
	}
}
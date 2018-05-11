using System;
using GameMath.Vectors;

namespace GameMath.Matrices
{
	public class Matrix3 : Matrix
	{
		public Matrix3() : base(3, 3)
		{
		}

		public Matrix3(double[,] data) : base(3, 3)
		{
			if (data.GetLength(0) != 3 || data.GetLength(1) != 3)
				throw new ArgumentException("Data not 3x3");

			_data = data;
		}

		/**
		 * Operators.
		 */
		public static Matrix3 operator +(Matrix3 a, Matrix3 b)
		{
			return (Matrix3) a.Addition(b);
		}

		public static Matrix3 operator -(Matrix3 a, Matrix3 b)
		{
			return (Matrix3) a.Subtraction(b);
		}

		public static Matrix3 operator *(Matrix3 a, Matrix3 b)
		{
			return (Matrix3) a.Multiplication(b);
		}

		public static Matrix3 operator *(Matrix3 a, double b)
		{
			return (Matrix3) a.Multiplication(b);
		}

		public static Matrix3 operator *(double a, Matrix3 b)
		{
			return (Matrix3) b.Multiplication(a);
		}

		public static Vector3 operator *(Matrix3 a, Vector3 b)
		{
			return Vector.ToVector3(a.Multiplication(b));
		}

		public static Vector3 operator *(Vector3 a, Matrix3 b)
		{
			return Vector.ToVector3(b.Multiplication(a));
		}

		/**
		 * Casts.
		 */
		public static explicit operator Vector3(Matrix3 a)
		{
			return new Vector3(a[0, 0], a[1, 0], a[2, 0]);
		}
	}
}
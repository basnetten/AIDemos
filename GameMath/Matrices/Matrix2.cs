using System;
using GameMath.Vectors;

namespace GameMath.Matrices
{
	public class Matrix2 : Matrix
	{
		public Matrix2() : base(2, 2)
		{
		}

		public Matrix2(double[,] data) : base(2, 2)
		{
			if (data.GetLength(0) != 2 || data.GetLength(1) != 2)
				throw new ArgumentException("Data not 2x2");

			_data = data;
		}

		/**
		 * Operators.
		 */
		public static Matrix2 operator +(Matrix2 a, Matrix2 b)
		{
			return (Matrix2) a.Addition(b);
		}

		public static Matrix2 operator -(Matrix2 a, Matrix2 b)
		{
			return (Matrix2) a.Subtraction(b);
		}

		public static Matrix2 operator *(Matrix2 a, Matrix2 b)
		{
			return (Matrix2) a.Multiplication(b);
		}

		public static Matrix2 operator *(Matrix2 a, double b)
		{
			return (Matrix2) a.Multiplication(b);
		}

		public static Matrix2 operator *(double a, Matrix2 b)
		{
			return (Matrix2) b.Multiplication(a);
		}

		public static Vector2 operator *(Matrix2 a, Vector2 b)
		{
			return (Vector2) a.Multiplication(b);
		}

		public static Vector2 operator *(Vector2 a, Matrix2 b)
		{
			return (Vector2) b.Multiplication(a);
		}

		/**
		 * Casts.
		 */
		public static explicit operator Vector2(Matrix2 a)
		{
			return new Vector2(a[0, 0], a[1, 0]);
		}
	}
}
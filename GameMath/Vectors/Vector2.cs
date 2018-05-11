using System;
using System.Drawing;
using GameMath.Matrices;

namespace GameMath.Vectors
{
	public class Vector2 : Vector
	{
		/// <summary>
		/// Create a new Vector2, initilized with X=0 and Y=0.
		/// </summary>
		public Vector2() : this(0d, 0d)
		{
		}

		/// <summary>
		/// Create a new Vector2, initialized with an X and Y value.
		/// </summary>
		public Vector2(double x, double y) : base(new[] {x, y})
		{
		}

		/// <summary>
		/// Downscale a Vector3 to a Vector2.
		/// </summary>
		public Vector2(Vector3 vector3) : base(new[] {vector3.X, vector3.Y})
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

		/**
		 * 
		 */
		public new Vector2 Normalize()
		{
			return ToVector2(base.Normalize());
		}

		public double AngleTo(Vector2 vector)
		{
			return Math.Atan2(vector.Y - Y, vector.X - X);
		}

		public new Vector2 Truncate(double value)
		{
			return ToVector2(base.Truncate(value));
		}

		/// <summary>
		/// Calculate the angle to another vector in diamonds. This function is ~3x faster compared to using
		/// atan(). The function does return the angle in diamonds, which can only be used to compare angles.
		/// https://stackoverflow.com/a/14675998
		/// </summary>
		public double AngleToDiamonds(Vector2 vector)
		{
			double angle = 0d;
			Vector2 diff = vector - this;
			double x = diff.X;
			double y = diff.Y;
			if (y >= 0)
				return (x >= 0 ? (y / (x + y)) : (1 - x / (-x + y)));
			else
				return (x < 0 ? (2 - y / (-x - y)) : (3 + x / (x - y)));
		}

		public double DistanceToPythagorean(Vector2 vector)
		{
			return (this - vector).MagnitudePythagorean();
		}

		public double DistanceToManhattan(Vector2 vector)
		{
			return (this - vector).MagnitudeManhattan();
		}

		/**
		 * Operators.
		 */
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return ToVector2(a.Addition(b));
		}

		public static Vector2 operator -(Vector2 a)
		{
			return ToVector2(a.Negative());
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return ToVector2(a.Subtraction(b));
		}

		public static double operator *(Vector2 a, Vector2 b)
		{
			return a.DotProduct(b);
		}

		public static Vector2 operator *(Vector2 a, double b)
		{
			return ToVector2(a.Multiplication(b));
		}

		public static Vector2 operator *(double a, Vector2 b)
		{
			return ToVector2(b.Multiplication(a));
		}

		public static Vector2 operator /(Vector2 a, double b)
		{
			return ToVector2(a.Division(b));
		}

		public static Vector2 operator /(double a, Vector2 b)
		{
			return ToVector2(b.Division(a));
		}

		/**
		 * Casts.
		 */
		public static explicit operator Point(Vector2 a)
		{
			// TODO is this the right place for the transformation to drawspace?
			return new Point((int) a[0], (int) a[1]);
		}

		public static explicit operator Matrix2(Vector2 a)
		{
			return new Matrix2(new[,]
			{
				{a.X, 0},
				{a.Y, 0},
			});
		}

		public Vector2 Scale(double sx, double sy)
		{
			return new Vector2(X * sx, Y * sy);
		}
	}
}
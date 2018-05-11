using System;
using System.Drawing;
using System.Linq;
using GameMath.Matrices;

namespace GameMath.Vectors
{
	public class Vector
	{
		/// <summary>
		/// The actual data.
		/// </summary>
		private readonly double[] _data;

		/// <summary>
		/// Initialize the vector with predetermined data.
		/// </summary>
		public Vector(double[] data)
		{
			_data = data;
		}

		/// <summary>
		/// Initialize the vector empty with a certain element count.
		/// </summary>
		public Vector(int count)
		{
			_data = new double[count];
		}

		/// <summary>
		/// The number of elements in this vector.
		/// </summary>
		public int Count => _data.Length;

		/// <summary>
		/// Indexer, delegates to the underlying array.
		/// </summary>
		public double this[int index]
		{
			get => _data[index];
			set => _data[index] = value;
		}

		/// <summary>
		/// The magnitude of this vector, using the manhattan method.
		/// </summary>
		public double MagnitudeManhattan()
		{
			double sum = 0d;
			foreach (var value in _data)
			{
				sum += value;
			}

			return sum;
		}

		/// <summary>
		/// The magnitude of this vector, using the pythagorean method.
		/// </summary>
		public double MagnitudePythagorean()
		{
			double sum = 0d;
			foreach (var value in _data)
			{
				sum += value * value;
			}

			return Math.Sqrt(sum);
		}

		/// <summary>
		/// Normalize the vector.
		/// </summary>
		public Vector Normalize()
		{
			double magnitude = MagnitudePythagorean();
			if (magnitude == 0) return new Vector(Count);

			double[] data = new double[Count];
			for (var i = 0; i < _data.Length; i++)
			{
				data[i] = _data[i] / magnitude;
			}

			return new Vector(data);
		}

		public Vector Truncate(double value)
		{
			if (value > MagnitudePythagorean())
			{
				return this;
			}

			return Normalize() * value;
		}

		public override string ToString()
		{
			return $"Vector{Count}<{string.Join(", ", _data)}>";
		}

		/**
		 * Operations.
		 */
		public Vector Negative()
		{
			double[] data = new double[Count];
			for (var i = 0; i < _data.Length; i++)
			{
				data[i] = -_data[i];
			}

			return new Vector(data);
		}

		public Vector Addition(Vector vector)
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
			{
				data[i] = _data[i] + vector._data[i];
			}

			return new Vector(data);
		}

		public Vector Subtraction(Vector vector)
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
			{
				data[i] = _data[i] - vector._data[i];
			}

			return new Vector(data);
		}

		public double DotProduct(Vector vector)
		{
			double sum = 0d;
			for (int i = 0; i < Count; i++)
			{
				sum += _data[i] * vector._data[i];
			}

			return sum;
		}

		public Vector Multiplication(double scalar)
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
			{
				data[i] = _data[i] * scalar;
			}

			return new Vector(data);
		}

		public Vector Division(double scalar)
		{
			return Multiplication(1 / scalar);
		}

		/**
		 * Operators.
		 */
		public static Vector operator +(Vector a, Vector b)
		{
			return a.Addition(b);
		}

		public static Vector operator -(Vector a)
		{
			return a.Negative();
		}

		public static Vector operator -(Vector a, Vector b)
		{
			return a.Subtraction(b);
		}

		public static double operator *(Vector a, Vector b)
		{
			return a.DotProduct(b);
		}

		public static Vector operator *(Vector a, double b)
		{
			return a.Multiplication(b);
		}

		public static Vector operator *(double a, Vector b)
		{
			return b.Multiplication(a);
		}

		public static Vector operator /(Vector a, double b)
		{
			return a.Division(b);
		}

		public static Vector operator /(double a, Vector b)
		{
			return b.Division(a);
		}

		/**
		 * Casts.
		 */
		public static explicit operator Matrix(Vector a)
		{
			double[,] data = new double[a.Count, 1];
			for (int i = 0; i < a.Count; i++)
			{
				data[i, 0] = a._data[i];
			}

			return new Matrix(data);
		}

		public static Vector2 ToVector2(Vector a)
		{
			return new Vector2(a[0], a[1]);
		}

		public static Vector3 ToVector3(Vector a)
		{
			return new Vector3(a[0], a[1], a[2]);
		}
	}
}
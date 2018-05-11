using System;
using GameMath.Vectors;

namespace GameMath.Matrices
{
	public class Matrix
	{
		protected double[,] _data;

		public Matrix(int rowCount, int colCount)
		{
			_data = new double[rowCount, colCount];
		}

		public Matrix(double[,] data)
		{
			_data = data;
		}

		public int RowCount => _data.GetLength(0);
		public int ColCount => _data.GetLength(1);

		public double this[int row, int col]
		{
			get => _data[row, col];
			set => _data[row, col] = value;
		}

		/**
		 * 
		 */
		public override string ToString()
		{
			string s = $"Matrix{RowCount}x{ColCount}<{Environment.NewLine}";

			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColCount; j++)
				{
					s += (j != 0 ? ", " : "") + _data[i, j];
				}

				s += Environment.NewLine;
			}

			return s + Environment.NewLine + ">";
		}

		/**
		 * Operations.
		 */
		public Matrix Addition(Matrix matrix)
		{
			double[,] data = new double[RowCount, ColCount];
			for (int row = 0; row < RowCount; row++)
			{
				for (int col = 0; col < ColCount; col++)
				{
					data[row, col] = _data[row, col] + matrix._data[row, col];
				}
			}

			return new Matrix(data);
		}

		public Matrix Subtraction(Matrix matrix)
		{
			double[,] data = new double[RowCount, ColCount];
			for (int row = 0; row < RowCount; row++)
			{
				for (int col = 0; col < ColCount; col++)
				{
					data[row, col] = _data[row, col] - matrix._data[row, col];
				}
			}

			return new Matrix(data);
		}

		public Matrix Multiplication(Matrix matrix)
		{
			double[,] data = new double[RowCount, matrix.ColCount];
			for (int row = 0; row < RowCount; row++)
			{
				for (int col = 0; col < matrix.ColCount; col++)
				{
					double sum = 0d;
					for (int i = 0; i < ColCount; i++)
					{
						sum += _data[row, i] * matrix._data[i, col];
					}

					data[row, col] = sum;
				}
			}

			return new Matrix(data);
		}

		public Matrix Multiplication(double scalar)
		{
			double[,] data = new double[RowCount, ColCount];
			for (int row = 0; row < RowCount; row++)
			{
				for (int col = 0; col < ColCount; col++)
				{
					data[row, col] = _data[row, col] * scalar;
				}
			}

			return new Matrix(data);
		}

		public Vector Multiplication(Vector vector)
		{
			return (Vector) Multiplication((Matrix) vector);
		}

		/**
		 * Operators.
		 */
		public static Matrix operator +(Matrix a, Matrix b)
		{
			return a.Addition(b);
		}

		public static Matrix operator -(Matrix a, Matrix b)
		{
			return a.Subtraction(b);
		}

		public static Matrix operator *(Matrix a, Matrix b)
		{
			return a.Multiplication(b);
		}

		public static Matrix operator *(Matrix a, double b)
		{
			return a.Multiplication(b);
		}

		public static Matrix operator *(double a, Matrix b)
		{
			return b.Multiplication(a);
		}

		public static Vector operator *(Matrix a, Vector b)
		{
			return a.Multiplication(b);
		}

		public static Vector operator *(Vector a, Matrix b)
		{
			return b.Multiplication(a);
		}

		/**
		 * Casts.
		 */
		public static explicit operator Vector(Matrix a)
		{
			double[] data = new double[a.RowCount];
			for (int i = 0; i < a.RowCount; i++)
			{
				data[i] = a._data[i, 0];
			}

			return new Vector(data);
		}
	}
}
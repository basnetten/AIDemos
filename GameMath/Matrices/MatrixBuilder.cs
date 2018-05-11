using System;

namespace GameMath.Matrices
{
	public class MatrixBuilder
	{
		public static Matrix3 RotationMatrix(double angle_r)
		{
			double cos = Math.Cos(angle_r);
			double sin = Math.Sin(angle_r);
			
			return new Matrix3(new[,]
			{
				{cos, -sin, 0},
				{sin, cos, 0},
				{0, 0, 1},
			});
		}
	}
}
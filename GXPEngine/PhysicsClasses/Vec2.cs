using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
	public float x;
	public float y;


	/// <summary>
	/// Set the values of the vector
	/// </summary>
	/// <param name="x">Value of X</param>
	/// <param name="y">Value of Y</param>
	public void SetXY(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	public bool checkNull()
	{
		if (x == 0f && y == 0f)
			return true;
		return false;
	}

	// TODO: Implement Length, Normalize, Normalized, SetXY methods (see Assignment 1)

	/// <summary>
	/// length of a vector
	/// </summary>
	/// <returns></returns>
	public float Length()
	{
		// TODO: return the vector length
		return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
	}

	/// <summary>
	/// same vector with length 1
	/// </summary>
	public void Normalize()
	{
		if (x == 0 && y == 0)
			return;
		float length = Length();
		x /= length;
		y /= length;
	}

	/// <summary>
	/// Normalize the passed vector
	/// </summary>
	/// <param name="ToNormalize">Vector to normalize</param>
	/// <returns>vector normalized</returns>
	public static Vec2 Normalized(Vec2 ToNormalize)
	{
		if (ToNormalize.x == 0 && ToNormalize.y == 0)
			return new Vec2(0, 0);
		float length = ToNormalize.Length();
		ToNormalize.x /= length;
		ToNormalize.y /= length;
		return ToNormalize;
	}

	public Vec2 Normalized()
	{
		return Vec2.Normalized(this);
	}

	// TODO: Implement subtract, scale operators

	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}
	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}
	public static Vec2 operator *(float scale, Vec2 vect)
	{
		return new Vec2(vect.x * scale, vect.y * scale);
	}
	public static Vec2 operator *(Vec2 vect, float scale)
	{
		return new Vec2(vect.x * scale, vect.y * scale);
	}

	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}
	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/
	public static float Deg2Rad(float degrees)
	{
		return degrees / 180f * Mathf.PI;
	}

	public static float Rad2Deg(float radians)
	{
		return radians * 180f / Mathf.PI;
	}
	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		return GetUnitVectorRad(Deg2Rad(degrees));
	}

	public static Vec2 GetUnitVectorRad(float radians)
	{
		return Normalized(new Vec2(Mathf.Cos(radians), Mathf.Sin(radians)));
	}


	public static Vec2 RandomUnitVector()
	{
		Random rnd = new Random();
		float max = 2 * Mathf.PI;
		float min = 0;
		return Vec2.GetUnitVectorDeg(Utils.Random(min, max));
	}
	public void SetAngleDegrees(float degrees, float offset=0)
	{
		SetAngleRadians(Deg2Rad(degrees+offset));
	}

	public void SetAngleRadians(float radians)
	{
		float l = Length();
		x = Mathf.Cos(radians) * l;
		y = Mathf.Sin(radians) * l;
	}

	public float GetAngleRadians() // TODO: test thoroughly!
	{
		return Mathf.Atan2(y, x);

		//if ((x < 0 && y > 0) || (y < 0 && x < 0))
		//	return Mathf.PI + Mathf.Atan(y / x);
		//return Mathf.Atan(y / x);


	}

	public float GetAngleDegrees()
	{
		return Rad2Deg(GetAngleRadians());
	}
	public void RotateRadians(float radians)
	{
		float x1 = x;
		float y1 = y;
		x = x1 * Mathf.Cos(radians) - y1 * Mathf.Sin(radians);
		y = x1 * Mathf.Sin(radians) + y1 * Mathf.Cos(radians);
	}
	public void RotateDegrees(float degrees)
	{
		RotateRadians(Deg2Rad(degrees));
	}

	public void RotateAroundRadians(float radians, Vec2 point)
	{
		this -= point;
		RotateRadians(radians);
		this += point;
	}

	public void RotateAroundDegrees(float degree, Vec2 point)
	{
		RotateAroundRadians(Deg2Rad(degree), point);
	}

	public Vec2 Normal()
	{
		return new Vec2(-y, x).Normalized();
		//Vec2 temp = Vec2.Normalized(this);
		//temp.RotateDegrees(90);
		//return temp;
	}

	public static float Dot(Vec2 a, Vec2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	public float Dot(Vec2 b)
	{
		return x * b.x + y * b.y;
	}

	public static float AngleRad(Vec2 a, Vec2 b)
	{
		return Mathf.Acos(Vec2.Dot(a.Normalized(), b.Normalized()));
	}

	public static float AngleDeg(Vec2 a, Vec2 b)
	{
		return Rad2Deg(AngleRad(a, b));
	}

	public Vec2 Reflect(Vec2 norm, float bounciness = 1)
	{
		Vec2 vi = this;
		return vi - (1 + bounciness) * vi.Dot(norm) * norm;
	}

}


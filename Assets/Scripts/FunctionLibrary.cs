using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{

	public delegate Vector3 Function(float u, float v, float t);

	public enum FunctionName { Sphere, Torus, TorusOriginal, TorusSpiral, TorusPulse, TorusCentrifuge }

	static Function[] functions = { Sphere, Torus, TorusOriginal, TorusSpiral, TorusPulse, TorusCentrifuge };

	public static int FunctionCount => functions.Length;

	public static Function GetFunction(FunctionName name) => functions[(int)name];

	public static FunctionName GetNextFunctionName(FunctionName name) =>
		(int)name < functions.Length - 1 ? name + 1 : 0;

	public static FunctionName GetRandomFunctionNameOtherThan(FunctionName name)
	{
		var choice = (FunctionName)Random.Range(1, functions.Length);
		return choice == name ? 0 : choice;
	}

	public static Vector3 Morph(
		float u, float v, float t, Function from, Function to, float progress
	)
	{
		return Vector3.LerpUnclamped(
			from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress)
		);
	}

	public static Vector3 Sphere(float u, float v, float t)
	{
		float r = 0.9f + 0.1f * Sin(PI * (12f * u + 8f * v + t));
		float s = r * Cos(0.5f * PI * v);
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r * Sin(0.5f * PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 Torus(float u, float v, float t)
	{
		float r1 = 0.7f + 0.02f * Sin(PI * (1.0f * u + 0.5f * t));
		float bulge = 0.05f * Sin(PI * (4.0f * u + t));
		float ripples = 0.05f * Sin(PI * (30.0f * u + 10.0f * v + 5.0f * t));
		float r2 = 0.3f + bulge + ripples;
		float twist = 0.5f * Sin(PI * (u + t * 0.5f));
		float angle = PI * (v + twist);

		float s = r2 * Cos(angle) + r1;
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(angle);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 TorusOriginal(float u, float v, float t)
	{
		float r1 = 0.7f + 0.1f;
		float r2 = 0.15f + 0.05f * Sin(PI * (50.0f * u + 8.0f * v + 3.0f * t));
		float s = r2 * Cos(PI * v) + r1;
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 TorusSpiral(float u, float v, float t)
	{
		float r1 = 0.7f;
		float r2 = 0.15f + 0.05f * Sin(PI * (8.0f * u + 4.0f * v + 2.0f * t));
		float twist = Sin(PI * (2.0f * u + t));
		float angle = PI * (v + twist);
		float s = r2 * Cos(angle) + r1;
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(angle);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 TorusPulse(float u, float v, float t)
	{
		float r1 = 0.7f;
		float bulge = 0.06f * Sin(PI * (2.0f * u + 0.5f * t));
		float ripples = 0.02f * Sin(PI * (20.0f * u + 10.0f * v + 4.0f * t));
		float r2 = 0.15f + bulge + ripples;
		float s = r2 * Cos(PI * v) + r1;
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}

	public static Vector3 TorusCentrifuge(float u, float v, float t)
	{
		float r1 = 0.7f;
		float r2 = 0.15f + 0.05f * Sin(PI * (8.0f * u + t));
		float angle = PI * v + t;
		float s = r2 * Cos(angle) + r1;
		Vector3 p;
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(angle);
		p.z = s * Cos(PI * u);
		return p;
	}
}
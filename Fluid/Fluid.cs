using UnityEngine;

public class Fluid : MonoBehaviour
{
	public int w = 128;
	public int h = 64;
	public float diff = 0.001f;
	public float visc = 0.0001f;
	public int linSolveIterNum = 10;
	public float densityReduce = 0.0001f;
	public float dt = 5f;

	public void AddVelocity(int x, int y, float amountX, float amountY)
	{
		uArr[((x) + (w + 2) * (y))] += amountX;
		vArr[((x) + (w + 2) * (y))] += amountY;
	}
	public void AddDensity(int x, int y, float amountR, float amountG, float amountB)
	{
		RArr[((x) + (w + 2) * (y))] += amountR;
		GArr[((x) + (w + 2) * (y))] += amountG;
		BArr[((x) + (w + 2) * (y))] += amountB;
	}

	Texture2D texture;
	Color32[] colors;
	void Start()
	{
		CreateFluid();

		texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
		colors = new Color32[w * h];
	}

	void Update()
	{
        //AddDensity(64, 32, Random.Range(1, 25), Random.Range(1, 25), Random.Range(1, 25));
        //AddVelocity(64, 32, Random.Range(-25, 25), Random.Range(-25, 25));

        FluidStep();

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                colors[((i) + (w) * (j))] = new Color(RArr[((i + 1) + (w + 2) * (j + 1))], GArr[((i + 1) + (w + 2) * (j + 1))], BArr[((i + 1) + (w + 2) * (j + 1))]);
            }
        }
        texture.SetPixels32(colors);
        texture.Apply();
    }
	
	int size;
	float[] RArr;
	float[] R0Arr;
	float[] GArr;
	float[] G0Arr;
	float[] BArr;
	float[] B0Arr;
	float[] uArr;
	float[] vArr;
	float[] u0Arr;
	float[] v0Arr;

	void CreateFluid()
	{
		size = (w + 2) * (h + 2);

		RArr = new float[size];
		R0Arr = new float[size];
		GArr = new float[size];
		G0Arr = new float[size];
		BArr = new float[size];
		B0Arr = new float[size];
		uArr = new float[size];
		vArr = new float[size];
		u0Arr = new float[size];
		v0Arr = new float[size];

		for (int i = 0; i < size; i++)
		{
			RArr[i] = 0;
			R0Arr[i] = 0;
			GArr[i] = 0;
			G0Arr[i] = 0;
			BArr[i] = 0;
			B0Arr[i] = 0;
			uArr[i] = 0;
			vArr[i] = 0;
			u0Arr[i] = 0;
			v0Arr[i] = 0;
		}
	}

	void FluidStep()
	{
		DensityStep();
		VelocityStep();
	}
	void DensityStep()
	{
		Diffuse(0, R0Arr, RArr, diff);
		Advect(0, RArr, R0Arr, uArr, vArr);
		Diffuse(0, G0Arr, GArr, diff);
		Advect(0, GArr, G0Arr, uArr, vArr);
		Diffuse(0, B0Arr, BArr, diff);
		Advect(0, BArr, B0Arr, uArr, vArr);

		ReduceDensity();
	}
	void VelocityStep()
	{
		Diffuse(1, u0Arr, uArr, visc);
		Diffuse(2, v0Arr, vArr, visc);
		Project(u0Arr, v0Arr, uArr, vArr);
		Advect(1, uArr, u0Arr, u0Arr, v0Arr);
		Advect(2, vArr, v0Arr, u0Arr, v0Arr);
		Project(uArr, vArr, u0Arr, v0Arr);
	}

	void SetBnd(int axesNum, float[] arr)
	{
		for (int i = 1; i <= h; i++)
		{
			arr[((0) + (w + 2) * (i))] = axesNum == 1 ? -arr[((1) + (w + 2) * (i))] : arr[((1) + (w + 2) * (i))];
			arr[((w + 1) + (w + 2) * (i))] = axesNum == 1 ? -arr[((w) + (w + 2) * (i))] : arr[((w) + (w + 2) * (i))];
		}
		for (int i = 1; i <= w; i++)
		{
			arr[((i) + (w + 2) * (0))] = axesNum == 2 ? -arr[((i) + (w + 2) * (1))] : arr[((i) + (w + 2) * (1))];
			arr[((i) + (w + 2) * (h + 1))] = axesNum == 2 ? -arr[((i) + (w + 2) * (h))] : arr[((i) + (w + 2) * (h))];
		}
		arr[((0) + (w + 2) * (0))] = 0.5f * (arr[((1) + (w + 2) * (0))] + arr[((0) + (w + 2) * (1))]);
		arr[((0) + (w + 2) * (h + 1))] = 0.5f * (arr[((1) + (w + 2) * (h + 1))] + arr[((0) + (w + 2) * (h))]);
		arr[((w + 1) + (w + 2) * (0))] = 0.5f * (arr[((w) + (w + 2) * (0))] + arr[((w + 1) + (w + 2) * (1))]);
		arr[((w + 1) + (w + 2) * (h + 1))] = 0.5f * (arr[((w) + (w + 2) * (h + 1))] + arr[((w + 1) + (w + 2) * (h))]);
	}
	void LinSolve(int axesNum, float[] arr, float[] arr0, float a, float b)
	{
		for (int n = 0; n < linSolveIterNum; n++)
		{
			for (int j = 1; j <= h; j++)
			{
				for (int i = 1; i <= w; i++)
				{
					arr[((i)+(w+2)*(j))] = (arr0[((i)+(w+2)*(j))] + a * (arr[((i - 1)+(w+2)*(j))] + arr[((i + 1)+(w+2)*(j))] + arr[((i)+(w+2)*(j - 1))] + arr[((i)+(w+2)*(j + 1))])) / b;
				}
			}
			SetBnd(axesNum, arr);
		}
	}
	void Diffuse(int axesNum, float[] arr, float[] arr0, float d)
	{
		float a = dt * d;
		LinSolve(axesNum, arr, arr0, a, 1 + 4 * a);
	}
	void Advect(int axesNum, float[] arr, float[] arr0, float[] u, float[] v)
	{
		int i0, j0, i1, j1;
		float x, y, s0, t0, s1, t1;
		for (int j = 1; j <= h; j++)
		{
			for (int i = 1; i <= w; i++)
			{
				x = i - dt * u[((i)+(w+2)*(j))]; y = j - dt * v[((i)+(w+2)*(j))];
				if (x < 0.5f) x = 0.5f; if (x > w + 0.5f) x = w + 0.5f; i0 = (int)
					x; i1 = i0 + 1;
				if (y < 0.5f) y = 0.5f; if (y > h + 0.5f) y = h + 0.5f; j0 = (int)
					y; j1 = j0 + 1;
				s1 = x - i0; s0 = 1 - s1; t1 = y - j0; t0 = 1 - t1;
				arr[((i)+(w+2)*(j))] = s0 * (t0 * arr0[((i0) + (w + 2) * (j0))] + t1 * arr0[((i0) + (w + 2) * (j1))]) +
					s1 * (t0 * arr0[((i1) + (w + 2) * (j0))] + t1 * arr0[((i1) + (w + 2) * (j1))]);
			}
		}
		SetBnd(axesNum, arr);
	}
	void Project(float[] u, float[] v, float[] p, float[] p0)
	{
		for (int j = 1; j <= h; j++)
		{
			for (int i = 1; i <= w; i++)
			{
				p0[((i)+(w+2)*(j))] = -0.5f * (u[((i + 1)+(w+2)*(j))] - u[((i - 1)+(w+2)*(j))] + v[((i)+(w+2)*(j + 1))] - v[((i)+(w+2)*(j - 1))]); //is divergence but has opposite sign
				p[((i)+(w+2)*(j))] = 0;
			}
		}
		SetBnd(0, p0); SetBnd(0, p);
		LinSolve(0, p, p0, 1, 4);
		for (int j = 1; j <= h; j++)
		{
			for (int i = 1; i <= w; i++)
			{
				u[((i)+(w+2)*(j))] -= 0.5f * (p[((i + 1)+(w+2)*(j))] - p[((i - 1)+(w+2)*(j))]);
				v[((i)+(w+2)*(j))] -= 0.5f * (p[((i)+(w+2)*(j + 1))] - p[((i)+(w+2)*(j - 1))]);
			}
		}
		SetBnd(1, u); SetBnd(2, v);
	}
	void ReduceDensity()
	{
		for (int i = 0; i < size; i++)
		{
			RArr[i] -= densityReduce * dt;
			R0Arr[i] -= densityReduce * dt;
			GArr[i] -= densityReduce * dt;
			G0Arr[i] -= densityReduce * dt;
			BArr[i] -= densityReduce * dt;
			B0Arr[i] -= densityReduce * dt;

			if (RArr[i] < 0) RArr[i] = 0;
			if (R0Arr[i] < 0) R0Arr[i] = 0;
			if (GArr[i] < 0) GArr[i] = 0;
			if (G0Arr[i] < 0) G0Arr[i] = 0;
			if (BArr[i] < 0) BArr[i] = 0;
			if (B0Arr[i] < 0) B0Arr[i] = 0;
		}
	}
}

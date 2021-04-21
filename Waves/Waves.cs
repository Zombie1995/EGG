using UnityEngine;

public class Waves : MonoBehaviour
{
    public int field_width;
    public int field_height;

	public float[] VelocityField;

	Vector3[] Vertices;

	float max_acceleration = 1.0f;

    MeshFilter filter;

	void Start()
    {
        VelocityField = new float[field_height * field_width];
        for (int i1 = 0; i1 < field_height; i1++)
        {
            for (int i2 = 0; i2 < field_width; i2++)
            {
                VelocityField[field_width * i1 + i2] = 0;
            }
        }

        filter = GetComponent<MeshFilter>();
    }

    void Update()
	{
		Vertices = filter.mesh.vertices;

		for (int i1 = 1; i1 < field_height - 1; i1++)
        {
            for (int i2 = 1; i2 < field_width - 1; i2++)
            {
                float average_height = (Vertices[field_width * i1 + i2].y + Vertices[field_width * i1 + i2 - 1].y + Vertices[field_width * (i1 + 1) + i2].y + Vertices[field_width * i1 + i2 + 1].y + Vertices[field_width * (i1 - 1) + i2].y) / 5;

                float acceleration;

				acceleration = (average_height - Vertices[field_width * i1 + i2].y);
				if (acceleration > max_acceleration)
				{
					acceleration = max_acceleration;
				}
				else if (acceleration < -max_acceleration)
				{
					acceleration = -max_acceleration;
				}
				VelocityField[field_width * i1 + i2] += acceleration * Time.deltaTime;

				acceleration = (average_height - Vertices[field_width * (i1 - 1) + i2].y);
				if (acceleration > max_acceleration)
				{
					acceleration = max_acceleration;
				}
				else if (acceleration < -max_acceleration)
				{
					acceleration = -max_acceleration;
				}
				VelocityField[field_width * (i1 - 1) + i2] += acceleration * Time.deltaTime;

				acceleration = (average_height - Vertices[field_width * i1 + i2 + 1].y);
				if (acceleration > max_acceleration)
				{
					acceleration = max_acceleration;
				}
				else if (acceleration < -max_acceleration)
				{
					acceleration = -max_acceleration;
				}
				VelocityField[field_width * i1 + i2 + 1] += acceleration * Time.deltaTime;

				acceleration = (average_height - Vertices[field_width * (i1 + 1) + i2].y);
				if (acceleration > max_acceleration)
				{
					acceleration = max_acceleration;
				}
				else if (acceleration < -max_acceleration)
				{
					acceleration = -max_acceleration;
				}
				VelocityField[field_width * (i1 + 1) + i2] += acceleration * Time.deltaTime;

				acceleration = (average_height - Vertices[field_width * i1 + i2 - 1].y);
				if (acceleration > max_acceleration)
				{
					acceleration = max_acceleration;
				}
				else if (acceleration < -max_acceleration)
				{
					acceleration = -max_acceleration;
				}
				VelocityField[field_width * i1 + i2 - 1] += acceleration * Time.deltaTime;
			}

        }

		for (int i1 = 1; i1 < field_height - 1; i1++)
		{
			for (int i2 = 1; i2 < field_width - 1; i2++)
			{
				Vertices[field_width * i1 + i2] += new Vector3(0, (VelocityField[field_width * i1 + i2] / 2) * Time.deltaTime, 0);
			}
		}

		filter.mesh.vertices = Vertices;
		GetComponent<MeshCollider>().sharedMesh = filter.mesh;
	}
}

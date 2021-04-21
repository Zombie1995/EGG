using UnityEngine;

public class MatrixLib : MonoBehaviour
{
    public class mat
    {
        public float[,] m;
        public int arr;
        public int col;
        public mat(int arr1, int col1)
        {
            m = new float[arr1, col1];

            for (int i = 0; i < arr1; i++)
            {
                for (int i2 = 0; i2 < col1; i2++)
                {
                    m[i, i2] = 0;
                }
            }

            arr = arr1;
            col = col1;

        }
        public void resize(int arr1, int col1)
        {
            m = null;

            m = new float[arr1, col1];

            for (int i = 0; i < arr1; i++)
            {
                for (int i2 = 0; i2 < col1; i2++)
                {
                    m[i, i2] = 0;
                }
            }

            arr = arr1;
            col = col1;

        }
        public float determinant()
        {
            float d = 0;

            if (col == 1)
            {
                d = m[0, 0];
                return d;
            }
            if (col == 2)
            {
                d = m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
                return d;
            }
            else
            {
                mat matrix = new mat((arr - 1), (col - 1));

                for (int i1 = 0; i1 < col; i1++)
                {
                    //Creating small matrix
                    int count = 0;
                    for (int i2 = 1; i2 < arr; i2++)
                    {
                        for (int i3 = 0; i3 < col; i3++)
                        {
                            if (i3 != i1)
                            {
                                matrix.m[(i2 - 1), count] = m[i2, i3];
                                count++;
                            }
                        }
                        count = 0;
                    }

                    if (i1 % 2 == 0)
                    {
                        d += m[0, i1] * matrix.determinant();
                    }
                    else
                    {
                        d -= m[0, i1] * matrix.determinant();
                    }
                }
                return d;
            }
        }

        public static mat operator *(mat matrix1, mat matrix2)
        {
            mat result = new mat(matrix1.arr, matrix2.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i2 = 0; i2 < matrix2.col; i2++)
                {
                    for (int i3 = 0; i3 < matrix1.col; i3++)
                    {
                        result.m[i, i2] += matrix1.m[i, i3] * matrix2.m[i3, i2];
                    }
                }
            }
            return result;
        }
        public static mat operator *(mat matrix1, float num)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = matrix1.m[i, i1] * num;
                }
            }
            return result;
        }
        public static mat operator *(float num, mat matrix1)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = num * matrix1.m[i, i1];
                }
            }
            return result;
        }
        public static mat operator +(mat matrix1, mat matrix2)
        {
            mat result = new mat(matrix1.arr, matrix2.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix2.col; i1++)
                {
                    result.m[i, i1] = matrix1.m[i, i1] + matrix2.m[i, i1];
                }
            }
            return result;
        }
        public static mat operator +(mat matrix1, float num)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = matrix1.m[i, i1] + num;
                }
            }
            return result;
        }
        public static mat operator +(float num, mat matrix1)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = num + matrix1.m[i, i1];
                }
            }
            return result;
        }
        public static mat operator -(mat matrix1, mat matrix2)
        {
            mat result = new mat(matrix1.arr, matrix2.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix2.col; i1++)
                {
                    result.m[i, i1] = matrix1.m[i, i1] - matrix2.m[i, i1];
                }
            }
            return result;
        }
        public static mat operator -(mat matrix1, float num)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = matrix1.m[i, i1] - num;
                }
            }
            return result;
        }
        public static mat operator -(float num, mat matrix1)
        {
            mat result = new mat(matrix1.arr, matrix1.col);
            for (int i = 0; i < matrix1.arr; i++)
            {
                for (int i1 = 0; i1 < matrix1.col; i1++)
                {
                    result.m[i, i1] = num - matrix1.m[i, i1];
                }
            }
            return result;
        }
    };

    mat transpose(mat matrix)
    {
        mat m = new mat(matrix.col, matrix.arr);
        for (int i = 0; i < matrix.col; i++)
        {
            for (int i2 = 0; i2 < matrix.arr; i2++)
            {
                m.m[i, i2] = matrix.m[i2, i];
            }
        }
        return m;
    }
    mat inverse(mat matrix)
    {
        mat m = new mat(matrix.arr, matrix.col);

        //Case when arr_num = 1
        if (matrix.arr == 1)
        {
            m.m[0, 0] = 1 / matrix.m[0, 0];
            return m;
        }

        //Creating minor matrix
        int count1 = 0;
        int count2 = 0;
        mat m2 = new mat((matrix.arr - 1), (matrix.col - 1));
        for (int i1 = 0; i1 < matrix.arr; i1++)
        {
            for (int i2 = 0; i2 < matrix.col; i2++)
            {
                for (int j1 = 0; j1 < matrix.arr; j1++)
                {
                    for (int j2 = 0; j2 < matrix.col; j2++)
                    {
                        if (j1 != i1 && j2 != i2)
                        {
                            m2.m[count1, count2] = matrix.m[j1, j2];
                            count2++;
                            if (count2 == (matrix.col - 1))
                            {
                                count2 = 0;
                                count1++;
                                if (count1 == (matrix.arr - 1))
                                {
                                    count1 = 0;
                                }
                            }
                        }
                    }
                }

                m.m[i1, i2] = m2.determinant();
            }
        }

        //Changing signs on some numbers
        for (int i1 = 0; i1 < m.arr; i1++)
        {
            for (int i2 = 0; i2 < m.col; i2++)
            {
                if ((i1 + i2) % 2 != 0)
                {
                    m.m[i1, i2] = -m.m[i1, i2];
                }
            }
        }

        m = (1 / matrix.determinant()) * transpose(m);

        return m;
    }
}

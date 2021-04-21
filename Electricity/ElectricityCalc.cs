using System;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityCalc : MonoBehaviour
{
    //MatrixLib
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

    mat ElecValues;
    mat EquValues;
    mat Result;

    float[] Resistances;

    GameObject[] Accs;

    /*void Update()
    {  
    }*/

    public void CalcCircuit() 
    {
        if (GetComponent<OccupiedDots>().crossingDots.Length >= 2)
        {
            CalcPaths();

            if (AccCount != 0) 
            {
                FirstLawCalc();

                ChooseContours();
                SecondLawCalc();

                ElecValues = inverse(EquValues) * Result;

                GetElectricity();
            }
        }
        if (GetComponent<OccupiedDots>().crossingDots.Length == 0)
        {
            GetSimpleElectricity();
        }
    }


    float pathResist = 0;
    void CalcPaths() 
    {
        int count = -1;

        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++) 
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].tag != "Crossing") 
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum == -1)
                    {
                        count++;
                        CalcPath(GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2], GetComponent<OccupiedDots>().crossingDots[i1], count);

                        //Adding resistances
                        float[] prResist = Resistances;
                        Resistances = new float[(count + 1)];
                        for (int i = 0; i < count; i++)
                        {
                            Resistances[i] = prResist[i];
                        }
                        Resistances[count] = pathResist;
                        pathResist = 0;
                    }
                }
            }
        }

        //Creating matrixes
        if (count != 0)
        {
            Result = new mat((count + 1), 1);
            EquValues = new mat((count + 1), (count + 1));
        }
    }
    int AccCount = 0;
    void CalcPath(GameObject obj, GameObject crossingDot, int elecNum)
    {
        bool end = false;
        GameObject dot;

        dot = crossingDot;

        while (!end)
        {
            //Counting resistance in the path
            pathResist += obj.GetComponent<ObjParametres>().Resistance;

            //Adding accs in the list
            if(obj.tag == "Accumulator") 
            {
                GameObject[] prObjs = Accs;
                Accs = new GameObject[AccCount + 1];
                for (int i = 0; i < AccCount; i++)
                {
                    Accs[i] = prObjs[i];
                }
                Accs[AccCount] = obj;
                AccCount++;
            }

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object, and choosing it for the ElecDirection of the current
            if (dot.GetComponent<DotObjects>().Objects.Length > 2)
            {
                for (int i = 0; i < dot.GetComponent<DotObjects>().Objects.Length; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i].tag == "Crossing")
                    {
                        obj.GetComponent<ObjParametres>().ElecNum = elecNum;
                        obj.GetComponent<ObjParametres>().DirectTo = dot.GetComponent<DotObjects>().Objects[i];
                        end = true;
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        obj.GetComponent<ObjParametres>().ElecNum = elecNum;
                        obj.GetComponent<ObjParametres>().DirectTo = dot.GetComponent<DotObjects>().Objects[i];
                        obj = dot.GetComponent<DotObjects>().Objects[i];
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                obj.GetComponent<ObjParametres>().ElecNum = elecNum;
                obj.GetComponent<ObjParametres>().DirectTo = obj;
                end = true;
            }
        }
    }

    void FirstLawCalc() 
    {
        for (int i1 = 0; i1 < (GetComponent<OccupiedDots>().crossingDots.Length - 1); i1++) 
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].tag != "Crossing")
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().DirectTo.tag == "Crossing")
                    {
                        EquValues.m[i1, GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum] = 1f;
                    }
                    else
                    {
                        EquValues.m[i1, GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum] = -1f;
                    }
                }
            }
        }
    }

    GameObject[][] DotsForContours;
    int[][] ElecNums;
    int AccNum = 0;
    int ContourNum = 0;
    void ChooseContours()
    {
        DotsForContours = new GameObject[(EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1))][];
        ElecNums = new int[(EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1))][];

        DotsForContours[0] = new GameObject[1] { FindStart() };
        ElecNums[0] = new int[1] { Accs[AccNum].GetComponent<ObjParametres>().ElecNum };

        FindNextDot(DotsForContours[0][0], 1);
    }
    GameObject FindStart() 
    {
        //Looking for the start
        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++)
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].tag != "Crossing")
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum == Accs[AccNum].GetComponent<ObjParametres>().ElecNum)
                    {
                        return GetComponent<OccupiedDots>().crossingDots[i1];
                    }
                }
            }
        }

        return null;
    }
    bool FindNextDot(GameObject firstDot, int segmentNum) 
    {
        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++)
        {
            //Search if this dot is already in the list
            if (!Presence(GetComponent<OccupiedDots>().crossingDots[i1]))
            {
                for (int i2 = 0; i2 < firstDot.GetComponent<DotObjects>().Objects.Length; i2++)
                {
                    if (firstDot.GetComponent<DotObjects>().Objects[i2].tag != "Crossing")
                    {
                        if (firstDot.GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum != Accs[AccNum].GetComponent<ObjParametres>().ElecNum)
                        {
                            for (int i3 = 0; i3 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i3++)
                            {
                                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i3].tag != "Crossing")
                                {

                                    if (firstDot.GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum == GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().ElecNum)
                                    {

                                        //Adding in the list dot and num
                                        AddDotAndNum(GetComponent<OccupiedDots>().crossingDots[i1], GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().ElecNum, segmentNum);

                                        if (End(GetComponent<OccupiedDots>().crossingDots[i1]))
                                        {
                                            if (ContourNum == ((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1)))
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                DotsForContours[ContourNum + 1] = DotsForContours[ContourNum];
                                                ElecNums[ContourNum + 1] = ElecNums[ContourNum];
                                                ContourNum++;

                                                DeleteLastDotAndNum();
                                            }
                                        }
                                        else
                                        {
                                            //If there is no dots further, delete last dot and num and search another path
                                            if (!FindNextDot(GetComponent<OccupiedDots>().crossingDots[i1], segmentNum + 1))
                                            {
                                                DeleteLastDotAndNum();
                                            }
                                            else
                                            {
                                                return true;
                                            }
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        
        return false;
    }
    void AddDotAndNum(GameObject Dot, int elecNum, int segmentNum) 
    {
        GameObject[] prDots = DotsForContours[ContourNum];
        int[] prNums = ElecNums[ContourNum];
        DotsForContours[ContourNum] = new GameObject[segmentNum + 1];
        ElecNums[ContourNum] = new int[segmentNum + 1];
        for (int i = 0; i < segmentNum; i++)
        {
            DotsForContours[ContourNum][i] = prDots[i];
            ElecNums[ContourNum][i] = prNums[i];
        }
        DotsForContours[ContourNum][segmentNum] = Dot;
        ElecNums[ContourNum][segmentNum] = elecNum;
    }
    void DeleteLastDotAndNum()
    {
        GameObject[] prDots = DotsForContours[ContourNum];
        int[] prNums = ElecNums[ContourNum];
        DotsForContours[ContourNum] = new GameObject[DotsForContours[ContourNum].Length - 1];
        ElecNums[ContourNum] = new int[ElecNums[ContourNum].Length - 1];
        for (int i = 0; i < DotsForContours[ContourNum].Length; i++)
        {
            DotsForContours[ContourNum][i] = prDots[i];
            ElecNums[ContourNum][i] = prNums[i];
        }
    }
    bool Presence(GameObject dot) 
    {
        for (int i = 0; i < DotsForContours[ContourNum].Length; i++)
        {
            if (DotsForContours[ContourNum][i] == dot)
            {
                return true;
            }
        }

        return false;
    }
    bool End(GameObject Dot) 
    {
        for (int i = 0; i < Dot.GetComponent<DotObjects>().Objects.Length; i++) 
        {
            if (Dot.GetComponent<DotObjects>().Objects[i].tag != "Crossing")
            {
                if (Dot.GetComponent<DotObjects>().Objects[i].GetComponent<ObjParametres>().ElecNum == Accs[AccNum].GetComponent<ObjParametres>().ElecNum)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void SecondLawCalc() 
    {
        //Determining resistances signs
        for (int i1 = 0; i1 < ContourNum + 1; i1++) 
        {
            for (int i2 = 0; i2 < DotsForContours[i1].Length; i2++)
            {
                for (int i3 = 0; i3 < DotsForContours[i1][i2].GetComponent<DotObjects>().Objects.Length; i3++)
                {
                    if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].tag != "Crossing")
                    {
                        if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().ElecNum == ElecNums[i1][i2])
                        {
                            int invert;

                            if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().DirectTo.tag == "Crossing")
                            {
                                EquValues.m[((GetComponent<OccupiedDots>().crossingDots.Length - 1) + i1), ElecNums[i1][i2]] = Resistances[ElecNums[i1][i2]];
                                invert = 1;
                            }
                            else
                            {
                                EquValues.m[((GetComponent<OccupiedDots>().crossingDots.Length - 1) + i1), ElecNums[i1][i2]] = -Resistances[ElecNums[i1][i2]];
                                invert = -1;
                            }

                            //Determining voltages signs
                            for (int i4 = 0; i4 < Accs.Length; i4++)
                            {
                                if (Accs[i4].GetComponent<ObjParametres>().ElecNum == ElecNums[i1][i2])
                                {
                                    if (Codirectional(Accs[i4]))
                                    {
                                        Result.m[((GetComponent<OccupiedDots>().crossingDots.Length - 1) + i1), 0] += Accs[i4].GetComponent<ObjParametres>().Voltage * invert;
                                    }
                                    else
                                    {
                                        Result.m[((GetComponent<OccupiedDots>().crossingDots.Length - 1) + i1), 0] -= Accs[i4].GetComponent<ObjParametres>().Voltage * invert;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
    GameObject commonDot;
    bool Codirectional(GameObject acc) 
    {
        Vector3 direction;
        Vector3 rotation;

        //Find common dot of directto and acc
        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().occupiedDots.Length; i1++) 
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects[i2] == acc) 
                {
                    for (int i3 = 0; i3 < GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects.Length; i3++)
                    {
                        if (GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects[i3] == acc.GetComponent<ObjParametres>().DirectTo)
                        {
                            commonDot = GetComponent<OccupiedDots>().occupiedDots[i1];

                            i1 = GetComponent<OccupiedDots>().occupiedDots.Length;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        direction = (commonDot.transform.position - acc.transform.position).normalized;

        //Rounding
        direction = new Vector3(Mathf.Round(direction.x * 100.0f) * 0.01f, 0, Mathf.Round(direction.z * 100.0f) * 0.01f);
        rotation = new Vector3(Mathf.Round(acc.transform.forward.x * 100.0f) * 0.01f, 0, Mathf.Round(acc.transform.forward.z * 100.0f) * 0.01f);

        return (direction == rotation);
    }

    void GetElectricity() 
    {
        //Massive where showed if electricity was spread in the path
        bool[] spread = new bool[ElecValues.arr];
        for (int i = 0; i < spread.Length; i++) 
        {
            spread[i] = false;
        }

        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++)
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].tag != "Crossing")
                {
                    if (!spread[GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum])
                    {
                        GetPathElectricity(GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2], GetComponent<OccupiedDots>().crossingDots[i1], ElecValues.m[GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum, 0]);
                        
                        spread[GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum] = true;
                    }
                }
            }
        }
    }
    void GetPathElectricity(GameObject obj, GameObject crossingDot, float elecVal)
    {
        bool end = false;
        GameObject dot;

        dot = crossingDot;

        while (!end)
        {
            obj.GetComponent<ObjParametres>().ElecVal = elecVal;

            //Updating values on devices
            if (obj.tag == "Ammeter") 
            {
                obj.transform.Find("Canvas").transform.Find("Amper").GetComponent<Text>().text = Math.Abs(elecVal).ToString();
            }
            if (obj.tag == "Voltmeter")
            {
                obj.transform.Find("Canvas").transform.Find("Volt").GetComponent<Text>().text = (Math.Abs(elecVal) * obj.GetComponent<ObjParametres>().Resistance).ToString();
            }

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object
            if (dot.GetComponent<DotObjects>().Objects.Length > 2)
            {
                for (int i = 0; i < dot.GetComponent<DotObjects>().Objects.Length; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i].tag == "Crossing")
                    {
                        end = true;
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        obj = dot.GetComponent<DotObjects>().Objects[i];
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                end = true;
            }

        }
    }

    void GetSimpleElectricity()
    {
        bool end = false;
        GameObject start;
        
        GameObject dot;
        GameObject obj;

        float resistance = 0;
        float voltage = 0;

        //Getting start dot
        start = GetComponent<OccupiedDots>().occupiedDots[0];
        dot = start;

        //Getting start object
        obj = GetComponent<OccupiedDots>().occupiedDots[0].GetComponent<DotObjects>().Objects[0];

        while (!end)
        {
            obj.GetComponent<ObjParametres>().ElecNum = 0;

            //Counting voltages
            if (obj.tag == "Accumulator")
            {
                Vector3 direction;
                Vector3 rotation;

                direction = (dot.transform.position - obj.transform.position).normalized;

                //Rounding
                direction = new Vector3(Mathf.Round(direction.x * 100.0f) * 0.01f, 0, Mathf.Round(direction.z * 100.0f) * 0.01f);
                rotation = new Vector3(Mathf.Round(obj.transform.forward.x * 100.0f) * 0.01f, 0, Mathf.Round(obj.transform.forward.z * 100.0f) * 0.01f);

                if (direction == rotation)
                {
                    voltage += obj.GetComponent<ObjParametres>().Voltage;
                }
                else
                {
                    voltage -= obj.GetComponent<ObjParametres>().Voltage;
                }
            }
            
            //Counting resistance
            resistance += obj.GetComponent<ObjParametres>().Resistance;

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object, and choosing it for the ElecDirection of the current
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        obj.GetComponent<ObjParametres>().DirectTo = dot.GetComponent<DotObjects>().Objects[i];

                        if (dot == start)
                        {
                            end = true;
                        }
                        else
                        {
                            obj = dot.GetComponent<DotObjects>().Objects[i];
                        }
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                obj.GetComponent<ObjParametres>().DirectTo = obj;
                end = true;
            }
        }
        end = false;

        //Ohm's law
        float current = Math.Abs(voltage / resistance);

        while (!end)
        {
            obj.GetComponent<ObjParametres>().ElecVal = current;

            //Updating values on devices
            if (obj.tag == "Ammeter")
            {
                obj.transform.Find("Canvas").transform.Find("Amper").GetComponent<Text>().text = Math.Abs(current).ToString();
            }
            if (obj.tag == "Voltmeter")
            {
                obj.transform.Find("Canvas").transform.Find("Volt").GetComponent<Text>().text = (Math.Abs(current) * obj.GetComponent<ObjParametres>().Resistance).ToString();
            }

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object, and choosing it for the ElecDirection of the current
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        if (dot == start)
                        {
                            end = true;
                        }
                        else
                        {
                            obj = dot.GetComponent<DotObjects>().Objects[i];
                        }
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                end = true;
            }
        }
    }

    public void ResetVars() 
    {
        if (GetComponent<OccupiedDots>().crossingDots.Length >= 2 && AccCount != 0)
        {
            Resistances = null;
            Accs = null;

            pathResist = 0;
            AccCount = 0;

            for (int i = 0; i < ContourNum + 1; i++)
            {
                DotsForContours[i] = null;
                ElecNums[i] = null;
            }
            DotsForContours = null;
            ElecNums = null;
            AccNum = 0;
            ContourNum = 0;

            ResetLines();
        }
        if (GetComponent<OccupiedDots>().crossingDots.Length == 0)
        {
            ResetSimpleContour();
        }
    }
    void ResetLines()
    {
        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++)
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].tag != "Crossing")
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum != -1)
                    {
                        ResetLine(GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2], GetComponent<OccupiedDots>().crossingDots[i1]);
                    }
                }
            }
        }
    }
    void ResetLine(GameObject obj, GameObject crossingDot)
    {
        bool end = false;
        GameObject dot;

        dot = crossingDot;

        while (!end)
        {
            obj.GetComponent<ObjParametres>().ElecVal = 0;
            obj.GetComponent<ObjParametres>().ElecNum = -1;

            //Updating values on devices
            if (obj.tag == "Ammeter")
            {
                obj.transform.Find("Canvas").transform.Find("Amper").GetComponent<Text>().text = Math.Abs(0).ToString();
            }
            if (obj.tag == "Voltmeter")
            {
                obj.transform.Find("Canvas").transform.Find("Volt").GetComponent<Text>().text = Math.Abs(0).ToString();
            }

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object
            if (dot.GetComponent<DotObjects>().Objects.Length > 2)
            {
                for (int i = 0; i < dot.GetComponent<DotObjects>().Objects.Length; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i].tag == "Crossing")
                    {
                        end = true;
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        obj = dot.GetComponent<DotObjects>().Objects[i];
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                end = true;
            }

        }
    }
    void ResetSimpleContour()
    {
        bool end = false;
        GameObject start;

        GameObject dot;
        GameObject obj;

        //Getting start dot
        start = GetComponent<OccupiedDots>().occupiedDots[0];
        dot = start;

        //Getting start object
        obj = GetComponent<OccupiedDots>().occupiedDots[0].GetComponent<DotObjects>().Objects[0];

        while (!end)
        {
            obj.GetComponent<ObjParametres>().ElecNum = -1;
            obj.GetComponent<ObjParametres>().ElecVal = 0;

            //Updating values on devices
            if (obj.tag == "Ammeter")
            {
                obj.transform.Find("Canvas").transform.Find("Amper").GetComponent<Text>().text = Math.Abs(0).ToString();
            }
            if (obj.tag == "Voltmeter")
            {
                obj.transform.Find("Canvas").transform.Find("Volt").GetComponent<Text>().text = Math.Abs(0).ToString();
            }

            //Finding next dot
            Vector3 normVec = (obj.transform.position - dot.transform.position).normalized;
            Vector3 nextPos;
            if (Math.Abs(normVec.x) <= 0.1 || Math.Abs(normVec.z) <= 0.1)
            {
                nextPos = dot.transform.position + normVec;
            }
            else
            {
                nextPos = dot.transform.position + new Vector3((normVec.x > 0 ? 0.5f : -0.5f), 0, (normVec.z > 0 ? 0.5f : -0.5f));
            }
            for (int i = 0; i < GetComponent<OccupiedDots>().occupiedDots.Length; i++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i].transform.position == nextPos)
                {
                    dot = GetComponent<OccupiedDots>().occupiedDots[i];
                    break;
                }
            }

            //Finding next object, and choosing it for the ElecDirection of the current
            if (dot.GetComponent<DotObjects>().Objects.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (dot.GetComponent<DotObjects>().Objects[i] != obj)
                    {
                        if (dot == start)
                        {
                            end = true;
                        }
                        else
                        {
                            obj = dot.GetComponent<DotObjects>().Objects[i];
                        }
                        break;
                    }
                }
            }
            if (dot.GetComponent<DotObjects>().Objects.Length == 1)
            {
                end = true;
            }
        }
    }
}


/*if (i == GetComponent<OccupiedDots>().occupiedDots.Length - 1) 
                {
                    print(obj.transform.position);
                    print(dot.transform.position);
                    print(nextPos);////////////////////////////
                    end = true;
                }*/
/*if (obj.tag == "Wire")
{
    if (Math.Abs(obj.transform.up.x) <= 0.1 || Math.Abs(obj.transform.up.z) <= 0.1)
    {
        nextPos = dot.transform.position + obj.transform.up;
    }
    else
    {
        nextPos = dot.transform.position + new Vector3((obj.transform.up.x > 0 ? 0.5f : -0.5f), 0, (obj.transform.up.z > 0 ? 0.5f : -0.5f));
    }
}
else
{
    if (Math.Abs(obj.transform.forward.x) <= 0.1 || Math.Abs(obj.transform.forward.z) <= 0.1)
    {
        nextPos = dot.transform.position + obj.transform.forward;
    }
    else
    {
        nextPos = dot.transform.position + new Vector3((obj.transform.forward.x > 0 ? 0.5f : -0.5f), 0, (obj.transform.forward.z > 0 ? 0.5f : -0.5f));
    }
}*/
/*
if (AccPresence)
    {
        int[] prPaths = PathsWithAcc;
        PathsWithAcc = new int[PathsWithAcc.Length + 1];
        for (int i = 0; i < (PathsWithAcc.Length - 1); i++)
        {
            PathsWithAcc[i] = prPaths[i];
        }
        PathsWithAcc[PathsWithAcc.Length - 1] = count;
        AccPresence = false;
     }
*/
/*
 void ChoosingContours() 
    {
        int AccsCount = 0;
        GameObject start;

        DotsForContour = new GameObject[(EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1))][];

        for (int i = 0; i < (EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1)); i++)
        {
            //Looking for the start
            for (int i1 = 0; i1 < GetComponent<OccupiedDots>().crossingDots.Length; i1++)
            {
                for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i1].GetComponent<DotObjects>().Objects[i2].GetComponent<ObjParametres>().ElecNum == Accs[AccsCount].GetComponent<ObjParametres>().ElecNum)
                    {
                        start = GetComponent<OccupiedDots>().crossingDots[i1];

                        //Adding in the list
                        DotsForContour[i] = new GameObject[1];
                        DotsForContour[i][0] = start;

                        //Choose dots for contour
                        ChoosingDotsForContour(start, i);

                        i1 = GetComponent<OccupiedDots>().crossingDots.Length;
                        break;
                    }
                }
            }
        }
    }
    void ChoosingDotsForContour(GameObject start, int contNum)
    {
        bool end = false;

        GameObject dot = start;

        while (!end)
        {
            //Looking for another dot with the common line
            for (int i1 = 0; i1 < dot.GetComponent<DotObjects>().Objects.Length; i1++)
            {
                for (int i2 = 0; i2 < GetComponent<OccupiedDots>().crossingDots.Length; i2++)
                {
                    if (GetComponent<OccupiedDots>().crossingDots[i2] != dot)
                    {
                        for (int i3 = 0; i3 < GetComponent<OccupiedDots>().crossingDots[i2].GetComponent<DotObjects>().Objects.Length; i3++)
                        {
                            if (dot.GetComponent<DotObjects>().Objects[i1].GetComponent<ObjParametres>().ElecNum == GetComponent<OccupiedDots>().crossingDots[i2].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().ElecNum)
                            {
                                //Check if this dot is in the list
                                for (int i4 = 0; i4 < DotsForContour[contNum].Length; i4++) 
                                {
                                    if (DotsForContour[contNum][i4] == GetComponent<OccupiedDots>().crossingDots[i2]) 
                                    {
                                        break;
                                    }
                                    if (i4 == DotsForContour[contNum].Length - 1) 
                                    {
                                        dot = GetComponent<OccupiedDots>().crossingDots[i2];

                                        //Adding in the list
                                        GameObject[] prObjs = DotsForContour[contNum];
                                        DotsForContour[contNum] = new GameObject[DotsForContour[contNum].Length + 1];
                                        for (int i5 = 0; i5 < (DotsForContour[contNum].Length - 1); i5++)
                                        {
                                            DotsForContour[contNum][i5] = prObjs[i5];
                                        }
                                        DotsForContour[contNum][DotsForContour[contNum].Length - 1] = dot;

                                        i1 = 100;
                                        i2 = GetComponent<OccupiedDots>().crossingDots.Length;
                                    }
                                }
                                
                                break;
                            }
                        }
                    }
                }
            }
        }
    }*/
/*bool end = false;
        while (!end)
        {
            if (FindNextDot(FindStart(), 1))
            {
                end = true;
            }
            print("cycle");
        }*/
/*

        for (int i = 0; i < ContourNum + 1; i++) 
        {
            for (int i2 = 0; i2 < ElecNums[i].Length; i2++)
            {
                print(ElecNums[i][i2]);
            }
            print("line");
        }*/
/*void SecondLawCalc() 
    {
        //Determining resistances signs
        for (int i1 = 0; i1 < ContourNum + 1; i1++) 
        {
            for (int i2 = 0; i2 < DotsForContours[i1].Length; i2++)
            {
                for (int i3 = 0; i3 < DotsForContours[i1][i2].GetComponent<DotObjects>().Objects.Length; i3++)
                {
                    if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].tag != "Crossing")
                    {
                        if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().ElecNum == ElecNums[i1][i2])
                        {
                            if (DotsForContours[i1][i2].GetComponent<DotObjects>().Objects[i3].GetComponent<ObjParametres>().DirectTo.tag == "Crossing")
                            {
                                EquValues.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), ElecNums[i1][i2]] = Resistances[ElecNums[i1][i2]];
                            }
                            else
                            {
                                EquValues.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), ElecNums[i1][i2]] = -Resistances[ElecNums[i1][i2]];
                            }
                        }
                    }
                }
            }
        }

        //Determining voltages signs
        for (int i1 = 0; i1 < ContourNum + 1; i1++)
        {
            for (int i2 = 0; i2 < ElecNums[i1].Length; i2++)
            {
                for (int i3 = 0; i3 < Accs.Length; i3++)
                {
                    if (Accs[i3].GetComponent<ObjParametres>().ElecNum == ElecNums[i1][i2]) 
                    {
                        if (Codirectional(Accs[i3]))
                        {
                            Result.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), 0] += Accs[i3].GetComponent<ObjParametres>().Voltage;
                        }
                        else 
                        {
                            Result.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), 0] -= Accs[i3].GetComponent<ObjParametres>().Voltage;
                        }
                    }
                }
            }
        }
    }
    GameObject commonDot;
    bool Codirectional(GameObject acc) 
    {
        Vector3 direction;
        Vector3 rotation;

        //Find common dot of directto and acc
        for (int i1 = 0; i1 < GetComponent<OccupiedDots>().occupiedDots.Length; i1++) 
        {
            for (int i2 = 0; i2 < GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects.Length; i2++)
            {
                if (GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects[i2] == acc) 
                {
                    for (int i3 = 0; i3 < GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects.Length; i3++)
                    {
                        if (GetComponent<OccupiedDots>().occupiedDots[i1].GetComponent<DotObjects>().Objects[i3] == acc.GetComponent<ObjParametres>().DirectTo)
                        {
                            commonDot = GetComponent<OccupiedDots>().occupiedDots[i1];

                            i1 = GetComponent<OccupiedDots>().occupiedDots.Length;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        direction = (commonDot.transform.position - acc.transform.position).normalized;

        //Rounding
        direction = new Vector3(Mathf.Round(direction.x * 100.0f) * 0.01f, 0, Mathf.Round(direction.z * 100.0f) * 0.1f);
        rotation = new Vector3(Mathf.Round(acc.transform.forward.x * 100.0f) * 0.1f, 0, Mathf.Round(acc.transform.forward.z * 100.0f) * 0.1f);

        return (direction == rotation);
    }*/
/*//Determining voltages signs
        for (int i1 = 0; i1 < ContourNum + 1; i1++)
        {
            for (int i2 = 0; i2 < ElecNums[i1].Length; i2++)
            {
                for (int i3 = 0; i3 < Accs.Length; i3++)
                {
                    if (Accs[i3].GetComponent<ObjParametres>().ElecNum == ElecNums[i1][i2]) 
                    {
                        if (Codirectional(Accs[i3]))
                        {
                            Result.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), 0] += Accs[i3].GetComponent<ObjParametres>().Voltage;
                        }
                        else 
                        {
                            Result.m[((EquValues.arr - (GetComponent<OccupiedDots>().crossingDots.Length - 1) - 1) + i1), 0] -= Accs[i3].GetComponent<ObjParametres>().Voltage;
                        }
                    }
                }
            }
        }*/
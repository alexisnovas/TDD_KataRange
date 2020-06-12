using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Range_Exercise
{
    public class Range
    {
        public string interval;
        public enum side {Opened, Closed, Wrong}
        private side rightside;
        private side leftside;
        private string[] arrTempInterval = new string[2];
        public int leftNumber = 0;
        public int rightNumber = 0;
        private List<int> listOfNumbers = new List<int>();


        public Range(string interval)
        {
            this.interval = interval.Trim().Replace(" ","");
            interval = interval.Trim().Replace(" ","");

            //Validar el lado izquierdo
            if (interval.StartsWith('('))
                leftside = side.Opened;
            else if (interval.StartsWith('['))
                leftside = side.Closed;
            else
            {
                leftside = side.Wrong;
                throw new NotImplementedException("Se ha ingresado un intervalo invalido");
            }


            //Validar el lado derecho
            if (interval.EndsWith(')'))
                rightside = side.Opened;
            else if (interval.EndsWith(']'))
                rightside = side.Closed;
            else
            {
                rightside = side.Wrong;
                throw new NotImplementedException("Se ha ingresado un intervalo invalido");
            }

            for (int i = 0; i < interval.Length; i++)
            {
                if (char.IsLetter(interval[i]))
                {
                    throw new NotImplementedException("Se ha ingresado un intervalo que contiene letras.");
                }
            }

            interval = interval.Remove(0, 1);
            interval = interval.Remove(interval.Length - 1);

            arrTempInterval = interval.Split(',');

            try
            {
                leftNumber = int.Parse(arrTempInterval[0]);
                rightNumber = int.Parse(arrTempInterval[1]);
            }
            catch (Exception)
            {

                throw new NotImplementedException("Se ha ingresado un intervalo invalido, los limites no son enteros");
            }

            //Validar que leftNumber sea menor que rightNumber
            if (leftNumber < rightNumber)
            {
                int tempLeft = leftNumber;
                int tempRight = rightNumber;

                if (leftside == side.Opened && rightside == side.Opened)
                {
                    for (int i = 0; i < (rightNumber - leftNumber) - 1; i++)
                    {
                        tempLeft = tempLeft + 1;
                        listOfNumbers.Add(tempLeft);
                    }

                }
                else if (leftside == side.Opened && rightside == side.Closed)
                {
                    for (int i = 0; i < (rightNumber - leftNumber); i++)
                    {
                        tempLeft = tempLeft + 1;
                        listOfNumbers.Add(tempLeft);
                    }
                }
                else if (leftside == side.Closed && rightside == side.Opened)
                {
                    for (int i = 0; i < (rightNumber - leftNumber); i++)
                    {
                        listOfNumbers.Add(tempLeft);
                        tempLeft = tempLeft + 1;
                    }
                }
                else if (leftside == side.Closed && rightside == side.Closed)
                {
                    for (int i = 0; i < (rightNumber - leftNumber) + 1; i++)
                    {
                        listOfNumbers.Add(tempLeft);
                        tempLeft = tempLeft + 1;
                    }
                }

            }
            else
            {
                throw new NotImplementedException("El limite izquierdo es mayor que el derecho");
            }


        }

        public List<int> AllPoints()
        {
            return listOfNumbers;
        }

        public bool Contains(int SomeNumber)
        {
            if (listOfNumbers.Contains(SomeNumber))
            {
                return true;
            }
            else
                return false;
        }

        public bool DoesNotContains(int someNumber)
        {
            if (!listOfNumbers.Contains(someNumber))
                return true;
            else
                return false;
        }

        public bool OverlapsRange(Range r)
        {
            List<int> tempList = r.AllPoints();
            List<int> result = listOfNumbers.Intersect(tempList).ToList();

            if (result.Count != 0)
                return true;
            else
                return false;
        }

        public bool ContainsRange(Range r)
        {
            List<int> tempList = r.AllPoints();
            List<int> result = listOfNumbers.Intersect(tempList).ToList();

            if (result.Count == tempList.Count)
                return true;
            else
                return false;
        }

        public bool DoesNotContainsRange(Range r)
        {
            List<int> tempList = r.AllPoints();
            List<int> result = listOfNumbers.Intersect(tempList).ToList();

            if (result.Count != tempList.Count)
                return true;
            else
                return false;
        }

        public int[] EndPoints()
        {
            int[] endPointsArr = new int[2];

            if(leftside == side.Opened && rightside == side.Opened)
            {
                endPointsArr[0] = leftNumber + 1;
                endPointsArr[1] = rightNumber - 1;
            }
            else if (leftside == side.Opened && rightside == side.Closed)
            {
                endPointsArr[0] = leftNumber + 1;
                endPointsArr[1] = rightNumber;
            }
            else if (leftside == side.Closed && rightside == side.Opened)
            {
                endPointsArr[0] = leftNumber;
                endPointsArr[1] = rightNumber -1;
            }
            else if (leftside == side.Closed && rightside == side.Closed)
            {
                endPointsArr[0] = leftNumber;
                endPointsArr[1] = rightNumber;
            }

            return endPointsArr;
        }

        public bool Equals(Range r) 
        {
            if (interval == r.interval)
                return true;
            else
                return false;
        }

        public bool NotEquals(Range r)
        {
            if (interval != r.interval)
                return true;
            else
                return false;
        }

    }
}

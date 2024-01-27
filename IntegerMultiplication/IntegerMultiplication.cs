using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            byte[] X_B = new byte[(N / 2)];
            byte[] X_A = new byte[(N / 2)];
            byte[] Y_C = new byte[(N / 2)];
            byte[] Y_D = new byte[(N / 2)];
            byte[] M2 = new byte[2*N];
            byte[] M1 = new byte[2*N];
            byte[] ABCD = new byte[2 * N];

            // Base cases
            if (N == 2)
            {
                byte[] res_f = new byte[(2 * N)];
                byte[] res_s = new byte[(2 * N)];
                int carry;
                res_f[0] = (byte)(((int)X[0] * (int)Y[0]) % 10);
                carry = (X[0] * Y[0]) / 10;
                res_f[1] = (byte)((((int)X[1] * (int)Y[0]) + (int)carry) % 10);
                res_f[2] = (byte)((((int)X[1] * (int)Y[0]) + (int)carry) / 10);
                res_f[3] = 0;

                carry = 0;
                res_s[0] = 0;
                res_s[1] = (byte)(((int)X[0] * (int)Y[1]) % 10);
                carry = (X[0] * Y[1]) / 10;
                res_s[2] = (byte)((((int)X[1] * (int)Y[1]) + (int)carry) % 10);
                res_s[3] = (byte)((((int)X[1] * (int)Y[1]) + (int)carry) / 10);

                byte[] add = new byte[2 * N];
                carry = 0;
                add[0] = (byte)(((int)res_f[0] + (int)res_s[0]) % 10);
                carry = ((int)res_f[0] + (int)res_s[0]) / 10;
                add[1] = (byte)(((int)res_f[1] + (int)res_s[1] + carry) % 10);
                carry = (((int)res_f[1] + (int)res_s[1]) + carry) / 10;
                add[2] = (byte)(((int)res_f[2] + (int)res_s[2] + carry) % 10);
                carry = (((int)res_f[2] + (int)res_s[2]) + carry) / 10;
                add[3] = (byte)((int)res_f[3] + (int)res_s[3] + carry);
                return add;
            }

            X_B = X.Skip(N / 2).ToArray();
            X_A = X.Take(N / 2).ToArray();
            Y_D = Y.Skip(N / 2).ToArray();
            Y_C = Y.Take(N / 2).ToArray();

            if (X_A.GetLength(0) % 2 != 0)
            {
                X_A = X_A.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (X_B.GetLength(0) % 2 != 0)
            {
                X_B = X_B.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (Y_D.GetLength(0) % 2 != 0)
            {
                Y_D = Y_D.Concat(new byte[] { (byte)0 }).ToArray();                
            }
            if (Y_C.GetLength(0) % 2 != 0)
            {
                Y_C = Y_C.Concat(new byte[] { (byte)0 }).ToArray();
            }
            int length_BD = Math.Max(X_B.GetLength(0), Y_D.GetLength(0));
            if (length_BD > X_B.GetLength(0))
            {
                for (int i = X_B.GetLength(0); i < length_BD; i++)
                    X_B = X_B.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (length_BD > Y_D.GetLength(0))
            {
                for (int i = Y_D.GetLength(0); i < length_BD; i++)
                    Y_D = Y_D.Concat(new byte[] { (byte)0 }).ToArray();
            }

            int length_AC = Math.Max(X_A.GetLength(0), Y_C.GetLength(0));
            if (length_AC > X_A.GetLength(0))
            {
                for (int i = X_A.GetLength(0); i < length_AC; i++)
                    X_A = X_A.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (length_AC > Y_C.GetLength(0))
            {
                for (int i = Y_C.GetLength(0); i < length_AC; i++)
                    Y_C = Y_C.Concat(new byte[] { (byte)0 }).ToArray();
            }

            

            // Recursive
            if (X_A.GetLength(0) > (N / 2))
            {
                M1 = IntegerMultiply(X_A, Y_C, X_A.GetLength(0));
            }
            else
            {
                M1 = IntegerMultiply(X_A, Y_C, N / 2);
            }
            // Remove leading zeros from m1
            int k;
            for (k = N - 1; k >= 0 && M1[k] == 0; k--) { }
            byte[] MM1 = new byte[k + 1];
            Array.Copy(M1, MM1, k + 1);

            if (X_B.GetLength(0) > (N / 2))
            {
                M2 = IntegerMultiply(X_B, Y_D, X_B.GetLength(0));
            }
            else
            {
                M2 = IntegerMultiply(X_B, Y_D, N / 2);
            }
            // Remove leading zeros from m2
            int j;
            for (j = N - 1; j >= 0 && M2[j] == 0; j--) { }
            byte[] MM2 = new byte[j + 1];
            Array.Copy(M2, MM2, j + 1);
            
            byte[] AB = AddFun(X_A, X_B);
            byte[] CD = AddFun(Y_C, Y_D);
            int length = Math.Max(AB.GetLength(0), CD.GetLength(0));
            if (length > AB.GetLength(0))
            {
                for (int i = AB.GetLength(0); i < length; i++)
                    AB = AB.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (length > CD.GetLength(0))
            {
                for (int i = CD.GetLength(0); i < length; i++)
                    CD = CD.Concat(new byte[] { (byte)0 }).ToArray();
            }
            
            if (AB.GetLength(0) % 2 != 0)
            {
                AB = AB.Concat(new byte[] { (byte)0 }).ToArray();
            }
            if (CD.GetLength(0) % 2 != 0)
            {
                CD = CD.Concat(new byte[] { (byte)0 }).ToArray();
            }
            
            if (AB.GetLength(0) > (N / 2))
            {
                ABCD = IntegerMultiply(AB, CD, AB.GetLength(0));
            }
            else
            {
                ABCD = IntegerMultiply(AB, CD, N/2);
            }

            // Combine 
            byte[] Z = new byte[ABCD.GetLength(0)];
            Z = SubtractFun(SubtractFun(ABCD, MM1), MM2);
            
            byte[] new_mat1 = new byte[(2* N)];
            MM2.CopyTo(new_mat1, N);
            byte[] new_mat2 = new byte[(2 * N)];
            Z.CopyTo(new_mat2, (N / 2));
            byte[] new_mat3 = new byte[(2 * N)];
            MM1.CopyTo(new_mat3, 0);

            byte[] final_sum = new byte[2 * N];
            final_sum = AddFun(AddFun(new_mat1, new_mat2), new_mat3);
            return final_sum;
        }
        public static byte[] AddFun(byte[] A, byte[] B)
        {
            int N = Math.Max(A.GetLength(0), B.GetLength(0));
            byte[] result = new byte[N];
            int carry = 0;
            for (int i = 0; i < N; i++)
            {
                int sum = carry;
                if (i < A.GetLength(0)) sum += A[i];
                if (i < B.GetLength(0)) sum += B[i];
                result[i] = (byte)(sum % 10);
                carry = sum / 10;
            }
            if (carry > 0)
            {
                result = result.Concat(new byte[] { (byte)carry }).ToArray();
            }
            return result;
        }

        public static byte[] SubtractFun(byte[] A, byte[] B)
        {
            int N = Math.Max(A.GetLength(0), B.GetLength(0));
            byte[] result = new byte[N];
            int borrow = 0;
            for (int i = 0; i < N; i++)
            {
                int diff = borrow;
                if (i < A.GetLength(0)) diff += A[i];
                if (i < B.GetLength(0)) diff -= B[i];
                if (diff < 0)
                {
                    diff += 10;
                    borrow = -1;
                }
                else
                {
                    borrow = 0;
                }
                result[i] = (byte)diff;
            }
            
            // Remove leading zeros
            int j;
            for (j = N - 1; j >= 0 && result[j] == 0; j--) { }
            byte[] finalResult = new byte[j + 1];
            Array.Copy(result, finalResult, j + 1);
            return finalResult;
        }

        #endregion
    }
}
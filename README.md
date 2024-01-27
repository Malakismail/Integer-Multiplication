Given TWO LARGE positive integers of N digits/each. Each integer is stored in 1D array.
Implement an efficient algorithm based on Karatsuba’s method to multiply them?
NOTES:
• N is power of 2 (i.e. 2, 4, 8, 16, 32… 2i)
• Result MUST be stored in 2×N digits (left padded by 0’s if necessary)
• Least significant digit is stored at index 0 while most significant is stored at index N-1

Complexity
The complexity of your algorithm should be less than O(N2)

Function: Implement it!
static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
IntegerMultiplication.cs includes this method

Examples
EX#1 
X:
9 9 9 9
Y:
9 9 9 9
Res:
D7 D6 D5 D4 D3 D2 D1 D0
9 9 9 8 0 0 0 1

EX#2
X:
0 2 2 2
Y:
0 0 1 1
Res:
D7 D6 D5 D4 D3 D2 D1 D0
0 0 0 0 2 4 4 2

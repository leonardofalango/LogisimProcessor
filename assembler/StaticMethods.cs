using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class StaticMethods
{
    public static byte[] toBin(this byte b, int bits = 4)
    {
        byte[] binary = new byte[bits];
        byte result = b;
        byte r;
        for (byte i = 0; i < bits; i++)
        {
            r = (byte)(result % 2);
            result = (byte)(result / 2);
            binary[i] =  r;
        }

        return binary.Reverse().ToArray();
    }

    public static byte[] toBin(this int b, int bits = 4)
    {
        byte[] binary = new byte[bits];
        int result = b;
        byte r;
        for (int i = 0; i < bits; i++)
        {
            r = (byte)(result % 2);
            result = result / 2;
            binary[i] = (byte) r;
        }

        return binary.Reverse().ToArray();
    }

    public static void arrCopy(this byte[] arr, byte[] returnArr, byte index)
    {
        for (byte i = 0; i < arr.Length; i++, index++)
            returnArr[index] = arr[i];
    }

    public static byte countChar(this string line, char chr)
    {
        byte count = 0;
        for (int i = 0; i < line.Length; i++)
            if (line[i] == chr)
                count++; 
        
        return count;
    }

    public static char convertToHex(this int decimalNumber)
    {
        char chr = new char();

        if (decimalNumber > 10)
        {
            if (decimalNumber == 10)
                chr = 'A';
            else if (decimalNumber == 11)
                chr = 'B';
            else if (decimalNumber == 12)
                chr = 'C';
            else if (decimalNumber == 13)
                chr = 'D';
            else if (decimalNumber == 14)
                chr = 'E';
            else if (decimalNumber == 15)
                chr = 'F';
            else
                chr = '╪';
        }
        
        else chr = decimalNumber.ToString()[0];

        return chr;
    }

    public static byte[] complete(this byte[] bytes)
    {
        byte[] b = new byte[16];

        b.arrCopy(bytes, 0);

        byte start = (byte)(b.Length - bytes.Length);

        for (byte i = start; i < b.Length; i++)
            b[i] = 0;
        return b;
    }
    
    public static byte[] add = new byte[]{
        0,0,0,1,0,0,0,0
    };

    public static byte[] sub = new byte[]{
        0,0,0,1,0,0,0,1
    };

    public static byte[] mul = new byte[]{
        0,0,0,1,0,0,1,0
    };

    public static byte[] div = new byte[]{
        0,0,0,1,0,0,1,1
    };

    public static byte[] leftShift = new byte[]{
        0,0,0,1,0,1,0,0
    };

    public static byte[] rightShift = new byte[]{
        0,0,0,1,0,1,0,1
    };

    public static byte[] and = new byte[]{
        0,0,0,0,1,1,1,1
    };

    public static byte[] or = new byte[]{
        0,0,0,1,0,0,0,0
    };

    public static byte[] not = new byte[]{
        0,0,0,1,0,0,0,1
    };

    public static byte[] movconst = new byte[]{
        0,0,1,1
    };

    public static byte[] mov = new byte[]{
        0,0,0,1,0,0,0,0
    };

    public static byte[] movstore = new byte[]{
        0,0,1,0,0,0,1,0
    };

    public static byte[] moveload = new byte[]{
        0,0,1,0,0,0,0,1
    };
    
    public static byte[] je = new byte[]{
        1,0,0,1
    };

    public static byte[] inc = new byte[]{
        0,0,0,1,1,0,1,1
    };
    
    public static byte[] cmpReg = new byte[]{
        0,1,0,0
    };
    
    public static byte[] cmp = new byte[]{
        0,1,0,1
    };
    
    public static byte[] push = new byte[]{
        0,0,1,0,0,0,1,1
    };
    
    public static byte[] pop = new byte[]{
        0,0,1,0,0,1,0,0
    };

    public static byte[] jump = new byte[]{
        1,0,0,0
    };

    public static void labelCheck(string path, List<string> labels, List<int> labelIndexes)
    {
        var reader = new StreamReader(path);
        int lineIndex = 0;

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':')
                {
                    labels.Add(line.Substring(0, i));
                    labelIndexes.Add(lineIndex);
                    lineIndex--;
                }    
            }

            lineIndex++;
        }
    }

}
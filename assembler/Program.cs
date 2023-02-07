using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


List<string> labels = new List<string>();
List<int> labelIndex = new List<int>();
int lineIndex = 0;

// if (args.Length == 0)
// {
//     Console.WriteLine("Você precisa passar um parâmetro para o arquivo a ser montado.");
//     return;
// }

// var filePath = args[0];

// if (!File.Exists(filePath))
// {
//     Console.WriteLine("O arquivo especifiado não existe.");
//     return;
// }
// StreamWriter writer = null;
// StreamReader reader = null;
// try
// {
//     writer = new StreamWriter("memory");
//     writer.WriteLine("v2.0 raw");
//     reader = new StreamReader(filePath);

//     while (!reader.EndOfStream)
//     {
//         string line = reader.ReadLine();
//         line = processLine(line);
//         writer.Write(line);
//         writer.Write(" ");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine("O seguinte erro ocorreu durante o processo:");
//     Console.WriteLine(ex.Message);
// }
// finally
// {
//     reader.Close();
//     writer.Close();
// }

string[] txt = new string[]{"    mov     $1, 255",
    "loop: ",
        "inc     $0",
        "cmp     $0, $1",
        "je      end",
    "jump    loop",
    "end:",
        "jump    end",
};

foreach (var xisde in txt)
    Console.WriteLine(processLine(xisde));

string processLine(string line)
{
    byte[] opCode = new byte[16];

    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");

    if (line.Contains("mov"))
    {
        byte counter = line.countChar('$');
        
        if (counter == 1)
        {
            StaticMethods.mov.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.IndexOf(',') - 1 - line.IndexOf('$')));
            byte constant = byte.Parse(line.Substring(line.IndexOf(',') + 1));

            regA.toBin().arrCopy(opCode, 3);
            constant.toBin().arrCopy(opCode, 7);   
        }
        
        else
        {
            StaticMethods.movconst.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.IndexOf(',') - 1 - line.IndexOf('$')));
            byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

            regA.toBin().arrCopy(opCode, 7);
            regB.toBin().arrCopy(opCode, 11); 
        }
    }
    
    else if (line.Contains("add"))
    {
        StaticMethods.add.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));

        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }
    
    else if (line.Contains("sub"))
    {
        StaticMethods.sub.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }
    
    else if (line.Contains("Imul"))
    {
        StaticMethods.mul.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }
    
    else if (line.Contains("div"))
    {
        StaticMethods.div.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }
    
    else if (line.Contains("lsh"))
    {
        StaticMethods.leftShift.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }
    
    else if (line.Contains("rsh"))
    {
        StaticMethods.rightShift.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        byte regB = byte.Parse(line.Substring(line.IndexOf(',') + 1));

        regA.toBin().arrCopy(opCode, 7);
        regB.toBin().arrCopy(opCode, 11); 
    }

    else if (line.Contains("je"))
    {
        StaticMethods.je.arrCopy(opCode, 0);
        opCode = opCode.complete();
    }
    
    else if (line.Contains("inc"))
    {
        StaticMethods.inc.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1));
        
        regA.toBin().arrCopy(opCode, 7);
    }
    
    else if (line.Contains("cmp"))
    {
        byte counter = line.countChar('$');
        if (counter == 1)
        {
            StaticMethods.cmpReg.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.IndexOf(',') - 1 - line.IndexOf('$')));
            
            byte constant = byte.Parse(line.Substring(line.IndexOf(',') + 1));

            regA.toBin().arrCopy(opCode, 3);
            constant.toBin().arrCopy(opCode, 7);
        }

        else
        {
            StaticMethods.cmpReg.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
            byte regB = byte.Parse(line.Substring((line.IndexOf('$', line.IndexOf('$') + 1) + 1)));
            
            regA.toBin().arrCopy(opCode, 7);
            regB.toBin().arrCopy(opCode, 11); 
        }
    
    }
    
    else if (line.Contains("push"))
    {
        StaticMethods.push.arrCopy(opCode, 0);
        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.Length - 1 - line.IndexOf('$')));
        
        regA.toBin().arrCopy(opCode, 7);
        
        opCode.complete();
    }

    else if (line.Contains("pop"))
    {
        StaticMethods.pop.arrCopy(opCode, 0);
        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.Length - 1 - line.IndexOf('$')));
        
        regA.toBin().arrCopy(opCode, 7);
        
        opCode.complete();
    }
    
    else if (line.Contains("jump"))
    {
        StaticMethods.jump.arrCopy(opCode, 0);
        
        string label = line.Split(" ")[1];

        labelIndex[labels.IndexOf(label + ":")]
            .toBin(12)
            .arrCopy(opCode, 3);
    }
    
    else if (line.Contains("nop"))
        opCode = opCode.complete();
        
    else
    {
        opCode = opCode.complete();
        labels.Add(line.Split(" ")[0]);
        labelIndex.Add(lineIndex);;
        return "";
    }
    lineIndex ++;
    return toHex(opCode);
}

string toHex(byte[] code)
{
    string str = "";
    for (byte i = 0; i < code.Length; i+=4)
    {
        byte[] bits4 = new byte[]{code[i+3], code[i+2], code[i+1], code[i]};
        
        byte pot = 0;
        int decimalConv = 0;

        foreach (var item in bits4)
        {
            decimalConv += item * (int)Math.Pow(2, pot);
            pot++;
        }

        str += $"{decimalConv.convertToHex():0000}";
    }
    return str;
}

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

    public static byte[] mov = new byte[]{
        1,1,1,1
    };

    public static byte[] movconst = new byte[]{
        0,0,0,1,0,1,1,1
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


}
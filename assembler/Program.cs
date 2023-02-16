using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



List<string> labels = new List<string>();
List<int> labelIndex = new List<int>();

// if (args.Length == 0)
// {
//     Console.WriteLine("Você precisa passar um parâmetro para o arquivo a ser montado.");
//     return;
// }

var filePath = "code.asm";

if (!File.Exists(filePath))
{
    Console.WriteLine("O arquivo especifiado não existe.");
    return;
}
StreamWriter writer = null;
StreamReader reader = null;
int lineIndex = 0;
try
{
    writer = new StreamWriter("memory");
    writer.WriteLine("v2.0 raw");
    reader = new StreamReader(filePath);

    StaticMethods.labelCheck(filePath, labels, labelIndex);


    while (!reader.EndOfStream)
    {
        string line = reader.ReadLine();
        line = processLine(line);
        if (line == "label")
            continue;    
        writer.Write(line);
        writer.Write(" ");
        lineIndex++;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"O seguinte erro ocorreu durante o processo na linha {lineIndex}:");
    Console.WriteLine(ex.Message);
}
finally
{
    reader.Close();
    writer.Close();
}

string processLine(string line)
{
    byte[] opCode = new byte[16];

    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");
    line = line.Replace("  ", " ");

    if (line.Contains("mov"))
    {
        byte counter = line.countChar('$');
        
        if (counter == 1)
        {
            StaticMethods.movconst.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.IndexOf(',') - 1 - line.IndexOf('$')));
            byte constant = byte.Parse(line.Substring(line.IndexOf(',') + 1));

            regA.toBin().arrCopy(opCode, 4);
            var debug = constant.toBin(8);
            debug.arrCopy(opCode, 8);

            Console.WriteLine();
        }
        
        else
        {
            byte quantPointer = line.countChar('[');
            if (quantPointer == 0)
            {
                StaticMethods.mov.arrCopy(opCode, 0);

                byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                    line.IndexOf(',') - 1 - line.IndexOf('$')));
                int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

                regA.toBin().arrCopy(opCode, 8);
                regB.toBin().arrCopy(opCode, 12); 
            }

            else
            {
                if (line.IndexOf('[') > line.IndexOf(','))
                {
                    line = line
                        .Replace("[", "")
                        .Replace("]", "");

                    StaticMethods.movstore.arrCopy(opCode, 0);
                    
                    byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                        line.IndexOf(',') - 1 - line.IndexOf('$')));
                    
                    int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

                    regA.toBin().arrCopy(opCode, 8);
                    regB.toBin().arrCopy(opCode, 12);
                }
                
                else
                {
                    line = line
                        .Replace("[", "")
                        .Replace("]", "");

                    StaticMethods.movstore.arrCopy(opCode, 0);
                    
                    byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                        line.IndexOf(',') - 1 - line.IndexOf('$')));
                    
                    int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

                    regA.toBin().arrCopy(opCode, 8);
                    regB.toBin().arrCopy(opCode, 12);
                }
            }

        }
    }
    
    else if (line.Contains("add"))
    {
        StaticMethods.add.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));

        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }
    
    else if (line.Contains("sub"))
    {
        StaticMethods.sub.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }
    
    else if (line.Contains("imul"))
    {
        StaticMethods.mul.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }
    
    else if (line.Contains("div"))
    {
        StaticMethods.div.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

        byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }
    
    else if (line.Contains("lsh"))
    {
        StaticMethods.leftShift.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

        byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }
    
    else if (line.Contains("rsh"))
    {
        StaticMethods.rightShift.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
        int virg = line.IndexOf('$', line.IndexOf('$') + 1);

                    byte regB = byte.Parse(line.Substring(virg + 1));

        regA.toBin().arrCopy(opCode, 8);
        regB.toBin().arrCopy(opCode, 12); 
    }

    else if (line.Contains("je"))
    {
        StaticMethods.je.arrCopy(opCode, 0);

        string label = line.Split(" ").Skip(1).ToArray()[1];

        labelIndex[labels.IndexOf(label)]
            .toBin(12)
            .arrCopy(opCode, 4);
    }
    
    else if (line.Contains("inc"))
    {
        StaticMethods.inc.arrCopy(opCode, 0);

        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1));
        
        regA.toBin().arrCopy(opCode, 8);
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

            regA.toBin().arrCopy(opCode, 4);
            constant.toBin().arrCopy(opCode, 8);
        }

        else
        {
            StaticMethods.cmpReg.arrCopy(opCode, 0);

            byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
            line.IndexOf(',') - 1 - line.IndexOf('$')));
            
            byte regB = byte.Parse(line.Substring((line.IndexOf('$', line.IndexOf('$') + 1) + 1)));
            
            regA.toBin().arrCopy(opCode, 8);
            regB.toBin().arrCopy(opCode, 12); 
        }
    
    }
    
    else if (line.Contains("push"))
    {
        StaticMethods.push.arrCopy(opCode, 0);
        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.Length - 1 - line.IndexOf('$')));
        
        regA.toBin().arrCopy(opCode, 8);
        
        opCode.complete();
    }

    else if (line.Contains("pop"))
    {
        StaticMethods.pop.arrCopy(opCode, 0);
        byte regA = byte.Parse(line.Substring(line.IndexOf('$') + 1,
                line.Length - 1 - line.IndexOf('$')));
        
        regA.toBin().arrCopy(opCode, 8);
        
        opCode.complete();
    }
    
    else if (line.Contains("jump"))
    {
        StaticMethods.jump.arrCopy(opCode, 0);
        
        string label = line.Split(" ")[2];

        labelIndex[labels.IndexOf(label)]
            .toBin(12)
            .arrCopy(opCode, 4);
    }
    
    else if (line.Contains("nop"))
        opCode = opCode.complete();
    
    else return "label";
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
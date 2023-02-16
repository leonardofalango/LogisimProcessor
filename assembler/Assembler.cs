using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Assembler
{
    public Dictionary<string, byte[]> Prefixes { get; private set; } = new Dictionary<string, byte[]>();  
    public string Path { get; private set; }

    private List<string> labels = new List<string>();

    private List<int> labelIndexes = new List<int>();

    public Assembler(string filePath)
    {
        this.Path = filePath;

        Prefixes = getInstructions();
        labelCheck();
    }


    public void processLine(string line)
    {
        // line types:
            // 4 bits sep -> cccc cccc aaaa bbbb

        line = line.Trim();
        string[] s = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        string command = s[0];
        // commands that are ambigous

        if (command == "mov")
        {
            if (s.Contains("["))
            {
                if (s[1].Contains("["))
                    command = "movStore";
                else
                    command = "movLoad";
            }
        }
        
        int[] bitsToConvert = new int[2];

        bitsToConvert[0] = int.Parse(
            s[1].Replace("$", "")
            .Replace(",", "")
            .Replace("[", "")
            .Replace("]", "")
        );

        bitsToConvert[1] = int.Parse(
            s[2].Replace("$", "")
            .Replace(",", "")
            .Replace("[", "")
            .Replace("]", "")
        );

        //Tranforming bits to real bits

        byte[][] realBits = new byte[2][];
        realBits[0] = bitsToConvert[0].toBin(); //int.toBin() is a extension method, from StaticMethods
        realBits[1] = bitsToConvert[1].toBin();

        byte[] commandByte = Prefixes[command];

        string processedLine = string.Join("", commandByte)
            + string.Join("", realBits[0])
            + string.Join("", realBits[1]);

            // 2 bits sep -> cccc ffff ffff ffff
    }

    private Dictionary<string, byte[]> getInstructions()
    {
        Dictionary<string, byte[]> p = new Dictionary<string, byte[]>();

        p.Add("add", StaticMethods.add);
        p.Add("lsh", StaticMethods.leftShift);
        p.Add("rsh", StaticMethods.rightShift);
        p.Add("and", StaticMethods.and);
        p.Add("div", StaticMethods.div);
        p.Add("jump", StaticMethods.jump);
        p.Add("imul", StaticMethods.mul);
        p.Add("inc", StaticMethods.inc);
        p.Add("je", StaticMethods.je);
        p.Add("push", StaticMethods.push);
        p.Add("pop", StaticMethods.pop);

        p.Add("mov", StaticMethods.mov);
        p.Add("moveStore", StaticMethods.movstore);
        p.Add("moveLoad", StaticMethods.moveload);
        p.Add("movConst", StaticMethods.movconst);
        
        p.Add("cmp", StaticMethods.cmp);
        p.Add("cmpReg", StaticMethods.cmpReg);

        return p;
    }

    private void labelCheck()
    {
        var reader = new StreamReader(Path);
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
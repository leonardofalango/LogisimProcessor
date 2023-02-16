// using System;
// using System.Collections.Generic;
// using System.IO;

// public class ImageAssembler
// {
//     public string Path {get; private set};
//     public ImageAssembler(string imagePath)
//     {
//         this.Path = imagePath;
//     }

//     public List<string> returnAsm()
//     {
//         Bitmap bmp = (Bitmap)Image
//             .FromFile(imagePath);

//         var binaryBmp = BinaryImage(bmp, 128);

//         List<string> allLines = new List<string>();

//         for (int i = 0; i < bmp.Width; i++)
//         {
//             string aux = "";
//             for (int j = 0; j < bmp.Height / 2; j++)
//             {
//                 int index = j + i * bmp.Width;
//                 aux += (binaryBmp[index].ToString());
//             }
//             allLines.Add(aux);

//             aux = "";
//             for (int j = bmp.Height / 2; j < bmp.Height; j++)
//             {
//                 int index = j + i * bmp.Width;
//                 aux += (binaryBmp[index].ToString());
//             }
//             allLines.Add(aux);
//         }

//         File.WriteAllLines(
//             "mengao.leozin",
//             allLines
//         );

//         allLines = new List<string>();

//         StreamReader sr = new StreamReader("mengao.leozin");
//         while (!sr.EndOfStream)
//         {
//             string line = sr.ReadLine();
            
//             string halfLine = "";
//             for (int i = 0; i < line.Length / 2; i++)
//                 halfLine += line[i];
            
//             int dec = convertToDecimal(halfLine);

//             allLines.Add($"mov     $14, {dec}");
//             allLines.Add($"mov     $15, 8");
//             allLines.Add($"lsh     $14, $15");

//             halfLine = "";
//             for (int i = line.Length / 2; i < line.Length; i++)
//                 halfLine += line[i];
            
//             dec = convertToDecimal(halfLine);

//             allLines.Add($"mov     $15, {dec}");
//             allLines.Add($"add     $14, $15");
//             allLines.Add($"mov     [$0], $14");
//             allLines.Add($"inc     $0");
//         }

//         return allLines;
//     }

//     private int[] BinaryImage(Bitmap bmp, int threshold = 128)
//     {
//         int[] returnBmp = new int[bmp.Width * bmp.Height];

//         for (int i = 0; i < bmp.Width; i++)
//         {
//             for (int j = 0; j < bmp.Height; j++)
//             {
//                 int index =  j + bmp.Width * i;
//                 Color pixel = bmp.GetPixel(i, j);
//                 int mean = (pixel.R + pixel.G + pixel.B) / 3;
                
//                 if (mean < threshold)
//                     returnBmp[index] = 0;
//                 else
//                     returnBmp[index] = 1; 
//             }
//         }

//         return returnBmp;
//     }

//     private string Bin(int number)
//     {
//         string ret = "";
//         for (int i = 0; number > 0; i++)
//         {
//             int result = number%2;
//             ret += result.ToString();
//             number = number / 2;
//         }
//         return ret;
//     }

//     private int convertToDecimal(string bin)
//     {
//         int m = bin.Length - 1;
//         int dec = 0;

//         for (int i = 0; i < bin.Length; i++)
//         {
//             int n = int.Parse(bin[i].ToString());
//             int pot = (int)Math.Pow(2, m);
//             if (n == 1)
//                 dec += pot;
//             m--;
//         }

//         return dec;
//     }
// }

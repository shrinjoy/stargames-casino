using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Drawing.Printing;
using System;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Common;

public class PrintDocs : MonoBehaviour
{
   public void printDoc(string data,string barcodedata)
    {
        string s = data;
        PrintDocument p = new PrintDocument();

        p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
        {
            e1.Graphics.DrawImage(GenerateBarcode(barcodedata, BarcodeFormat.CODE_128, 256, 256), 0,250, 256, 25);
            e1.Graphics.DrawString(s, new System.Drawing.Font("Times New Roman",9), new SolidBrush(System.Drawing.Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            e1.Graphics.DrawString(barcodedata, new System.Drawing.Font("Times New Roman", 12), new SolidBrush(System.Drawing.Color.Black), new RectangleF(20, 275, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));



        };
        try
        {
            p.Print();
        }
        catch (Exception ex)
        {
            throw new Exception("Exception Occured While Printing", ex);
        }
    }
    private  Bitmap GenerateBarcode(string data, BarcodeFormat format, int width, int height)
    {
        // Generate the BitMatrix
        BitMatrix bitMatrix = new MultiFormatWriter()
            .encode(data, format, width, height);

        // Generate the pixel array
        System.Drawing.Color[] pixels = new System.Drawing.Color[bitMatrix.Width * bitMatrix.Height];
        Bitmap bmp = new Bitmap(width, height);
        int pos = 0;
        for (var y = 0; y < bitMatrix.Height; y++)
        {
            for (var x = 0; x < bitMatrix.Width; x++)
            {
                pixels[pos++] = bitMatrix[x, y] ? System.Drawing.Color.Black :System.Drawing.Color.White;
                bmp.SetPixel(x, y, bitMatrix[x, y] ? System.Drawing.Color.Black : System.Drawing.Color.White);
            }
        }

        // Setup the texture
      
        
        return bmp;
    }
}

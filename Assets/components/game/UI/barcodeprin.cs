using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
public class barcodeprin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Texture2D texture;
    [SerializeField] RawImage img;
    string barcode;
    Bitmap bmp;
    void Start()
    {
        texture = new Texture2D(256,256);
    }

    // Update is called once per frame
    public void generateBarcode()
    {
        string path = @"C:\barcode.png";
        texture.SetPixels32(encode(GameObject.FindObjectOfType<claimmanager>().barcode, texture.width, texture.height));
        texture.Apply();
        img.texture= texture;
        
       
        PrintDocument printdoc = new PrintDocument();
        File.WriteAllBytes(path, texture.EncodeToPNG());
        System.Diagnostics.Process.Start("mspaint.exe",path);
    }
    Color32[] encode(string barcodetoprint,int w,int h)
    {
        barcode = barcodetoprint;
        
        BarcodeWriter wr = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = h,
                Width = w
            }


        };
        return wr.Write(barcodetoprint);
        
    }
}

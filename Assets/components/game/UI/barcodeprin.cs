using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        texture = new Texture2D(256,256);
    }

    // Update is called once per frame
    public void generateBarcode()
    {
        texture.SetPixels32(encode(GameObject.FindObjectOfType<claimmanager>().barcode, texture.width, texture.height));
        texture.Apply();
        img.texture= texture;
        print(Application.persistentDataPath + "Img1.png");
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath + "Img1.png"),texture.EncodeToPNG());
        PrintDocument printdoc = new PrintDocument();
        printdoc.DocumentName = Application.persistentDataPath + "Img1.png";
        printdoc.Print();
    }
    Color32[] encode(string barcodetoprint,int w,int h)
    {
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

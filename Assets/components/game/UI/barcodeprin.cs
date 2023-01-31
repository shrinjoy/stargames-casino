using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System;
using System.Linq;

public class barcodeprin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Texture2D texture;
    [SerializeField] RawImage img;
    string barcode;
    Bitmap bmp;
    [SerializeField] betButtons[] btns;
    void Start()
    {
        btns = GameObject.FindObjectOfType<jokerGameManager>().btns;
        texture = new Texture2D(256,256);
    }

    // Update is called once per frame
    public void generateBarcode()
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        string data="";
        string barcode="";
        sqlCmnd.CommandText = "SELECT * FROM [star].[dbo].[tasp] where bar='" + GameObject.FindObjectOfType<claimmanager>().barcode + "' and g_id=" + GameObject.FindObjectOfType<claimmanager>().gameid;//this is the sql command we use to get data about user
       
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
           data = "Star Games \n For Amusement Only \n Agent:" + sqlData["ter_id"].ToString() + "\n Game ID:" + sqlData["g_id"].ToString() + "\nGame Name:Joker \nDraw Time:" + DateTime.Parse(sqlData["p_time"].ToString()).ToString("yyyy-MM-dd") +sqlData["g_time"].ToString() + "\nTicket Time:" + DateTime.Parse(sqlData["p_time"].ToString()).ToString("yyyy-MM-dd hh:mm:ss tt") + "\n Total Point:" + sqlData["tot"].ToString()
               + "\nItem Point Item Point"
               + "\n" + btns[00].name + ":    " + sqlData["a00"].ToString() + "        " + btns[01].name + ":    " + sqlData["a01"].ToString()
               + "\n" + btns[02].name + ":    " + sqlData["a02"].ToString() + "        " + btns[03].name + ":    " + sqlData["a03"].ToString()
               + "\n" + btns[04].name + ":    " + sqlData["a04"].ToString() + "        " + btns[05].name + ":    " + sqlData["a05"].ToString()
               + "\n" + btns[06].name + ":    " + sqlData["a06"].ToString() + "        " + btns[07].name + ":    " + sqlData["a07"].ToString()
               + "\n" + btns[08].name + ":    " + sqlData["a08"].ToString()+  "        " + btns[09].name + ":    " + sqlData["a09"].ToString()
               + "\n" + btns[10].name + ":    " + sqlData["a10"].ToString() + "        " + btns[11].name + ":    " + sqlData["a11"].ToString();
                barcode = sqlData["bar"].ToString();
        }
       
        sqlData.Close();
        sqlData.DisposeAsync();
        GameObject.FindObjectOfType<PrintDocs>().printDoc(data, barcode);
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

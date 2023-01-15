using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class report_info_data : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TMP_Text from_time;
    [SerializeField] TMPro.TMP_Text to_time;
    [SerializeField] TMPro.TMP_Text salepoint;
    [SerializeField] TMPro.TMP_Text winpoint;
    [SerializeField] TMPro.TMP_Text commipoint;
    [SerializeField] TMPro.TMP_Text ntppoint;
    [SerializeField] TMPro.TMP_Text operatorpoint;
    [SerializeField] calender tocalender;
    [SerializeField] calender fromcalender;
    public void setdata()
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as ppoint,ISNULL(sum(clm),0) as wpoint,\r\nISNULL(sum(qty),0)-ISNULL(sum(clm),0) as epoint,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as npoint,\r\nISNULL(sum(qty),0)*g.comm/100 as ppoints from tasp n, g_master g \r\nwhere g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and g_date between '"+fromcalender.date+"' and  '"+tocalender.date+"'\r\ngroup by n.ter_id,g.term_name,g.comm";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if(sqlData.Read())
        {
            from_time.text = fromcalender.date;
            to_time.text=tocalender.date;
            salepoint.text = sqlData["ppoint"].ToString();
            winpoint.text = sqlData["wpoint"].ToString();
            commipoint.text = sqlData["epoint"].ToString();
            ntppoint.text = sqlData["npoint"].ToString();
            operatorpoint.text = sqlData["ppoint"].ToString();
            print(sqlData["plyid"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }
}

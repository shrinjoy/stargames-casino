using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using System;

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
        sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as sale_point,ISNULL(sum(clm),0) as win_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as operator_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as ntp_point,ISNULL(sum(qty),0)*g.comm/100 as commision_points from tasp n, g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and g_date between '" + fromcalender.date + "' and  '" + tocalender.date + "'group by n.ter_id,g.term_name,g.comm";


            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);
        if(sqlData.Read())
        {
            from_time.text = fromcalender.date;
            to_time.text=tocalender.date;
            salepoint.text = sqlData["sale_point"].ToString();
            winpoint.text = sqlData["win_point"].ToString();
            commipoint.text =  sqlData["operator_point"].ToString();
            operatorpoint.text = sqlData["ntp_point"].ToString();
            if (Convert.ToInt32(sqlData["ntp_point"].ToString()) < 0)
            {
                int x = Convert.ToInt32(sqlData["operator_point"].ToString()) - Convert.ToInt32(sqlData["ntp_point"].ToString());
                ntppoint.text = x.ToString() ;
            }
            else if (Convert.ToInt32(sqlData["ntp_point"].ToString()) > 0)
            {
                ntppoint.text = sqlData["commision_points"].ToString();
            }
           
            print(sqlData["plyid"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }
}

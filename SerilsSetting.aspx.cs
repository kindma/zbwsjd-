using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Management;
using Microsoft.Win32;
using EasyExam;

namespace qphr
{
    public partial  class SerilsSetting : System.Web.UI.Page
    {
        PublicFunction List1 = new PublicFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
//                this.TextBox1.Text = GetMoAddress();
				this.TextBox1.Text ="123456";

            }
        }

        //获得网卡序列号----MAc地址
        public string GetMoAddress()
        {
            string MoAddress = " ";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                    MoAddress = mo["MacAddress"].ToString();
                mo.Dispose();
            }
            return MoAddress.ToString();
        }

		private void InitializeComponent()
		{

		}
	
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string SqlStr = "update ERPSerils set SerilsStr='" + this.TextBox2.Text.Trim().Split('|')[0] + "',DateStr='" + this.TextBox2.Text.Trim().Split('|')[1] + "'";
                List1.ExeSql(SqlStr);
                PublicFunction.ShowAndRedirect(this, "序列号已经写入！", "Login.aspx");
            }
            catch
            {
                PublicFunction.ShowAndRedirect(this, "序列号写入时发生错误！请重新获取！", "SerilsSetting.aspx");
            }
        }
    }
}
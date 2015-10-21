using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.OleDb;
using EasyExam;
using System.Management;
using System.Text;
using System.Security.Cryptography;

namespace EasyExam
{
	/// <summary>
	/// Login ��ժҪ˵����
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
//        PublicFunction pf=new PublicFunction();


		string myLoginID="";
		string strUserID="";
		string strLoginID="";
		string strUserName="";
		string strUserPwd="";
		PublicFunction ObjFun=new PublicFunction(); 
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
//				try
//				{
//					//��ǰ���к�---------------------------------------------------------------------------
//					string NowSerils = "select top 1 SerilsStr from ERPSerils";
//					OleDbDataReader adaSerils =ObjFun.GetList(NowSerils);
//					if (adaSerils.Read())
//					{
//						string NowSerils1 = adaSerils["SerilsStr"].ToString();
//						string NowSerils2 = Encrypt(GetMoAddress().ToString());
//						if (NowSerils2 != NowSerils1)
//						{
//							//���кŴ���
//     						PublicFunction.ShowAndRedirect(this, "��֤ϵͳ���к�ʱ�������������»�ȡ���кţ�", "SerilsSetting.aspx");
////							this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��֤ϵͳ���к�ʱ�������������»�ȡ���кţ�');</script>");
////							Response.Redirect("SerilsSetting.aspx",false);
//						}
//					}
//				}
//				catch
//				{
////					PublicFunction.ShowAndRedirect(this, "��֤ϵͳ���к�ʱ�������������»�ȡ���кţ�", "SerilsSetting.aspx");
//				}
//
//				try
//				{
//					//ʱ���ַ���-----------------------------------------------------------------------------
//					string strTime = "select top 1 DateStr from ERPSerils";
//					OleDbDataReader adaTime =ObjFun.GetList(strTime);
//					if (adaTime.Read())
//					{
//						string strTime1 = adaTime["DateStr"].ToString();
//						DateTime DateStr = DateTime.Parse(Decrypt(strTime1));
//						if (DateStr < DateTime.Now)
//						{
//							//ʹ��ʱ�䵽��
//							PublicFunction.ShowAndRedirect(this, "�����кŵ�ʹ��ʱ���ѵ��������»�ȡ���к�", "SerilsSetting.aspx");
////							Response.Redirect("<script>alert('��֤ϵͳ���к�ʱ�������������»�ȡ���кţ�');window.location='SerilsSetting.aspx'<script>");
//						}
//					}
//				}
//				catch
//				{
//					//ZWL.Common.MessageBox.ShowAndRedirect(this, "�����кŵ�ʹ��ʱ���ѵ��������»�ȡ���кţ�", "SerilsSetting.aspx");
//				}


				string strSql="";
				string strConn="";
				strSql="select * from SystemSet where SetName='OnLineRegist'";
				strConn=ConfigurationSettings.AppSettings["strConn"];
				SqlConnection ObjConn = new SqlConnection(strConn);
				ObjConn.Open();
				SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);
				SqlDataReader ObjDR=ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
				if (ObjDR.Read())
				{
					if(Convert.ToString(ObjDR["StartValue"])=="0")//�ж��Ƿ������ʺ�ע��
					{
						ButRegist.Enabled=false;
						ButRegist.Attributes.Add("onclick","return false;");
					}
					else
					{
						ButRegist.Enabled=true;
						ButRegist.Attributes.Add("onclick","var left=(screen.width-550)/2;var top=(screen.height-60-422)/2;NewWin=window.open('PersonInfo/RegistUser.aspx','RegistUser','titlebar=yes,menubar=no,toolbar=no,location=no,directories=no,status=yes,scrollbars=no,resizable=no,copyhistory=yes,top='+top+',left='+left+',width=550,height=422');return false;");
					}	
				}
				ObjDR.Close();
				ObjConn.Dispose();
				//ע���ʻ�
				Session["UserID"]="";
				Session["LoginID"]="";
				Session["UserName"]="";
				Session["UserPwd"]="";
				//�Զ���¼
				if ((Request["LoginID"]!=null)&&(Request["UserPwd"]!=null))
				{
					ButLogin_Click(sender,e);
				}
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void ButLogin_Click(object sender, System.EventArgs e)
		{
			string strSql="";
			string strConn="";

			strLoginID=ObjFun.CheckString(Convert.ToString(Request["LoginID"]).Trim());
			strUserPwd=ObjFun.CheckString(Convert.ToString(Request["UserPwd"]).Trim());
			if ((TimeRestrict()==true)&&(strLoginID.ToUpper()!="ADMIN"))//�жϵ�¼ʱ���Ƿ�����
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�������¼��ʱ�����ޣ�');</script>");
				return;
			}
			if ((IPRestrict()==true)&&(strLoginID.ToUpper()!="ADMIN"))//�жϵ�¼IP�Ƿ�����
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ�������¼��IP���ޣ�');</script>");
				return;
			}
			
			strSql="select * from UserInfo where LoginID='"+strLoginID+"' and UserPwd='"+strUserPwd+"'";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn=new SqlConnection(strConn);
			ObjConn.Open();
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);
			SqlDataReader ObjDR=ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				string strUserIP=Convert.ToString(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);//��ȡ�ô���IP
				if ((strUserIP=="")||(strUserIP==null))
				{
					strUserIP=Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);//�������IPΪ�վ�ȡֱ��IP
				}
				if ((Convert.ToString(ObjDR["LoginIP"])!="")&&(Convert.ToString(ObjDR["LoginIP"])!=strUserIP))//�жϵ�¼IP�Ƿ�����
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ���������ָ���ĵ�¼IP��');</script>");
				}
				else
				{
					if ((Convert.ToInt32(ObjDR["UserState"])==0)&&(strLoginID.ToUpper()!="ADMIN"))//�ж��ʺ��Ƿ񱻽���
					{
						this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ��𣬴��ʺ��ѱ����ã�');</script>");
					}
					else
					{
						Session["UserID"]=Convert.ToString(ObjDR["UserID"]);
						Session["LoginID"]=Convert.ToString(ObjDR["LoginID"]);
						Session["UserName"]=Convert.ToString(ObjDR["UserName"]);
						Session["UserPwd"]=Convert.ToString(ObjDR["UserPwd"]);

						string strScript="";
						strScript=strScript+"<script language='javascript'>";
						

strScript=strScript+"location.href='MainFrame.aspx';";
					
						strScript=strScript+"</script>";
						Response.Write(strScript);
					}
				}
			}
			else
			{
				this.RegisterStartupScript("newWindow","<script language='javascript'>alert('�Բ����ʺŻ��������');</script>");
			}
			ObjDR.Close();
			ObjConn.Dispose();
		}

		#region//******�ж�ʱ�䷶Χ�Ƿ�����*****
		private bool TimeRestrict()
		{
			bool bTmp=false;
			string strConn="";
			string strStartTime="";
			string strEndTime="";
			string strNowTime=DateTime.Now.ToShortTimeString();
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn=new SqlConnection(strConn);
			SqlDataAdapter ObjCmd=new SqlDataAdapter("select * from SystemSet where SetName='LoginTime'",ObjConn);
			DataSet ObjDS=new DataSet();
			ObjCmd.Fill(ObjDS,"SystemSet");
			if (ObjDS.Tables["SystemSet"].Rows.Count>0)
			{
				strStartTime=ObjDS.Tables["SystemSet"].Rows[0]["StartValue"].ToString().Trim();
				strEndTime=ObjDS.Tables["SystemSet"].Rows[0]["EndValue"].ToString().Trim();
				if ((strStartTime!="")&&(strEndTime!=""))
				{
					DateTime DTNowTime;
					DateTime DTStartTime;
					DateTime DTEndTime;

					DTNowTime=Convert.ToDateTime(strNowTime);
					DTStartTime=Convert.ToDateTime(strStartTime);
					DTEndTime=Convert.ToDateTime(strEndTime);
			
					if ((DTNowTime<DTStartTime)||(DTNowTime>DTEndTime))
					{
						bTmp=true;
					}
				}
			}

			ObjConn.Dispose();
			return bTmp;
		}
		#endregion

		#region//******�ж�IP��Χ�Ƿ�����*****
		private bool IPRestrict()
		{
			bool bTmp=false;
			string strConn="";
			string strUserIP=Convert.ToString(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);//��ȡ�ô���IP
			string strStartIP="";
			string strEndIP="";
			if ((strUserIP=="")||(strUserIP==null))
			{
				strUserIP=Convert.ToString(Request.ServerVariables["REMOTE_ADDR"]);//�������IPΪ�վ�ȡֱ��IP
			}
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn=new SqlConnection(strConn);
			SqlDataAdapter ObjCmd=new SqlDataAdapter("select * from SystemSet where SetName='LoginIP'",ObjConn);
			DataSet ObjDS=new DataSet();
			ObjCmd.Fill(ObjDS,"SystemSet");
			if (ObjDS.Tables["SystemSet"].Rows.Count>0)
			{
				strStartIP=ObjDS.Tables["SystemSet"].Rows[0]["StartValue"].ToString().Trim();
				strEndIP=ObjDS.Tables["SystemSet"].Rows[0]["EndValue"].ToString().Trim();
				if ((strStartIP!="")&&(strEndIP!=""))
				{
					string[] ArrUserIP;
					string[] ArrStartIP;
					string[] ArrEndIP;
					ArrUserIP=strUserIP.Split('.');
					ArrStartIP=strStartIP.Split('.');
					ArrEndIP=strEndIP.Split('.');

					for(int i=0;i<ArrUserIP.Length;i++)
					{
						if ((Convert.ToInt16(ArrUserIP[i])<Convert.ToInt16(ArrStartIP[i]))||(Convert.ToInt16(ArrUserIP[i])>Convert.ToInt16(ArrEndIP[i])))
						{
							bTmp=true;
						}
					}
				}
			}

			ObjConn.Dispose();
			return bTmp;
		}
		#endregion

	
		//����������к�----MAc��ַ
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

		//����
		public static string Encrypt(string Text)
		{
			return Encrypt(Text, "zhangweilong");
		}
		public static string Encrypt(string Text, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			byte[] inputByteArray;
			inputByteArray = Encoding.Default.GetBytes(Text);
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			StringBuilder ret = new StringBuilder();
			foreach (byte b in ms.ToArray())
			{
				ret.AppendFormat("{0:X2}", b);
			}
			return ret.ToString();
		}

		//����
		public static string Decrypt(string Text)
		{
			return Decrypt(Text, "zhangweilong");
		}
		public static string Decrypt(string Text, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			int len;
			len = Text.Length / 2;
			byte[] inputByteArray = new byte[len];
			int x, i;
			for (x = 0; x < len; x++)
			{
				i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
				inputByteArray[x] = (byte)i;
			}
			des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			return Encoding.Default.GetString(ms.ToArray());
		}

	}
}


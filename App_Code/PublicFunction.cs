using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Management;
using System.Collections;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.OleDb;

namespace EasyExam
{
	/// <summary>
	/// PublicFunction 的摘要说明。
	/// </summary>
	public class PublicFunction
	{
		public static string strConn = "";

		public PublicFunction()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region//CheckString******屏蔽非法字符*******
		public string CheckString(string str)
		{
			str=str.Trim();
			
			str=str.Replace("[","［");
		
			str=str.Replace("]","］");
		
			str=str.Replace("-","－");
		
			str=str.Replace("=","＝");
		
			str=str.Replace(@"\","＼");
		
			str=str.Replace("|","︱");
		
			str=str.Replace("'","＇");
		
			str=str.Replace(":","：");
		
			str=str.Replace(",","，");
		
			str=str.Replace(".","．");
		
			str=str.Replace("/","／");
		
			str=str.Replace("?","？"); 
		
			str=str.Replace(";","；");
		
			str=str.Replace(">","＞");
		
			str=str.Replace("<","＜");
		
			str=str.Replace("+","＋");
		
			str=str.Replace("_","＿");
		
			str=str.Replace(")","）");
		
			str=str.Replace("(","（");
		
			str=str.Replace("*","＊");
		
			str=str.Replace("&","＆");
		
			str=str.Replace("^","∧");
		
			str=str.Replace("%","％");
		
			str=str.Replace("$","＄");
		
			str=str.Replace("#","＃");
		
			str=str.Replace("@","＠");
		
			str=str.Replace("!","！");
		
			str=str.Replace("~","～");
		
			return str;
		}
		#endregion

		#region//CheckTestStr****屏蔽试题非法字符*****
		public string CheckTestStr(string str)
		{
			str=str.Trim();
			
			str=str.Replace("'","＇");//屏蔽sql语句

			return str;
		}
		#endregion

		#region//GetValues******获取指定的值********
		public string GetValues(string strSql,string strFiled)
		{
			string strConn="";
			string strTmp="";
			strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn = new SqlConnection(strConn);
			ObjConn.Open();
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);
			SqlDataReader ObjDR=ObjCmd.ExecuteReader(CommandBehavior.CloseConnection);
			if (ObjDR.Read())
			{
				strTmp=Convert.ToString(ObjDR[strFiled.Trim()]);
			}
			ObjDR.Close();
			ObjConn.Close();
			ObjConn.Dispose();
			return strTmp;
		}
		#endregion

		#region//ExecuteSql******完成指定的操作********
		public int ExecuteSql(string strSql)
		{
			int intTmp=1;
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection ObjConn=new SqlConnection(strConn);
			ObjConn.Open();
			SqlCommand ObjCmd=new SqlCommand(strSql,ObjConn);
			SqlDataReader ObjDR= ObjCmd.ExecuteReader();
			if (ObjDR.Read())
			{
				intTmp=Convert.ToInt32(ObjDR[0].ToString());
			}
			ObjConn.Close();
			ObjConn.Dispose();
			return intTmp;
		}
		#endregion

		#region//ExecuteSqlCmd******完成指定的操作********
		public int ExecuteSqlCmd(string strSql)
		{
			int intTmp=1;
			string strConn=ConfigurationSettings.AppSettings["strConn"];
			SqlConnection SqlConn=new SqlConnection(strConn);
			SqlConn.Open();
			SqlCommand SqlCmd=new SqlCommand(strSql,SqlConn);
			SqlCmd.ExecuteNonQuery();
			SqlConn.Close();
			return intTmp;
		}
		#endregion

		#region//******验证是否注册********
		public bool JoySoftware()
		{
			return false;
		}
		#endregion

		#region//******Des加密算法********
		public string DesEncrypt(string pToEncrypt, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
			des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
			des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			StringBuilder ret = new StringBuilder();
			foreach (byte b in ms.ToArray())
			{
				ret.AppendFormat("{0:X2}", b);
			}
			ret.ToString();
			return ret.ToString();
		}
		#endregion

		#region//******Des解密算法********
		public string DesDecrypt(string pToDecrypt, string sKey)
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
			for (int x = 0; x < pToDecrypt.Length / 2; x++)
			{
				int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
				inputByteArray[x] = (byte)i;
			}
			des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
			des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
			MemoryStream ms = new MemoryStream();
			CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			StringBuilder ret = new StringBuilder();
			return System.Text.Encoding.Default.GetString(ms.ToArray());
		}
		#endregion

		#region//*******GetSheetName*******
		public string GetSheetName(string filePath)
		{
			string sheetName="";

			System.IO.FileStream tmpStream=File.OpenRead(filePath);
			byte[] fileByte=new byte[tmpStream.Length];
			tmpStream.Read(fileByte,0,fileByte.Length);
			tmpStream.Close();
   
			byte[] tmpByte=new byte[]{Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),
										 Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),
										 Convert.ToByte(30),Convert.ToByte(16),Convert.ToByte(0),Convert.ToByte(0)};
   
			int index=GetSheetIndex(fileByte,tmpByte);
			if(index>-1)
			{

				index+=16+12;
				System.Collections.ArrayList sheetNameList=new System.Collections.ArrayList();
    
				for(int i=index;i<fileByte.Length-1;i++)
				{
					byte temp=fileByte[i];
					if(temp!=Convert.ToByte(0))
						sheetNameList.Add(temp);
					else
						break;
				}
				byte[] sheetNameByte=new byte[sheetNameList.Count];
				for(int i=0;i<sheetNameList.Count;i++)
					sheetNameByte[i]=Convert.ToByte(sheetNameList[i]);
   
				sheetName=System.Text.Encoding.Default.GetString(sheetNameByte);
			}
			return sheetName;
		}
		#endregion

		#region//*******GetSheetIndex*******
		public int GetSheetIndex(byte[] FindTarget,byte[] FindItem)
		{
			int index=-1;

			int FindItemLength=FindItem.Length;
			if(FindItemLength<1) return -1;
			int FindTargetLength=FindTarget.Length;
			if((FindTargetLength-1)<FindItemLength) return -1;

			for(int i=FindTargetLength-FindItemLength-1;i>-1;i--)
			{
				System.Collections.ArrayList tmpList=new System.Collections.ArrayList();
				int find=0;
				for(int j=0;j<FindItemLength;j++)
				{
					if(FindTarget[i+j]==FindItem[j]) find+=1;
				}
				if(find==FindItemLength)
				{
					index=i;
					break;
				}
			}
			return index;
		}
		#endregion

		#region//*******数字转换成中文*******
		public string convertint(string str) 
		{ 
			string[] cstr={"零","一","二","三","四", "五", "六","七","八","九"}; 
		    string[] wstr={"","","十","佰","仟","萬","十","佰","仟","億","十","佰","仟"}; 

			int len=str.Length; 
			int i; 
			string tmpstr,rstr; 
			rstr=""; 
			for(i=1;i<=len;i++) 
			{ 
				tmpstr=str.Substring(len-i,1);
				rstr=string.Concat(cstr[Int32.Parse(tmpstr)]+wstr[i],rstr);
			} 
			rstr=rstr.Replace("十零","十"); 
			rstr=rstr.Replace("零十","零"); 
			rstr=rstr.Replace("零佰","零"); 
			rstr=rstr.Replace("零仟","零"); 
			rstr=rstr.Replace("零萬","萬"); 
			for(i=1;i<=6;i++)
			{
				rstr=rstr.Replace("零零","零"); 
			}
			rstr=rstr.Replace("零萬","零"); 
			rstr=rstr.Replace("零億","億"); 
			rstr=rstr.Replace("零零","零"); 
			//rstr+="圆整"; 
			return rstr; 
		} 
		#endregion

		#region//*******求中英文混合字符串长度*******
		public int StrLength(string str)
		{
			int len = 0;
			for (int i = 0; i < str.Length; i++)
			{
				byte[] byte_len = System.Text.Encoding.Default.GetBytes(str.Substring(i, 1));
				if (byte_len.Length > 1)
					len += 2;
				else
					len += 1;
			}
			return len;
		}
		#endregion

		#region//*******求中英文混合字符串子串*******
		public string getStr(string s,int l)
		{    
			string temp = s ;
			//if (Regex.Replace(temp,"[\u4e00-\u9fa5]","zz",RegexOptions.IgnoreCase).Length<=l)
			//{
			//	return temp;
			//}
			//for (int i=s.Length;i>=0;i--)
			//{
			//	temp = s.Substring(0,i);
			//	if (Regex.Replace(temp,"[\u4e00-\u9fa5]","zz",RegexOptions.IgnoreCase).Length<=l)
			//	{
			//		return temp + "";
			//	}    
			//}
			if (StrLength(temp)<=l)
			{
				return temp;
			}
			for (int i=s.Length;i>=0;i--)
			{
				temp = s.Substring(0,i);
				if (StrLength(temp)<=l)
				{
					return temp + "";
				}    
			}
			return "";
		}
		#endregion

		#region//*******弹出窗口信息*******
		public void Alert(string message)
		{
			string js="<Script language='JavaScript'>alert('"+message+"');window.close();</Script>";
			HttpContext.Current.Response.Write(js);
			HttpContext.Current.Response.End();
		}
 		#endregion

		#region//*******数据导出到Excel文件*******
		public void DataTableToExcel(DataTable dt,string Path) 
		{ 
			string strConn="Provider=Microsoft.Jet.OLEDB.4.0;"+"Data Source="+Path+";"+"Extended Properties=Excel 8.0";
			OleDbConnection  myConn=new OleDbConnection(strConn);
			string strCom="select * from [Sheet1$]";
			myConn.Open();
			OleDbDataAdapter myCommand=new OleDbDataAdapter(strCom,myConn);
			
			OleDbCommand objCmd=new OleDbCommand(); 
			objCmd.Connection=myConn; 

			DataSet newds=new DataSet();
			myCommand.Fill(newds,"Table1");

			StringBuilder sb=new StringBuilder();

			OleDbParameterCollection param=objCmd.Parameters; 
			DataRow row;

			int rows=dt.Rows.Count; 
			int cols=dt.Columns.Count;

			if (newds.Tables["Table1"].Rows.Count>=1)
			{
				sb.Append("update [Sheet1$] set ");

				for(int i=0;i<cols;i++) 
				{ 
					if(i<cols-1)
						sb.Append(newds.Tables["Table1"].Columns[i].ColumnName+"=@"+newds.Tables["Table1"].Columns[i].ColumnName+","); 
					else 
						sb.Append(newds.Tables["Table1"].Columns[i].ColumnName+"=@"+newds.Tables["Table1"].Columns[i].ColumnName+""); 
				}

				//建立插入动作的Command 
				objCmd.CommandText=sb.ToString(); 
			
				for(int i=0;i<cols;i++) 
				{ 
					param.Add(new OleDbParameter("@"+newds.Tables["Table1"].Columns[i].ColumnName,OleDbType.VarChar)); 
				} 
			
				//遍历DataTable将数据插入新建的Excel文件中
				for(int j=0;j<newds.Tables["Table1"].Rows.Count;j++)
				{
					row=dt.Rows[j];
					for(int i=0;i<param.Count;i++) 
					{ 
						param[i].Value=row[i]; 
					} 
					objCmd.ExecuteNonQuery(); 
				}
			}
            
			sb.Remove(0,sb.Length);
			param.Clear();
			sb.Append("insert into "); 
			sb.Append("[Sheet1$]"+"("); 
			
			for(int i=0;i<cols;i++) 
			{ 
				if(i<cols-1)
					sb.Append(newds.Tables["Table1"].Columns[i].ColumnName+","); 
				else 
					sb.Append(newds.Tables["Table1"].Columns[i].ColumnName+") values("); 
			} 
			for(int i=0;i<cols;i++)
			{ 
				if(i<cols-1)
				{ 
					sb.Append("@"+newds.Tables["Table1"].Columns[i].ColumnName+","); 
				} 
				else 
				{ 
					sb.Append("@"+newds.Tables["Table1"].Columns[i].ColumnName+")");
				} 
			} 
			
			//建立插入动作的Command 
			objCmd.CommandText=sb.ToString(); 
			
			for(int i=0;i<cols;i++) 
			{ 
				param.Add(new OleDbParameter("@"+newds.Tables["Table1"].Columns[i].ColumnName,OleDbType.VarChar)); 
			} 
			
			//遍历DataTable将数据插入新建的Excel文件中 
			for(int j=newds.Tables["Table1"].Rows.Count;j<dt.Rows.Count;j++)
			{
				row=dt.Rows[j];
				for(int i=0;i<param.Count;i++) 
				{ 
					param[i].Value=row[i]; 
				} 
				objCmd.ExecuteNonQuery();
			}
 
			myConn.Close();
		}
		#endregion

		#region//*******获取网卡硬件地址*******
		public string GetMacAddress()
		{
			try
			{
				string strMAC="";
				ManagementClass mc=new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection moc=mc.GetInstances();
				foreach(ManagementObject mo in moc)
				{
					if ((bool)mo["IPEnabled"]==true)
					{
						string []a=mo["MacAddress"].ToString().Split(':');
						for(int i=0;i<a.Length;i++)
						{
							strMAC=strMAC+a[i].ToString();
						}
						//strMAC=strMAC.Replace("A","10");
						//strMAC=strMAC.Replace("B","11");
						//strMAC=strMAC.Replace("C","12");
						//strMAC=strMAC.Replace("D","13");
						//strMAC=strMAC.Replace("E","14");
						//strMAC=strMAC.Replace("F","15");
					
						break;
					}
				}
				moc=null;
				mc=null;
				return strMAC;
			}
			catch
			{
				return "";
			}
			finally
			{
			}
		} 
		#endregion

		#region//*******获取硬盘ID*******
		public string GetDiskID()
		{
			try
			{
				string strHDid="";
				ManagementClass mc=new ManagementClass("Win32_DiskDrive");
				ManagementObjectCollection moc=mc.GetInstances();
				foreach (ManagementObject mo in moc)
				{
					strHDid=(string)mo.Properties["Model"].Value;
				}
				moc=null;
				mc=null;
				return strHDid;
			}
			catch
			{
				return "";
			}
			finally
			{
			}
		} 
		#endregion

		#region//*******获取CpuID*******
		public string GetCpuID()
		{
			try
			{
				string CpuInfo="";
				ManagementClass mc=new ManagementClass("Win32_Processor");
				ManagementObjectCollection moc=mc.GetInstances();
				foreach (ManagementObject mo in moc)
				{
					CpuInfo=mo.Properties["ProcessorId"].Value.ToString();
				}
				moc=null;
				mc=null;
				return CpuInfo;
			}
			catch
			{
				return "";
			}
			finally
			{
			}
		} 
		#endregion

		#region//*******获取主板信息*******
		public string GetMainBoardInfo()
		{
			try
			{
				string mbInfo="";
				ManagementObjectSearcher mos=new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
				foreach (ManagementObject mo in mos.Get())
				{   
					mbInfo=mo["Manufacturer"].ToString()+mo["Product"].ToString()+mo["SerialNumber"].ToString();
					break;
				}
				mos=null;
				return mbInfo;
			}
			catch
			{
				return "";
			}
			finally
			{
			}
		} 
		#endregion
	
		public string DbPath()
		{
			strConn = "Data Source=localhost;Initial Catalog=xgnKS;User Id=sa;PASSWORD=sa;;Provider=SQLOLEDB";
			return strConn;
		}// 数据库连接字符串。
		public static string ConnectionString
		{
			get
			{
				PublicFunction ConnectionString = new PublicFunction();
				return ConnectionString.DbPath();
			}
		}// 数据库连接字符串属性。
		public OleDbDataReader GetList(string Sql)
		{

			OleDbConnection myConnection = new OleDbConnection(PublicFunction.ConnectionString);
			OleDbCommand myCommand = new OleDbCommand(Sql, myConnection);

			myConnection.Open();
			OleDbDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return result;
		}// 提取数据操作——用于“DataReader”控件。
		public int ExeSql(string Sql)
		{
			OleDbConnection myConnection = new OleDbConnection(PublicFunction.ConnectionString);
			OleDbCommand myCommand = new OleDbCommand(Sql, myConnection);
			try
			{
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myCommand.Dispose();
				myConnection.Close();
				return 1;
			}
			catch
			{
				myConnection.Close();
				return 0;
			}
		}  // 执行无提取值的 SQL 语句。
		/// <summary>
		/// 出现提示信息，并指向新打开的页面
		/// </summary>
		/// <param name="page"></param>
		/// <param name="msg"></param>
		/// <param name="url"></param>
		public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
		{
			StringBuilder Builder = new StringBuilder();
			Builder.Append("<script language='javascript' defer>");
			Builder.AppendFormat("alert('{0}');", msg);
			Builder.AppendFormat("window.location.href='{0}'", url);
			Builder.Append("</script>");

//			page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
			page.GetType();
			page.RegisterStartupScript("message", Builder.ToString());
		}

	}
}


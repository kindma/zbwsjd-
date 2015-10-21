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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyExam.UserManag
{
	/// <summary>
	/// ImportUser 的摘要说明。
	/// </summary>
	public partial class ImportUser : System.Web.UI.Page
	{
	
		string myUserID="";
		string myLoginID="";
		PublicFunction ObjFun=new PublicFunction();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				myUserID=Session["UserID"].ToString();
				myLoginID=Session["LoginID"].ToString();
			}
			catch
			{
			}
			if (myLoginID=="")
			{
				Response.Redirect("../Login.aspx");
			}
			if (!IsPostBack)
			{
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=2)))","UserType")!="1")
				{
					Response.Write("<script>alert('对不起，您没有此操作权限！')</script>");
					Response.End();
				}
				else
				{
					ButInput.Attributes.Add("onclick","javascript:document.all('LabelMessage').innerHTML='正在导入，请稍候......';");
				}
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region//*******导入帐户数据*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (UpLoadFile.PostedFile!=null)//检查上传文件不为空
			{
				if (UpLoadFile.PostedFile.FileName=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择正确的Excel文件！')</script>");
					return;
				}
				string strName=UpLoadFile.PostedFile.FileName.Substring(UpLoadFile.PostedFile.FileName.LastIndexOf("."));//获取后缀名
				if (strName.ToUpper()!=".XLS")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('请选择正确的Excel文件！')</script>");
					return;
				}

				UpLoadFile.PostedFile.SaveAs(Server.MapPath("..\\UpLoadFiles\\ImportUser.xls")); 

				string FilePath=Server.MapPath("..\\UpLoadFiles\\ImportUser.xls");
				int intTmp=0;
				string SheetName="";
				bool canOpen=false;

				System.Data.DataTable rs=new System.Data.DataTable();
				OleDbConnection conn=new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;\"");
    
				try//尝试数据连接是否可用
				{
					conn.Open();
					conn.Close();
					canOpen=true;
				}
				catch//不是Excel文件格式
				{
					canOpen=false;
					LabelMessage.Text="不是Excel文件格式，导入帐户失败！";
				}
				if (canOpen)
				{
					try//如果数据连接可以打开则尝试读入数据
					{
						SheetName="Sheet1";
						OleDbCommand myOleDbCommand=new OleDbCommand("SELECT * FROM ["+SheetName+"$]",conn);
						OleDbDataAdapter myData=new OleDbDataAdapter(myOleDbCommand);
						myData.Fill(rs);
						conn.Close();
					}
					catch//如果数据连接可以打开但是读入数据失败，则从文件中提取出工作表的名称，再读入数据
					{
						SheetName=ObjFun.GetSheetName(FilePath);
						if (SheetName.Length>0)
						{
							OleDbCommand myOleDbCommand= new OleDbCommand("SELECT * FROM ["+SheetName+"$]",conn);
							OleDbDataAdapter myData= new OleDbDataAdapter(myOleDbCommand);
							myData.Fill(rs);
							conn.Close();
						}
					}
					intTmp=ObjFun.ExecuteSqlCmd("delete from ImportErr");//清空ImportErr数据表
					string strLoginID,strUserName,strUserPwd,strUserSex,strBirthday,strDept,strJob,strTelephone,strCertType,strCertNum,strLoginIP,strUserType,strUserState;
					string strDeptID,strJobID;
					string strCreateUserID=Convert.ToString(myUserID);
					DateTime dtmCreateDate;
					System.DateTime currentTime=new System.DateTime();
					currentTime=System.DateTime.Now;
					dtmCreateDate=Convert.ToDateTime(Convert.ToString(currentTime.ToString("d")));

					int i=0,index=0;
					for(i=0;i<rs.Rows.Count;i++)
					{
						strLoginID=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][0].ToString().Trim()),20);//帐号
						strUserSex=rs.Rows[i][3].ToString().Trim();//性别
						strBirthday=rs.Rows[i][4].ToString().Trim();//出生年月
						strDept=rs.Rows[i][5].ToString().Trim();//部门
						strJob=rs.Rows[i][6].ToString().Trim();//职务
						strUserType=rs.Rows[i][11].ToString().Trim();//类型
						strUserState=rs.Rows[i][12].ToString().Trim();//状态
						//帐号
						if (strLoginID=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'帐号为空')");
						}
						else
						{
							if (ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.CheckString(strLoginID)+"'","LoginID")!="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'帐号已存在')");
							}
						}
						//性别
						if (strUserSex!="")
						{
							if ((strUserSex!="男")&&(strUserSex!="女"))
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'性别应为男或女')");
							}
						}
						//出生年月
						if (strBirthday!="")
						{
							try
							{
								Convert.ToDateTime(strBirthday);
							}
							catch
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'出生年月为空或格式不正确')");
							}
						}
						//部门
						if ((strDept!="")&&(ObjFun.GetValues("select DeptID from DeptInfo where DeptName='"+ObjFun.CheckString(strDept)+"'","DeptID")==""))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'部门不存在，请先创建')");
						}
						//职务
						if ((strJob!="")&&(ObjFun.GetValues("select JobID from JobInfo where JobName='"+ObjFun.CheckString(strJob)+"'","JobID")==""))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'职务不存在，请先创建')");
						}
						//类型
						if (myLoginID.Trim().ToUpper()=="ADMIN")
						{
							if ((strUserType!="普通帐户")&&(strUserType!="管理帐户"))
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'类型应为普通帐户或管理帐户')");
							}
						}
						else
						{
							if (strUserType!="普通帐户")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'管理帐户只能创建普通帐户')");
							}
						}
						//状态
						if ((strUserState!="正常")&&(strUserState!="禁止"))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'状态应为正常或禁止')");
						}
					}
					string strSql="select a.ErrID,a.RowNum,a.ErrInfo from ImportErr a order by a.ErrID asc";
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
					DataSet SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"ImportErr");
					if (SqlDS.Tables["ImportErr"].Rows.Count>0)//如果导入有错误，则绑定DataGridErr
					{
						DataGridErr.DataSource=SqlDS;
						DataGridErr.AllowPaging=false;
						DataGridErr.DataBind();
						DataGridErr.Visible=true;
						LabelMessage.Text="导入帐户失败！";
					}
					else//帐户导入UserInfo数据表
					{
						DataGridErr.DataSource=null;
						DataGridErr.Visible=false;

						strConn=ConfigurationSettings.AppSettings["strConn"];
						SqlConnection ObjConn = new SqlConnection(strConn);
						ObjConn.Open();
						SqlTransaction ObjTran=ObjConn.BeginTransaction();
						SqlCommand ObjCmd=new SqlCommand();
						ObjCmd.Connection=ObjConn;
						ObjCmd.Transaction=ObjTran;
						try
						{
							for(i=0;i<rs.Rows.Count;i++)
							{
								strLoginID=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][0].ToString().Trim()),20);//帐号
								strUserName=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][1].ToString().Trim()),20);//姓名
								strUserPwd=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][2].ToString().Trim()),20);//密码
								strUserSex=rs.Rows[i][3].ToString().Trim();//性别
								strBirthday=rs.Rows[i][4].ToString().Trim();//出生年月
								strDept=rs.Rows[i][5].ToString().Trim();//部门
								strJob=rs.Rows[i][6].ToString().Trim();//职务
								strTelephone=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][7].ToString().Trim()),20);//电话
								strCertType=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][8].ToString().Trim()),20);//证件类型
								strCertNum=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][9].ToString().Trim()),20);//证件号码
								strLoginIP=ObjFun.getStr(rs.Rows[i][10].ToString().Trim(),20);//登录IP
								strUserType=rs.Rows[i][11].ToString().Trim();//类型
								strUserState=rs.Rows[i][12].ToString().Trim();//状态
								//性别
								if (strUserSex=="")
								{
									strUserSex="男";
								}
								//部门
								if (strDept!="")
								{
									strDeptID=ObjFun.GetValues("select DeptID from DeptInfo where DeptName='"+ObjFun.CheckString(strDept)+"'","DeptID");
								}
								else
								{
									strDeptID="0";
								}
								//职务
								if (strJob!="")
								{
									strJobID=ObjFun.GetValues("select JobID from JobInfo where JobName='"+ObjFun.CheckString(strJob)+"'","JobID");
								}
								else
								{
									strJobID="0";
								}
								//类型
								if (strUserType=="普通帐户")
								{
									strUserType="0";
								}
								else
								{
									strUserType="1";
								}
								//状态
								if (strUserState=="正常")
								{
									strUserState="1";
								}
								else
								{
									strUserState="0";
								}
								//权限
								int intJudgeUser=0;
								int intJudgeTestType=0;
								int intRoleMenu=0;
								if (strUserType=="1")
								{
									intJudgeUser=1;
									intJudgeTestType=1;
									intRoleMenu=1;
								}
								if (ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+strLoginID+"'","LoginID")=="")
								{
									if (strBirthday!="")
									{
										intTmp=ObjFun.ExecuteSqlCmd("Insert into UserInfo(LoginID,UserName,UserPwd,UserSex,Birthday,DeptID,JobID,Telephone,CertType,CertNum,LoginIP,UserType,UserState,JudgeUser,JudgeTestType,RoleMenu,CreateUserID,CreateDate) Values ('"+strLoginID+"','"+strUserName+"','"+strUserPwd+"','"+strUserSex+"','"+Convert.ToDateTime(strBirthday)+"','"+strDeptID+"','"+strJobID+"','"+strTelephone+"','"+strCertType+"','"+strCertNum+"','"+strLoginIP+"','"+strUserType+"','"+strUserState+"',"+intJudgeUser+","+intJudgeTestType+","+intRoleMenu+",'"+strCreateUserID+"','"+dtmCreateDate+"')");
									}
									else
									{
										intTmp=ObjFun.ExecuteSqlCmd("Insert into UserInfo(LoginID,UserName,UserPwd,UserSex,DeptID,JobID,Telephone,CertType,CertNum,LoginIP,UserType,UserState,JudgeUser,JudgeTestType,RoleMenu,CreateUserID,CreateDate) Values ('"+strLoginID+"','"+strUserName+"','"+strUserPwd+"','"+strUserSex+"','"+strDeptID+"','"+strJobID+"','"+strTelephone+"','"+strCertType+"','"+strCertNum+"','"+strLoginIP+"','"+strUserType+"','"+strUserState+"',"+intJudgeUser+","+intJudgeTestType+","+intRoleMenu+",'"+strCreateUserID+"','"+dtmCreateDate+"')");
									}
								}
							}

							ObjTran.Commit();
						}
						catch
						{
							ObjTran.Rollback();
							LabelMessage.Text="导入帐户失败！";
						}
						finally
						{
							ObjConn.Close();
							ObjConn.Dispose();
							LabelMessage.Text="导入帐户成功！";
						}
					}
				}
			}
		}
		#endregion

	}
}

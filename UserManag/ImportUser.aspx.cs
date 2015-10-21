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
	/// ImportUser ��ժҪ˵����
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
					Response.Write("<script>alert('�Բ�����û�д˲���Ȩ�ޣ�')</script>");
					Response.End();
				}
				else
				{
					ButInput.Attributes.Add("onclick","javascript:document.all('LabelMessage').innerHTML='���ڵ��룬���Ժ�......';");
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

		#region//*******�����ʻ�����*******
		protected void ButInput_Click(object sender, System.EventArgs e)
		{
			if (UpLoadFile.PostedFile!=null)//����ϴ��ļ���Ϊ��
			{
				if (UpLoadFile.PostedFile.FileName=="")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ����ȷ��Excel�ļ���')</script>");
					return;
				}
				string strName=UpLoadFile.PostedFile.FileName.Substring(UpLoadFile.PostedFile.FileName.LastIndexOf("."));//��ȡ��׺��
				if (strName.ToUpper()!=".XLS")
				{
					this.RegisterStartupScript("newWindow","<script language='javascript'>alert('��ѡ����ȷ��Excel�ļ���')</script>");
					return;
				}

				UpLoadFile.PostedFile.SaveAs(Server.MapPath("..\\UpLoadFiles\\ImportUser.xls")); 

				string FilePath=Server.MapPath("..\\UpLoadFiles\\ImportUser.xls");
				int intTmp=0;
				string SheetName="";
				bool canOpen=false;

				System.Data.DataTable rs=new System.Data.DataTable();
				OleDbConnection conn=new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;\"");
    
				try//�������������Ƿ����
				{
					conn.Open();
					conn.Close();
					canOpen=true;
				}
				catch//����Excel�ļ���ʽ
				{
					canOpen=false;
					LabelMessage.Text="����Excel�ļ���ʽ�������ʻ�ʧ�ܣ�";
				}
				if (canOpen)
				{
					try//����������ӿ��Դ����Զ�������
					{
						SheetName="Sheet1";
						OleDbCommand myOleDbCommand=new OleDbCommand("SELECT * FROM ["+SheetName+"$]",conn);
						OleDbDataAdapter myData=new OleDbDataAdapter(myOleDbCommand);
						myData.Fill(rs);
						conn.Close();
					}
					catch//����������ӿ��Դ򿪵��Ƕ�������ʧ�ܣ�����ļ�����ȡ������������ƣ��ٶ�������
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
					intTmp=ObjFun.ExecuteSqlCmd("delete from ImportErr");//���ImportErr���ݱ�
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
						strLoginID=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][0].ToString().Trim()),20);//�ʺ�
						strUserSex=rs.Rows[i][3].ToString().Trim();//�Ա�
						strBirthday=rs.Rows[i][4].ToString().Trim();//��������
						strDept=rs.Rows[i][5].ToString().Trim();//����
						strJob=rs.Rows[i][6].ToString().Trim();//ְ��
						strUserType=rs.Rows[i][11].ToString().Trim();//����
						strUserState=rs.Rows[i][12].ToString().Trim();//״̬
						//�ʺ�
						if (strLoginID=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�ʺ�Ϊ��')");
						}
						else
						{
							if (ObjFun.GetValues("select LoginID from UserInfo where LoginID='"+ObjFun.CheckString(strLoginID)+"'","LoginID")!="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�ʺ��Ѵ���')");
							}
						}
						//�Ա�
						if (strUserSex!="")
						{
							if ((strUserSex!="��")&&(strUserSex!="Ů"))
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�Ա�ӦΪ�л�Ů')");
							}
						}
						//��������
						if (strBirthday!="")
						{
							try
							{
								Convert.ToDateTime(strBirthday);
							}
							catch
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��������Ϊ�ջ��ʽ����ȷ')");
							}
						}
						//����
						if ((strDept!="")&&(ObjFun.GetValues("select DeptID from DeptInfo where DeptName='"+ObjFun.CheckString(strDept)+"'","DeptID")==""))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'���Ų����ڣ����ȴ���')");
						}
						//ְ��
						if ((strJob!="")&&(ObjFun.GetValues("select JobID from JobInfo where JobName='"+ObjFun.CheckString(strJob)+"'","JobID")==""))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'ְ�񲻴��ڣ����ȴ���')");
						}
						//����
						if (myLoginID.Trim().ToUpper()=="ADMIN")
						{
							if ((strUserType!="��ͨ�ʻ�")&&(strUserType!="�����ʻ�"))
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����ӦΪ��ͨ�ʻ�������ʻ�')");
							}
						}
						else
						{
							if (strUserType!="��ͨ�ʻ�")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�����ʻ�ֻ�ܴ�����ͨ�ʻ�')");
							}
						}
						//״̬
						if ((strUserState!="����")&&(strUserState!="��ֹ"))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'״̬ӦΪ�������ֹ')");
						}
					}
					string strSql="select a.ErrID,a.RowNum,a.ErrInfo from ImportErr a order by a.ErrID asc";
					string strConn=ConfigurationSettings.AppSettings["strConn"];
					SqlConnection SqlConn=new SqlConnection(strConn);
					SqlDataAdapter SqlCmd=new SqlDataAdapter(strSql,SqlConn);
					DataSet SqlDS=new DataSet();
					SqlCmd.Fill(SqlDS,"ImportErr");
					if (SqlDS.Tables["ImportErr"].Rows.Count>0)//��������д������DataGridErr
					{
						DataGridErr.DataSource=SqlDS;
						DataGridErr.AllowPaging=false;
						DataGridErr.DataBind();
						DataGridErr.Visible=true;
						LabelMessage.Text="�����ʻ�ʧ�ܣ�";
					}
					else//�ʻ�����UserInfo���ݱ�
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
								strLoginID=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][0].ToString().Trim()),20);//�ʺ�
								strUserName=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][1].ToString().Trim()),20);//����
								strUserPwd=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][2].ToString().Trim()),20);//����
								strUserSex=rs.Rows[i][3].ToString().Trim();//�Ա�
								strBirthday=rs.Rows[i][4].ToString().Trim();//��������
								strDept=rs.Rows[i][5].ToString().Trim();//����
								strJob=rs.Rows[i][6].ToString().Trim();//ְ��
								strTelephone=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][7].ToString().Trim()),20);//�绰
								strCertType=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][8].ToString().Trim()),20);//֤������
								strCertNum=ObjFun.getStr(ObjFun.CheckString(rs.Rows[i][9].ToString().Trim()),20);//֤������
								strLoginIP=ObjFun.getStr(rs.Rows[i][10].ToString().Trim(),20);//��¼IP
								strUserType=rs.Rows[i][11].ToString().Trim();//����
								strUserState=rs.Rows[i][12].ToString().Trim();//״̬
								//�Ա�
								if (strUserSex=="")
								{
									strUserSex="��";
								}
								//����
								if (strDept!="")
								{
									strDeptID=ObjFun.GetValues("select DeptID from DeptInfo where DeptName='"+ObjFun.CheckString(strDept)+"'","DeptID");
								}
								else
								{
									strDeptID="0";
								}
								//ְ��
								if (strJob!="")
								{
									strJobID=ObjFun.GetValues("select JobID from JobInfo where JobName='"+ObjFun.CheckString(strJob)+"'","JobID");
								}
								else
								{
									strJobID="0";
								}
								//����
								if (strUserType=="��ͨ�ʻ�")
								{
									strUserType="0";
								}
								else
								{
									strUserType="1";
								}
								//״̬
								if (strUserState=="����")
								{
									strUserState="1";
								}
								else
								{
									strUserState="0";
								}
								//Ȩ��
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
							LabelMessage.Text="�����ʻ�ʧ�ܣ�";
						}
						finally
						{
							ObjConn.Close();
							ObjConn.Dispose();
							LabelMessage.Text="�����ʻ��ɹ���";
						}
					}
				}
			}
		}
		#endregion

	}
}

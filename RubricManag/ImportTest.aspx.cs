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


namespace EasyExam.RubricManag
{
	/// <summary>
	/// ImportTest ��ժҪ˵����
	/// </summary>
	public partial class ImportTest : System.Web.UI.Page
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
				if (ObjFun.GetValues("select UserType from UserInfo where LoginID='"+myLoginID+"' and UserType=1 and (RoleMenu=1 or (RoleMenu=2 and Exists(select OptionID from UserPower where UserID=UserInfo.UserID and PowerID=3 and OptionID=3)))","UserType")!="1")
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

		#region//*******������������*******
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

				UpLoadFile.PostedFile.SaveAs(Server.MapPath("..\\UpLoadFiles\\ImportTest.xls")); 

				string FilePath=Server.MapPath("..\\UpLoadFiles\\ImportTest.xls");
				int intTmp=0;
				string strTmp="";
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
					LabelMessage.Text="����Excel�ļ���ʽ����������ʧ�ܣ�";
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
					int intSubjectID,intLoreID,intTestTypeID,intOptionNum,intTypeTime,intStandardSpeed,intCreateUserID;
					string strSubjectName,strLoreName,strTestTypeName,strBaseTestType,strTestDiff,strTestMark,strTestContent,strOptionContent,strStandardAnswer,strTestParse;
					double dblTestMark;
					string[] strArrOptionContent,strArrTypeStandardAnswer;
					
					intCreateUserID=Convert.ToInt32(myUserID);
					DateTime dtmCreateDate;
					System.DateTime currentTime=new System.DateTime();
					currentTime=System.DateTime.Now;
					dtmCreateDate=Convert.ToDateTime(Convert.ToString(currentTime.ToString("d")));

					int i=0,index=0;
					for(i=0;i<rs.Rows.Count;i++)
					{
						strSubjectName=ObjFun.CheckString(rs.Rows[i][0].ToString().Trim());//��Ŀ����
						strLoreName=ObjFun.CheckString(rs.Rows[i][1].ToString().Trim());//֪ʶ��
						strTestTypeName=ObjFun.CheckString(rs.Rows[i][2].ToString().Trim());//��������
						strTestDiff=ObjFun.CheckString(rs.Rows[i][3].ToString().Trim());//�����Ѷ�
						strTestMark=rs.Rows[i][4].ToString().Trim();//�������
						strTestContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][5].ToString().Trim()),4000);//��������
						strOptionContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][6].ToString().Trim()),1800);//����ѡ��
						strStandardAnswer=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][7].ToString().Trim()),2000);//��׼��
						strTestParse=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][8].ToString().Trim()),500);//�������

						intSubjectID=0;
						intLoreID=0;
						intTestTypeID=0;
						//��Ŀ����
						if (strSubjectName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��Ŀ����Ϊ��')");
						}
						else
						{
							strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
							if (strTmp=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��Ŀ���Ʋ�����')");
							}
							else
							{
								intSubjectID=Convert.ToInt32(strTmp);
							}
						}
						//֪ʶ��
						if (strLoreName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'֪ʶ��Ϊ��')");
						}
						else
						{
							strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
							if (strTmp!="")
							{
								if (ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+Convert.ToInt32(strTmp)+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID")=="")
								{
									index=index+1;
									intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��ǰ��Ŀ֪ʶ�㲻����')");
								}
								else
								{
									intLoreID=Convert.ToInt32(ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+Convert.ToInt32(strTmp)+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID"));
								}
							}
						}
						//��������
						if (strTestTypeName=="")
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��������Ϊ��')");
						}
						else
						{
							if (ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID")=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�������Ʋ�����')");
							}
							else
							{
								intTestTypeID=Convert.ToInt32(ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID"));
							}
						}
						//�����Ѷ�
						if ((strTestDiff!="��")&&(strTestDiff!="����")&&(strTestDiff!="�е�")&&(strTestDiff!="����")&&(strTestDiff!="��"))
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�����ѶȲ���ȷ')");
						}
						//�������
						try
						{
							dblTestMark=Convert.ToDouble(strTestMark);
						}
						catch
						{
							index=index+1;
							intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�����������ȷ')");
						}
						if ((intSubjectID!=0)&&(intLoreID!=0)&&(intTestTypeID!=0))
						{
							//��������
							if (strTestContent=="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��������Ϊ��')");
							}
							//���ͼ��
							strTmp=ObjFun.GetValues("select RubricID from RubricInfo where SubjectID='"+intSubjectID+"' and LoreID='"+intLoreID+"' and TestContent='"+strTestContent+"'","RubricID");
							if (strTmp.Trim()!="")
							{
								index=index+1;
								intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��ǰ��Ŀ֪ʶ�����Ѿ����ڴ�����')");
							}
							else
							{
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="��ѡ��")
								{
									if (strOptionContent.IndexOf("|")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����ѡ������δ�á�|������')");
									}
									if ((strStandardAnswer.ToUpper()!="A")&&(strStandardAnswer.ToUpper()!="B")&&(strStandardAnswer.ToUpper()!="C")&&(strStandardAnswer.ToUpper()!="D")&&(strStandardAnswer.ToUpper()!="E")&&(strStandardAnswer.ToUpper()!="F"))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����𰸲���ȷ')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="��ѡ��")
								{
									if (strOptionContent.IndexOf("|")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����ѡ������δ�á�|������')");
									}
									if ((strStandardAnswer.ToUpper().IndexOf("A")<0)&&(strStandardAnswer.ToUpper().IndexOf("B")<0)&&(strStandardAnswer.ToUpper().IndexOf("C")<0)&&(strStandardAnswer.ToUpper().IndexOf("D")<0)&&(strStandardAnswer.ToUpper().IndexOf("E")<0)&&(strStandardAnswer.ToUpper().IndexOf("F")<0))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����𰸲���ȷ')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�ж���")
								{
									if ((strStandardAnswer.ToUpper()!="��ȷ")&&(strStandardAnswer.ToUpper()!="����"))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����𰸲���ȷ')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�����")
								{
									if (strTestContent.IndexOf("___")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����������δ��3���»��ߡ�_����ʾ���λ��')");
									}
									Regex regex1=new Regex("___");
									Regex regex2=new Regex(",");
									if (regex1.Matches(strTestContent,0).Count!=(regex2.Matches(strStandardAnswer,0).Count+1))
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'���λ�����������һ�£��𰸼��ð�Ƕ��š�,������')");
									}
								}
								if ((ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�ʴ���")||(ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������"))
								{
									if (strStandardAnswer=="")
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'����𰸲���Ϊ��')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������")
								{
									if (strStandardAnswer.IndexOf(",")<0)
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'�������δ�á�,������')");
									}
									strArrTypeStandardAnswer=strStandardAnswer.Split(',');
									try
									{
										intTypeTime=Convert.ToInt32(strArrTypeStandardAnswer[0]);
									}
									catch
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��������Ĵ���ʱ�䲻��ȷ')");
									}
									try
									{
										intStandardSpeed=Convert.ToInt32(strArrTypeStandardAnswer[1]);
									}
									catch
									{
										index=index+1;
										intTmp=ObjFun.ExecuteSqlCmd("insert into ImportErr(ErrID,RowNum,ErrInfo) values("+index+","+(i+2)+",'��������ı�׼�ٶȲ���ȷ')");
									}
								}
								if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������")
								{
									
								}
							}
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
						LabelMessage.Text="��������ʧ�ܣ�";
					}
					else//���⵼��UserInfo���ݱ�
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
								strSubjectName=ObjFun.CheckString(rs.Rows[i][0].ToString().Trim());//��Ŀ����
								strLoreName=ObjFun.CheckString(rs.Rows[i][1].ToString().Trim());//֪ʶ��
								strTestTypeName=ObjFun.CheckString(rs.Rows[i][2].ToString().Trim());//��������
								strTestDiff=ObjFun.CheckString(rs.Rows[i][3].ToString().Trim());//�����Ѷ�
								strTestMark=rs.Rows[i][4].ToString().Trim();//�������
								strTestContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][5].ToString().Trim()),4000);//��������
								strOptionContent=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][6].ToString().Trim()),1800);//����ѡ��
								strStandardAnswer=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][7].ToString().Trim()),2000);//��׼��
								strTestParse=ObjFun.getStr(ObjFun.CheckTestStr(rs.Rows[i][8].ToString().Trim()),500);//�������

								intSubjectID=0;
								intLoreID=0;
								intTestTypeID=0;
								strTmp=ObjFun.GetValues("select SubjectID from SubjectInfo where SubjectName='"+ObjFun.CheckString(strSubjectName)+"'","SubjectID");
								if (strTmp!="") 
								{
									intSubjectID=Convert.ToInt32(strTmp);
								}
								strTmp=ObjFun.GetValues("select LoreID from LoreInfo where SubjectID="+intSubjectID+" and LoreName='"+ObjFun.CheckString(strLoreName)+"'","LoreID");
								if (strTmp!="")
								{
									intLoreID=Convert.ToInt32(strTmp);
								}
								strTmp=ObjFun.GetValues("select TestTypeID from TestTypeInfo where TestTypeName='"+ObjFun.CheckString(strTestTypeName)+"'","TestTypeID");
								if (strTmp!="")
								{
									intTestTypeID=Convert.ToInt32(strTmp);
								}
								if ((intSubjectID!=0)&&(intLoreID!=0)&&(intTestTypeID!=0))
								{
									strTmp=ObjFun.GetValues("select RubricID from RubricInfo where SubjectID='"+intSubjectID+"' and LoreID='"+intLoreID+"' and TestContent='"+strTestContent.Trim()+"'","RubricID");
									if (strTmp.Trim()=="")
									{
										dblTestMark=Convert.ToDouble(strTestMark);
										strBaseTestType="";
										intOptionNum=4;
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="��ѡ��")
										{
											strBaseTestType="��ѡ��";
											strArrOptionContent=strOptionContent.Split('|');
											intOptionNum=strArrOptionContent.Length;
											strStandardAnswer=strStandardAnswer.ToUpper();
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="��ѡ��")
										{
											strBaseTestType="��ѡ��";
											strArrOptionContent=strOptionContent.Split('|');
											intOptionNum=strArrOptionContent.Length;
											strStandardAnswer=strStandardAnswer.ToUpper();
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�ж���")
										{
											strBaseTestType="�ж���";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�����")
										{
											strBaseTestType="�����";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="�ʴ���")
										{
											strBaseTestType="�ʴ���";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������")
										{
											strBaseTestType="������";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������")
										{
											strBaseTestType="������";
										}
										if (ObjFun.GetValues("select BaseTestType from TestTypeInfo where TestTypeID='"+intTestTypeID+"'","BaseTestType")=="������")
										{
											strBaseTestType="������";
											strStandardAnswer="";
										}
										intTmp=ObjFun.ExecuteSqlCmd("Insert into RubricInfo(SubjectID,LoreID,TestTypeID,TestDiff,OptionNum,TestMark,TestContent,OptionContent,StandardAnswer,TestParse,CreateUserID,CreateDate) Values ("+intSubjectID+","+intLoreID+","+intTestTypeID+",'"+strTestDiff+"',"+intOptionNum+","+dblTestMark+",'"+strTestContent+"','"+strOptionContent+"','"+strStandardAnswer+"','"+strTestParse+"',"+intCreateUserID+",'"+dtmCreateDate+"')");
									}
								}
							}
							ObjTran.Commit();
						}
						catch
						{
							ObjTran.Rollback();
							LabelMessage.Text="��������ʧ�ܣ�";
						}
						finally
						{
							ObjConn.Close();
							ObjConn.Dispose();
							LabelMessage.Text="��������ɹ���";
						}
					}
				}
			}
		}
		#endregion

	}
}

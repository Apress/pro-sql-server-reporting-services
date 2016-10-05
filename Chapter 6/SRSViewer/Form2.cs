using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Web.Services.Protocols;
using SRSViewer.ReportService;
using System.Diagnostics;
using System.DirectoryServices;
using System.Security.Principal;

namespace SRSViewer
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private string url;
		private string server;
		private string report;
		ReportingService rs;

		public string URL
		{
			get
			{
				return url;
			}
		}

		public Form2(string URL)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			url = URL;
			string[] reportInfo = url.Split('?');
			server = reportInfo[0];
			report = reportInfo[1];
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(8, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 344);
			this.panel1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(104, 416);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(336, 416);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Location = new System.Drawing.Point(8, 368);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(520, 40);
			this.panel2.TabIndex = 4;
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(72, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(440, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.Text = "comboBox1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Schedule";
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 446);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		private void Form2_Load(object sender, System.EventArgs e)
		{
			rs = new ReportingService();
			rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

			bool forRendering = true;
			string historyID = null;
			ParameterValue[] values = null;
			DataSourceCredentials[] credentials = null;
			ReportParameter[] parameters = null;
			ValidValue[] pvs = null;

			int x=5;
			int y=30;

			try
			{
				parameters = rs.GetReportParameters(report, historyID, forRendering, values, credentials);

				if (parameters != null)
				{
					foreach (ReportParameter rp in parameters)
					{
						Debug.WriteLine("Name: {0}", rp.Name);
						this.SuspendLayout();
						this.panel1.SuspendLayout();
						this.panel1.SendToBack();
						// now create a label for the combo box below
						Label lbl = new Label();
						lbl.Anchor = (System.Windows.Forms.AnchorStyles.Top |
							System.Windows.Forms.AnchorStyles.Left);
						lbl.Location= new System.Drawing.Point(x,y);
						lbl.Name = rp.Name;
						lbl.Text = rp.Name;
						lbl.Size = new System.Drawing.Size(150,20);
						this.panel1.Controls.Add(lbl);
						x=x+150;
						// now make a combo box and fill it
						ComboBox a = new ComboBox();
						a.Anchor = (System.Windows.Forms.AnchorStyles.Top |
							System.Windows.Forms.AnchorStyles.Right);
						a.Location= new System.Drawing.Point(x,y);
						a.Name = rp.Name;
						a.Size = new System.Drawing.Size(200,20);
						x=5;
						y=y+30;
						this.panel1.Controls.Add(a);
						this.panel1.ResumeLayout(false);
						this.ResumeLayout(false);

						if (rp.ValidValues != null)
						{
							//Build listitems
							ArrayList aList = new ArrayList();
							pvs = rp.ValidValues;
							foreach (ValidValue pv in pvs)
							{
								aList.Add(new ComboItem(pv.Label,pv.Value));
								Debug.WriteLine(String.Format("Name: {0} - Value: {1}", pv.Label, pv.Value));
							}
							//Bind listitmes to combobox
							a.DataSource = aList;
							a.DisplayMember="Display";
							a.ValueMember="Value";
						}
					}
				}
			}

			catch (SoapException ex)
			{
				MessageBox.Show(ex.Detail.InnerXml.ToString()); 
			}

			Schedule[] schedules = null;

			try 
			{
				schedules = rs.ListSchedules();
				if (schedules != null)
				{
					//Build listitems
					ArrayList aList = new ArrayList();
					// Now lets add the Do not schedule item
					aList.Add(new ComboItem("Do not schedule", "NS"));
					// And the Snapshot schedule 
					aList.Add(new ComboItem("Schedule with Snapshot", "SS"));
					foreach (Schedule s in schedules)
					{
						aList.Add(new ComboItem(s.Description, s.ScheduleID));
						Debug.WriteLine(String.Format("Desc: {0} - ID: {1}", s.Description, s.ScheduleID));
					}
					//Bind listitmes to combobox
					comboBox1.DataSource = aList;
					comboBox1.DisplayMember="Display";
					comboBox1.ValueMember="Value";
				}
			}
			catch (SoapException ex)
			{
				MessageBox.Show(ex.Detail.InnerXml.ToString()); 
			}
		}

		private void ScheduleReport()
		{
			
			// See if they want to schedule this vs run it now
			if (comboBox1.SelectedValue.ToString() != "NS")
			{
				string desc = "Send report via email";
				string eventType = String.Empty;
				string matchData = String.Empty;
				// If they selected SnapShot then we setup the parameters for a snapshot
				if (comboBox1.SelectedValue.ToString() == "SS")
				{
					eventType = "SnapshotUpdated";
					matchData = null;
				}
				// otherwise they are using a subscription
				else
				{
					eventType = "TimedSubscription";
					matchData = comboBox1.SelectedValue.ToString();
				}

				ParameterValue[] extensionParams = new ParameterValue[8];

				extensionParams[0] = new ParameterValue();
				extensionParams[0].Name = "TO";
				extensionParams[0].Value = GetEmailFromAD();

				extensionParams[1] = new ParameterValue();
				extensionParams[1].Name = "ReplyTo";
				extensionParams[1].Value = "reporting@entegro.com";

				extensionParams[2] = new ParameterValue();
				extensionParams[2].Name = "IncludeReport";
				extensionParams[2].Value = "True";

				extensionParams[3] = new ParameterValue();
				extensionParams[3].Name = "RenderFormat";
				extensionParams[3].Value = "MHTML";

				extensionParams[4] = new ParameterValue();
				extensionParams[4].Name = "Subject";
				extensionParams[4].Value = "@ReportName was executed at @ExecutionTime";

				extensionParams[5] = new ParameterValue();
				extensionParams[5].Name = "Comment";
				extensionParams[5].Value = "Here is your @ReportName report.";

				extensionParams[6] = new ParameterValue();
				extensionParams[6].Name = "IncludeLink";
				extensionParams[6].Value = "True";

				extensionParams[7] = new ParameterValue();
				extensionParams[7].Name = "Priority";
				extensionParams[7].Value = "NORMAL";

				ParameterValue[] pvs = ReportParameters();

				ExtensionSettings extSettings = new ExtensionSettings();
				extSettings.ParameterValues = extensionParams;
				extSettings.Extension = "Report Server Email";

				try
				{
					rs.CreateSubscription(report, extSettings, desc, eventType, matchData, pvs);
				}

				catch (SoapException e)
				{
					Console.WriteLine(e.Detail.InnerXml.ToString());
				}
			}
		}

		private ParameterValue[] ReportParameters()
		{
			int numCtrls = (this.panel1.Controls.Count/2);
			ParameterValue[] pvs = new ParameterValue[numCtrls];
			int i = 0;

			foreach (Control ctrl in this.panel1.Controls)
			{
				if (ctrl.GetType() == typeof(ComboBox))
				{
					ComboBox a = (ComboBox) ctrl;
					pvs[i] = new ParameterValue();
					pvs[i].Name = a.Name;
					if (a.SelectedValue != null && a.SelectedValue.ToString() != String.Empty)
					{
						pvs[i].Value = a.SelectedValue.ToString();
					}
					i++;
				}
			}

			return pvs;
		}

		private string ReportParametersURL()
		{
			ParameterValue[] pvs = ReportParameters();
			string URL = url + "&rs:Command=Render&rs:Format=HTML4.0&rc:Parameters=false";
			foreach (ParameterValue pv in pvs)
			{
				if (pv.Value != null && pv.Value != String.Empty)
					URL += "&" + pv.Name + "=" + pv.Value;
			}
			return URL;
		}

		private string GetEmailFromAD()
		{

			DirectoryEntry rootEntry;
			DirectoryEntry contextEntry;
			DirectorySearcher searcher;
			SearchResult result;

			string currentUserName;
			string contextPath;

			WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			currentUserName = wp.Identity.Name.Split('\\')[1];

			rootEntry = new DirectoryEntry("LDAP://RootDSE");
			contextPath = rootEntry.Properties["defaultNamingContext"].Value.ToString();

			rootEntry.Dispose();
			contextEntry = new DirectoryEntry("LDAP://" + contextPath);

			searcher = new DirectorySearcher();
			searcher.SearchRoot = contextEntry;
			searcher.Filter = String.Format("(&(objectCategory=person)(samAccountName={0}))",currentUserName);
			searcher.PropertiesToLoad.Add("mail");
			searcher.PropertiesToLoad.Add("cn");
			searcher.SearchScope = SearchScope.Subtree;

			result = searcher.FindOne();
//			Debug.WriteLine(result.Properties["cn"][0].ToString());
//			Debug.WriteLine(result.Properties["mail"][0].ToString());

			return result.Properties["mail"][0].ToString();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (comboBox1.SelectedValue.ToString() == "NS")
			{
				string URL = ReportParametersURL();
				url = URL;
				this.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				ScheduleReport();
				this.DialogResult = DialogResult.Cancel;
				Close();
			}
		}
		
		private void button2_Click(object sender, System.EventArgs e)
		{
			string bs = GetEmailFromAD();
			this.DialogResult = DialogResult.Cancel;
		}

	}
	public class ComboItem
	{
		public ComboItem(string disp, string myvalue)
		{
			if (disp != null)
				display=disp;
			else
				display = "";
			if (myvalue != null)
				val=myvalue;
			else
				val = "";
		}

		private string val;
		public string Value
		{
			get{return val;}
			set{val=value;}
		}

		private string display;
		public string Display
		{
			get{return display;}
			set{display=value;}
		}

		public override string ToString()
		{
			return display;
		}
	}
}

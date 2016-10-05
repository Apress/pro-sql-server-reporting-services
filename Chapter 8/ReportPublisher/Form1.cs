using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.Services.Protocols;
using ReportPublisher.ReportService;
using System.Text.RegularExpressions;


namespace ReportPublisher
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private	ReportingService rs;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button OpenFile;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button GetFolders;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.OpenFile = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.GetFolders = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(96, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(248, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "hwc04";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Report Server";
			// 
			// OpenFile
			// 
			this.OpenFile.Location = new System.Drawing.Point(112, 488);
			this.OpenFile.Name = "OpenFile";
			this.OpenFile.TabIndex = 2;
			this.OpenFile.Text = "Open";
			this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(248, 488);
			this.button3.Name = "button3";
			this.button3.TabIndex = 3;
			this.button3.Text = "Cancel";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(16, 80);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(416, 392);
			this.treeView1.TabIndex = 4;
			// 
			// GetFolders
			// 
			this.GetFolders.Location = new System.Drawing.Point(360, 40);
			this.GetFolders.Name = "GetFolders";
			this.GetFolders.TabIndex = 5;
			this.GetFolders.Text = "Go";
			this.GetFolders.Click += new System.EventHandler(this.GetFolders_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 526);
			this.Controls.Add(this.GetFolders);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.OpenFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

		}

		private void OpenFile_Click(object sender, System.EventArgs e)
		{

			// Get the full pathname from the treeview control
			string pathName = treeView1.SelectedNode.FullPath;

			if (pathName == "Root")
				pathName = "/";
			else
			{
				// Strip off the Root name from the path and correct the path separators for use with SRS
				pathName = pathName.Substring(4,pathName.Length-4);
				pathName = pathName.Replace(@"\", "/");
			}

			byte[] definition = null;
			Warning[] warnings = null;
			string warningMsg = String.Empty;

			openFileDialog1.Filter = "RDL files (*.rdl)|*.rdl|All files (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1;
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// Read the file and put it into a byte array to pass to SRS
					FileStream stream = File.OpenRead(openFileDialog1.FileName);
					definition = new byte[stream.Length];
					stream.Read(definition, 0, (int)(stream.Length));
					stream.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show (ex.Message);
				}

				// We are going to use the name of the rdl file as the name of our report
				string reportName =  Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

				// Now lets use this information to publish the report
				try
				{
					warnings = rs.CreateReport(reportName, pathName, true, definition, null);
					if (warnings != null)
					{
						foreach (Warning warning in warnings)
						{
							warningMsg += warning.Message + "\n";
						}
						MessageBox.Show("Report creation failed with the following warnings:\n" + warningMsg);
					}
					else
						MessageBox.Show(String.Format("Report: {0} created successfully with no warnings", reportName));
				}
				catch (SoapException ex)
				{
					MessageBox.Show(ex.Detail.InnerXml.ToString());
				}

			}
		}

		private void GetFolders_Click(object sender, System.EventArgs e)
		{
			rs = new ReportingService();
			rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
			CatalogItem[] items = null;
			rs.Url = GetRSURL();;

			TreeNode root = new TreeNode();
			root.Text = "Root";
			treeView1.Nodes.Add(root);
			treeView1.SelectedNode = treeView1.TopNode;

			// Retrieve a list items from the server 
			try
			{
				items = rs.ListChildren("/", true);
			}

			catch (SoapException ex)
			{
				MessageBox.Show(ex.Detail.InnerXml.ToString());
			}

			int j = 1;

			// Iterate through the list of items and find all of the folders and display them to the user
			foreach (CatalogItem ci in items)
			{
				if (ci.Type == ItemTypeEnum.Folder)
				{
					Regex rx = new Regex("/");
					int matchCnt = rx.Matches(ci.Path).Count;
					if (matchCnt > j)
					{
						treeView1.SelectedNode = treeView1.SelectedNode.LastNode;
						j = matchCnt;
					}
					else if (matchCnt < j)
					{
						treeView1.SelectedNode = treeView1.SelectedNode.Parent;
						j = matchCnt;
					}
					AddNode(ci.Name);
				}
			}
			// Make sure the user can see that the root folder is selected by default
			treeView1.HideSelection = false;
		}

		private string GetRSURL()
		{
			if (textBox1.Text.StartsWith("http://"))
				return textBox1.Text + "/reportserver/ReportService.asmx";
			else
				return "http://" + textBox1.Text + "/reportserver/ReportService.asmx";

		}

		private void AddNode(string name)
		{
			TreeNode newNode = new TreeNode(name);
			treeView1.SelectedNode.Nodes.Add(newNode);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}
	}
}

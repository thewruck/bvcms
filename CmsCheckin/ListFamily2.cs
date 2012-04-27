using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace CmsCheckin
{
	public partial class ListFamily2 : UserControl
	{
		private const int ExtraPixelsName = 15;
		private const string STR_CheckMark = "�";
		public ListFamily2()
		{
			InitializeComponent();
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			const int WM_KEYDOWN = 0x100;
			const int WM_SYSKEYDOWN = 0x104;

			if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
			{
				switch (keyData)
				{
					case Keys.PageUp:
						if (pgup.Visible)
							ShowPage(page - 1);
						return true;
					case Keys.PageDown:
						if (pgdn.Visible)
							ShowPage(page + 1);
						return true;
					case Keys.Escape:
						Program.TimerStop();
						this.GoHome(string.Empty);
						return true;
					case Keys.Return:
						Program.TimerStop();
						//DoPrinting(null, null);
						this.GoHome(string.Empty);
						return true;
					case Keys.S | Keys.Alt:
						Program.TimerReset();
						Program.CursorShow();
						foreach (var c in sucontrols)
						{
							c.Enabled = true;
							c.BackColor = Color.Coral;
						}
						return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private bool hasprinter;
		DoPrinting doprint = new DoPrinting();
		List<PersonInfo> list;
		int page = 1;
		List<Control> controls = new List<Control>();
		List<Control> sucontrols = new List<Control>();
		XDocument xdoc;
		const string Verdana = "Verdana";
		Font pfont = new Font(Verdana, 14f, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

		public void ShowFamily(int fid)
		{
			page = 1;
			xdoc = this.GetDocument("Checkin2/Family/" + fid);
			ShowFamily(xdoc);
		}
		public void ShowFamily(XDocument x)
		{
			xdoc = x;
			hasprinter = PrintRawHelper.HasPrinter(Program.Printer);
			this.Focus();

			list = new List<PersonInfo>();
			if (x.Descendants("member").Count() == 0)
			{
				ClearControls();
				var lab = new Label();
				lab.Font = pfont;
				lab.Location = new Point(15, 200);
				lab.AutoSize = true;
				pgup.Visible = false;
				pgdn.Visible = false;
				lab.Text = "Not Found, try another phone number, or 411?";
				this.Controls.Add(lab);
				Return.Text = "Try Again";
				controls.Add(lab);
				return;
			}

			foreach (var e in x.Descendants("member"))
			{
				var a = new PersonInfo
				{
					pid = e.Attribute("id").Value.ToInt(),
					first = e.Attribute("first").Value,
					last = e.Attribute("last").Value,
					dob = e.Attribute("dob").Value,

					goesby = e.Attribute("goesby").Value,
					email = e.Attribute("email").Value,
					addr = e.Attribute("addr").Value,
					zip = e.Attribute("zip").Value,
					home = e.Attribute("home").Value.FmtFone(),
					cell = e.Attribute("cell").Value.FmtFone(),
					gender = e.Attribute("gender").Value.ToInt(),
					marital = e.Attribute("marital").Value.ToInt(),
					Row = list.Count,
					HasPicture = bool.Parse(e.Attribute("haspicture").Value),
				};
				list.Add(a);
			}
			ShowPage(1);
		}
		public void ShowPage(int page)
		{
			ClearControls();
			this.page = page;

			const int sep = 10;
			const int rowheight = 50;
			int top = 50;
			const int bsize = 45;
			const int bwid = 65;
			const int mwid = 80;

			var points = 14F;
			var g = this.CreateGraphics();

			Font font;
			Font labfont;
			string Present = "Attend";
			string Labels = "Labels";
			Return.Text = "Print Labels, Return";

			var cols = new int[6];

			const int PageSize = 10;

			int srow = (page - 1) * PageSize;
			int erow = srow + PageSize;
			if (erow > list.Count)
				erow = list.Count;
			pgdn.Visible = list.Count > erow;
			pgup.Visible = srow > 0;

			int maxheight;
			int twidab, widname, widorg, twidlb;
			int totalwid;

			while (true)
			{
				twidab = widname = widorg = twidlb = maxheight = 0;
				font = new Font(Verdana, points, FontStyle.Regular,
					GraphicsUnit.Point, ((byte)(0)));
				labfont = new Font(Verdana, points,
					((FontStyle)((FontStyle.Italic | FontStyle.Underline))),
					GraphicsUnit.Point, ((byte)(0)));
				maxheight = 0;
				foreach (var c in list)
				{
					var size = g.MeasureString(Present, labfont);
					twidab = Math.Max(twidab, (int)Math.Ceiling(size.Width));
					twidab = Math.Max(twidab, bwid);

					size = g.MeasureString(c.name, font);
					widname = Math.Max(widname, (int)Math.Ceiling(size.Width) + ExtraPixelsName);

					size = g.MeasureString(Labels, labfont);
					twidlb = Math.Max(twidlb, (int)Math.Ceiling(size.Width));
					twidlb = Math.Max(twidlb, mwid);

					size = g.MeasureString("Choose activities to show here, this needs to be really long", font);
					widorg = Math.Max(widorg, (int)Math.Ceiling(size.Width));

					size = g.MeasureString("|", labfont);
					maxheight = Math.Max(maxheight, (int)Math.Ceiling(size.Height));
				}

				totalwid = sep + twidab + sep + widname + sep + widorg
									+ sep + twidlb + sep;
				if (totalwid > 1024)
				{
					points -= 1F;
					continue;
				}
				break;
			}
			var labtop = top - rowheight;
			var LeftEdge = (1024 - totalwid) / 2;

			var head = new Label();
			LeftEdge += sep;
			head.Location = new Point(LeftEdge, labtop);
			head.Size = new Size(twidab + 5, maxheight);
			head.Font = labfont;
			head.Text = Present;
			this.Controls.Add(head);
			controls.Add(head);

			head = new Label();
			LeftEdge += twidab + sep;
			head.Location = new Point(LeftEdge, labtop);
			head.Size = new Size(widname + 5, maxheight);
			head.Font = labfont;
			head.Text = "Name";
			this.Controls.Add(head);
			controls.Add(head);

			head = new Label();
			LeftEdge += mwid + sep + widname + sep;
			head.Location = new Point(LeftEdge, labtop);
			head.Size = new Size(widorg + 5, maxheight);
			head.Font = labfont;
			head.Text = "Activity";
			this.Controls.Add(head);
			controls.Add(head);

			for (var r = srow; r < erow; r++)
			{
				var c = list[r];

				LeftEdge = (1024 - totalwid) / 2;
				top += rowheight;

				var ab = new Button();
				LeftEdge += sep;
				ab.Location = new Point(LeftEdge, top - 5);
				ab.Size = new Size(bwid, bsize);

				ab.FlatStyle = FlatStyle.Flat;
				ab.FlatAppearance.BorderSize = 1;

				ab.BackColor = Color.CornflowerBlue;
				ab.FlatAppearance.BorderColor = Color.Black;


				ab.ForeColor = Color.White;
				ab.Font = new Font("Wingdings", 24, FontStyle.Bold,
					GraphicsUnit.Point, ((byte)(2)));
				ab.Name = "attend" + c.Row;
				ab.TextAlign = ContentAlignment.TopCenter;
				ab.UseVisualStyleBackColor = false;
				this.Controls.Add(ab);
				ab.KeyDown += AttendButton_KeyDown;
				ab.Click += Attend_Click;
				ab.Tag = c.Row;
				controls.Add(ab);

				var nam = new Button();
				LeftEdge += twidab + sep;
				nam.UseVisualStyleBackColor = false;
				nam.FlatStyle = FlatStyle.Flat;
				nam.FlatAppearance.BorderSize = 1;
				nam.FlatAppearance.BorderColor = Color.Black;
				if (c.HasPicture)
					nam.BackColor = Color.FromArgb(0xFF, 0xCC, 0x99);
				else
					nam.BackColor = Color.White;

				nam.Location = new Point(LeftEdge, top - 5);
				nam.Size = new Size(widname, bsize);

				nam.Font = font;
				nam.UseMnemonic = false;
				nam.Text = c.name;
				nam.Name = "name" + c.Row;
				nam.TextAlign = ContentAlignment.MiddleLeft;

				nam.Click += new EventHandler(ShowPic_Click);
				nam.Enabled = false;
				nam.Tag = c.Row;
				this.Controls.Add(nam);
				controls.Add(nam);
				sucontrols.Add(nam);

				var menu = new Button();
				LeftEdge += widname + 5 + sep;
				menu.Location = new Point(LeftEdge, top - 5);
				menu.Size = new Size(mwid, bsize);
				menu.Text = "menu";
				menu.BackColor = SystemColors.Control;
				menu.Enabled = false;
				menu.Font = pfont;
				menu.Name = "menu" + c.Row;
				menu.Tag = c.Row;
				menu.TextAlign = ContentAlignment.TopCenter;
				menu.UseVisualStyleBackColor = false;
				this.Controls.Add(menu);
				menu.Click += new EventHandler(Menu_Click);
				controls.Add(menu);
				sucontrols.Add(menu);

				var org = new Label();
				LeftEdge += mwid + 5 + sep;
				org.Location = new Point(LeftEdge, top);
				org.Size = new Size(widorg + 5, maxheight);
				org.Font = font;
				org.UseMnemonic = false;
				org.Text = "Choose Activities";
				org.TextAlign = ContentAlignment.MiddleLeft;
				org.Name = "org" + c.Row;
				this.Controls.Add(org);
				controls.Add(org);
			}
			Program.TimerStart(timer1_Tick);
			ComputeLabels();
		}

		void timer1_Tick(object sender, EventArgs e)
		{
			Program.TimerStop();
			Util.UnLockFamily();
			Program.ClearFields();
			this.GoHome("");
		}

		void AttendButton_KeyDown(object sender, KeyEventArgs e)
		{
			Program.TimerReset();
			if (e.KeyValue == 27)
			{
				Program.TimerStop();
				Util.UnLockFamily();
				this.GoHome(string.Empty);
			}
		}

		void Attend_Click(object sender, EventArgs e)
		{
			Attend_Click((Button)sender);
		}
		public void Attend_Click(Button ab)
		{
			Program.TimerReset();
			var c = list[(int)ab.Tag];
			if (c.lastpress.HasValue && DateTime.Now.Subtract(c.lastpress.Value).TotalSeconds < 1)
				return;
			Cursor.Current = Cursors.WaitCursor;
			Program.CursorShow();
			if (ab.Text == String.Empty)
				ab.Text = STR_CheckMark;
			else
				ab.Text = String.Empty;
			c.lastpress = DateTime.Now;
			ComputeLabels();
			var bw = new BackgroundWorker();
			bw.DoWork += CheckUnCheckDoWork;
			bw.RunWorkerCompleted += CheckUncheckCompleted;
			bw.RunWorkerAsync();
		}

		void ShowPic_Click(object sender, EventArgs e)
		{
			Program.TimerReset();
			var eb = sender as Button;
			var ab = this.Controls[this.Controls.IndexOfKey("attend" + eb.Tag.ToString())] as Button;
			var c = list[(int)ab.Tag];
			Program.PeopleId = c.pid;
			var f = new Picture();
			f.ShowDialog();
		}

		public List<ClassInfo> classlist = new List<ClassInfo>();
		private Label mask;
		private Menu menu;

		void Menu_Click(object sender, EventArgs e)
		{
			Program.TimerReset();
			var MenuButton = sender as Button;
			menu = new Menu();
			menu.Tag = MenuButton.Tag;
			menu.Parent = this;
			menu.Location = new Point(MenuButton.Location.X - 100, MenuButton.Location.Y + MenuButton.Height);
			mask = new Label();
			mask.BackColor = this.BackColor;
			mask.Size = this.Size;
			//mask.Location = this.Location;
			mask.Parent = this;
			mask.BringToFront();
			var nam = this.Controls[this.Controls.IndexOfKey("name" + menu.Tag.ToString())] as Button;
			var org = this.Controls[this.Controls.IndexOfKey("org" + menu.Tag.ToString())] as Label;
			nam.BringToFront();
			org.BringToFront();

			mask.Show();
			var c = list[(int)menu.Tag];
			nam.Enabled = false;
			menu.EditRecord += EditRecord_Click;
			menu.AddFamily += AddToFamily_Click;
			menu.DropJoin.Visible = false;
			menu.CancelMenu += CancelMenu_Click;
			menu.Show();
			menu.BringToFront();
		}

		void CancelMenu_Click(object sender, EventArgs e)
		{
			RemoveMenu();
		}
		private void ComputeLabels()
		{
			Return.Text = "Print Labels, Return";
		}

		void EditRecord_Click(object sender, EventArgs e)
		{
			var c = list[(int)menu.Tag];

			var home = Program.home2;
			Program.PeopleId = c.pid;
			home.SetFields(c.last, c.email, c.addr, c.zip, c.home);
			home.first.textBox1.Text = c.first;
			home.goesby.textBox1.Text = c.goesby;
			home.dob.textBox1.Text = c.dob;
			home.cellphone.textBox1.Text = c.cell.FmtFone();
			home.gendermarital.Marital = c.marital;
			home.gendermarital.Gender = c.gender;
			Util.UnLockFamily();

			Program.editing = true;
			RemoveMenu();
			this.Swap(home.first);
		}
		private void RemoveMenu()
		{
			var nam = this.Controls[this.Controls.IndexOfKey("name" + menu.Tag.ToString())] as Button;
			nam.Enabled = true;
			this.Controls.Remove(menu);
			this.Controls.Remove(mask);
			menu.Dispose();
			mask.Dispose();
		}

		PleaseWait PleaseWaitForm = null;
		private void ClearControls()
		{
			foreach (var c in controls)
			{
				this.Controls.Remove(c);
				c.Dispose();
			}
			controls.Clear();
			doprint.LabelsPrinted = 0;
			sucontrols.Clear();
		}

		private void MagicButton_Click(object sender, EventArgs e)
		{
			Program.TimerStop();
			if (list.Count == 0)
			{
				if (Program.baseform.textbox.Parent is Home)
					this.Swap(Program.home.namesearch);
				else if (Program.baseform.textbox.Parent is Home2)
					this.Swap(Program.home2.namesearch);
				return;
			}
			foreach (var c in sucontrols)
			{
				c.Enabled = true;
				if (c.BackColor == SystemColors.Control)
					c.BackColor = Color.Coral;
			}
		}

		private void pgup_Click(object sender, EventArgs e)
		{
			ShowPage(page - 1);
		}

		private void pgdn_Click(object sender, EventArgs e)
		{
			ShowPage(page + 1);
		}

		private void AddToFamily_Click(object sender, EventArgs e)
		{
			var c = list[(int)menu.Tag];
			Program.editing = false;
			Util.UnLockFamily();
			RemoveMenu();

			Program.home2.SetFields(c.last, c.email, c.addr, c.zip, c.home);
			this.Swap(Program.home2.first);
		}

		private void CheckUnCheckDoWork(object sender, DoWorkEventArgs e)
		{
			var info = e.Argument as Util.ClassCheckedInfo;

		}
		private void CheckUncheckCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Cursor.Current = Cursors.Default;
			Program.CursorHide();
		}

		private void Return_Click(object sender, EventArgs e)
		{
			Program.TimerStop();

			if (Return.Text.Contains("Try Again"))
			{
				this.GoHome(string.Empty);
				return;
			}
			PleaseWaitForm = new PleaseWait();
			PleaseWaitForm.Show();

			var bw = new BackgroundWorker();
			bw.DoWork += DoPrinting;
			bw.RunWorkerCompleted += PrintingCompleted;
			bw.RunWorkerAsync();
		}
        private void DoPrinting(object sender, DoWorkEventArgs e)
        {

        }
        private void PrintingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PleaseWaitForm.Hide();
            PleaseWaitForm.Dispose();
            PleaseWaitForm = null;
            Program.FamilyId = 0;
            classlist = new List<ClassInfo>();
            this.GoHome(string.Empty);
        }
		public class PersonInfo
		{
			public DateTime? lastpress { get; set; }
			public int pid { get; set; }
			public string first { get; set; }
			public string last { get; set; }
			public string dob { get; set; }

			public string goesby { get; set; }
			public string email { get; set; }
			public string addr { get; set; }
			public string zip { get; set; }
			public string home { get; set; }
			public string cell { get; set; }

			public int gender { get; set; }
			public int marital { get; set; }

			public string activities { get; set; }
			public int Row { get; set; }
			public bool HasPicture { get; set; }

			public string name
			{
				get { return first + " " + last; }
			}
		}

		// all of these come from the attribtues on the attendee element
		// attributes have the same name unless noted otherwise
		[Serializable]
		public class LabelInfo
		{
			public int n { get; set; } // numlabels attribute
			public string location { get; set; } // loc attribute
			public string allergies { get; set; }
			public string org { get; set; } // orgname attribute
			public DateTime? hour { get; set; }
			public int pid { get; set; } // id attribute
			public string mv { get; set; }
			public string first { get; set; }
			public string last { get; set; }
			public bool transport { get; set; }
			public bool custody { get; set; }
			public bool requiressecuritylabel { get; set; }
		}
		[Serializable]
		public class PrintJob
		{
			// securitycode comes from the attribute on the root element (Attendees)
			public string securitycode { get; set; }
			// the following is a list of each person/class that was checked present
			public List<LabelInfo> list { get; set; }
		}
		[Serializable]
		public class PrintJobs
		{
			public List<PrintJob> jobs { get; set; }
		}

	}
}
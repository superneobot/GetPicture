
namespace GetPicture
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.clearbtn = new System.Windows.Forms.Button();
            this.downloadbtn = new System.Windows.Forms.Button();
            this.listlinks = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkbox = new System.Windows.Forms.TextBox();
            this.searchbtn = new System.Windows.Forms.Button();
            this.picname = new System.Windows.Forms.TextBox();
            this.LV = new System.Windows.Forms.ListView();
            this.timeout = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1073, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(45, 17);
            this.status.Text = "Готово";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.clearbtn);
            this.panel1.Controls.Add(this.downloadbtn);
            this.panel1.Controls.Add(this.listlinks);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.linkbox);
            this.panel1.Controls.Add(this.searchbtn);
            this.panel1.Controls.Add(this.picname);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 612);
            this.panel1.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar1.Location = new System.Drawing.Point(6, 380);
            this.progressBar1.Maximum = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(128, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // clearbtn
            // 
            this.clearbtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.clearbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearbtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.clearbtn.Location = new System.Drawing.Point(6, 438);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(128, 23);
            this.clearbtn.TabIndex = 9;
            this.clearbtn.Text = "Очистить";
            this.clearbtn.UseVisualStyleBackColor = false;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // downloadbtn
            // 
            this.downloadbtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.downloadbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadbtn.Enabled = false;
            this.downloadbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadbtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.downloadbtn.Location = new System.Drawing.Point(6, 409);
            this.downloadbtn.Name = "downloadbtn";
            this.downloadbtn.Size = new System.Drawing.Size(128, 23);
            this.downloadbtn.TabIndex = 8;
            this.downloadbtn.Text = "Скачать все";
            this.downloadbtn.UseVisualStyleBackColor = false;
            this.downloadbtn.Click += new System.EventHandler(this.downloadbtn_Click);
            // 
            // listlinks
            // 
            this.listlinks.BackColor = System.Drawing.Color.DimGray;
            this.listlinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listlinks.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.listlinks.FormattingEnabled = true;
            this.listlinks.HorizontalScrollbar = true;
            this.listlinks.Location = new System.Drawing.Point(6, 84);
            this.listlinks.Name = "listlinks";
            this.listlinks.Size = new System.Drawing.Size(128, 288);
            this.listlinks.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Список ссылок ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 2, 5, 0);
            this.label1.Size = new System.Drawing.Size(139, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ключевое слово поиска";
            // 
            // linkbox
            // 
            this.linkbox.Location = new System.Drawing.Point(6, 467);
            this.linkbox.Name = "linkbox";
            this.linkbox.Size = new System.Drawing.Size(126, 20);
            this.linkbox.TabIndex = 4;
            this.linkbox.Visible = false;
            this.linkbox.Click += new System.EventHandler(this.linkbox_Click);
            // 
            // searchbtn
            // 
            this.searchbtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.searchbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.searchbtn.Location = new System.Drawing.Point(6, 42);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(128, 23);
            this.searchbtn.TabIndex = 3;
            this.searchbtn.Text = "Поиск";
            this.searchbtn.UseVisualStyleBackColor = false;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click_1);
            // 
            // picname
            // 
            this.picname.BackColor = System.Drawing.Color.DimGray;
            this.picname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picname.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.picname.Location = new System.Drawing.Point(5, 16);
            this.picname.Margin = new System.Windows.Forms.Padding(5);
            this.picname.Name = "picname";
            this.picname.Size = new System.Drawing.Size(129, 20);
            this.picname.TabIndex = 2;
            this.picname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.picname.Click += new System.EventHandler(this.picname_Click);
            this.picname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.picname_KeyDown);
            // 
            // LV
            // 
            this.LV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.LV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LV.HideSelection = false;
            this.LV.Location = new System.Drawing.Point(143, 0);
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(930, 612);
            this.LV.TabIndex = 5;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LV_MouseClick);
            // 
            // timeout
            // 
            this.timeout.Tick += new System.EventHandler(this.timeout_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 634);
            this.Controls.Add(this.LV);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GetPicture";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.TextBox picname;
        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.TextBox linkbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listlinks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button downloadbtn;
        private System.Windows.Forms.Button clearbtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.Timer timeout;
    }
}


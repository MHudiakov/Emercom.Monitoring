namespace Init.Tools.UI.Forms
{
    partial class fmError
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmError));
            this.lcMessage = new DevExpress.XtraEditors.LabelControl();
            this.lcDetails = new DevExpress.XtraEditors.LabelControl();
            this.pcBottom = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.peIcon = new DevExpress.XtraEditors.PictureEdit();
            this.pcTop = new DevExpress.XtraEditors.PanelControl();
            this.meError = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).BeginInit();
            this.pcBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).BeginInit();
            this.pcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meError.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMessage
            // 
            this.lcMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lcMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lcMessage.Location = new System.Drawing.Point(57, 3);
            this.lcMessage.Name = "lcMessage";
            this.lcMessage.Size = new System.Drawing.Size(524, 13);
            this.lcMessage.TabIndex = 0;
            this.lcMessage.Text = "labelControl1";
            // 
            // lcDetails
            // 
            this.lcDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lcDetails.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lcDetails.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lcDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lcDetails.Location = new System.Drawing.Point(506, 3);
            this.lcDetails.Name = "lcDetails";
            this.lcDetails.Size = new System.Drawing.Size(75, 13);
            this.lcDetails.TabIndex = 3;
            this.lcDetails.Text = "Подробнее...";
            this.lcDetails.Click += new System.EventHandler(this.LcDetailsClick);
            // 
            // pcBottom
            // 
            this.pcBottom.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pcBottom.Appearance.Options.UseBackColor = true;
            this.pcBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcBottom.Controls.Add(this.simpleButton1);
            this.pcBottom.Controls.Add(this.lcDetails);
            this.pcBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcBottom.Location = new System.Drawing.Point(0, 55);
            this.pcBottom.MaximumSize = new System.Drawing.Size(584, 40);
            this.pcBottom.MinimumSize = new System.Drawing.Size(584, 40);
            this.pcBottom.Name = "pcBottom";
            this.pcBottom.Size = new System.Drawing.Size(584, 40);
            this.pcBottom.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Image = global::Init.Tools.UI.Properties.Resources.xClose_16;
            this.simpleButton1.Location = new System.Drawing.Point(246, 14);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(92, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Закрыть";
            // 
            // peIcon
            // 
            this.peIcon.EditValue = global::Init.Tools.UI.Properties.Resources.warning_png;
            this.peIcon.Location = new System.Drawing.Point(5, 5);
            this.peIcon.Name = "peIcon";
            this.peIcon.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peIcon.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.peIcon.Properties.Appearance.Options.UseBackColor = true;
            this.peIcon.Properties.Appearance.Options.UseForeColor = true;
            this.peIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peIcon.Properties.ReadOnly = true;
            this.peIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peIcon.Properties.UseParentBackground = true;
            this.peIcon.Size = new System.Drawing.Size(48, 48);
            this.peIcon.TabIndex = 1;
            // 
            // pcTop
            // 
            this.pcTop.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pcTop.Appearance.Options.UseBackColor = true;
            this.pcTop.AutoSize = true;
            this.pcTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTop.Controls.Add(this.peIcon);
            this.pcTop.Controls.Add(this.lcMessage);
            this.pcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTop.Location = new System.Drawing.Point(0, 0);
            this.pcTop.MaximumSize = new System.Drawing.Size(584, 0);
            this.pcTop.MinimumSize = new System.Drawing.Size(584, 56);
            this.pcTop.Name = "pcTop";
            this.pcTop.Size = new System.Drawing.Size(584, 56);
            this.pcTop.TabIndex = 7;
            // 
            // meError
            // 
            this.meError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meError.Location = new System.Drawing.Point(0, 56);
            this.meError.Name = "meError";
            this.meError.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meError.Properties.ReadOnly = true;
            this.meError.Size = new System.Drawing.Size(584, 0);
            this.meError.TabIndex = 8;
            this.meError.Visible = false;
            // 
            // fmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 95);
            this.Controls.Add(this.meError);
            this.Controls.Add(this.pcTop);
            this.Controls.Add(this.pcBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Внимание!";
            this.Load += new System.EventHandler(this.FmErrorLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).EndInit();
            this.pcBottom.ResumeLayout(false);
            this.pcBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).EndInit();
            this.pcTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meError.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lcMessage;
        private DevExpress.XtraEditors.PictureEdit peIcon;
        private DevExpress.XtraEditors.LabelControl lcDetails;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl pcBottom;
        private DevExpress.XtraEditors.PanelControl pcTop;
        private DevExpress.XtraEditors.MemoEdit meError;

    }
}
namespace Client.UI
{
    partial class ucCatalogkGroup
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.tlGroup = new DevExpress.XtraTreeList.TreeList();
            this.tlclmName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlclmDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.sbAddGroup = new DevExpress.XtraEditors.SimpleButton();
            this.sbEditGroup = new DevExpress.XtraEditors.SimpleButton();
            this.sbDeleteGroup = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciAddGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEditGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDeleteGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEditGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.tlGroup);
            this.layoutControl.Controls.Add(this.sbAddGroup);
            this.layoutControl.Controls.Add(this.sbEditGroup);
            this.layoutControl.Controls.Add(this.sbDeleteGroup);
            this.layoutControl.Controls.Add(this.panelControl);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(1200, 750);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // tlGroup
            // 
            this.tlGroup.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlclmName,
            this.tlclmDescription});
            this.tlGroup.KeyFieldName = "Id";
            this.tlGroup.Location = new System.Drawing.Point(12, 112);
            this.tlGroup.Name = "tlGroup";
            this.tlGroup.ParentFieldName = "ParentId";
            this.tlGroup.Size = new System.Drawing.Size(1176, 600);
            this.tlGroup.TabIndex = 10;
            // 
            // tlclmName
            // 
            this.tlclmName.Caption = "Название";
            this.tlclmName.FieldName = "Name";
            this.tlclmName.Name = "tlclmName";
            this.tlclmName.OptionsColumn.AllowEdit = false;
            this.tlclmName.OptionsColumn.ReadOnly = true;
            this.tlclmName.Visible = true;
            this.tlclmName.VisibleIndex = 0;
            this.tlclmName.Width = 427;
            // 
            // tlclmDescription
            // 
            this.tlclmDescription.Caption = "Описание";
            this.tlclmDescription.FieldName = "Description";
            this.tlclmDescription.Name = "tlclmDescription";
            this.tlclmDescription.Visible = true;
            this.tlclmDescription.VisibleIndex = 1;
            // 
            // sbAddGroup
            // 
            this.sbAddGroup.Image = global::Client.Properties.Resources.xAdd;
            this.sbAddGroup.Location = new System.Drawing.Point(12, 716);
            this.sbAddGroup.Name = "sbAddGroup";
            this.sbAddGroup.Size = new System.Drawing.Size(97, 22);
            this.sbAddGroup.StyleController = this.layoutControl;
            this.sbAddGroup.TabIndex = 7;
            this.sbAddGroup.Text = "Добавить...";
            // 
            // sbEditGroup
            // 
            this.sbEditGroup.Image = global::Client.Properties.Resources.xEdit;
            this.sbEditGroup.Location = new System.Drawing.Point(113, 716);
            this.sbEditGroup.Name = "sbEditGroup";
            this.sbEditGroup.Size = new System.Drawing.Size(97, 22);
            this.sbEditGroup.StyleController = this.layoutControl;
            this.sbEditGroup.TabIndex = 8;
            this.sbEditGroup.Text = "Изменить...";
            // 
            // sbDeleteGroup
            // 
            this.sbDeleteGroup.Image = global::Client.Properties.Resources.xDelete;
            this.sbDeleteGroup.Location = new System.Drawing.Point(214, 716);
            this.sbDeleteGroup.Name = "sbDeleteGroup";
            this.sbDeleteGroup.Size = new System.Drawing.Size(97, 22);
            this.sbDeleteGroup.StyleController = this.layoutControl;
            this.sbDeleteGroup.TabIndex = 9;
            this.sbDeleteGroup.Text = "Удалить";
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.lblDescription);
            this.panelControl.Controls.Add(this.lblHeader);
            this.panelControl.Controls.Add(this.pictureEdit);
            this.panelControl.Location = new System.Drawing.Point(12, 12);
            this.panelControl.MaximumSize = new System.Drawing.Size(0, 96);
            this.panelControl.MinimumSize = new System.Drawing.Size(0, 96);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1176, 96);
            this.panelControl.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AllowHtmlString = true;
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblDescription.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescription.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            this.lblDescription.LineVisible = true;
            this.lblDescription.Location = new System.Drawing.Point(102, 27);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(1054, 69);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Данный раздел содержит информацию о группах оборудования и предназначен для редак" +
    "тирования этой информации.";
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(102, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(221, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Справочник групп оборудования";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Client.Properties.Resources.OperPrograms_96;
            this.pictureEdit.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit.MaximumSize = new System.Drawing.Size(96, 96);
            this.pictureEdit.MinimumSize = new System.Drawing.Size(96, 96);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit.Size = new System.Drawing.Size(96, 96);
            this.pictureEdit.TabIndex = 0;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciAddGroup,
            this.lciEditGroup,
            this.lciDeleteGroup,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(1200, 750);
            this.layoutControlGroup.TextVisible = false;
            // 
            // lciAddGroup
            // 
            this.lciAddGroup.Control = this.sbAddGroup;
            this.lciAddGroup.CustomizationFormText = "layoutControlItem4";
            this.lciAddGroup.Location = new System.Drawing.Point(0, 704);
            this.lciAddGroup.MaxSize = new System.Drawing.Size(101, 26);
            this.lciAddGroup.MinSize = new System.Drawing.Size(101, 26);
            this.lciAddGroup.Name = "lciAddGroup";
            this.lciAddGroup.Size = new System.Drawing.Size(101, 26);
            this.lciAddGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciAddGroup.TextSize = new System.Drawing.Size(0, 0);
            this.lciAddGroup.TextVisible = false;
            // 
            // lciEditGroup
            // 
            this.lciEditGroup.Control = this.sbEditGroup;
            this.lciEditGroup.CustomizationFormText = "layoutControlItem5";
            this.lciEditGroup.Location = new System.Drawing.Point(101, 704);
            this.lciEditGroup.MaxSize = new System.Drawing.Size(101, 26);
            this.lciEditGroup.MinSize = new System.Drawing.Size(101, 26);
            this.lciEditGroup.Name = "lciEditGroup";
            this.lciEditGroup.Size = new System.Drawing.Size(101, 26);
            this.lciEditGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciEditGroup.TextSize = new System.Drawing.Size(0, 0);
            this.lciEditGroup.TextVisible = false;
            // 
            // lciDeleteGroup
            // 
            this.lciDeleteGroup.Control = this.sbDeleteGroup;
            this.lciDeleteGroup.CustomizationFormText = "layoutControlItem6";
            this.lciDeleteGroup.Location = new System.Drawing.Point(202, 704);
            this.lciDeleteGroup.MaxSize = new System.Drawing.Size(101, 26);
            this.lciDeleteGroup.MinSize = new System.Drawing.Size(101, 26);
            this.lciDeleteGroup.Name = "lciDeleteGroup";
            this.lciDeleteGroup.Size = new System.Drawing.Size(978, 26);
            this.lciDeleteGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciDeleteGroup.TextSize = new System.Drawing.Size(0, 0);
            this.lciDeleteGroup.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tlGroup;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 604);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 100);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(24, 100);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1180, 100);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucCatalogkGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "ucCatalogkGroup";
            this.Size = new System.Drawing.Size(1200, 750);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEditGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.SimpleButton sbAddGroup;
        private DevExpress.XtraEditors.SimpleButton sbEditGroup;
        private DevExpress.XtraEditors.SimpleButton sbDeleteGroup;
        private DevExpress.XtraLayout.LayoutControlItem lciAddGroup;
        private DevExpress.XtraLayout.LayoutControlItem lciEditGroup;
        private DevExpress.XtraLayout.LayoutControlItem lciDeleteGroup;
        private DevExpress.XtraTreeList.TreeList tlGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlclmName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlclmDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}

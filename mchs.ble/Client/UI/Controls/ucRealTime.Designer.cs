namespace Client.UI
{
    partial class ucRealTime
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression1 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression2 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression3 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression4 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule5 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression5 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule6 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression formatConditionRuleExpression6 = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRealTime));
            this.clmPaining = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmUnitPaint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmUnitType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.gcUnit = new DevExpress.XtraGrid.GridControl();
            this.gvUnit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmUnitId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tmRefresh = new System.Windows.Forms.Timer();
            this.imageCollection = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // clmPaining
            // 
            this.clmPaining.Caption = "clmPaining";
            this.clmPaining.FieldName = "ColorForAppearance";
            this.clmPaining.Name = "clmPaining";
            // 
            // clmUnitPaint
            // 
            this.clmUnitPaint.Caption = "Индикатор раскраски";
            this.clmUnitPaint.Name = "clmUnitPaint";
            this.clmUnitPaint.OptionsColumn.AllowEdit = false;
            this.clmUnitPaint.OptionsColumn.ReadOnly = true;
            this.clmUnitPaint.Visible = true;
            this.clmUnitPaint.VisibleIndex = 2;
            this.clmUnitPaint.Width = 41;
            // 
            // clmType
            // 
            this.clmType.Caption = "clmType";
            this.clmType.FieldName = "Type";
            this.clmType.Name = "clmType";
            this.clmType.OptionsColumn.AllowEdit = false;
            this.clmType.OptionsColumn.ReadOnly = true;
            this.clmType.Width = 35;
            // 
            // clmUnitType
            // 
            this.clmUnitType.Caption = "Тип объекта";
            this.clmUnitType.ColumnEdit = this.repositoryItemImageComboBox;
            this.clmUnitType.FieldName = "Type";
            this.clmUnitType.Name = "clmUnitType";
            this.clmUnitType.OptionsColumn.AllowEdit = false;
            this.clmUnitType.OptionsColumn.ReadOnly = true;
            this.clmUnitType.Visible = true;
            this.clmUnitType.VisibleIndex = 0;
            this.clmUnitType.Width = 30;
            // 
            // repositoryItemImageComboBox
            // 
            this.repositoryItemImageComboBox.AutoHeight = false;
            this.repositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 1)});
            this.repositoryItemImageComboBox.LargeImages = this.imageCollection;
            this.repositoryItemImageComboBox.Name = "repositoryItemImageComboBox";
            // 
            // repositoryItemMemoEdit
            // 
            this.repositoryItemMemoEdit.Name = "repositoryItemMemoEdit";
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.gcUnit);
            this.layoutControl.Controls.Add(this.panelControl);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(720, 219, 584, 562);
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
            // gcUnit
            // 
            this.gcUnit.Location = new System.Drawing.Point(12, 112);
            this.gcUnit.MainView = this.gvUnit;
            this.gcUnit.Name = "gcUnit";
            this.gcUnit.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit,
            this.repositoryItemImageComboBox});
            this.gcUnit.Size = new System.Drawing.Size(1176, 626);
            this.gcUnit.TabIndex = 4;
            this.gcUnit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUnit});
            // 
            // gvUnit
            // 
            this.gvUnit.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvUnit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmUnitType,
            this.clmUnitName,
            this.clmUnitPaint,
            this.clmUnitId,
            this.clmType,
            this.clmPaining});
            this.gvUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridFormatRule1.Column = this.clmPaining;
            gridFormatRule1.ColumnApplyTo = this.clmUnitPaint;
            gridFormatRule1.Name = "IsGreen";
            formatConditionRuleExpression1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleExpression1.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression1.Expression = "[ColorForAppearance] == 1";
            gridFormatRule1.Rule = formatConditionRuleExpression1;
            gridFormatRule2.Column = this.clmPaining;
            gridFormatRule2.ColumnApplyTo = this.clmUnitPaint;
            gridFormatRule2.Name = "IsGrey";
            formatConditionRuleExpression2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            formatConditionRuleExpression2.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression2.Expression = "[ColorForAppearance] == 2";
            gridFormatRule2.Rule = formatConditionRuleExpression2;
            gridFormatRule3.Column = this.clmPaining;
            gridFormatRule3.ColumnApplyTo = this.clmUnitPaint;
            gridFormatRule3.Name = "IsRed";
            formatConditionRuleExpression3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            formatConditionRuleExpression3.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression3.Expression = "[ColorForAppearance] == 3";
            gridFormatRule3.Rule = formatConditionRuleExpression3;
            gridFormatRule4.Column = this.clmPaining;
            gridFormatRule4.ColumnApplyTo = this.clmUnitPaint;
            gridFormatRule4.Name = "IsYellow";
            formatConditionRuleExpression4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleExpression4.Appearance.Options.UseBackColor = true;
            formatConditionRuleExpression4.Expression = "[ColorForAppearance] == 4";
            gridFormatRule4.Rule = formatConditionRuleExpression4;
            gridFormatRule5.Column = this.clmType;
            gridFormatRule5.ColumnApplyTo = this.clmUnitType;
            gridFormatRule5.Name = "IsUnit";
            formatConditionRuleExpression5.Appearance.Image = global::Client.Properties.Resources.FireTruck_Red1;
            formatConditionRuleExpression5.Appearance.Options.UseImage = true;
            formatConditionRuleExpression5.Expression = "[Type] == 2";
            gridFormatRule5.Rule = formatConditionRuleExpression5;
            gridFormatRule6.Column = this.clmType;
            gridFormatRule6.ColumnApplyTo = this.clmUnitType;
            gridFormatRule6.Name = "IsStore";
            formatConditionRuleExpression6.Appearance.Image = global::Client.Properties.Resources.FireTruck_Red1;
            formatConditionRuleExpression6.Appearance.Options.UseImage = true;
            formatConditionRuleExpression6.Expression = "[Type] == 1";
            gridFormatRule6.Rule = formatConditionRuleExpression6;
            this.gvUnit.FormatRules.Add(gridFormatRule1);
            this.gvUnit.FormatRules.Add(gridFormatRule2);
            this.gvUnit.FormatRules.Add(gridFormatRule3);
            this.gvUnit.FormatRules.Add(gridFormatRule4);
            this.gvUnit.FormatRules.Add(gridFormatRule5);
            this.gvUnit.FormatRules.Add(gridFormatRule6);
            this.gvUnit.GridControl = this.gcUnit;
            this.gvUnit.Name = "gvUnit";
            this.gvUnit.OptionsView.RowAutoHeight = true;
            this.gvUnit.OptionsView.ShowColumnHeaders = false;
            this.gvUnit.OptionsView.ShowGroupPanel = false;
            this.gvUnit.OptionsView.ShowIndicator = false;
            this.gvUnit.RowSeparatorHeight = 3;
            this.gvUnit.DoubleClick += new System.EventHandler(this.gvUnit_DoubleClick);
            // 
            // clmUnitName
            // 
            this.clmUnitName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 16F);
            this.clmUnitName.AppearanceCell.Options.UseFont = true;
            this.clmUnitName.AppearanceCell.Options.UseTextOptions = true;
            this.clmUnitName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.clmUnitName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.clmUnitName.Caption = "Название объекта";
            this.clmUnitName.ColumnEdit = this.repositoryItemMemoEdit;
            this.clmUnitName.FieldName = "BortNumName";
            this.clmUnitName.Name = "clmUnitName";
            this.clmUnitName.OptionsColumn.AllowEdit = false;
            this.clmUnitName.OptionsColumn.ReadOnly = true;
            this.clmUnitName.Visible = true;
            this.clmUnitName.VisibleIndex = 1;
            this.clmUnitName.Width = 553;
            // 
            // clmUnitId
            // 
            this.clmUnitId.Caption = "clmUnitId";
            this.clmUnitId.FieldName = "Id";
            this.clmUnitId.Name = "clmUnitId";
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
            this.lblDescription.Size = new System.Drawing.Size(849, 23);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Данный раздел предназначен для наблюдения за объектами и их комплектацией, а такж" +
    "е для слежения за оборудованием и его передвижениями.";
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(102, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(126, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Мониторинг машин";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Client.Properties.Resources.HistoryLog;
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
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Size = new System.Drawing.Size(1200, 750);
            this.layoutControlGroup.TextVisible = false;
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
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcUnit;
            this.layoutControlItem1.CustomizationFormText = "Объекты";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 630);
            this.layoutControlItem1.Text = "Объекты";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 10000;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // imageCollection
            // 
            this.imageCollection.ImageSize = new System.Drawing.Size(41, 32);
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "_store.png");
            this.imageCollection.Images.SetKeyName(1, "_unit.png");
            // 
            // ucRealTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "ucRealTime";
            this.Size = new System.Drawing.Size(1200, 750);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraGrid.GridControl gcUnit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUnit;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitName;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Timer tmRefresh;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitId;
        private DevExpress.XtraEditors.ImageListBoxControl ilbNonUniqForUnit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn clmType;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitPaint;
        private DevExpress.XtraGrid.Columns.GridColumn clmPaining;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox;
        private DevExpress.Utils.ImageCollection imageCollection;
    }
}

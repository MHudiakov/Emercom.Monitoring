namespace Client.UI
{
    partial class ucCatalogkObject
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.gckObject = new DevExpress.XtraGrid.GridControl();
            this.gvkObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmkObjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmkObjectDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbAddObject = new DevExpress.XtraEditors.SimpleButton();
            this.sbEditObject = new DevExpress.XtraEditors.SimpleButton();
            this.sbDeleteObject = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciAddObject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEditObject = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDeleteObject = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gckObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvkObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEditObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.gckObject);
            this.layoutControl.Controls.Add(this.sbAddObject);
            this.layoutControl.Controls.Add(this.sbEditObject);
            this.layoutControl.Controls.Add(this.sbDeleteObject);
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
            // gckObject
            // 
            this.gckObject.Cursor = System.Windows.Forms.Cursors.Default;
            this.gckObject.Location = new System.Drawing.Point(12, 112);
            this.gckObject.MainView = this.gvkObject;
            this.gckObject.Name = "gckObject";
            this.gckObject.Size = new System.Drawing.Size(1176, 600);
            this.gckObject.TabIndex = 6;
            this.gckObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvkObject});
            // 
            // gvkObject
            // 
            this.gvkObject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmkObjectType,
            this.clmkObjectDescription});
            this.gvkObject.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvkObject.GridControl = this.gckObject;
            this.gvkObject.Name = "gvkObject";
            this.gvkObject.OptionsFind.AlwaysVisible = true;
            this.gvkObject.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvkObject.OptionsView.ShowAutoFilterRow = true;
            this.gvkObject.OptionsView.ShowGroupPanel = false;
            // 
            // clmkObjectType
            // 
            this.clmkObjectType.Caption = "Название";
            this.clmkObjectType.FieldName = "Type";
            this.clmkObjectType.Name = "clmkObjectType";
            this.clmkObjectType.OptionsColumn.AllowEdit = false;
            this.clmkObjectType.OptionsColumn.ReadOnly = true;
            this.clmkObjectType.Visible = true;
            this.clmkObjectType.VisibleIndex = 0;
            // 
            // clmkObjectDescription
            // 
            this.clmkObjectDescription.Caption = "Описание";
            this.clmkObjectDescription.FieldName = "Description";
            this.clmkObjectDescription.Name = "clmkObjectDescription";
            this.clmkObjectDescription.OptionsColumn.AllowEdit = false;
            this.clmkObjectDescription.OptionsColumn.ReadOnly = true;
            this.clmkObjectDescription.Visible = true;
            this.clmkObjectDescription.VisibleIndex = 1;
            // 
            // sbAddObject
            // 
            this.sbAddObject.Image = global::Client.Properties.Resources.xAdd;
            this.sbAddObject.Location = new System.Drawing.Point(12, 716);
            this.sbAddObject.Name = "sbAddObject";
            this.sbAddObject.Size = new System.Drawing.Size(97, 22);
            this.sbAddObject.StyleController = this.layoutControl;
            this.sbAddObject.TabIndex = 13;
            this.sbAddObject.Text = "Добавить...";
            // 
            // sbEditObject
            // 
            this.sbEditObject.Image = global::Client.Properties.Resources.xEdit;
            this.sbEditObject.Location = new System.Drawing.Point(113, 716);
            this.sbEditObject.Name = "sbEditObject";
            this.sbEditObject.Size = new System.Drawing.Size(97, 22);
            this.sbEditObject.StyleController = this.layoutControl;
            this.sbEditObject.TabIndex = 14;
            this.sbEditObject.Text = "Изменить...";
            // 
            // sbDeleteObject
            // 
            this.sbDeleteObject.Image = global::Client.Properties.Resources.xDelete;
            this.sbDeleteObject.Location = new System.Drawing.Point(214, 716);
            this.sbDeleteObject.Name = "sbDeleteObject";
            this.sbDeleteObject.Size = new System.Drawing.Size(97, 22);
            this.sbDeleteObject.StyleController = this.layoutControl;
            this.sbDeleteObject.TabIndex = 15;
            this.sbDeleteObject.Text = "Удалить";
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
            this.lblDescription.Text = "Данный раздел содержит информацию о типах объектов и предназначен для редактирова" +
    "ния этой информации.";
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(102, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(190, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Справочник типов объектов";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Client.Properties.Resources.Products;
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
            this.layoutControlItem3,
            this.lciAddObject,
            this.lciEditObject,
            this.lciDeleteObject,
            this.layoutControlItem1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(1200, 750);
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gckObject;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1180, 604);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lciAddObject
            // 
            this.lciAddObject.Control = this.sbAddObject;
            this.lciAddObject.CustomizationFormText = "lciAddObject";
            this.lciAddObject.Location = new System.Drawing.Point(0, 704);
            this.lciAddObject.MaxSize = new System.Drawing.Size(101, 26);
            this.lciAddObject.MinSize = new System.Drawing.Size(101, 26);
            this.lciAddObject.Name = "lciAddObject";
            this.lciAddObject.Size = new System.Drawing.Size(101, 26);
            this.lciAddObject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciAddObject.TextSize = new System.Drawing.Size(0, 0);
            this.lciAddObject.TextVisible = false;
            // 
            // lciEditObject
            // 
            this.lciEditObject.Control = this.sbEditObject;
            this.lciEditObject.CustomizationFormText = "lciEditObject";
            this.lciEditObject.Location = new System.Drawing.Point(101, 704);
            this.lciEditObject.MaxSize = new System.Drawing.Size(101, 26);
            this.lciEditObject.MinSize = new System.Drawing.Size(101, 26);
            this.lciEditObject.Name = "lciEditObject";
            this.lciEditObject.Size = new System.Drawing.Size(101, 26);
            this.lciEditObject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciEditObject.TextSize = new System.Drawing.Size(0, 0);
            this.lciEditObject.TextVisible = false;
            // 
            // lciDeleteObject
            // 
            this.lciDeleteObject.Control = this.sbDeleteObject;
            this.lciDeleteObject.CustomizationFormText = "lciDeleteObject";
            this.lciDeleteObject.Location = new System.Drawing.Point(202, 704);
            this.lciDeleteObject.MaxSize = new System.Drawing.Size(101, 26);
            this.lciDeleteObject.MinSize = new System.Drawing.Size(101, 26);
            this.lciDeleteObject.Name = "lciDeleteObject";
            this.lciDeleteObject.Size = new System.Drawing.Size(978, 26);
            this.lciDeleteObject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciDeleteObject.TextSize = new System.Drawing.Size(0, 0);
            this.lciDeleteObject.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 100);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(24, 100);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 100);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem2";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 15000;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // ucCatalogkObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "ucCatalogkObject";
            this.Size = new System.Drawing.Size(1200, 750);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gckObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvkObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAddObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEditObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraGrid.GridControl gckObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvkObject;
        private DevExpress.XtraGrid.Columns.GridColumn clmkObjectType;
        private DevExpress.XtraGrid.Columns.GridColumn clmkObjectDescription;
        private DevExpress.XtraEditors.SimpleButton sbAddObject;
        private DevExpress.XtraEditors.SimpleButton sbEditObject;
        private DevExpress.XtraEditors.SimpleButton sbDeleteObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem lciAddObject;
        private DevExpress.XtraLayout.LayoutControlItem lciEditObject;
        private DevExpress.XtraLayout.LayoutControlItem lciDeleteObject;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Timer tmRefresh;
    }
}

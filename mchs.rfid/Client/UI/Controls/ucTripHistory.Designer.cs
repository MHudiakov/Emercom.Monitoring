namespace Client.UI.Controls
{
    partial class ucTripHistory
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
            this.sbView = new DevExpress.XtraEditors.SimpleButton();
            this.gcTrip = new DevExpress.XtraGrid.GridControl();
            this.gvTrip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmTripDtBegin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTripDtEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueUnit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gvlueUnit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.teFrom = new DevExpress.XtraEditors.TimeEdit();
            this.teTo = new DevExpress.XtraEditors.TimeEdit();
            this.sbFind = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvlueUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.sbView);
            this.layoutControl.Controls.Add(this.gcTrip);
            this.layoutControl.Controls.Add(this.lueUnit);
            this.layoutControl.Controls.Add(this.panelControl);
            this.layoutControl.Controls.Add(this.deFrom);
            this.layoutControl.Controls.Add(this.deTo);
            this.layoutControl.Controls.Add(this.teFrom);
            this.layoutControl.Controls.Add(this.teTo);
            this.layoutControl.Controls.Add(this.sbFind);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(720, 357, 250, 350);
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
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(1200, 750);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // sbView
            // 
            this.sbView.Image = global::Client.Properties.Resources.filefind;
            this.sbView.Location = new System.Drawing.Point(1050, 716);
            this.sbView.Name = "sbView";
            this.sbView.Size = new System.Drawing.Size(138, 22);
            this.sbView.StyleController = this.layoutControl;
            this.sbView.TabIndex = 9;
            this.sbView.Text = "Показать на карте...";
            this.sbView.Click += new System.EventHandler(this.sbView_Click);
            // 
            // gcTrip
            // 
            this.gcTrip.Location = new System.Drawing.Point(12, 227);
            this.gcTrip.MainView = this.gvTrip;
            this.gcTrip.Name = "gcTrip";
            this.gcTrip.Size = new System.Drawing.Size(1176, 485);
            this.gcTrip.TabIndex = 8;
            this.gcTrip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrip});
            this.gcTrip.DoubleClick += new System.EventHandler(this.gcTrip_Click);
            // 
            // gvTrip
            // 
            this.gvTrip.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmTripDtBegin,
            this.clmTripDtEnd});
            this.gvTrip.GridControl = this.gcTrip;
            this.gvTrip.Name = "gvTrip";
            this.gvTrip.OptionsView.ShowAutoFilterRow = true;
            this.gvTrip.OptionsView.ShowGroupPanel = false;
            // 
            // clmTripDtBegin
            // 
            this.clmTripDtBegin.Caption = "Дата/время выезда";
            this.clmTripDtBegin.DisplayFormat.FormatString = "G";
            this.clmTripDtBegin.FieldName = "StartTime";
            this.clmTripDtBegin.Name = "clmTripDtBegin";
            this.clmTripDtBegin.OptionsColumn.AllowEdit = false;
            this.clmTripDtBegin.OptionsColumn.ReadOnly = true;
            this.clmTripDtBegin.Visible = true;
            this.clmTripDtBegin.VisibleIndex = 0;
            // 
            // clmTripDtEnd
            // 
            this.clmTripDtEnd.Caption = "Дата/время прибытия";
            this.clmTripDtEnd.DisplayFormat.FormatString = "G";
            this.clmTripDtEnd.FieldName = "EndTime";
            this.clmTripDtEnd.Name = "clmTripDtEnd";
            this.clmTripDtEnd.OptionsColumn.AllowEdit = false;
            this.clmTripDtEnd.OptionsColumn.ReadOnly = true;
            this.clmTripDtEnd.Visible = true;
            this.clmTripDtEnd.VisibleIndex = 1;
            // 
            // lueUnit
            // 
            this.lueUnit.Location = new System.Drawing.Point(69, 143);
            this.lueUnit.Name = "lueUnit";
            this.lueUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueUnit.Properties.DisplayMember = "Name";
            this.lueUnit.Properties.ValueMember = "Id";
            this.lueUnit.Properties.View = this.gvlueUnit;
            this.lueUnit.Size = new System.Drawing.Size(1107, 20);
            this.lueUnit.StyleController = this.layoutControl;
            this.lueUnit.TabIndex = 6;
            // 
            // gvlueUnit
            // 
            this.gvlueUnit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmUnitName});
            this.gvlueUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvlueUnit.Name = "gvlueUnit";
            this.gvlueUnit.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvlueUnit.OptionsView.ShowGroupPanel = false;
            // 
            // clmUnitName
            // 
            this.clmUnitName.Caption = "Наименование";
            this.clmUnitName.FieldName = "Name";
            this.clmUnitName.Name = "clmUnitName";
            this.clmUnitName.Visible = true;
            this.clmUnitName.VisibleIndex = 0;
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
            this.lblDescription.Size = new System.Drawing.Size(849, 42);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Данный раздел предназначен для просмотра истории вызовов машин и движения машин в" +
    "о время вызовов, а также для слежения за комплектацией оборудования во время выз" +
    "овов.";
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(102, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(115, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "История вызовов";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Client.Properties.Resources.MoveHistory1;
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
            // deFrom
            // 
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(36, 167);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Size = new System.Drawing.Size(131, 20);
            this.deFrom.StyleController = this.layoutControl;
            this.deFrom.TabIndex = 3;
            // 
            // deTo
            // 
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(42, 191);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Size = new System.Drawing.Size(125, 20);
            this.deTo.StyleController = this.layoutControl;
            this.deTo.TabIndex = 5;
            // 
            // teFrom
            // 
            this.teFrom.EditValue = new System.DateTime(2016, 6, 16, 0, 0, 0, 0);
            this.teFrom.Location = new System.Drawing.Point(206, 167);
            this.teFrom.Name = "teFrom";
            this.teFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teFrom.Size = new System.Drawing.Size(138, 20);
            this.teFrom.StyleController = this.layoutControl;
            this.teFrom.TabIndex = 4;
            // 
            // teTo
            // 
            this.teTo.EditValue = new System.DateTime(2016, 6, 16, 0, 0, 0, 0);
            this.teTo.Location = new System.Drawing.Point(206, 191);
            this.teTo.Name = "teTo";
            this.teTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teTo.Size = new System.Drawing.Size(138, 20);
            this.teTo.StyleController = this.layoutControl;
            this.teTo.TabIndex = 6;
            // 
            // sbFind
            // 
            this.sbFind.Image = global::Client.Properties.Resources.xSave;
            this.sbFind.Location = new System.Drawing.Point(1079, 191);
            this.sbFind.Name = "sbFind";
            this.sbFind.Size = new System.Drawing.Size(97, 20);
            this.sbFind.StyleController = this.layoutControl;
            this.sbFind.TabIndex = 7;
            this.sbFind.Text = "Применить";
            this.sbFind.Click += new System.EventHandler(this.sbFind_Click);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem7,
            this.layoutControlGroup1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
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
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcTrip;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 215);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1180, 489);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 704);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1038, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sbView;
            this.layoutControlItem7.Location = new System.Drawing.Point(1038, 704);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(142, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(142, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(142, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.emptySpaceItem4,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 100);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1180, 115);
            this.layoutControlGroup1.Text = "Фильтры";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lueUnit;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1156, 24);
            this.layoutControlItem1.Text = "Машина";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(40, 13);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deFrom;
            this.layoutControlItem4.CustomizationFormText = "С";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "С";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(7, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.teFrom;
            this.layoutControlItem8.CustomizationFormText = "время";
            this.layoutControlItem8.Location = new System.Drawing.Point(147, 24);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(177, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "время";
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(30, 13);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.deTo;
            this.layoutControlItem5.CustomizationFormText = "По";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(147, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "По";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(13, 13);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.teTo;
            this.layoutControlItem9.CustomizationFormText = "время";
            this.layoutControlItem9.Location = new System.Drawing.Point(147, 48);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(177, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(177, 24);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "время";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(30, 13);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(324, 48);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(731, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem2.Location = new System.Drawing.Point(324, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(832, 24);
            this.emptySpaceItem2.Text = "emptySpaceItem1";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbFind;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(1055, 48);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // ucTripHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "ucTripHistory";
            this.Size = new System.Drawing.Size(1200, 750);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvlueUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.SearchLookUpEdit lueUnit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvlueUnit;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton sbView;
        private DevExpress.XtraGrid.GridControl gcTrip;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrip;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.TimeEdit teFrom;
        private DevExpress.XtraEditors.TimeEdit teTo;
        private DevExpress.XtraEditors.SimpleButton sbFind;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn clmTripDtBegin;
        private DevExpress.XtraGrid.Columns.GridColumn clmTripDtEnd;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    }
}

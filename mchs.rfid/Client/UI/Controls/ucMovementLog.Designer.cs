namespace Client.UI
{
    partial class ucMovementLog
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
            this.lueEquipment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmEquipmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueUnit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMovement = new DevExpress.XtraGrid.GridControl();
            this.gvMovement = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmTypeArrived = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDateOfMovement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmEquipment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmUnit = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueEquipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMovement)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lueEquipment);
            this.layoutControl.Controls.Add(this.lueUnit);
            this.layoutControl.Controls.Add(this.gcMovement);
            this.layoutControl.Controls.Add(this.panelControl);
            this.layoutControl.Controls.Add(this.deFrom);
            this.layoutControl.Controls.Add(this.deTo);
            this.layoutControl.Controls.Add(this.teFrom);
            this.layoutControl.Controls.Add(this.teTo);
            this.layoutControl.Controls.Add(this.sbFind);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(720, 521, 250, 350);
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
            // lueEquipment
            // 
            this.lueEquipment.Location = new System.Drawing.Point(92, 136);
            this.lueEquipment.Name = "lueEquipment";
            this.lueEquipment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lueEquipment.Properties.DisplayMember = "EquipmentName";
            this.lueEquipment.Properties.NullText = "Всё оборудование";
            this.lueEquipment.Properties.ValueMember = "Id";
            this.lueEquipment.Properties.View = this.searchLookUpEdit2View;
            this.lueEquipment.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueEquipmentButtonClick);
            this.lueEquipment.Size = new System.Drawing.Size(1096, 20);
            this.lueEquipment.StyleController = this.layoutControl;
            this.lueEquipment.TabIndex = 9;
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmEquipmentName});
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // clmEquipmentName
            // 
            this.clmEquipmentName.Caption = "Наименование";
            this.clmEquipmentName.FieldName = "EquipmentName";
            this.clmEquipmentName.Name = "clmEquipmentName";
            this.clmEquipmentName.Visible = true;
            this.clmEquipmentName.VisibleIndex = 0;
            // 
            // lueUnit
            // 
            this.lueUnit.Location = new System.Drawing.Point(56, 112);
            this.lueUnit.Name = "lueUnit";
            this.lueUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.lueUnit.Properties.DisplayMember = "Name";
            this.lueUnit.Properties.NullText = "Все объекты";
            this.lueUnit.Properties.ValueMember = "Id";
            this.lueUnit.Properties.View = this.searchLookUpEdit1View;
            this.lueUnit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lueUnitButtonClick);
            this.lueUnit.Size = new System.Drawing.Size(1132, 20);
            this.lueUnit.StyleController = this.layoutControl;
            this.lueUnit.TabIndex = 8;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmUnitName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // clmUnitName
            // 
            this.clmUnitName.Caption = "Наименование";
            this.clmUnitName.FieldName = "Name";
            this.clmUnitName.Name = "clmUnitName";
            this.clmUnitName.Visible = true;
            this.clmUnitName.VisibleIndex = 0;
            // 
            // gcMovement
            // 
            this.gcMovement.Location = new System.Drawing.Point(12, 208);
            this.gcMovement.MainView = this.gvMovement;
            this.gcMovement.Name = "gcMovement";
            this.gcMovement.Size = new System.Drawing.Size(1176, 530);
            this.gcMovement.TabIndex = 6;
            this.gcMovement.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMovement});
            // 
            // gvMovement
            // 
            this.gvMovement.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmTypeArrived,
            this.clmDateOfMovement,
            this.clmEquipment,
            this.clmUnit});
            this.gvMovement.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvMovement.GridControl = this.gcMovement;
            this.gvMovement.Name = "gvMovement";
            this.gvMovement.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gvMovement.OptionsView.ShowAutoFilterRow = true;
            this.gvMovement.OptionsView.ShowGroupPanel = false;
            // 
            // clmTypeArrived
            // 
            this.clmTypeArrived.Caption = "Тип движения";
            this.clmTypeArrived.FieldName = "Arrived";
            this.clmTypeArrived.Name = "clmTypeArrived";
            this.clmTypeArrived.Visible = true;
            this.clmTypeArrived.VisibleIndex = 0;
            // 
            // clmDateOfMovement
            // 
            this.clmDateOfMovement.Caption = "Дата движения";
            this.clmDateOfMovement.DisplayFormat.FormatString = "G";
            this.clmDateOfMovement.FieldName = "DateOfMovement";
            this.clmDateOfMovement.Name = "clmDateOfMovement";
            this.clmDateOfMovement.Visible = true;
            this.clmDateOfMovement.VisibleIndex = 1;
            // 
            // clmEquipment
            // 
            this.clmEquipment.Caption = "Оборудование";
            this.clmEquipment.FieldName = "EquipmentName";
            this.clmEquipment.Name = "clmEquipment";
            this.clmEquipment.Visible = true;
            this.clmEquipment.VisibleIndex = 2;
            // 
            // clmUnit
            // 
            this.clmUnit.Caption = "Объект";
            this.clmUnit.FieldName = "UnitName";
            this.clmUnit.Name = "clmUnit";
            this.clmUnit.Visible = true;
            this.clmUnit.VisibleIndex = 3;
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
            this.lblDescription.Size = new System.Drawing.Size(849, 69);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Данный раздел предназначен для наблюдения за движением оборудования по машинам.";
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(102, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(226, 16);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Журнал движения оборудования";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::Client.Properties.Resources.Shedule96;
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
            this.deFrom.Location = new System.Drawing.Point(24, 160);
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
            this.deTo.Location = new System.Drawing.Point(30, 184);
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
            this.teFrom.Location = new System.Drawing.Point(194, 160);
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
            this.teTo.Location = new System.Drawing.Point(194, 184);
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
            this.sbFind.Location = new System.Drawing.Point(1091, 184);
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
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem9,
            this.emptySpaceItem4,
            this.layoutControlItem6,
            this.layoutControlItem3,
            this.layoutControlItem7});
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
            this.layoutControlItem1.Control = this.gcMovement;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 196);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 534);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deFrom;
            this.layoutControlItem4.CustomizationFormText = "С";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 148);
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
            this.layoutControlItem8.Location = new System.Drawing.Point(147, 148);
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
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem2.Location = new System.Drawing.Point(324, 148);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(856, 24);
            this.emptySpaceItem2.Text = "emptySpaceItem1";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.deTo;
            this.layoutControlItem5.CustomizationFormText = "По";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 172);
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
            this.layoutControlItem9.Location = new System.Drawing.Point(147, 172);
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
            this.emptySpaceItem4.Location = new System.Drawing.Point(324, 172);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(755, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sbFind;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(1079, 172);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(101, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lueUnit;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 100);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1180, 24);
            this.layoutControlItem3.Text = "Объект";
            this.layoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(39, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lueEquipment;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 124);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(1180, 24);
            this.layoutControlItem7.Text = "Оборудование";
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(75, 13);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // ucMovementLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "ucMovementLog";
            this.Size = new System.Drawing.Size(1200, 750);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueEquipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMovement)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcMovement;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMovement;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.TimeEdit teFrom;
        private DevExpress.XtraEditors.TimeEdit teTo;
        private DevExpress.XtraEditors.SimpleButton sbFind;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn clmTypeArrived;
        private DevExpress.XtraGrid.Columns.GridColumn clmDateOfMovement;
        private DevExpress.XtraGrid.Columns.GridColumn clmEquipment;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnit;
        private DevExpress.XtraEditors.SearchLookUpEdit lueEquipment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn clmEquipmentName;
        private DevExpress.XtraEditors.SearchLookUpEdit lueUnit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn clmUnitName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}

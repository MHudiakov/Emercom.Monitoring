namespace Client.UI.EditForms
{
    partial class fmEditMovement
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmEditMovement));
            this.luekEquipment = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.deDateOfMovement = new DevExpress.XtraEditors.DateEdit();
            this.rgArrive = new DevExpress.XtraEditors.RadioGroup();
            this.lueEquipmentRFId = new DevExpress.XtraEditors.LookUpEdit();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.luekEquipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateOfMovement.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateOfMovement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgArrive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueEquipmentRFId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // luekEquipment
            // 
            this.luekEquipment.EditValue = "";
            this.luekEquipment.Location = new System.Drawing.Point(187, 12);
            this.luekEquipment.Name = "luekEquipment";
            this.luekEquipment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luekEquipment.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.luekEquipment.Properties.NullText = "";
            this.luekEquipment.Properties.ShowFooter = false;
            this.luekEquipment.Properties.ShowHeader = false;
            this.luekEquipment.Properties.ShowLines = false;
            this.luekEquipment.Size = new System.Drawing.Size(285, 20);
            this.luekEquipment.StyleController = this.layoutControl1;
            this.luekEquipment.TabIndex = 7;
            this.luekEquipment.EditValueChanged += new System.EventHandler(this.luekEquipmentEditValueChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.deDateOfMovement);
            this.layoutControl1.Controls.Add(this.rgArrive);
            this.layoutControl1.Controls.Add(this.lueEquipmentRFId);
            this.layoutControl1.Controls.Add(this.luekEquipment);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(619, 165, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(484, 148);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // deDateOfMovement
            // 
            this.deDateOfMovement.EditValue = null;
            this.deDateOfMovement.Location = new System.Drawing.Point(187, 89);
            this.deDateOfMovement.Name = "deDateOfMovement";
            this.deDateOfMovement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateOfMovement.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.deDateOfMovement.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateOfMovement.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.deDateOfMovement.Properties.DisplayFormat.FormatString = "g";
            this.deDateOfMovement.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDateOfMovement.Properties.EditFormat.FormatString = "g";
            this.deDateOfMovement.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDateOfMovement.Properties.Mask.EditMask = "g";
            this.deDateOfMovement.Properties.MaxValue = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.deDateOfMovement.Properties.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.deDateOfMovement.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.deDateOfMovement.Size = new System.Drawing.Size(285, 20);
            this.deDateOfMovement.StyleController = this.layoutControl1;
            this.deDateOfMovement.TabIndex = 48;
            // 
            // rgArrive
            // 
            this.rgArrive.EditValue = true;
            this.rgArrive.Location = new System.Drawing.Point(187, 60);
            this.rgArrive.Name = "rgArrive";
            this.rgArrive.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rgArrive.Properties.Appearance.Options.UseBackColor = true;
            this.rgArrive.Properties.Columns = 2;
            this.rgArrive.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Прибило"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Убыло")});
            this.rgArrive.Size = new System.Drawing.Size(285, 25);
            this.rgArrive.StyleController = this.layoutControl1;
            this.rgArrive.TabIndex = 47;
            // 
            // lueEquipmentRFId
            // 
            this.lueEquipmentRFId.EditValue = "";
            this.lueEquipmentRFId.Location = new System.Drawing.Point(187, 36);
            this.lueEquipmentRFId.Name = "lueEquipmentRFId";
            this.lueEquipmentRFId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueEquipmentRFId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RFId", "Номер")});
            this.lueEquipmentRFId.Properties.NullText = "";
            this.lueEquipmentRFId.Properties.ShowFooter = false;
            this.lueEquipmentRFId.Properties.ShowHeader = false;
            this.lueEquipmentRFId.Properties.ShowLines = false;
            this.lueEquipmentRFId.Size = new System.Drawing.Size(285, 20);
            this.lueEquipmentRFId.StyleController = this.layoutControl1;
            this.lueEquipmentRFId.TabIndex = 8;
            // 
            // sbSave
            // 
            this.sbSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbSave.Image = global::Client.Properties.Resources.xSave;
            this.sbSave.Location = new System.Drawing.Point(274, 113);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(97, 23);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "Сохранить";
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.sbCancel.Image = global::Client.Properties.Resources.xCancel;
            this.sbCancel.Location = new System.Drawing.Point(375, 113);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(97, 23);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 5;
            this.sbCancel.Text = "Отменить";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(484, 148);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 101);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(262, 27);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCancel;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(363, 101);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbSave;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(262, 101);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.luekEquipment;
            this.layoutControlItem5.CustomizationFormText = "Группа оборудования";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(138, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Оборудование";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(172, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lueEquipmentRFId;
            this.layoutControlItem8.CustomizationFormText = "Оборудование";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(138, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "Уникальный номер оборудования";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(172, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.rgArrive;
            this.layoutControlItem9.CustomizationFormText = "Вид движения";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(0, 29);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(150, 29);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(464, 29);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Вид движения";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(172, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deDateOfMovement;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 77);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(138, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Дата";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(172, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.luekEquipment;
            this.layoutControlItem6.CustomizationFormText = "Группа оборудования";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem5";
            this.layoutControlItem6.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem6.Text = "Склад";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lueEquipmentRFId;
            this.layoutControlItem7.CustomizationFormText = "Группа оборудования";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem5";
            this.layoutControlItem7.Size = new System.Drawing.Size(465, 24);
            this.layoutControlItem7.Text = "Склад";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(48, 13);
            // 
            // fmEditMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 148);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 186);
            this.MinimumSize = new System.Drawing.Size(500, 186);
            this.Name = "fmEditMovement";
            this.Text = "Редактирование движения оборудования на складе";
            ((System.ComponentModel.ISupportInitialize)(this.luekEquipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDateOfMovement.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateOfMovement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgArrive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueEquipmentRFId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit luekEquipment;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.RadioGroup rgArrive;
        private DevExpress.XtraEditors.LookUpEdit lueEquipmentRFId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.DateEdit deDateOfMovement;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
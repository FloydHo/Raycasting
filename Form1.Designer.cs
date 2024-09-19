namespace Raycasting
{
    partial class form_Raycasting
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            canvas_thirdperson = new Panel();
            cnv_TopDown = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)cnv_TopDown).BeginInit();
            SuspendLayout();
            // 
            // canvas_thirdperson
            // 
            canvas_thirdperson.BackColor = SystemColors.ActiveCaptionText;
            canvas_thirdperson.Location = new Point(547, 12);
            canvas_thirdperson.Name = "canvas_thirdperson";
            canvas_thirdperson.Size = new Size(525, 675);
            canvas_thirdperson.TabIndex = 1;
            // 
            // cnv_TopDown
            // 
            cnv_TopDown.BackColor = SystemColors.ControlText;
            cnv_TopDown.Location = new Point(12, 12);
            cnv_TopDown.Name = "cnv_TopDown";
            cnv_TopDown.Size = new Size(525, 675);
            cnv_TopDown.TabIndex = 0;
            cnv_TopDown.TabStop = false;
            cnv_TopDown.Click += cnv_TopDown_Click;
            cnv_TopDown.Paint += cnv_TopDown_Paint;
            // 
            // form_Raycasting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 699);
            Controls.Add(cnv_TopDown);
            Controls.Add(canvas_thirdperson);
            Name = "form_Raycasting";
            Text = "Raycasting";
            KeyDown += form_Raycasting_KeyDown;
            KeyUp += form_Raycasting_KeyUp;
            ((System.ComponentModel.ISupportInitialize)cnv_TopDown).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel canvas_thirdperson;
        private PictureBox cnv_TopDown;
    }
}

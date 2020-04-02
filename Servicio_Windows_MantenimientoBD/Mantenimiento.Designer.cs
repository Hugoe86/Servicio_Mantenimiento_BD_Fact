namespace Servicio_Windows_MantenimientoBD
{
    partial class Mantenimiento
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
            this.Btn_Prueba = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Prueba
            // 
            this.Btn_Prueba.Location = new System.Drawing.Point(81, 82);
            this.Btn_Prueba.Name = "Btn_Prueba";
            this.Btn_Prueba.Size = new System.Drawing.Size(166, 104);
            this.Btn_Prueba.TabIndex = 0;
            this.Btn_Prueba.Text = "Prueba";
            this.Btn_Prueba.UseVisualStyleBackColor = true;
            // 
            // Mantenimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 308);
            this.Controls.Add(this.Btn_Prueba);
            this.Name = "Mantenimiento";
            this.Text = "Mantenimiento";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Prueba;
    }
}
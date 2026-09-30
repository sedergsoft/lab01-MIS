namespace Lab1;

public partial class Form1 : Form
{
    private System.ComponentModel.IContainer components = null;
    private TextBox textBox1;
    private Button button1;
    private ListView listView1;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        this.button1 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.listView1 = new System.Windows.Forms.ListView();

        this.SuspendLayout();

        this.button1.Location = new System.Drawing.Point(64, 96);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(120, 30);
        this.button1.TabIndex = 0;
        this.button1.Text = "Guardar";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);

        this.textBox1.Location = new System.Drawing.Point(64, 56);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(300, 23);
        this.textBox1.TabIndex = 1;
        this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
        this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);

        this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.listView1.FullRowSelect = true;
        this.listView1.GridLines = true;
        this.listView1.Location = new System.Drawing.Point(64, 150);
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(660, 220);
        this.listView1.TabIndex = 2;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = System.Windows.Forms.View.Details;
        this.listView1.Columns.Add("Datos guardados", 620);

        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.listView1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.button1);
        this.Name = "Form1";
        this.Text = "Edición automática de FIO";

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}

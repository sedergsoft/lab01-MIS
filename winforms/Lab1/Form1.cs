using Fio.Core;

namespace Lab1;

public partial class Form1 : Form
{
    private readonly List<string> saved = new();
    private bool inInitials;
    private int initCount;
    private bool entryReady;
    private FioTemplate? template;

    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var val = (textBox1.Text ?? string.Empty).Trim();

        if (!FioValidator.TryParse(val, out var candidate) || candidate is null)
        {
            MessageBox.Show("Formato inválido. Ejemplo: Novikov-Priboy A.S.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox1.Focus();
            return;
        }

        if (template is not null && !template.Matches(candidate))
        {
            MessageBox.Show($"Formato inválido. La entrada no coincide con la plantilla activa: {template}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox1.Focus();
            return;
        }

        template ??= candidate;

        saved.Add(val);
        UpdateList();
        MessageBox.Show($"Se guardó: {val}\nPlantilla activa: {template}");
        textBox1.Clear();
        ResetState();
    }

    private void UpdateList()
    {
        listView1.Items.Clear();

        foreach (var item in saved)
        {
            listView1.Items.Add(new ListViewItem(item));
        }
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == '\b' || e.KeyChar == '\r' || e.KeyChar == '\n')
            return;

        if (entryReady && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
            return;
        }

        if (e.KeyChar == ' ')
        {
            if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text.Contains(' ') || inInitials)
            {
                e.Handled = true;
                return;
            }

            AppendText(" ");
            inInitials = true;
            initCount = 0;
            e.Handled = true;
            return;
        }

        if (e.KeyChar == '-')
        {
            if (string.IsNullOrEmpty(textBox1.Text) || inInitials || textBox1.Text.Contains(' ') || textBox1.Text.EndsWith("-"))
            {
                e.Handled = true;
                return;
            }

            AppendText("-");
            e.Handled = true;
            return;
        }

        if (!char.IsLetter(e.KeyChar))
        {
            e.Handled = true;
            return;
        }

        if (inInitials)
        {
            AppendText(char.ToUpper(e.KeyChar).ToString());
            AppendText(".");
            initCount++;

            if (initCount == 2)
            {
                entryReady = true;
                SelectCurrentEntry();
            }

            e.Handled = true;
            return;
        }

        if (textBox1.Text.Length == 0 || textBox1.Text.EndsWith("-"))
        {
            AppendText(char.ToUpper(e.KeyChar).ToString());
        }
        else
        {
            AppendText(char.ToLower(e.KeyChar).ToString());
        }

        e.Handled = true;
    }

    private void textBox1_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
        {
            ResetState();
            return;
        }

        if (entryReady)
        {
            SelectCurrentEntry();
        }
    }

    private void ResetState()
    {
        inInitials = false;
        initCount = 0;
        entryReady = false;
    }

    private void AppendText(string txt)
    {
        var pos = textBox1.SelectionStart;
        textBox1.Text = textBox1.Text.Insert(pos, txt);
        textBox1.SelectionStart = pos + txt.Length;
        textBox1.SelectionLength = 0;
    }

    private void SelectCurrentEntry()
    {
        if (string.IsNullOrEmpty(textBox1.Text))
            return;

        textBox1.SelectionStart = 0;
        textBox1.SelectionLength = textBox1.Text.Length;
    }

}

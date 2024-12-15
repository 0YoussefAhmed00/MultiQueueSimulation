using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCG
{
    public partial class LCG_Program : Form
    {
        public List<string> FieldNames = new List<string>
        {
            "Multiplier (a)",
            "Seed (x0)",
            "Increment (c)",
            "Modulus (m)",
            "Number of Iterations",
            "Period Length in Generated Numbers"
        };
        public LCG_Program()
        {
            InitializeComponent();
            this.Size = new Size(1100, 700);
            CreateFields();
        }

        private void CreateLabel(String name, String text, int x, int y, int fontSz)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font("Microsoft Sans Serif", fontSz);
            lbl.Name = name;
            this.Controls.Add(lbl);
        }
        private void CreateFields()
        {

            for (int i = 0; i < FieldNames.Count; i++)
            {
                CreateLabel("label " + FieldNames[i], FieldNames[i], 20, 20 + i * 50 + (i == FieldNames.Count - 1 ? 110 : 0), 14);
                TextBox txtBox = new TextBox();
                txtBox.Location = new Point(270 + (i == 5 ? 100 : 0), 20 + i * 50 + (i == FieldNames.Count - 1 ? 110 : 0));
                txtBox.Size = new Size(150, 30);
                txtBox.Font = new Font("Microsoft Sans Serif", 14);
                txtBox.Name = "textbox " + FieldNames[i];

                this.Controls.Add(txtBox);
            }
            // Create Button
            Button btn = new Button();
            btn.Text = "Generate Random Numbers";
            btn.Location = new Point(110, 300);
            btn.AutoSize = true;
            btn.Font = new Font("Microsoft Sans Serif", 16);
            btn.Click += Generate_click;
            this.Controls.Add(btn);

            (this.Controls["textbox " + FieldNames[5]] as TextBox).ReadOnly = true;

            // Create DataGridView
            DataGridView gridView = new DataGridView();
            gridView.Name = "gridView";
            gridView.Location = new Point(600, 20);
            gridView.Size = new Size(450, 600);
            gridView.ColumnCount = 3;
            gridView.ColumnHeadersHeight = 60;
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridView.Font = new Font("Microsoft Sans Serif", 14);
            gridView.RowHeadersVisible = false;
            gridView.ReadOnly = true;

            gridView.Columns[0].HeaderText = "i";
            gridView.Columns[1].HeaderText = "Xi on [0, m-1]";
            gridView.Columns[2].HeaderText = "Ui on [0, 1]";

            this.Controls.Add(gridView);
            CreateLabel("msg", "", 125, 450, 10);
        }
        private void Generate_click(object sender, EventArgs e)
        {
            long a, x, c, m, n;
            try
            {
                a = long.Parse(this.Controls["textbox " + FieldNames[0]].Text);
                x = long.Parse(this.Controls["textbox " + FieldNames[1]].Text);
                c = long.Parse(this.Controls["textbox " + FieldNames[2]].Text);
                m = long.Parse(this.Controls["textbox " + FieldNames[3]].Text);
                n = long.Parse(this.Controls["textbox " + FieldNames[4]].Text);

                if (m <= 0)
                {
                    this.Controls["msg"].Text = "'m' must be a positive integer.";
                    this.Controls["msg"].Show();
                    return;
                }
                if (a < 0 || a >= m || x < 0 || x >= m || c < 0 || c >= m)
                {
                    this.Controls["msg"].Text = "'a', 'x0', and 'c' must be in the range [0, m-1].";
                    this.Controls["msg"].Show();
                    return;
                }
                if (n <= 0)
                {
                    this.Controls["msg"].Text = "Number of iterations 'n' must be a positive integer.";
                    this.Controls["msg"].Show();
                    return;
                }
            }
            catch
            {
                this.Controls["msg"].Text = "Input must be valid integers.";
                this.Controls["msg"].Show();
                return;
            }

            DataGridView gv = this.Controls["gridView"] as DataGridView;
            gv.Rows.Clear();


            long l = LCG(a, x, c, m, n, gv);
            if (l == -1)
                this.Controls["textbox " + FieldNames[5]].Text = "Not Found";
            else
                this.Controls["textbox " + FieldNames[5]].Text = l.ToString();
            long actual_length = l;
            if (actual_length == -1) actual_length = PeriodLength_SpecialCases(a, x, c, m);

            if (actual_length == -1)
            {
                this.Controls["msg"].Text = "Can't calculate the actual period length";
            }
            else
            {
                this.Controls["msg"].Text = "The actual period length is " + actual_length.ToString();
            }
            this.Controls["msg"].Show();
        }
        private long LCG(long a, long x, long c, long m, long n, DataGridView gv)
        {
            long l = -1;
            Dictionary<long, int> vis = new Dictionary<long, int>();
            bool cycleFound = false;

            for (int i = 0; i <= n; i++)
            {
                if (!cycleFound)
                {
                    if (vis.ContainsKey(x))
                    {
                        l = i - vis[x];
                        cycleFound = true;
                    }
                    else vis[x] = i;
                }
                gv.Rows.Add(i.ToString(), x.ToString(), ((double)x / m).ToString("F5"));
                x = (a * x) % m;
                x = (x + c) % m;
            }
            return l;
        }
        static long GCD(long a, long b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }

        static long modPow(long a, long p, long mod)
        {
            long res = 1;
            while (p > 0)
            {
                if (p % 2 == 1)
                    res = (res * a) % mod;
                p /= 2;
                a = (a * a) % mod;
            }
            return res;
        }
        long PeriodLength_SpecialCases(long a, long x, long c, long m)
        {
            bool M_powerOf2 = ((m - 1) & m) == 0;
            if (M_powerOf2 && c > 0 && (a - 1) % 4 == 0 && GCD(c, m) == 1)
                return m;
            if (M_powerOf2 && c == 0 && x % 2 == 1 && ((a >= 3 && (a - 3) % 8 == 0) || (a >= 5 && (a - 5) % 8 == 0)))
                return m / 4;

            if (m < 1e14 && c == 0 && modPow(a, m - 1, m) == 1)
            {

                for (long i = 2; i * i <= m; i++)
                {
                    if (m % i == 0) return FullPeriod_Length(a, x, c, m);
                }
                return m - 1;
            }
            return FullPeriod_Length(a, x, c, m);
        }
        long FullPeriod_Length(long a, long x, long c, long m)
        {
            // returns m or -1

            if ((m % 4 == 0 & (a - 1) % 4 != 0) || GCD(m, c) != 1)
                return -1;
            long i;
            for (i = 2; i < 1e7 && i * i <= m; i++)
            {
                if (m % i == 0)
                {
                    if ((a - 1) % i != 0) return -1;
                    while (m % i == 0) m /= i;

                }
            }
            return i < 1e7 && (a - 1) % m == 0 ? m : -1;
        }
    }

}

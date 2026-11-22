using ScottPlot.WinForms;
using StudentInformationSystem.Data;

namespace StudentInformationSystem;

public class DashboardForm : Form
{
    private const string HexPrimary = "#1F6FEB";
    private const string HexPageBg = "#F6F8FA";
    private const string HexBorder = "#D0D7DE";
    private const string HexText = "#1F2328";
    private const string HexMuted = "#656D76";
    private static readonly string[] ChartHex =
        { "#1F6FEB", "#2DA44E", "#BF8700", "#CF222E", "#8250DF", "#0969DA" };

    private static Color C(string hex) => ColorTranslator.FromHtml(hex);
    private static ScottPlot.Color SP(string hex) => ScottPlot.Color.FromHex(hex);

    private readonly FormsPlot _plotYear = new();
    private readonly FormsPlot _plotProgram = new();
    private readonly Label[] _kpiValues = new Label[5];
    private readonly Label _lblStatus = new()
    {
        Dock = DockStyle.Fill,
        TextAlign = ContentAlignment.MiddleRight,
        Padding = new Padding(0, 0, 20, 0),
        Font = new Font("Segoe UI", 9F)
    };

    public DashboardForm()
    {
        Text = "Student Analytics Dashboard";
        ClientSize = new Size(1100, 700);
        MinimumSize = new Size(900, 620);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = C(HexPageBg);
        Font = new Font("Segoe UI", 9F);
        _lblStatus.ForeColor = C(HexMuted);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            BackColor = C(HexPageBg),
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 84));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
        root.Controls.Add(BuildHeader(), 0, 0);
        root.Controls.Add(BuildKpiStrip(), 0, 1);
        root.Controls.Add(BuildCharts(), 0, 2);
        root.Controls.Add(_lblStatus, 0, 3);
        Controls.Add(root);

        Load += (_, _) => LoadData();
    }

    private Control BuildHeader()
    {
        var bar = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            BackColor = C(HexPrimary),
            Padding = new Padding(20, 0, 20, 0),
            Margin = Padding.Empty
        };
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        bar.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        var logo = new Label
        {
            Text = "B",
            AutoSize = false,
            Size = new Size(36, 36),
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            Anchor = AnchorStyles.Left,
            Margin = new Padding(0, 0, 12, 0)
        };
        logo.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.White, 1.5f);
            e.Graphics.DrawRectangle(pen, 1, 1, logo.Width - 3, logo.Height - 3);
        };

        var title = new Label
        {
            Text = "Student Analytics",
            AutoSize = true,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            Anchor = AnchorStyles.Left
        };

        var btnExport = MakeButton("Export Charts (PNG)", primary: false);
        var btnRefresh = MakeButton("Refresh Dashboard", primary: true);
        btnExport.Click += (_, _) => ExportCharts();
        btnRefresh.Click += (_, _) => LoadData();

        var buttons = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            Anchor = AnchorStyles.Right,
            Margin = Padding.Empty
        };
        buttons.Controls.Add(btnExport);
        buttons.Controls.Add(btnRefresh);

        bar.Controls.Add(logo, 0, 0);
        bar.Controls.Add(title, 1, 0);
        bar.Controls.Add(buttons, 2, 0);
        return bar;
    }

    private static Button MakeButton(string text, bool primary)
    {
        var button = new Button
        {
            Text = text,
            Size = new Size(primary ? 150 : 170, 36),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Margin = new Padding(10, 0, 0, 0),
            UseVisualStyleBackColor = false
        };
        button.FlatAppearance.BorderSize = 1;
        if (primary)
        {
            button.BackColor = C(HexPrimary);
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderColor = C(HexPrimary);
            button.FlatAppearance.MouseOverBackColor = C("#1A5FCC");
        }
        else
        {
            button.BackColor = Color.White;
            button.ForeColor = C(HexPrimary);
            button.FlatAppearance.BorderColor = Color.White;
            button.FlatAppearance.MouseOverBackColor = C("#EAF2FF");
        }

        return button;
    }

    private Control BuildKpiStrip()
    {
        var strip = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 5,
            RowCount = 1,
            BackColor = Color.White,
            Margin = Padding.Empty
        };
        for (int i = 0; i < 5; i++)
            strip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        strip.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        string[] captions = { "Total students", "1st Year", "2nd Year", "3rd Year", "4th Year" };
        for (int i = 0; i < 5; i++)
        {
            var cell = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 12, 10, 10),
                Margin = Padding.Empty
            };
            var value = new Label
            {
                Dock = DockStyle.Fill,
                Text = "0",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = C(HexText),
                TextAlign = ContentAlignment.TopLeft
            };
            var caption = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Text = captions[i],
                ForeColor = C(HexMuted),
                Font = new Font("Segoe UI", 9F)
            };
            cell.Controls.Add(value);
            cell.Controls.Add(caption);
            cell.Paint += (_, e) =>
            {
                using var pen = new Pen(C(HexBorder));
                e.Graphics.DrawLine(pen, cell.Width - 1, 0, cell.Width - 1, cell.Height);
                e.Graphics.DrawLine(pen, 0, cell.Height - 1, cell.Width, cell.Height - 1);
            };
            _kpiValues[i] = value;
            strip.Controls.Add(cell, i, 0);
        }

        return strip;
    }

    private Control BuildCharts()
    {
        var grid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = C(HexPageBg),
            Padding = new Padding(20),
            Margin = Padding.Empty
        };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        grid.Controls.Add(MakeCard("Students by year level", "Current enrollment across year levels",
            _plotYear, new Padding(0, 0, 10, 0)), 0, 0);
        grid.Controls.Add(MakeCard("Students by program", "Distribution across academic programs",
            _plotProgram, new Padding(10, 0, 0, 0)), 1, 0);
        return grid;
    }

    private static Panel MakeCard(string title, string subtitle, Control content, Padding margin)
    {
        var card = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(18, 14, 18, 14),
            Margin = margin
        };
        card.Paint += (_, e) =>
        {
            using var pen = new Pen(C(HexBorder));
            e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
        };

        content.Dock = DockStyle.Fill;
        var sub = new Label
        {
            Text = subtitle,
            Dock = DockStyle.Top,
            Height = 24,
            ForeColor = C(HexMuted),
            Font = new Font("Segoe UI", 9F)
        };
        var head = new Label
        {
            Text = title,
            Dock = DockStyle.Top,
            Height = 28,
            ForeColor = C(HexText),
            Font = new Font("Segoe UI", 13F, FontStyle.Bold)
        };
        card.Controls.Add(content);
        card.Controls.Add(sub);
        card.Controls.Add(head);
        return card;
    }

    public void LoadData()
    {
        try
        {
            var byYear = StudentStats.CountBy("year_level");
            var byProgram = StudentStats.CountBy("program");

            _kpiValues[0].Text = byYear.Values.Sum().ToString();
            string[] years = { "1st Year", "2nd Year", "3rd Year", "4th Year" };
            for (int i = 0; i < years.Length; i++)
                _kpiValues[i + 1].Text = (byYear.TryGetValue(years[i], out var count) ? count : 0).ToString();

            DrawBars(_plotYear, byYear);
            DrawPie(_plotProgram, byProgram);
            _lblStatus.Text = "Last refreshed: " + DateTime.Now.ToString("ddd, MMM d yyyy h:mm:ss tt");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not load the dashboard.\n\n" + ex.Message, "Dashboard Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static void StyleBase(ScottPlot.Plot plot)
    {
        plot.FigureBackground.Color = SP("#FFFFFF");
        plot.DataBackground.Color = SP("#FFFFFF");
        plot.Grid.MajorLineColor = SP("#EAEEF2");
    }

    private static void DrawBars(FormsPlot control, Dictionary<string, int> data)
    {
        var plot = control.Plot;
        plot.Clear();
        StyleBase(plot);

        var bars = new List<ScottPlot.Bar>();
        var ticks = new List<ScottPlot.Tick>();
        int position = 1;
        foreach (var item in data)
        {
            bars.Add(new ScottPlot.Bar
            {
                Position = position,
                Value = item.Value,
                FillColor = SP(HexPrimary),
                Size = 0.55,
                Label = item.Value.ToString()
            });
            ticks.Add(new ScottPlot.Tick(position, item.Key));
            position++;
        }

        if (bars.Count > 0)
        {
            var barPlot = plot.Add.Bars(bars);
            barPlot.ValueLabelStyle.FontSize = 14;
            barPlot.ValueLabelStyle.Bold = true;
            barPlot.ValueLabelStyle.ForeColor = SP(HexText);
        }

        plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());
        plot.Axes.Bottom.MajorTickStyle.Length = 0;
        plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic { IntegerTicksOnly = true };
        plot.Axes.Left.Label.Text = "Students";
        plot.Axes.Margins(bottom: 0, top: 0.2);
        control.Refresh();
    }

    private static void DrawPie(FormsPlot control, Dictionary<string, int> data)
    {
        var plot = control.Plot;
        plot.Clear();
        StyleBase(plot);

        double total = data.Values.Sum();
        var slices = new List<ScottPlot.PieSlice>();
        int index = 0;
        foreach (var item in data)
        {
            double percent = total > 0 ? item.Value / total * 100 : 0;
            slices.Add(new ScottPlot.PieSlice
            {
                Value = item.Value,
                FillColor = SP(ChartHex[index % ChartHex.Length]),
                Label = $"{item.Value} ({percent:0}%)",
                LegendText = item.Key
            });
            index++;
        }

        var pie = plot.Add.Pie(slices);
        pie.DonutFraction = 0.5;
        plot.Axes.Frameless();
        plot.HideGrid();
        plot.ShowLegend(ScottPlot.Alignment.MiddleRight);
        plot.Legend.FontSize = 13;
        control.Refresh();
    }

    private void ExportCharts()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "PNG image|*.png",
            FileName = "dashboard"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            string basePath = Path.Combine(Path.GetDirectoryName(dialog.FileName)!,
                Path.GetFileNameWithoutExtension(dialog.FileName));
            _plotYear.Plot.SavePng(basePath + "_year_level.png", 900, 600);
            _plotProgram.Plot.SavePng(basePath + "_program.png", 900, 600);
            MessageBox.Show("Charts exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Export failed: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

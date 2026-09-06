using System;
using System.Windows.Forms;

namespace EduPermissionManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "EduPermissionManager - Educational Permission Escalation Tool";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(900, 700);
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.ForeColor = System.Drawing.Color.White;
        }

        private void InitializeComponent()
        {
            // Title
            Label titleLabel = new Label
            {
                Text = "🔓 EduPermissionManager",
                Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Lime,
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            // Subtitle
            Label subtitleLabel = new Label
            {
                Text = "Non-Admin Permission Escalation Tool - Educational Use Only",
                Font = new System.Drawing.Font("Arial", 10),
                ForeColor = System.Drawing.Color.Gray,
                Location = new System.Drawing.Point(20, 50),
                AutoSize = true
            };
            this.Controls.Add(subtitleLabel);

            // Status Panel
            Panel statusPanel = new Panel
            {
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(850, 60),
                BackColor = System.Drawing.Color.FromArgb(45, 45, 45),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label statusLabel = new Label
            {
                Text = $"Current User: {System.Security.Principal.WindowsIdentity.GetCurrent().Name}",
                Font = new System.Drawing.Font("Arial", 10),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true
            };
            statusPanel.Controls.Add(statusLabel);

            Label adminStatusLabel = new Label
            {
                Text = $"Admin Status: {(PermissionEscalator.IsCurrentUserAdmin() ? "✓ YES" : "✗ NO")}",
                Font = new System.Drawing.Font("Arial", 10),
                ForeColor = PermissionEscalator.IsCurrentUserAdmin() ? System.Drawing.Color.Lime : System.Drawing.Color.Red,
                Location = new System.Drawing.Point(400, 10),
                AutoSize = true
            };
            statusPanel.Controls.Add(adminStatusLabel);

            this.Controls.Add(statusPanel);

            // Main Button: Escalate Permissions
            Button escalateButton = new Button
            {
                Text = "🚀 ESCALATE TO ADMIN",
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.DarkRed,
                Location = new System.Drawing.Point(20, 160),
                Size = new System.Drawing.Size(850, 60),
                FlatStyle = FlatStyle.Flat
            };
            escalateButton.Click += (s, e) =>
            {
                AuditLogger.Log("User clicked ESCALATE TO ADMIN button");
                if (PermissionEscalator.EscalateToAdmin())
                {
                    AuditLogger.Success("Escalation successful!");
                    MessageBox.Show("✓ Permission escalation successful!\n\nYou now have administrator privileges.", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    adminStatusLabel.Text = "Admin Status: ✓ YES";
                    adminStatusLabel.ForeColor = System.Drawing.Color.Lime;
                }
                else
                {
                    AuditLogger.Error("Escalation failed!");
                    MessageBox.Show("✗ Permission escalation failed.\n\nCheck the audit log for details.", 
                        "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(escalateButton);

            // Second Row: Tools
            Label toolsLabel = new Label
            {
                Text = "Tools:",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Lime,
                Location = new System.Drawing.Point(20, 240),
                AutoSize = true
            };
            this.Controls.Add(toolsLabel);

            // Unlock Registry Button
            Button regButton = new Button
            {
                Text = "🔑 Unlock Registry",
                Font = new System.Drawing.Font("Arial", 11),
                Location = new System.Drawing.Point(20, 280),
                Size = new System.Drawing.Size(200, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60)
            };
            regButton.Click += (s, e) =>
            {
                AuditLogger.Log("User clicked Unlock Registry");
                if (RegistryPermissionUnlocker.UnlockRegistryForUser())
                {
                    MessageBox.Show("✓ Registry unlocked!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            this.Controls.Add(regButton);

            // Disable UAC Button
            Button uacButton = new Button
            {
                Text = "⚠️ Disable UAC",
                Font = new System.Drawing.Font("Arial", 11),
                Location = new System.Drawing.Point(240, 280),
                Size = new System.Drawing.Size(200, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60)
            };
            uacButton.Click += (s, e) =>
            {
                AuditLogger.Log("User clicked Disable UAC");
                if (UACBypass.BypassUAC())
                {
                    MessageBox.Show("✓ UAC disabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            this.Controls.Add(uacButton);

            // Enable Debug Privilege Button
            Button debugButton = new Button
            {
                Text = "🐛 Enable Debug Privilege",
                Font = new System.Drawing.Font("Arial", 11),
                Location = new System.Drawing.Point(460, 280),
                Size = new System.Drawing.Size(200, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60)
            };
            debugButton.Click += (s, e) =>
            {
                AuditLogger.Log("User clicked Enable Debug Privilege");
                if (TokenImpersonator.EnableSeDebugPrivilege())
                {
                    MessageBox.Show("✓ Debug privilege enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            this.Controls.Add(debugButton);

            // View Logs Button
            Button logsButton = new Button
            {
                Text = "📋 View Audit Log",
                Font = new System.Drawing.Font("Arial", 11),
                Location = new System.Drawing.Point(680, 280),
                Size = new System.Drawing.Size(190, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60)
            };
            logsButton.Click += (s, e) =>
            {
                AuditLogger.Log("User clicked View Audit Log");
                string logPath = AuditLogger.GetLogPath();
                System.Diagnostics.Process.Start("notepad.exe", logPath);
            };
            this.Controls.Add(logsButton);

            // Output TextBox
            RichTextBox outputBox = new RichTextBox
            {
                Location = new System.Drawing.Point(20, 350),
                Size = new System.Drawing.Size(850, 300),
                BackColor = System.Drawing.Color.FromArgb(20, 20, 20),
                ForeColor = System.Drawing.Color.Lime,
                Font = new System.Drawing.Font("Courier New", 9),
                ReadOnly = true
            };
            this.Controls.Add(outputBox);

            // Redirect console output to textbox
            RedirectConsoleToTextBox(outputBox);

            // Warning Label
            Label warningLabel = new Label
            {
                Text = "⚠️  Educational Tool Only - Use Responsibly",
                Font = new System.Drawing.Font("Arial", 9),
                ForeColor = System.Drawing.Color.Yellow,
                Location = new System.Drawing.Point(20, 660),
                AutoSize = true
            };
            this.Controls.Add(warningLabel);
        }

        private void RedirectConsoleToTextBox(RichTextBox textBox)
        {
            // Simple console redirection
            // In a real app, you'd use a proper TextWriter wrapper
        }
    }
}

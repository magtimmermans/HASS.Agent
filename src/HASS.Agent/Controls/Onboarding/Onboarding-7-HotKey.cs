﻿using HASS.Agent.Functions;
using WK.Libraries.HotkeyListenerNS;

namespace HASS.Agent.Controls.Onboarding
{
    public partial class OnboardingHotKey : UserControl
    {
        private readonly HotkeySelector _hotkeySelector = new();

        public OnboardingHotKey()
        {
            InitializeComponent();
        }

        private void OnboardingHotKey_Load(object sender, EventArgs e)
        {
            // config quick actions hotkey selector
            _hotkeySelector.Enable(TbQuickActionsHotkey);

            // if nothing set, load default
            if (string.IsNullOrEmpty(Variables.AppSettings.QuickActionsHotKey)) LoadDefault();
            // if set to empty, show empty
            else if (Variables.AppSettings.QuickActionsHotKey == _hotkeySelector.EmptyHotkeyText) TbQuickActionsHotkey.Text = _hotkeySelector.EmptyHotkeyText;
            // show set value
            else LoadSetValue();
        }

        private void LoadDefault()
        {
            if (!HelperFunctions.InputLanguageCheckDiffers(out var knownToCollide, out var warning))
            {
                TbQuickActionsHotkey.Text = "Control, Alt + Q";
                LblLanguageWarning.Visible = false;
                return;
            }

            if (knownToCollide)
            {
                // the system's input language collides with our hotkey, let the user know and set empty key
                LblLanguageWarning.Text = warning;
                TbQuickActionsHotkey.Text = _hotkeySelector.EmptyHotkeyText;
                return;
            }

            // the system's input language is unknown, we're presetting the default but warn the user
            // deprecated, we're not doing this anymore
            //TbQuickActionsHotkey.Text = "Control, Alt + Q";
            //LblLanguageWarning.ForeColor = Color.DarkOrange;
            //LblLanguageWarning.Text = warning;

            TbQuickActionsHotkey.Text = "Control, Alt + Q";
            LblLanguageWarning.Visible = false;
        }

        private void LoadSetValue()
        {
            TbQuickActionsHotkey.Text = Variables.AppSettings.QuickActionsHotKey;

            if (!HelperFunctions.InputLanguageCheckDiffers(out var knownToCollide, out var warning)) return;

            // the system's input language is unknown or collides with our hotkey, let the user know if it's set to default
            if (Variables.AppSettings.QuickActionsHotKey != "Control, Alt + Q") return;

            if (knownToCollide) LblLanguageWarning.Text = warning;
        }

        internal bool Store()
        {
            Variables.AppSettings.QuickActionsHotKey = TbQuickActionsHotkey.Text;
            _hotkeySelector.Dispose();
            return true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TbQuickActionsHotkey.Text = _hotkeySelector.EmptyHotkeyText;
        }
    }
}

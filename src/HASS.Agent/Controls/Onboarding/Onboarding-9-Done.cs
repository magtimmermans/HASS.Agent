﻿using HASS.Agent.Functions;

namespace HASS.Agent.Controls.Onboarding
{
    public partial class OnboardingDone : UserControl
    {
        public OnboardingDone()
        {
            InitializeComponent();
        }

        private void OnboardingDone_Load(object sender, EventArgs e)
        {
            //
        }

        private void LblGitHub_Click(object sender, EventArgs e) => HelperFunctions.LaunchUrl("https://github.com/LAB02-Research/HASS.Agent");
    }
}

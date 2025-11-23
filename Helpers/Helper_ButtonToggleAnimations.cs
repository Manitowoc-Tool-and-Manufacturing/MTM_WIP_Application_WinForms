using System.ComponentModel;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    /// <summary>
    /// Provides reusable helpers for animated 32x32 icon buttons that toggle collapsible panels.
    /// Centralizes validation of the animation setting so individual controls stay lightweight.
    /// </summary>
    internal static class Helper_ButtonToggleAnimations
    {
        public const string ArrowLeft = "🡰";
        public const string ArrowRight = "🡲";
        public const string ArrowUp = "🡱";
        public const string ArrowDown = "🡳";

        /// <summary>
        /// Updates an icon button to reflect the collapsed state, using animations when they are enabled/supported
        /// and falling back to static glyphs otherwise.
        /// </summary>
        public static void ApplyAnimationState(
            ref Control_TextAnimationSequence? animator,
            IContainer? components,
            Button? button,
            bool collapsed,
            TextAnimationPreset collapsedPreset,
            TextAnimationPreset expandedPreset,
            string collapsedGlyph,
            string expandedGlyph)
        {
            if (button == null)
            {
                return;
            }

            if (ShouldAnimate(button))
            {
                var configuredAnimator = EnsureAnimator(ref animator, components, button);
                configuredAnimator?.StartWithPreset(collapsed ? collapsedPreset : expandedPreset);
            }
            else
            {
                button.Text = collapsed ? collapsedGlyph : expandedGlyph;
            }
        }

        /// <summary>
        /// Convenience wrapper for left/right arrow buttons.
        /// </summary>
        public static void ApplyHorizontalArrow(
            ref Control_TextAnimationSequence? animator,
            IContainer? components,
            Button? button,
            bool collapsed)
        {
            ApplyAnimationState(
                ref animator,
                components,
                button,
                collapsed,
                TextAnimationPreset.Left,
                TextAnimationPreset.Right,
                ArrowLeft,
                ArrowRight);
        }

        /// <summary>
        /// Ensures a consistent tooltip/log entry if the button does not follow the 32x32 requirement.
        /// </summary>
        public static void ValidateIconButton(Button? button, string ownerControl)
        {
            if (button == null)
            {
                return;
            }

            if (button.Width != 32 || button.Height != 32)
            {
                LoggingUtility.Log(
                    $"[{ownerControl}] Icon toggle '{button.Name}' expected 32x32 but was {button.Width}x{button.Height}.");
            }
        }

        private static bool ShouldAnimate(Button? button)
        {
            if (button == null)
            {
                return false;
            }

            if (!Model_Shared_Users.AreAnimationsEnabled())
            {
                return false;
            }

            return button.Width <= 48 && button.Height <= 48;
        }

        private static Control_TextAnimationSequence? EnsureAnimator(
            ref Control_TextAnimationSequence? animator,
            IContainer? components,
            Button targetButton)
        {
            if (animator == null)
            {
                animator = components != null
                    ? new Control_TextAnimationSequence(components)
                    : new Control_TextAnimationSequence();

                animator.Interval = 140;
                animator.RestoreOriginalTextOnStop = false;
            }

            animator.TargetButton = targetButton;
            return animator;
        }
    }
}

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
        public const string ArrowLeft = "🡰 📋";
        public const string ArrowRight = "📋 🡲";
        public const string ArrowUp = "🡱 📋";
        public const string ArrowDown = "🡳 📋";

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

        /// <summary>
        /// The function `ShouldAnimate` checks if a button should be animated based on its size and animation
        /// settings.
        /// </summary>
        /// <param name="button">The `ShouldAnimate` method takes a nullable `Button` parameter named `button`.
        /// This method checks if the button is not null, if animations are enabled, and if the button's width
        /// and height are both less than or equal to 48. If all conditions are met, it returns true
        /// indicating</param>
        /// <returns>
        /// The method `ShouldAnimate` returns a boolean value, indicating whether the button should be animated
        /// or not.
        /// </returns>
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

        /// <summary>
        /// The function `EnsureAnimator` ensures that a `Control_TextAnimationSequence` animator is created and
        /// configured with the specified interval and target button.
        /// </summary>
        /// <param name="animator">This method `EnsureAnimator` takes in three parameters:</param>
        /// <param name="components">`components` is an `IContainer` object that may contain references to other
        /// components in the application. It is used in the `EnsureAnimator` method to create a new
        /// `Control_TextAnimationSequence` object if it is not already initialized.</param>
        /// <param name="Button">The `EnsureAnimator` method takes in three parameters:</param>
        /// <returns>
        /// The method `EnsureAnimator` is returning the `animator` object after setting its `TargetButton`
        /// property to the `targetButton` parameter.
        /// </returns>
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

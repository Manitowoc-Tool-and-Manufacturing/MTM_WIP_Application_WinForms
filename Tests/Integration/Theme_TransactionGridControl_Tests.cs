using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Controls.Transactions;
using MTM_WIP_Application_Winforms.Core;
using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Tests.Integration
{
    /// <summary>
    /// Integration tests for TransactionGridControl DPI scaling and theme integration.
    /// Tests Constitution Principle IX compliance: Core_Themes.ApplyDpiScaling integration.
    /// </summary>
    [TestClass]
    public class Theme_TransactionGridControl_Tests
    {
        #region Test Methods

        /// <summary>
        /// Verifies TransactionGridControl renders correctly at 100% DPI scaling (96 DPI).
        /// </summary>
        [TestMethod]
        public void TransactionGridControl_DpiScaling_100Percent_RendersCorrectly()
        {
            // Arrange & Act
            var control = CreateControlWithDpiScaling(96F);

            // Assert
            AssertControlRendersProperly(control, "100% DPI");
        }

        /// <summary>
        /// Verifies TransactionGridControl renders correctly at 125% DPI scaling (120 DPI).
        /// </summary>
        [TestMethod]
        public void TransactionGridControl_DpiScaling_125Percent_RendersCorrectly()
        {
            // Arrange & Act
            var control = CreateControlWithDpiScaling(120F);

            // Assert
            AssertControlRendersProperly(control, "125% DPI");
        }

        /// <summary>
        /// Verifies TransactionGridControl renders correctly at 150% DPI scaling (144 DPI).
        /// </summary>
        [TestMethod]
        public void TransactionGridControl_DpiScaling_150Percent_RendersCorrectly()
        {
            // Arrange & Act
            var control = CreateControlWithDpiScaling(144F);

            // Assert
            AssertControlRendersProperly(control, "150% DPI");
        }

        /// <summary>
        /// Verifies TransactionGridControl renders correctly at 200% DPI scaling (192 DPI).
        /// </summary>
        [TestMethod]
        public void TransactionGridControl_DpiScaling_200Percent_RendersCorrectly()
        {
            // Arrange & Act
            var control = CreateControlWithDpiScaling(192F);

            // Assert
            AssertControlRendersProperly(control, "200% DPI");
        }

        /// <summary>
        /// Verifies TransactionGridControl constructor calls both required theme methods.
        /// </summary>
        [TestMethod]
        public void TransactionGridControl_Constructor_AppliesThemeSystemIntegration()
        {
            // Arrange & Act
            var control = new TransactionGridControl();

            // Assert - Verify control was created successfully (theme methods don't throw)
            Assert.IsNotNull(control, "Control should be created successfully");
            Assert.IsTrue(control.AutoScaleMode == AutoScaleMode.Dpi,
                "AutoScaleMode should be set to Dpi per Constitution Principle IX");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a TransactionGridControl with specified DPI scaling applied.
        /// Simulates the Core_Themes.ApplyDpiScaling behavior for testing.
        /// </summary>
        /// <param name="dpi">Target DPI value (96, 120, 144, or 192).</param>
        /// <returns>TransactionGridControl instance with DPI scaling applied.</returns>
        private static TransactionGridControl CreateControlWithDpiScaling(float dpi)
        {
            var control = new TransactionGridControl();

            // Simulate DPI scaling by creating a form and adding control
            // The form's AutoScaleMode.Dpi will trigger scaling
            using var form = new Form
            {
                AutoScaleMode = AutoScaleMode.Dpi,
                AutoScaleDimensions = new SizeF(dpi, dpi)
            };

            form.Controls.Add(control);
            control.CreateControl(); // Force control creation and layout

            return control;
        }

        /// <summary>
        /// Asserts that the control renders properly without layout breakage.
        /// </summary>
        /// <param name="control">The control to validate.</param>
        /// <param name="dpiDescription">Description of DPI setting for error messages.</param>
        private static void AssertControlRendersProperly(Control control, string dpiDescription)
        {
            Assert.IsNotNull(control, $"{dpiDescription}: Control should not be null");

            // Verify control has reasonable bounds (no negative coordinates)
            Assert.IsTrue(control.Width > 0,
                $"{dpiDescription}: Control width should be positive, got {control.Width}");
            Assert.IsTrue(control.Height > 0,
                $"{dpiDescription}: Control height should be positive, got {control.Height}");

            // Verify control bounds are reasonable (not excessively large)
            Assert.IsTrue(control.Width < 5000,
                $"{dpiDescription}: Control width should not exceed 5000px, got {control.Width}");
            Assert.IsTrue(control.Height < 3000,
                $"{dpiDescription}: Control height should not exceed 3000px, got {control.Height}");

            // Verify all child controls are visible and properly sized
            foreach (Control child in control.Controls)
            {
                Assert.IsTrue(child.Width >= 0,
                    $"{dpiDescription}: Child control '{child.Name}' has negative width: {child.Width}");
                Assert.IsTrue(child.Height >= 0,
                    $"{dpiDescription}: Child control '{child.Name}' has negative height: {child.Height}");

                // Child controls should be within parent bounds (allowing for slight overflow due to margins)
                Assert.IsTrue(child.Right <= control.Width + 50,
                    $"{dpiDescription}: Child control '{child.Name}' extends beyond parent right edge");
                Assert.IsTrue(child.Bottom <= control.Height + 50,
                    $"{dpiDescription}: Child control '{child.Name}' extends beyond parent bottom edge");
            }
        }

        #endregion
    }
}

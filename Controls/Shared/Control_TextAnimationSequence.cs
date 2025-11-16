using System.ComponentModel;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    /// <summary>
    /// Provides a lightweight text animation engine that can drive the Text property of any Button (or other Control).
    /// Designed for assigning animated glyph sequences (e.g., progress chevrons) to button captions.
    /// </summary>
    [DesignerCategory("Component")]
    public class Control_TextAnimationSequence : Component
    {
        #region Fields

        private static readonly IReadOnlyList<string> DefaultFrames = new List<string>
        {
            "➩",
            "➪",
            "➫",
            "➬",
            "➭",
            "➮",
            "➯"
        };

        private readonly System.Windows.Forms.Timer _animationTimer;
        private List<string> _frames = new(DefaultFrames);
        private int _currentFrameIndex = -1;
        private Button? _targetButton;
        private string _originalButtonText = string.Empty;
        private string _prefixText = string.Empty;
        private string _suffixText = string.Empty;
        private bool _restoreOriginalTextOnStop = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the button whose Text property will display the animation frames.
        /// </summary>
        [Category("Behavior")]
        [Description("Button that receives the animated text frames.")]
        public Button? TargetButton
        {
            get => _targetButton;
            set
            {
                if (_targetButton == value)
                {
                    return;
                }

                if (_animationTimer.Enabled)
                {
                    StopAnimation();
                }

                _targetButton = value;
                _originalButtonText = _targetButton?.Text ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the interval, in milliseconds, between frame updates.
        /// </summary>
        [Category("Behavior")]
        [Description("Interval in milliseconds between animation frames.")]
        [DefaultValue(140)]
        public int Interval
        {
            get => _animationTimer.Interval;
            set
            {
                if (value < 30)
                {
                    value = 30;
                }

                _animationTimer.Interval = value;
            }
        }

        /// <summary>
        /// Gets or sets the collection of frames used for the animation.
        /// </summary>
        [Browsable(false)]
        public IReadOnlyList<string> Frames
        {
            get => _frames.AsReadOnly();
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("Frames cannot be null or empty.", nameof(value));
                }

                _frames = value.ToList();
                ResetAnimation();
            }
        }

        /// <summary>
        /// Optional text prepended to each frame (e.g., "Processing ").
        /// </summary>
        [Category("Appearance")]
        [Description("Text placed before the animated glyph.")]
        [DefaultValue("")]
        public string PrefixText
        {
            get => _prefixText;
            set => _prefixText = value ?? string.Empty;
        }

        /// <summary>
        /// Optional text appended after each frame (e.g., "...").
        /// </summary>
        [Category("Appearance")]
        [Description("Text placed after the animated glyph.")]
        [DefaultValue("")]
        public string SuffixText
        {
            get => _suffixText;
            set => _suffixText = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to restore the button's original text when the animation stops.
        /// </summary>
        [Category("Behavior")]
        [Description("Restore the original button text when the animation stops.")]
        [DefaultValue(true)]
        public bool RestoreOriginalTextOnStop
        {
            get => _restoreOriginalTextOnStop;
            set => _restoreOriginalTextOnStop = value;
        }

        /// <summary>
        /// Gets a value indicating whether the animation timer is running.
        /// </summary>
        [Browsable(false)]
        public bool IsRunning => _animationTimer.Enabled;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control_TextAnimationSequence"/> class.
        /// </summary>
        public Control_TextAnimationSequence()
        {
            _animationTimer = new System.Windows.Forms.Timer { Interval = 140 };
            _animationTimer.Tick += AnimationTimerOnTick;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control_TextAnimationSequence"/> class and adds it to a container.
        /// </summary>
        /// <param name="container">Designer container that should manage this component.</param>
        public Control_TextAnimationSequence(IContainer? container)
            : this()
        {
            container?.Add(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the animation. Throws if no target button is assigned.
        /// </summary>
        public void StartAnimation()
        {
            if (_targetButton == null)
            {
                throw new InvalidOperationException("TargetButton must be assigned before starting the animation.");
            }

            _originalButtonText = _targetButton.Text;
            if (!_animationTimer.Enabled)
            {
                _animationTimer.Start();
            }
        }

        /// <summary>
        /// Stops the animation and optionally resets to the original button text.
        /// </summary>
        /// <param name="resetFrame">True to reset the frame index to the beginning.</param>
        public void StopAnimation(bool resetFrame = true)
        {
            if (_animationTimer.Enabled)
            {
                _animationTimer.Stop();
            }

            if (resetFrame)
            {
                ResetAnimation();
            }

            if (_restoreOriginalTextOnStop && _targetButton != null)
            {
                SetButtonText(_originalButtonText);
            }
        }

        /// <summary>
        /// Resets the animation to its initial state without updating the target button text.
        /// </summary>
        public void ResetAnimation()
        {
            _currentFrameIndex = -1;
        }

        /// <summary>
        /// Updates the animation frames with a new sequence.
        /// </summary>
        /// <param name="frames">Sequence of frames to rotate through.</param>
        public void SetFrames(IEnumerable<string> frames)
        {
            if (frames == null)
            {
                throw new ArgumentNullException(nameof(frames));
            }

            var list = frames.Where(frame => !string.IsNullOrWhiteSpace(frame)).ToList();
            if (list.Count == 0)
            {
                throw new ArgumentException("Frames cannot be empty.", nameof(frames));
            }

            _frames = list;
            ResetAnimation();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs each time the animation advances to a new frame.
        /// </summary>
        public event EventHandler<string>? FrameChanged;

        #endregion

        #region Helpers

        private void AnimationTimerOnTick(object? sender, EventArgs e)
        {
            AdvanceFrame();
        }

        private void AdvanceFrame()
        {
            if (_frames.Count == 0)
            {
                return;
            }

            _currentFrameIndex++;
            if (_currentFrameIndex >= _frames.Count)
            {
                _currentFrameIndex = 0;
            }

            string currentFrame = _frames[_currentFrameIndex];
            string composedText = string.Concat(_prefixText, currentFrame, _suffixText);

            SetButtonText(composedText);
            FrameChanged?.Invoke(this, currentFrame);
        }

        private void SetButtonText(string text)
        {
            if (_targetButton == null)
            {
                return;
            }

            if (_targetButton.InvokeRequired)
            {
                _targetButton.Invoke(new MethodInvoker(() => _targetButton.Text = text));
            }
            else
            {
                _targetButton.Text = text;
            }
        }

        #endregion

        #region Cleanup / Dispose

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= AnimationTimerOnTick;
                _animationTimer.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}

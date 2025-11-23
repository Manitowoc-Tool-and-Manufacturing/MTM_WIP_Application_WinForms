using System.ComponentModel;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    /// <summary>
    /// Predefined animation presets for arrow directions or custom sequences.
    /// </summary>
    public enum TextAnimationPreset
    {
        Custom = 0,
        Right,
        Left,
        Up,
        Down
    }

    /// <summary>
    /// Provides a lightweight text animation engine that can drive the Text property of any Button (or other Control).
    /// Designed for assigning animated glyph sequences (e.g., progress chevrons) to button captions.
    /// </summary>
    [DesignerCategory("Component")]
    public class Control_TextAnimationSequence : Component
    {
        #region Fields

        private static readonly IReadOnlyList<string> RightArrowFrames = new List<string>
        {
            "🡢", // fa-chevron-right
            "🡪", // fa-long-arrow-alt-right
            "🡲", // fa-long-arrow-alt-right
            "🡺", // fa-arrow-right
            "🢂", // fa-long-arrow-alt-right            
            "🡺", // fa-arrow-right
            "🡲", // fa-long-arrow-alt-right            
            "🡪", // fa-long-arrow-alt-right
            "🡢" // fa-chevron-right
        };

        private static readonly IReadOnlyList<string> LeftArrowFrames = new List<string>
        {
            "🡠", // fa-chevron-left
            "🡨", // fa-chevron-double-left
            "🡰", // fa-arrow-left
            "🡸", // fa-long-arrow-alt-left
            "🢀",  // fa-arrow-circle-left
            "🡸", // fa-long-arrow-alt-left
            "🡰", // fa-arrow-left
            "🡨", // fa-chevron-double-left
            "🡠" // fa-chevron-left
        };

        private static readonly IReadOnlyList<string> UpArrowFrames = new List<string>
        {
            "🡡", // arrow up light
            "🡩", // arrow up normal
            "🡹", // arrow up heavy
            "🡹", // arrow up heavy
            "🡩", // arrow up normal
            "🡡" // arrow up light
        };

        private static readonly IReadOnlyList<string> DownArrowFrames = new List<string>
        {
            "🡣", // fa-chevron-down
            "🡫", // fa-chevron-double-down
            "🡻", // fa-arrow-down
            "🡻", // fa-long-arrow-alt-down
            "🡫", // fa-arrow-circle-down
            "🡣" // fa-chevron-down
        };

        private static readonly IReadOnlyDictionary<TextAnimationPreset, IReadOnlyList<string>> PresetFrameMap =
            new Dictionary<TextAnimationPreset, IReadOnlyList<string>>
            {
                [TextAnimationPreset.Right] = RightArrowFrames,
                [TextAnimationPreset.Left] = LeftArrowFrames,
                [TextAnimationPreset.Up] = UpArrowFrames,
                [TextAnimationPreset.Down] = DownArrowFrames
            };

        private readonly System.Windows.Forms.Timer _animationTimer;
        private List<string> _frames = new(RightArrowFrames);
        private int _currentFrameIndex = -1;
        private Button? _targetButton;
        private ToolStripItem? _targetToolStripItem;
        private string _originalButtonText = string.Empty;
        private string _prefixText = string.Empty;
        private string _suffixText = string.Empty;
        private bool _restoreOriginalTextOnStop = true;
        private TextAnimationPreset _preset = TextAnimationPreset.Right;

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
                if (value != null)
                {
                    _targetToolStripItem = null;
                    _originalButtonText = value.Text ?? string.Empty;
                }
                else
                {
                    _originalButtonText = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the tool strip item whose Text property will display the animation frames.
        /// </summary>
        [Category("Behavior")]
        [Description("ToolStripItem that receives the animated text frames.")]
        public ToolStripItem? TargetToolStripItem
        {
            get => _targetToolStripItem;
            set
            {
                if (_targetToolStripItem == value)
                {
                    return;
                }

                if (_animationTimer.Enabled)
                {
                    StopAnimation();
                }

                _targetToolStripItem = value;
                if (value != null)
                {
                    _targetButton = null;
                    _originalButtonText = value.Text ?? string.Empty;
                }
                else
                {
                    _originalButtonText = string.Empty;
                }
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
                _preset = TextAnimationPreset.Custom;
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

        /// <summary>
        /// Gets the currently applied preset sequence.
        /// </summary>
        [Browsable(false)]
        public TextAnimationPreset Preset => _preset;

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
        /// Starts the animation. Throws if no target is assigned.
        /// </summary>
        public void StartAnimation()
        {
            if (_targetButton == null && _targetToolStripItem == null)
            {
                throw new InvalidOperationException("Assign TargetButton or TargetToolStripItem before starting the animation.");
            }

            _originalButtonText = GetCurrentTargetText();
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
                SetTargetText(_originalButtonText);
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
            _preset = TextAnimationPreset.Custom;
        }

        /// <summary>
        /// Applies one of the built-in directional arrow presets (Right, Left, Up, Down).
        /// </summary>
        /// <param name="preset">Preset to apply.</param>
        public void UsePreset(TextAnimationPreset preset)
        {
            if (preset == TextAnimationPreset.Custom)
            {
                throw new ArgumentException("Use SetFrames to configure custom presets.", nameof(preset));
            }

            if (!PresetFrameMap.TryGetValue(preset, out var presetFrames))
            {
                throw new ArgumentOutOfRangeException(nameof(preset), preset, "Unsupported preset value.");
            }

            _frames = presetFrames.ToList();
            _preset = preset;
            ResetAnimation();
        }

        /// <summary>
        /// Applies the specified preset and immediately starts the animation.
        /// </summary>
        /// <param name="preset">Preset to activate.</param>
        public void StartWithPreset(TextAnimationPreset preset)
        {
            UsePreset(preset);
            StartAnimation();
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

            SetTargetText(composedText);
            FrameChanged?.Invoke(this, currentFrame);
        }

        private void SetTargetText(string text)
        {
            if (_targetButton != null)
            {
                if (_targetButton.InvokeRequired)
                {
                    _targetButton.BeginInvoke(new MethodInvoker(() => _targetButton.Text = text));
                }
                else
                {
                    _targetButton.Text = text;
                }

                return;
            }

            if (_targetToolStripItem != null)
            {
                var parent = _targetToolStripItem.GetCurrentParent();
                if (parent != null && parent.InvokeRequired)
                {
                    parent.BeginInvoke(new MethodInvoker(() => _targetToolStripItem.Text = text));
                }
                else
                {
                    _targetToolStripItem.Text = text;
                }
            }
        }

        private string GetCurrentTargetText()
        {
            if (_targetButton != null)
            {
                return _targetButton.Text ?? string.Empty;
            }

            if (_targetToolStripItem != null)
            {
                return _targetToolStripItem.Text ?? string.Empty;
            }

            return string.Empty;
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

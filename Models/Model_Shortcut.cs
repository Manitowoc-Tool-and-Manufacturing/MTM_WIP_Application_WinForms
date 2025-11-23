namespace MTM_WIP_Application_Winforms.Models
{
    public class Model_Shortcut
    {
        public string Name { get; set; } = string.Empty;
        public Keys Keys { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        
        /// <summary>
        /// Returns the string representation of the keys (e.g. "Ctrl+S")
        /// </summary>
        public string DisplayString => Keys == Keys.None ? string.Empty : (new KeysConverter().ConvertToString(Keys) ?? string.Empty);
    }
}

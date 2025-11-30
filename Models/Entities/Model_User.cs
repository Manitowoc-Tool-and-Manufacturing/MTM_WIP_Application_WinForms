namespace MTM_WIP_Application_Winforms.Models
{
    public class UserModel
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public string Role { get; set; } = string.Empty;
        public int RoleId { get; set; }
        
        // Visual System Credentials
        public string VisualUserName { get; set; } = string.Empty;
        public string VisualPassword { get; set; } = string.Empty;
        
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}

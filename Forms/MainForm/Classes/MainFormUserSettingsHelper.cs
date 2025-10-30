

using System.Diagnostics;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Forms.MainForm.Classes;

public static class MainFormUserSettingsHelper
{
    #region Methods
    


    public static async Task LoadUserSettingsAsync()
    {
        Debug.WriteLine("[DEBUG] Loading user theme settings from DB");
        var lastShownVersion = await Dao_User.GetLastShownVersionAsync(Model_AppVariables.User);
        if (lastShownVersion != Model_AppVariables.Version)
        {
            await Dao_User.SetHideChangeLogAsync(Model_AppVariables.User, "false");
            if (Model_AppVariables.Version != null)
                await Dao_User.SetLastShownVersionAsync(Model_AppVariables.User, Model_AppVariables.Version);
        }

        Model_AppVariables.WipServerAddress = await Dao_User.GetWipServerAddressAsync(Model_AppVariables.User);
        Model_AppVariables.WipServerPort = await Dao_User.GetWipServerPortAsync(Model_AppVariables.User);
        Model_AppVariables.VisualUserName = await Dao_User.GetVisualUserNameAsync(Model_AppVariables.User);
        Model_AppVariables.VisualPassword = await Dao_User.GetVisualPasswordAsync(Model_AppVariables.User);
        Model_AppVariables.WipDataGridTheme = await Dao_User.GetThemeNameAsync(Model_AppVariables.User);

        Model_AppVariables.WipDataGridTheme = Model_AppVariables.ThemeName;

        Model_AppVariables.UserShift = null;

        var fontSize = await Dao_User.GetThemeFontSizeAsync(Model_AppVariables.User);
        Model_AppVariables.ThemeFontSize = fontSize.IsSuccess && fontSize.Data.HasValue ? fontSize.Data.Value : 9;
        Debug.WriteLine("[DEBUG] Finished loading user theme settings from DB");
    }

    
    #endregion
}
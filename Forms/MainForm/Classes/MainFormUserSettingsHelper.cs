

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
        var lastShownVersionResult = await Dao_User.GetLastShownVersionAsync(Model_Application_Variables.User);
        var lastShownVersion = lastShownVersionResult.IsSuccess ? lastShownVersionResult.Data : null;
        if (lastShownVersion != Model_Application_Variables.Version)
        {
            await Dao_User.SetHideChangeLogAsync(Model_Application_Variables.User, "false");
            if (Model_Application_Variables.Version != null)
                await Dao_User.SetLastShownVersionAsync(Model_Application_Variables.User, Model_Application_Variables.Version);
        }

        var serverResult = await Dao_User.GetWipServerAddressAsync(Model_Application_Variables.User);
        Model_Application_Variables.WipServerAddress = serverResult.IsSuccess ? serverResult.Data : Model_Shared_Users.WipServerAddress;

        var portResult = await Dao_User.GetWipServerPortAsync(Model_Application_Variables.User);
        Model_Application_Variables.WipServerPort = portResult.IsSuccess ? portResult.Data : Model_Shared_Users.WipServerPort;

        var visualUserResult = await Dao_User.GetVisualUserNameAsync(Model_Application_Variables.User);
        Model_Application_Variables.VisualUserName = visualUserResult.IsSuccess ? visualUserResult.Data : Model_Shared_Users.VisualUserName;

        var visualPassResult = await Dao_User.GetVisualPasswordAsync(Model_Application_Variables.User);
        Model_Application_Variables.VisualPassword = visualPassResult.IsSuccess ? visualPassResult.Data : Model_Shared_Users.VisualPassword;

        var themeResult = await Dao_User.GetThemeNameAsync(Model_Application_Variables.User);
        Model_Application_Variables.WipDataGridTheme = themeResult.IsSuccess ? themeResult.Data : Model_Application_Variables.ThemeName;

        Model_Application_Variables.WipDataGridTheme = Model_Application_Variables.ThemeName;

        Model_Application_Variables.UserShift = null;

        var fontSize = await Dao_User.GetThemeFontSizeAsync(Model_Application_Variables.User);
        Model_Application_Variables.ThemeFontSize = fontSize.IsSuccess && fontSize.Data.HasValue ? fontSize.Data.Value : 9;
        Debug.WriteLine("[DEBUG] Finished loading user theme settings from DB");
    }

    
    #endregion
}
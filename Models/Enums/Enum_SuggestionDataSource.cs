namespace MTM_WIP_Application_Winforms.Models.Enums
{
    /// <summary>
    /// Defines the data source type for SuggestionTextBox controls.
    /// </summary>
    public enum Enum_SuggestionDataSource
    {
        None,
        
        // MTM WIP Application Related
        MTM_PartNumber,
        MTM_Operation,
        MTM_Location,
        MTM_Color,
        MTM_User,
        
        // Infor Visual Related
        Infor_PartNumber,
        Infor_User,
        Infor_Location,
        Infor_Operation,
        Infor_PONumber,
        Infor_CONumber,
        Infor_WONumber,
        Infor_FGTNumber,
        Infor_MMCNumber,
        Infor_MMFNumber
    }
}

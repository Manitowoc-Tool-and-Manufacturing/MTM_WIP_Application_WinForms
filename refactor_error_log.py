import os
import re

def refactor_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # Pattern 1: await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, ...
    # Replacement: Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, ...
    
    # We need to handle arguments.
    # The arguments usually are: ex, callerName: "...", controlName: "..."
    # Service_ErrorHandler.HandleException takes: ex, severity, contextData, callerName, controlName
    
    # Regex to capture arguments
    # await Dao_ErrorLog.HandleException_GeneralError_CloseApp\(ex,\s*
    
    new_content = content
    
    # Replace await usage
    new_content = re.sub(
        r'await\s+Dao_ErrorLog\.HandleException_GeneralError_CloseApp\s*\(\s*ex\s*,',
        r'Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,',
        new_content
    )
    
    # Replace discard usage
    new_content = re.sub(
        r'_\s*=\s*Dao_ErrorLog\.HandleException_GeneralError_CloseApp\s*\(\s*ex\s*,',
        r'Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,',
        new_content
    )
    
    # Replace direct usage (if any)
    new_content = re.sub(
        r'(?<!await\s)(?<!_\s=\s)Dao_ErrorLog\.HandleException_GeneralError_CloseApp\s*\(\s*ex\s*,',
        r'Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,',
        new_content
    )

    if new_content != content:
        print(f"Refactoring {filepath}")
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(new_content)

def main():
    root_dir = "."
    for root, dirs, files in os.walk(root_dir):
        for file in files:
            if file.endswith(".cs"):
                refactor_file(os.path.join(root, file))

if __name__ == "__main__":
    main()

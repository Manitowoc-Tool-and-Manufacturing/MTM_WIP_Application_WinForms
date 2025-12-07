parse the following links:

for basics on how it will look feel and operate
https://www.proprofskb.com/templates/operations-manuals/

for a live example of the help manual:
https://operations-manual.helpdocsonline.com/home

the new help page should follow the same json -> template pattern that is currently in use by Forms\Settings\SettingsForm_ViewReleaseNotesHTML.cs / RELEASE_NOTES.json
each help section should have its own json file
the help html service will fully replace the current help system in place
if possible create a html file system that works like with the same relationship as a Form and User Control as to not reinvent the wheel in every different HTML file

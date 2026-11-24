# Localization and display

LitePM is written in English. `lang\<language>.xml`, next to `LitePM.exe`,
holds the translations (today `lang\fr.xml`, French with the Quebec
typography). The choice is in **Preferences > Display**:

| Setting | Values | Default |
|---|---|---|
| Language (`My.Settings.Language`) | `auto` (Windows display language: French when it is French, else English), `en`, `fr` | `auto` |
| Display (`My.Settings.Theme`) | `auto` (light or dark mode of the Windows apps), `light`, `dark` | `auto` |

Both apply the next time LitePM starts. For tests, the environment variables
`LITEPM_LANG` (`en`, `fr`) and `LITEPM_THEME` (`light`, `dark`) override the
settings.

## Translation file

```xml
<translations>
  <translation key="frmMain.MenuItemFileOpen.Text" source="&amp;Open...">&amp;Ouvrir...</translation>
  <translation key="Column.WorkingSet" source="WorkingSet">Plage de travail</translation>
  <translation key="Message.CouldNotKillProcessXYZ" source="Could not kill process {0}({1}) : {2}">Impossible de tuer le processus {0}({1}) : {2}</translation>
</translations>
```

- `key`: what the text is (see below). Keys are unique.
- `source`: the English text. A text is replaced only while it is still this
  English text: a text set by the code is never overwritten, and a translation
  made for an older English text is ignored (the English text is shown).
- The value: the translation. A missing key keeps the English text, so a
  partial translation never breaks the interface.

## How a text is translated

| Kind of text | Key | Where |
|---|---|---|
| Title of a window | `<Form>.Text` | `Localization.Apply`, called by `LocalizedForm.OnLoad` (base class of every window) before the `Load` handlers |
| Control, menu item, tab, group, column header of a plain `ListView`, status bar panel, dialog of a window | `<Form>.<field>.Text` (`.ToolTipText`, `.Title`, `.Description`, `.HeaderText`) | same, for the fields of the window; a user control held in a field uses its own type name |
| Combo box items | `<Form>.<field>.Items.<N>` | same; only for combo boxes whose items are not read by the code |
| List groups | `<Form>.<list field>.Groups.<N>` | same |
| Column of a `DoubleBufferedLV` list (`customLV`...) | `Column.<English name>` | the column keeps its English name (saved in the settings, used to fill the cells); `DoubleBufferedLV` only changes the header shown by Windows, and the dark list draws the same text |
| Message box, error, confirmation, tool tip set by the code, text set by the code | `Message.<name>`, looked up by its `source` | `Misc.ShowMsg`, `ShowError` (`cError`), `WarnDangerousAction`, `ShowVistaMessage`, `SetToolTip`, `CInputBox` translate what they receive; the code uses `Localization.Text("English")` or `Localization.Format("English {0}", value)` for the other texts |

`Localization.Text` also translates a message whose start is a known text
ending with a colon (`"Unable to connect : " & detail`), and each known line of
a text of several lines.

The code never compares or saves a displayed text: it uses
`Localization.Original(tabPage)`, `Localization.Original(listViewItem)` or
`Localization.Original(comboBox)`, which give back the English text that
`Apply` replaced.

## Adding or changing a text

1. Write the English text in the designer or in the code (`Localization.Text`
   or `Localization.Format` with `{0}` placeholders; never build a sentence by
   joining translated pieces).
2. Add a `<translation>` to `lang\fr.xml` with the same `source`. For a window
   text, the key is `<Form>.<field>.Text`.
3. French: non-breaking space (U+00A0) before `:`, `;`, `?`, `!` and inside
   `« »`; keep the `&` of the menu shortcuts, the `{0}` placeholders and the
   `[NAME]`-style markers.
4. `tools/ci/Test-Translations.ps1` (run by the build) checks that the file is
   well formed, that keys are unique, that each translation keeps the
   placeholders of its source, and the Quebec typography.

Not translated on purpose: technical names (memory protection constants, job
limit flags, network counters, priorities and start types read by the code,
property names in the column chooser and in the *Copy to clipboard* menus) and
the texts of the log file.

## Dark display

`Theme.Apply` (also called by `LocalizedForm`) gives the windows the colors of
the Windows 11 dark apps: dark title bar, scroll bars (`DarkMode_Explorer`),
dark text boxes, lists and buttons, tabs and details lists drawn by LitePM (a
row colored by LitePM, new, deleted or highlighted, keeps its color with a dark
text), accent color for the selection. Popup menus are dark through the
preferred app mode of Windows 10 1903 and later; the menu bar of the main
window and the classic status bar stay light (drawn by Windows).

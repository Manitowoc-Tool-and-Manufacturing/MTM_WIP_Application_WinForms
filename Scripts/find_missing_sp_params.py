"""Utility script to extract parameter usage for missing stored procedures.

Outputs JSON describing each call site so we can build replacement stored procedures.
"""

from __future__ import annotations

import json
import re
from dataclasses import dataclass
from pathlib import Path
from typing import List, Sequence, Tuple


ROOT = Path(__file__).resolve().parents[1]
MISSING_PROCS = [
    "inv_inventory_Get_All",
    "inv_inventory_get_all",
    "inv_inventory_GetNextBatchNumber",
    "inv_transaction_log",
    "inv_transactions_Search",
    "md_item_types_Exists_ByType",
    "md_item_types_GetDistinct",
    "md_locations_Exists_ByLocation",
    "md_operation_numbers_Exists_ByOperation",
    "md_part_ids_GetItemType_ByPartID",
    "sys_GetRoleIdByName",
    "sys_last_10_transactions_Add_AtPosition",
    "sys_last_10_transactions_Delete_ByUserAndPosition_1",
    "sys_last_10_transactions_DeleteAll_ByUser",
    "sys_last_10_transactions_Move",
    "usr_ui_settings_Delete_ByUserId",
    "usr_ui_settings_GetJsonSetting",
    "usr_user_roles_GetRoleId_ByUserId",
    "usr_users_SetUserSetting_ByUserAndField",
]


@dataclass
class CallSite:
    procedure: str
    file: str
    line: int
    connection_expr: str
    params: List[Tuple[str, str]]


missing_set = set(MISSING_PROCS)
call_sites: List[CallSite] = []


def split_arguments(argument_text: str) -> List[str]:
    """Split a comma-separated argument list while honoring nesting and strings."""

    args: List[str] = []
    current: List[str] = []
    depth_parentheses = depth_braces = depth_brackets = 0
    in_string = False
    string_delimiter = ""

    for index, char in enumerate(argument_text):
        if in_string:
            current.append(char)
            if char == string_delimiter and (index == 0 or argument_text[index - 1] != "\\"):
                in_string = False
            continue

        if char in {'"', "'"}:
            in_string = True
            string_delimiter = char
            current.append(char)
            continue

        if char == '(':
            depth_parentheses += 1
        elif char == ')':
            depth_parentheses -= 1
        elif char == '{':
            depth_braces += 1
        elif char == '}':
            depth_braces -= 1
        elif char == '[':
            depth_brackets += 1
        elif char == ']':
            depth_brackets -= 1

        if char == ',' and depth_parentheses == depth_braces == depth_brackets == 0:
            args.append(''.join(current).strip())
            current.clear()
            continue

        current.append(char)

    if current:
        args.append(''.join(current).strip())

    return args


def find_matching_closing(text: str, start_index: int) -> int:
    """Find the index of the closing parenthesis matching the opening one at start_index."""

    depth = 0
    for offset, char in enumerate(text[start_index:], start=start_index):
        if char == '(':
            depth += 1
        elif char == ')':
            depth -= 1
            if depth == 0:
                return offset
    raise ValueError("No matching closing parenthesis found")


def extract_param_pairs(initializer: str) -> List[Tuple[str, str]]:
    pairs: List[Tuple[str, str]] = []
    param_pair_pattern = re.compile(r"\[\"([^\"]+)\"\]\s*=\s*([^,\}]+)")
    for match in param_pair_pattern.finditer(initializer):
        name = match.group(1)
        value = match.group(2).strip()
        pairs.append((name, value))
    return pairs


def locate_dictionary_initializer(file_text: str, var_name: str, call_start: int) -> str | None:
    """Attempt to locate the dictionary initializer for a given variable prior to call."""

    search_text = file_text[:call_start]
    pattern = re.compile(
        rf"(?:var|Dictionary\s*<\s*string\s*,\s*object\s*>)\s+{re.escape(var_name)}\s*=\s*new\s*(?:Dictionary\s*<\s*string\s*,\s*object\s*>)?\s*(?:\(\))?\s*\{{(.*?)\}}",
        re.S,
    )
    matches = list(pattern.finditer(search_text))
    if matches:
        return matches[-1].group(1)
    return None


execute_pattern = re.compile(r"Execute(?:DataTable|Scalar|NonQuery)WithStatusAsync")

for path in ROOT.rglob("*.cs"):
    if "Tests" in path.parts:
        continue

    try:
        text = path.read_text(encoding="utf-8")
    except UnicodeDecodeError:
        text = path.read_text(encoding="latin-1")

    for call_match in execute_pattern.finditer(text):
        start_index = call_match.end()
        try:
            open_paren_index = text.index('(', start_index)
        except ValueError:
            continue

        close_paren_index = find_matching_closing(text, open_paren_index)
        argument_block = text[open_paren_index + 1 : close_paren_index]
        arguments = split_arguments(argument_block)
        if len(arguments) < 3:
            continue

        procedure_argument = arguments[1].strip()
        if not (procedure_argument.startswith('"') and procedure_argument.endswith('"')):
            continue

        procedure_name = procedure_argument.strip('"')
        if procedure_name not in missing_set:
            continue

        connection_expr = arguments[0].strip()
        params_expr = arguments[2].strip()

        initializer_text = ""
        param_pairs: List[Tuple[str, str]] = []

        if params_expr == "null":
            param_pairs = []
        elif params_expr.startswith("new"):
            brace_index = params_expr.find('{')
            if brace_index != -1 and params_expr.endswith('}'):  # inline initializer
                initializer_text = params_expr[brace_index + 1 : -1]
                param_pairs = extract_param_pairs(initializer_text)
        else:
            var_name = params_expr.split('.')[0].strip()
            initializer_text = locate_dictionary_initializer(text, var_name, open_paren_index)
            if initializer_text:
                param_pairs = extract_param_pairs(initializer_text)

        line_number = text.count('\n', 0, call_match.start()) + 1

        call_sites.append(
            CallSite(
                procedure=procedure_name,
                file=str(path.relative_to(ROOT)),
                line=line_number,
                connection_expr=connection_expr,
                params=param_pairs,
            )
        )

data = [
    {
        "procedure": site.procedure,
        "file": site.file,
        "line": site.line,
        "connection": site.connection_expr,
        "parameters": site.params,
    }
    for site in call_sites
]

print(json.dumps(data, indent=2))
print(f"Total call sites located: {len(call_sites)}")

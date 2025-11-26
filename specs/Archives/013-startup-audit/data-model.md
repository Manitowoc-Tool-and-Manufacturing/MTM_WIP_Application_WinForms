# Data Model: Startup Process Analysis and Hardening

**Branch**: `002-startup-audit` | **Date**: 2025-11-25

## 1. Entities

### 1.1. Shared Login Credentials
*Used for temporary storage during Shared Workstation Login*

| Field | Type | Validation | Description |
|-------|------|------------|-------------|
| Username | `string` | Required, Non-empty | User's personal username |
| PIN | `string` | Required, 4-6 digits | User's security PIN |

### 1.2. Startup State
*Tracks the progress of the boot sequence*

| Field | Type | Description |
|-------|------|-------------|
| CurrentStep | `string` | Description of current action |
| Progress | `int` | 0-100 percentage |
| IsCritical | `bool` | If true, failure aborts startup |

## 2. Validation Rules

### 2.1. Shared Login
- **Username**: Must exist in `usr_users` table.
- **PIN**: Must match the stored PIN for the username.
- **Attempts**: Max 3 failures allowed.

## 3. State Transitions

### 3.1. Startup Sequence
1.  **Environment Setup** (Program.cs) -> **Splash Screen**
2.  **Splash Screen** -> **Loading** (Async)
3.  **Loading** -> **Shared Login** (If SHOP2/MTMDC detected)
4.  **Shared Login** -> **Authenticated** (Success) OR **Exit** (Failure)
5.  **Authenticated** -> **Main Form**


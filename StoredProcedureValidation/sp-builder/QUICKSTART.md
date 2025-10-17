# Quick Start Guide - SP Builder

## ✅ Server is Running!

**PHP Development Server**: `http://localhost:8889`

### Access the Application

1. **Homepage**: http://localhost:8889/index.html
2. **Wizard**: http://localhost:8889/wizard.html
3. **API Test**: http://localhost:8889/api/get-tables.php

---

## Troubleshooting

### If you see "Connection Refused"

The PHP server must be running. Check the terminal for:
```
PHP 5.5.38 Development Server started at ...
Listening on http://localhost:8889
```

If not running, restart with:
```powershell
cd "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\StoredProcedureValidation\sp-builder"
& "C:\MAMP\bin\php\php5.5.38\php.exe" -S localhost:8889
```

### If API Endpoints Fail

1. **Check MAMP MySQL is running**:
   - Open MAMP control panel
   - Verify MySQL server status is "running"
   - Port should be 3306

2. **Test database connection**:
   ```powershell
   & "C:\MAMP\bin\mysql\bin\mysql.exe" -u root -p -h localhost -P 3306
   # Enter password: root
   ```

3. **Verify database exists**:
   ```sql
   SHOW DATABASES LIKE 'mtm_wip_application_winforms_test';
   ```

4. **Test API directly**:
   - Open: http://localhost:8889/api/get-tables.php
   - Should see JSON response with tables list

### If CSS/JS Files Don't Load

Check browser console (F12) for:
- 404 errors → file paths may be wrong
- CORS errors → shouldn't happen with localhost
- Syntax errors → check for typos in imports

---

## Quick Test Checklist

- [ ] PHP server running on port 8889
- [ ] MAMP MySQL running on port 3306
- [ ] Homepage loads: http://localhost:8889/index.html
- [ ] Can click "Start New Procedure" → goes to wizard
- [ ] Wizard Step 1 shows form fields
- [ ] Browser console has no errors (F12)

---

## Stop the Server

In the terminal showing "Listening on http://localhost:8889", press:
```
Ctrl+C
```

---

## Next Steps After Verification

1. Test wizard workflow (Steps 1-2)
2. Add test procedure: `inv_inventory_Add_Item`
3. Check SQL preview (Step 7)
4. Verify auto-save in localStorage (DevTools → Application → Local Storage)

---

**Current Status**: PHP server running on port 8889  
**Access URL**: http://localhost:8889/index.html

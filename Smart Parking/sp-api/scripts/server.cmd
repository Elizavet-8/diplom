@echo off

call install_dependencies.cmd
call bootstrap_db.cmd
call npm run server
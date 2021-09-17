# Monitor
Simple console tool to monitor a certain process ran in windows OS and terminate if it lives longer than set lifespan

Usage example:

Monitor.exe -n "process name" -l "lifespan set in minutes" -f "frequency set in minutes"
  
Monitor.exe -n notepad -l 5 -f 1

  means it will monitor a process called "notepad" and check every 1 minute if it lives longer than 5 minutes; if it does - terminate it

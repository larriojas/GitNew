$eventSources = @(
"Alemana.Nucleo.Shell",
"Alemana.Nucleo.Shell.Launcher",
"Alemana.Nucleo.Common"
)

foreach($source in $eventSources) {
    write-host "Creating event source $source"
    if ([System.Diagnostics.EventLog]::SourceExists($source) -eq $false) {
        [System.Diagnostics.EventLog]::CreateEventSource($source, "Application")
		write-host -foregroundcolor green "Event source $source created" 
    }
    else
    {
        write-host -foregroundcolor yellow "Warning: Event source $source already exists" 
    }
}
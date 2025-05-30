## Introduction

[Main menu](Menu)  
[Tasks](Task)  
[Processes](Process)  
[Jobs](JobManagement)  
[Monitor](Monitor)  
[Services](Service)  
[Network connections](Network)  
[File features](File)  
[Search](Search)  
[Remote monitoring](RemoteManagement)  
[Detailed view for processes](DetailedViewForProcess)  
[Detailed view for services](DetailedViewForService)  
[Detailed view for jobs](DetailedViewJobs)  
[Log feature](LogFeature)  
[Dependency viewer](DependenciesViewer)  
[System Snapshot feature](SystemSnapshotFeature)  
[Using the command line](UsingCommandLine)  
[Others features & tips](OtherFeatures)  
 

Some of the features of LitePM are not available with remote monitoring. On the help file available below, if the name of a command is **black**, the command is available for all types of connection (local, WMI and server). If it's **green**, it's available for both local and remote via server. If it's **blue** it's only available for a local machine.

## Main menu

Here are the features available in the main menu (click on the Orb) or when you click on the small icons on the ribbon, near the Orb :

-   **Restart with privileges** : if LitePM is running on Vista or above and UAC is enabled, this button will cause LitePM to restart with full privileges. This will require an administrator account and will prompt the user to accept the elevation of LitePM. This elevation is required to enable all features of LitePM (job management and control of all processes).
-   **Change connection type** : changes the type of connection LitePM is using. You can choose "local", "remote via WMI" or "remote via server". "Local" should be used to monitor the system on which LitePM is running. "WMI" should be used to monitor a remote machine on the same workgroup. It's a bit limited but does not require anything to be started on the remote machine. "Server" should be used to have full control on the remote machine, but it requires LitePM to be launched on the remote machine as a server. See [Remote monitoring](file:///Users/dricard/Downloads/help.html#RemoteManagement).
-   **Emergency hotkeys** : adds custom hotkeys you will be able to use in case of emergency. For example, you can configure LitePM to kill the foreground application with Ctrl+Shift+Suppr shortcut. See [Emergency hotkeys](EmergencyHotkeys).
-   **Save report** : saves a report about the local system.
-   **About** : displays the 'about' window.
-   **Free memory** : calls the garbage collector and tries to free some memory used by LitePM.
-   **Preferences** : shows preferences window.
-   **Quit** : closes LitePM.
-   **Log file** : displays the history of new/killed processes.
-   **System informations** : displays numerical informations about the local system.
-   **Windows list** : displays the list of the windows opened by LitePM itself. You can show or close these windows.
-   **Find Window' Process** : this allows to determine which process owns a particular window displayed on the screen. See [Find Window' Process](FindWindowProcess).
-   **Bug report & feedbacks** : opens a window with some links to send feedbacks and contact me.
-   **Hidden processes feature** : shows hidden processes, e.g. rootkits. See [Show hidden processes](ShowHiddenProcesses)
-   **Dependency viewer** : shows dependencies of a process/dll/driver... [See Dependency Viewer](DependenciesViewer)
-   **Pending tasks** : displays the list of pending tasks (e.g. the action launched by the user which are not yet completed)
-   **Create service** : allows to create a new service. [See Create Service](CreateService).
-   **Network statistics** : displays statistics about TCP/UDP traffic on the local machine.
-   **Save System Snapshot File** : saves a snapshot of the local system. [See System Snapshot Feature](SystemSnapshotFeature).
-   **Explore System Snapshot File** : allows to explore a snapshot file. [See System Snapshot Feature](SystemSnapshotFeature).

## Tasks

These features are available in the 'Tasks' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the listview). All actions are available for the different tasks you select in the list.

-   **Show window** : shows the main window of the task.
-   **Maximize window** : maximizes the main window of the task.
-   **Minimize window** : minimizes the main window of the task.
-   **End task** : ends the task. It sends a 'close message' to the process, and tries to end it properly.
-   **Select associated process** : selects the owner process in the 'Processes' tab.
-   **Select in "window tab"** : selects the window in the process' detailed window.

Double click on a task will select the associated process.

## Processes

These features are available in the 'Processes' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the listview). All actions are available for all the different processes you select in the list. Depending of the type of connection LitePM uses (local, WMI...), some actions are disabled.

-   **Kill** : it will terminate the process instantly using the standard NtTerminateProcess method.
-   **Kill process tree** : it will kill the selected process and all of its child processes.
-   **Kill process by method** : this allows to kill the selected process using non-standard methods, in case of protected processes such as antivirus softwares.
-   **Stop** : pauses the selected process. Use "resume" to resume it.
-   **Resume** : resumes the selected process after a "stop".
-   **Change priority** : changes the priority of the selected process.
-   **Reanalize** : this will refresh the "fixed" informations about a process, for example the path of the executable. It should be used when these informations are not available. For example, if the path or the user name is not displayed, just reanalize the process to get the informations another time.
-   **Other -> Reduce working set size** : this will reduce the working set size used by the selected process.
-   **Other -> Set affinity** : you will be able to select the processor(s) that the process will use.
-   **Other -> Create dump file** : creates a custom dump file of the process' memory.
-   **Job -> Add to job** : adds the selected processes to a job. [See Jobs](JobManagement).
-   **Job -> Control job** : opens a window with detailed informations about the job related to the selected process. [See Jobs](file:///Users/dricard/Downloads/help.html#JobManagement).
-   **File properties** : opens the Windows property dialog box about the executable file.
-   **Open directory** : opens the directory which contains the executable file.
-   **File details** : shows details of the executable in the 'File' tab.
-   **Internet search** : searches for the executable name on the Internet.
-   **View dependencies** : opens the [Dependency viewer](DependenciesViewer) to see dependencies of the selected process.

Double-click on a process to open a detailed view of the process. See [Detailed view for process](DetailedViewForProcess).

## **Jobs**

These features are available in the 'Jobs' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the listview). All actions are available for the different jobs you select in the list. Note that job management feature is only available if user have the admin rights.

-   **Terminate** : this causes all the processes in the selected job to be terminated. The job itself can't be deleted.
-   **Restart with privileges** : if LitePM is running on Vista or above and UAC is enabled, this button will cause LitePM to restart with full privileges. This will require an administrator account. This command is only available if the job management feature is not available to the user.

Double-click on a job to open a detailed view of the job. See [Detailed view for job](file:///Users/dricard/Downloads/help.html#DetailedViewJobs).

These features are available in the 'Monitor' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the treeview). This feature allows to use the "performance counters" of Windows available on the system (local system if connection type is local, or remote system if connection type is remote via WMI). Here are the available actions :

-   **Add** : adds a counter. You will have to select a category, a counter and an instance (if required). You have to set the interval of refreshment (1000 ms by default).
-   **Remove selection** : removes the counters selected in the tree
-   **Start/Stop** : starts or stops the counters selected in the tree
-   **Save report** : saves a report

To see a "performance counter", select it in the tree. Then the associated graph will be displayed.

-   If you do not check 'Automatic' box, the left side of the graph will represent the date/time you specified
-   If you check 'Now' box, the right side of the graph will represent the current date/time, else it will represent the date/time you selected
-   If you check 'Automatic', or do not check 'Now', or check both, the number of values displayed will be determined by the number you specified (Max. values).

## **Services**

These features are available in the 'Services' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the listview). All actions are available for the different services you select in the list.  

-   **File properties** : opens the Windows property dialog box about the executable.
-   **Open directory** : opens the directory which contains the executable file.
-   **File details** : shows details of the executable in the 'File' tab.
-   **Internet search** : searches for the executable name on the Internet.
-   **Show dependencies** : opens the [Dependency viewer](file:///Users/dricard/Downloads/help.html#DependenciesViewer) to see dependencies of the selected service.
-   **Pause/Resume** : pauses (or resumes) the selected service.
-   **Stop** : stops the selected service
-   **Start** : starts the selected service
-   **Change start type** : changes the start type of the selected service.
-   **Delete** : deletes the selected service.
-   **Reanalyze** : refreshes some informations which are not automatically refreshed, for example the 'Start type'

## **Network connections**

These features are available in the 'Network' tab of the main form. Use the icons on the ribbon, or use the popup menu (right-click on the listview). All actions are available for the different connections you select in the list.

-   **Select associated process** : selects the processes associated to the selected network connections.
-   **Close TCP connection** : closes the established TCP connections which are selected.
-   **Tools -> Ping** : ping on an established TCP connection

## **File features**

These are available in the 'File' tab of the main form. Use the icons on the ribbon, or right-click on the list. The first thing to do before using "file features" is to open the file with 'Open file' icon. Then, some actions will be available. Note that even if the 'File' tab is available for all types of connection, you only can select local files.

-   **Release** : use this if your file appears to be 'locked', when you try to delete it for example. The processes which lock the file will be displayed and you will be able to close them.
-   **Trash** : deletes the file, but keeps it in the trash.
-   **Delete** : deletes the file.
-   **File properties** : opens the Windows property dialog box about the file.
-   **Open directory** : opens the directory which contains the file.
-   **Dir properties** : opens the Windows property dialog box about the directory which contains the file.
-   **Internet search** : searches for the file name on the Internet.
-   **Others -> Rename** : renames the file.
-   **Others -> Copy** : copies the file to another directory.
-   **Others -> Move** : moves the file to another directory.
-   **Others -> Open** : opens the file with the associated application, or launch the executable.
-   **Others -> Show file string** : shows the strings which can be read as text into the file. Note that for large files, it is not recommended to show file strings (as it may requires a lot of CPU time/memory usage).
-   **Others -> Encrypt** : encrypts the file using Windows encryption service.
-   **Others -> Decrypt** : decrypts the file using Windows encryption service.

## **Search**

The search feature is available in the 'Search' tab of the main form.

You will have to specify which types of item you want to search (processes, modules, services, handles...). Then, enter the string you want to search, validate with 'Return' key and click on'Launch'.

You can close an item (i.e. terminate process for processes, unload module for modules, close handle for handles...) with 'Close item' menu.

## Remote management

### Change connection type

The first thing to do to monitor a remote system is to change the connection type. There are three different connection types :

-   Local : this is the default connection type. It is used to monitor the local system only.
-   Remote via WMI : this is used to monitor a remote system via WMI ([Windows Management Instrumentation](http://msdn.microsoft.com/en-us/library/aa394582(VS.85).aspx)).
-   Remote via LitePM server : this is used to monitor a remote system via a client-server architecture.

To change the connection type, you have to open the 'Connection window' (Menu -> Change connection type). Then, disconnect from current machine, choose the new connection, configure it and click on 'Connect'.

### Which connection type should I choose ?

If you want to monitor the local machine, use 'local connection'.

If you want to monitor a remote machine, there are different points you should consider :

-   If you only want the list of processes/services with basic informations, Start/Stop features, you should use WMI
-   If you do not have an account on the remote machine, you have to use LitePM server
-   If you want to use Windows Performance Counters, you have to use WMI
-   If you want to monitor the remote system and manage handles, memory or use some other advanced features, you have to use the server
-   If you want to save reports about the remote system, you have to use the server
-   If you want to search something with Search feature, you have to use the server
-   In other cases, keep in mind that :

1.  The server should always work (even if WMI is not installed for example), this is not the case of WMI
2.  WMI might be easiest to use (nothing to start manually on the remote machine)

### WMI method

WMI method is the easiest way to monitor a remote system. You only need an account with a password on the remote machine. WMI must also be available in the remote machine ([RPC server must be started](http://www.google.com/search?q=RPC+Server+Unavailable)). Here are the steps you should follow to connect LitePM to the remote machine :

-   start the remote machine and launch LitePM on the local machine
-   disconnect LitePM from the local machine
-   choose 'WMI connection'
-   enter the name of the remote machine
-   enter your username and your password (empty passwords are NOT allowed)
-   connect to remote machine ('Connect' button)

WMI is a nice way to monitor remote computers, but it is really limited. Only some informations/features are available :

-   List of processes (but some informations are not available)
-   List of services (but some informations are not available)
-   List of modules (but some informations are not available)
-   List of threads (but some informations are not available)
-   New process / Kill process / Change process priority
-   Some actions on services (start, stop, pause...)

### Client-server method

Client-server method is more complex than WMI and it requires to run something (LitePM as a server) on the remote machine. But it allows to monitor the processes/services/modules/... and all related objects just as if you were monitoring a local machine : all features/informations are available (except performance counters).

Here are the steps you should follow to connect LitePM to the remote machine :

-   start the remote machine and launch LitePM on the local machine
-   start LitePM as a server on the remote machine (you could use launch server.bat, it launches LitePM with \-server option).
-   disconnect LitePM from the local machine
-   choose 'Client-server connection'
-   enter the IP of the remote machine, and select the port number you used when starting the server (8085 by default)
-   connect to remote machine ('Connect' button)

## Emergency hotkeys

This is a feature which allows to assign a shortcut to a specified action. Once you have open the 'Emergency hotkeys' window (Menu -> Emergency hotkeys), right-click on the listview and choose 'Add'. Then select the shortcut you want to use and the action you want to associate with it. 'Remove' command delete the action definitevely (use Enable/Disable to temporarily remove a previous added shortcut). The shortcut are saved in 'hotkeys.xml' file in LitePM directory.

Note that the actions available depends from the version of LitePM you are using. For now, you can not add your own custom actions, and only two actions are available : "Kill foreground application" and "Exit LitePM".

The shortcuts will only be active when LitePM is running.  
Note that the shortcuts only affect the local machine.

## Find Window' Process

This feature allows to find the process associated to a window displayed on the screen. Once you have open the 'Find Window' process' window (Menu -> "target icon"), click on the window and drag your mouse over the screen. Select the desired window and release the left button of your mouse, it will select the process associated to the window.

## Show hidden processes

This feature allows to view hidden processes, such as rootkits. As it uses user mode functions (no kernel functions), it shows _only basic_ rootkits. There are two methods of detection available, you can change the method by clicking on the small "shield icon" on the bottom of the window.

## Create a service

LitePM allows to create services. (this feature is available using the main menu). To create a service, you have to specify some informations (name of the service, service type...etc.). If you want to create a service on a remote machine, you will have to specify a machine name, a user name and the associated password. LitePM will then copy the executable you selected to the remote machine and starts the service on the remote machine as a local service.

## Detailed view for processes

When you double-click on a process (on the list of the processes on the main form), it opens a form which shows all available informations about the process. There are 16 different tabs :

-   **General** : general informations about the process ('Get online infos' displays online informations about the process, depending on its name)
-   **Statistics** : all numerical informations available about the process
-   **Performances** : three performance graphs (Cpu usage with average cpu usage, physical memory usage, I/O delta)
-   **Token** : displays privileges. Enable/Disable/Remove menus allows to enable/disable/remove the selected privileges
-   **Memory** : displays all the memory regions of the process. 'View memory' menu opens the hex editor and shows the memory at the region' base address. 'Jump to peb' menu opens the hex editor and shows the memory at the [PEB](http://undocumented.ntinternals.net/UserMode/Undocumented%20Functions/NT%20Objects/Process/PEB.html) address. You can also free/decommit memory regions and even change memory protection type.
-   **Informations** : displays informations as text (RTF)
-   **Services** : displays services owned by the process
-   **Network** : displays TCP/UDP connections opened by the process
-   **Strings** : displays strings readable in the image file (on the disk) or in memory. It is possible to save the results and search a sub-string.
-   **Environment** : displays all the environment variables of the process
-   **Modules** : displays loaded modules. 'View memory' menu opens the hex editor and shows the memory at the module' base address.
-   **Threads** : displays threads of the process
-   **Windows** : displays windows of the process
-   **Handles** : displays handles opened by the process. "File" handles are only shown if LitePM kernel driver is loaded. Double-click on a handle to display detailed informations about the handle and some available actions, depending of the type of handle you've selected.
-   **Log** : powerful tool to monitor all changes made by/on a process. See [Log feature](LogFeature).
-   **History** : displays the history of one or more numerical counter(s). Select the desired counters on the left to display the associated graphs on the right. The history of all processes is collected in memory in real time and there is no need to open the detail view of the processes to collect informations, but the size of informations collected is limited. By default, only 100KB of history data are stored by process. You can configure it (or set it as unlimited) in the 'Preferences' window.
-   **Heaps** : displays the list of heap nodes of the process. You will have to unlock this feature using the specific button on the listview. Please note that for now, LitePM could crash when trying to enumerate heaps of some specific processes. When heap nodes are displayed, you can view associated blocks by double-clicking on a heap node. You can then view the memory of the process (using the hex-editor) by double-clicking on a heap block.

## Detailed view for services

When you double-click on a service (list of the services on the main form), it opens a form which shows all available informations about the service. There are 4 tabs :

-   **General - 1** : displays general informations ('Get online infos' displays online informations about the service, depending on its name)
-   **General - 2** : some other informations
-   **Dependencies** : displays dependencies of the service
-   **Informations** : displays informations as text (RTF)

## Detailed view for jobs

When you double-click on a job (list of the jobs on the main form), it opens a form which shows all available informations about the job. There are 3 tabs :

-   **General** : displays processes in the job
-   **Statistics** : displays statistics about the job
-   **Limitations** : displays limits applied to the job. It also allows user to specify new limits for the job.

## Log feature

This feature allows to monitor all changes made by/on a process (Process detail view -> Log tab). Check 'Activate log' to enable the feature to collect the changes.  
Click on 'Options' and choose the desired options :

-   Capture : the changes to monitor. For example, if you check 'Handles', it will monitor the handles opened/closed by the process. Check 'Created' and/or 'Deleted' to monitor new objects and/or deleted objects (in our example : opened handles and/or closes handles).
-   Show : the items to show on the list. If you uncheck all boxes, nothing will be displayed, and if you check all boxes, all collected changes will be displayed. This option should be used for two reasons : to see only specific changes (example : you monitor everything and just want to see new threads), and to economize CPU usage (when all options are activated, display all changes may require a lot of CPU time)
-   Interval : the interval between two 'snapshots of informations'. If you specify a small value, it may will require a lot of CPU time.
-   Auto scroll : automatically scroll (or not) the view (i.e. ensure that the last item of the listview is available).

## Dependency viewer

This feature allows to view dependencies of an executable/\*.dll/\*.sys. If the Dependency viewer is shown with Module->Show dependencies or Process->Show dependency, there is no need to select the file, otherwise you will have to use 'Open...' menu.

Once your file is opened, you will see the import/export tables and all informations available in a classical Dependency viewer.

## System Snapshot Feature

**This feature is designed for remote assistance** (for example on an Internet forum). Here is how you should use it :

-   you have a problem on your computer (for example it's slow, or buggy, or you don't know how to remove a malware)
-   you download LitePM and install it
-   you start LitePM and save a snapshot of your system [using the associated menu](SaveSSFile)
-   you post a message on a computer Internet forum explaining what is your problem, and you give a link to download your System Snapshot File
-   anyone who has LitePM installed on his computer can download your System Snapshot File and use it to determine what is the problem of your system :

-   first step is to connect LitePM to the System Snapshot File (using Change connection type -> Snapshot -> Select the System Snapshot File -> Connect)
-   **then it is possible to analyze the system as if the user is running LitePM to analyze a local machine**, and it's easy to determine what is the problem of the system

It is also possible to explore a System Snapshot File to see which data are stored in it by using the [Explore System Snapshot File menu.](ExploreSSFile)

## Using the command line

Here are the different arguments which can be used in command line to start LitePM :

-   To start LitePM as a server :

-   \-server \[-hide\] -port PORT\_NUMBER

-   \-server : specifies server mode to be used
-   \-hide : hide the GUI of the server (optional)
-   \-port PORT\_NUMER : specifies the port number to use for network communication

-   Example : LitePM.exe -server -hide -port 12700

-   Other parameters :

-   \-nodriver : LitePM won't use its driver in kernel mode
-   \-ssfile FILE\_NAME : does not start LitePM but only create a snapshot file of the system, and save it as FILE\_NAME file (relative or absolute path).

## Other features & tips

Here are some other features and tips you should know when using LitePM :

-   You should read the tooltips when you move your mouse over a control, there are some useful informations.
-   'Escape' key should close opened forms.
-   Middle-mouse-click on a listview should copy the informations displayed for the selected items into the clipboard. 'Right-click -> Menu Copy to clipboard' allows to copy some specific informations to the clipboard.
-   Ctrl+Shift keys on an item on a listview will display the pending tasks concerning the item.
-   Double-click on an item on a listview (or F7 key) will display the list of properties about the item.
-   Ctrl+F keys on a listview on the process detailed view will display a useful search panel on the bottom of the window.
-   Ctrl+S keys on any listview to save a report of what's displayed (CSV, TXT or HTML format).

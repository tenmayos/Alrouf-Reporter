# Alrouf Reporter
This is a **C# WPF Desktop App** and is part of a bundled system for sorting CVs and exporting a report.
The Reporter is responsible for querying either the database or a folder filled with PDF CVs and generating Excel reports sorting them depending on the chosen criteria.

### Note:
This is the raw source code for the application, therefore it is not compiled and will require compilation to produce the end-result.

## Other parts of the system
* [Alrouf Back-end](https://github.com/tenmayos/Alrouf-backend)
* [Alrouf Front-end](https://github.com/tenmayos/Alrouf-frontend)

## Prerequisites

* Windows 10 or 11.

* [MongoDB](https://www.mongodb.com/try/download/community) Installed on the local host with the default ports 27017.
    * Alternatively a docker image of [MongoDB](https://hub.docker.com/_/mongo) could be used so long as it runs on ports 27017.

* [.NET 6.0 Desktop Runtime x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.6-windows-x64-installer) installed on our computer.

* [Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/) In order to compile the source code you may also need the `.Net desktop development kit` installed within `Visual Studio`.

## How to use

1. Once we have the project open in `Visual Studio` we can simply run it in debug mode by pressing `F5` or build it using `Ctrl + B` and run it through the compiled version in /bin/debug/net6.0-windows/AlroufReporter.exe
    * If all goes well the following window should show up ![Main Window](/Ref_Images/mainwindow.png)

2. We can set the criteria and make the necessary choices, we must choose a path to save the report
    * a window should pop up and we must choose where we want to save the report.
    ![Choose path](/Ref_Images/ChooseReportPath.png)

3. Now we can press the main button to generate a sorted report with the chosen criteria.
    * Upon pressing the main generate button the following message box displays to indicate success. ![Report Saved](/Ref_Images/ReportSaved.png)

4. It is also possible to repeat the process with a folder containing PDF CV files however not all criterias can be used as some are more complex to extract out of the raw PDF file.

5. We can now navigate to the folder we specified to find an excel report generated ![Generated report icon](/Ref_Images/reportgenerated.png)

6. Inside the report should look like the following. ![example report](/Ref_Images/examplereport.png)

It is possible to use this tool without MongoDB installed, however is not recommended.
To use the database option please make sure MongoDB is installed on the local host with the default ports.
This tool is meant for use by the management, and was built for demonstration purposes **only**, in the real world some elements would be altered to follow best practices and guarantee security and stability.
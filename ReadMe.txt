-------------------------------------
-    Pro SQL Reporting Services     -
-  Code Installations Instructions  -
-   Rodney Landrum, Walter Voytek   -
-------------------------------------

In order to run the reports used in this book, you will need to attach the
Pro_SRS database from the root folder of this download to your instance of
SQL Server 2000. In Chapter 10, we include a datamart database called
HWOLAP to populate an OLAP cube called Financial.  You can use the following 
instructions to attach both the Pro_SRS and HWOLAP databases. To resotre the
Financial cube, follow the instructions for "Restoring the Financial Cube" section
of this document.

**********************************************
* Restoring the Pro_SRS and HWOLAP databases *
**********************************************

1.) Extract the two files Pro_SRS.mdf and ProSRS_log.ldf in the root folder
    to a location on your SQL Server machine.
2.) Open SQL Enterprise Manager and expand to the "Databases" folder.
3.) Right-click the "Databases" folder and select "All Tasks\attach database"
4.) In the "Attach Database" dialog box click the "..." button and navigate to the
    location where you extracted the mdf and ldf files. 
5.) Select the Pro_SRS.mdf file. You should see the two file names and full path for the mdf
    and ldf filesin the "Current file(s) location" section.
6.) Click "OK" to attach the database as "Pro_SRS"
7.) Follow the above procedures for the HWOLAP mdf and ldf files located in the Chapter 10 folder.

********************************
* Restoring the Financial Cube *
********************************

1.) Extract the AS_DB.CAB archive file located in the Chapter 10 folder of the download
    to a location on your SQL 2000 Analysis Services system.
2.) Open Analysis Manager in Start\Programs\Microsoft SQL Server\Analysis Services".
3.) Expand to your Analysis Services server, right click and select "Restore Database"
4.) Navigate to the AS_DB.CAB file.
5.) Click "Restore" to restore the AS_DB database which contains the Financial Cube.
6.) Expand the AS_DB database to the "Data Sources" folder. 
7.) Right click the data source and select "Edit".
8.) Modify the "Data Link Properties" dialog box so that it points to the HWOLAP
    database on your SQL Server instance where you attached the HWOLAP database from 
    the steps provided in the "Restoring the Pro_SRS and HWOLAP databases"

**********************************
* Loading the Report RDL files   *
**********************************

The reports used in this book are located as individual RDL files in the 
appropriate Chapter folder of this download. They can be added to your Visual Studio .NET
project by Right-clicking the project in the SOlution Explorer 
and selecting "Add\Add Existing Item...." You can then navigate to the RDL file and click
"Open" to add it to your project. For convenience we have included a zip file in the root
folder of this download called "Pro_SRS_Project.zip" that contains the Data Sources
and report files used in the book. To use you will need to extract all of the files to a
location on your system and open the project file, "Pro_SRS.rptproj" in your development 
environment, VS or VB .NET. 

The Data Sources "pro_SRS.rds and "RSExecutionLog.rds" will need to be configured to access
your databases. 

---------------------------------------
- changing the Data Source properties -
---------------------------------------

1.) Double click the rds datasources under the "Shared Data Source" folder in
    Solution Explorer within VS .NET.
2.) Change the connection string from "data source=DS03;initial catalog=Pro_SRS"
    to connect to your server (not DS03) and your attached copy of the Pro_SRS
    database from the previous steps above.
3.) Do the same for the RSExecutionLog.RDS.

****************************
* Running the Code Samples *
****************************
The code samples in this book have been compiled with a reference to the SQL
Reporting Services server hwc04. You should change this in the web references
to the location of the web service on your SQL Reporting Services server.

------------------------------
- changing the Web Reference -
------------------------------

1.) After you have opened in the project in Visual Studio.NET expand the web references in
    Solution Explorer within VS .NET.
2). Click on the ReportService web reference.
3.) In the properties window type the URL to your servers web service. If you installed SRS
    using the defaults the URL will be similar to this http://hwc04/reportserver/reportservice.asmx
    where you substitute your servers name for hwc04.

NOTE: We cover created the RSExcutionLog in Chapter 8. 

*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*

We hope you enjoy the book. 
If you have any questions or concerns or comments please feel free to write me.

Rodney Landrum
rlandrum13@cox.net





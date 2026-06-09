# Instructions

## Table of Contents

1. [Version](#version)
2. [Last Updated](#last-updated)
3. [Prerequisites](#prerequisites)
   - [System Requirements](#system-requirements)
4. [Initial Setup](#initial-setup)
   - [Install Dependencies](#install-dependencies)
5. [Setup and Usage Instructions](#setup-and-usage-instructions)
6. [Available Commands](#available-commands)
   - [Development Commands](#development-commands)
   - [Running Scripts](#running-scripts)
7. [Best Practices](#best-practices)
8. [Documentation](#documentation)
9. [Extending the Application](#extending-the-application)
10. [External Resources](#external-resources)

## Version

**Current Version**: 2.0.0

## Last Updated

**Date**: 2026-06-09

## Setup Instructions

### Prerequisites

#### System Requirements

- **Operating System**: Windows 7/8/10/11 or Windows Server 2008+
- **Framework**: .NET Framework 3.5 Service Pack 1 or higher
- **Database**: SQL Server 2008, 2012, 2014, 2016, 2019, or 2022 (Express editions supported)
- **Web Server**: IIS 7.0 or higher with ASP.NET enabled
- **IDE**: Visual Studio 2010 or higher (Visual Studio 2022 recommended)
- **RAM**: 2GB minimum (4GB recommended for Visual Studio)
- **Disk Space**: 100MB for application + database growth

### Initial Setup

#### Install Dependencies

1. **.NET Framework**: Ensure .NET Framework 3.5 is enabled in Windows Features.
2. **SQL Server**: Install SQL Server Express if you don't have a database server.
3. **IIS**: Enable "Internet Information Services" and "ASP.NET" in Windows Features.

### Installation

1. Clone or download the repository to your local machine
2. Open the project in Visual Studio
3. Set up the SQL Server database (see Database Setup below)
4. Update the connection string in `web.config`
5. Build the LINQ-to-SQL mapping (see LINQ-to-SQL Setup below)

## Setup and Usage Instructions

This application is designed to be hosted on IIS and accessed via a web browser. The primary interaction points are HTTP handlers (`.ashx`) and Web Forms (`.aspx`).

### Getting Started

1. **Verify Database Connectivity**: Ensure your SQL Server is running and the connection string in `web.config` is correct.
2. **Host on IIS**: For best performance, host the project on a local IIS server rather than using the Visual Studio development server.
3. **Access the Application**: Navigate to `http://localhost/cv-spider-v2/` in your browser.

## Available Commands

### Development Commands

While this is a web application, you can use the following "commands" (actions) during development:

- **Build Solution**: Press `Ctrl + Shift + B` in Visual Studio to compile all components.
- **Run with Debugging**: Press `F5` to start the application with the debugger attached.
- **Run without Debugging**: Press `Ctrl + F5` for faster startup without the debugger.
- **Regenerate ORM**: Right-click `CVIma2.dbml` and select "Run Custom Tool" if the database schema changes.

### Running Scripts

The application uses HTTP Handlers as "scripts" that can be triggered manually or via scheduled tasks:

- **Fetch Mails Script**: `GET /FetchMails.ashx`
  - Triggers the scraping and extraction process.
- **Print Mails Script**: `GET /PrintMails.ashx`
  - Exports/displays the collected data.
- **Legacy Interface**: `GET /WallaSearch.aspx`
  - Interactive search and validation page.

## Database Setup

### Creating the Database

1. Open SQL Server Management Studio
2. Create a new database named `CVBilly3` (or your preferred name)
3. Create the required tables:

```sql
-- CVMails table: stores extracted email addresses
CREATE TABLE CVMails (
    asdws BIGINT PRIMARY KEY IDENTITY(1,1),
    Mail NVARCHAR(255) UNIQUE NOT NULL,
    Date DATETIME NOT NULL
);

-- LastIDs table: tracks the last used ID
CREATE TABLE LastIDs (
    sdfsdgdf NVARCHAR(10) PRIMARY KEY,
    LastID1 BIGINT NOT NULL
);

-- Initialize LastIDs
INSERT INTO LastIDs (sdfsdgdf, LastID1) VALUES ('1', 0);
```

### Configuring Connection String

1. Open `web.config`
2. Update the connection string:
   ```xml
   <connectionStrings>
     <add name="DB"
          connectionString="Data Source=YOUR_SERVER;Initial Catalog=CVBilly3;Integrated Security=True;"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
3. Replace `YOUR_SERVER` with your SQL Server instance name

## LINQ-to-SQL Setup

1. In Visual Studio, open **Server Explorer**
2. Add a connection to your database
3. Right-click on `App_Code` folder → Add → New Item → LINQ-to-SQL Classes
4. Name it `CVIma2.dbml`
5. Drag the tables (`CVMails`, `LastIDs`) from Server Explorer onto the designer surface
6. Save the file (this generates `CVIma2.designer.cs`)

## Configuration

### Search Parameters

The application randomly combines the following parameters to create search queries:

1. **Cities** (`App_Code/Cities.cs`)
   - Contains a list of Israeli cities
   - Add or remove cities as needed

2. **Professions** (`App_Code/Professions.cs`)
   - Contains job titles in Hebrew
   - Customize based on your target professions

3. **Mail Types** (`App_Code/MailTypes.cs`)
   - Contains Hebrew and English variations of "email"
   - Used to improve search accuracy

### Search Query Format

The application generates search queries in the format:

```
דרושים [PROFESSION] ב[CITY] [MAIL_TYPE]
```

Example: "דרושים מנהלת משרד בכפר סבא מייל"

## Running the Application

### Development Environment

1. Open the project in Visual Studio
2. Press F5 to run in debug mode
3. The application will open in your default browser
4. Access the web handlers:
   - `/FetchMails.ashx` - Main email fetching handler
   - `/WallaSearch.aspx` - Search interface (legacy)
   - `/PrintMails.ashx` - View collected emails

### Production Deployment

1. Build the project in Release mode
2. Publish to IIS:
   - Right-click project → Publish
   - Choose IIS, FTP, or File System
3. Configure IIS Application Pool (.NET Framework 3.5)
4. Set appropriate permissions for database access

## Using the Application

### Fetching Emails

**Endpoint**: `/FetchMails.ashx`

**Parameters**:

- `i` (optional): Previous count to add to the result

**Example**:

```
http://localhost/cv-spider-v2/FetchMails.ashx?i=100
```

**How it works**:

1. Generates random search query from cities, professions, and mail types
2. Searches Walla search engine (pages 2-11)
3. Extracts URLs from search results
4. Visits each URL and extracts email addresses using regex
5. Validates and cleans email addresses
6. Stores unique emails in the database
7. Returns the count of newly found emails

### Email Validation

The application performs several validation steps:

1. Checks for `@` symbol
2. Rejects image file extensions (.jpg, .png)
3. Validates minimum length for email parts
4. Cleans common typos and malformed domains

### Email Cleaning

The `ClearEmail` method fixes common issues:

- Removes special characters
- Corrects Israeli domain typos (.co → .co.il)
- Fixes common misspellings (.con → .com)
- Removes mailto: prefixes
- Normalizes domain extensions

## File Structure

```
cv-spider-v2/
├── App_Code/              # Server-side classes
│   ├── BLL.cs            # Business Logic Layer
│   ├── DAL.cs            # Data Access Layer
│   ├── DbUtilsDal.cs     # Database utilities
│   ├── CVIma2.designer.cs # LINQ-to-SQL generated code
│   ├── Cities.cs         # City list provider
│   ├── Professions.cs    # Profession list provider
│   └── MailTypes.cs      # Email keyword variations
├── FetchMails.ashx       # Main email fetching handler
├── NewFetchMails.ashx    # Alternative fetching handler
├── PrintMails.ashx       # Email display handler
├── WallaSearch.aspx      # Legacy search page
├── WallaSearch.aspx.cs   # Legacy search code-behind
├── Default.aspx          # Default landing page
├── web.config            # Application configuration
├── jquery.min.js         # jQuery library
└── jquery.timer.js       # Timer utility
```

## Notes

- **Rate Limiting**: Be mindful of search engine rate limits to avoid IP blocking
- **Ethical Considerations**: This tool is for educational purposes; ensure compliance with anti-spam laws
- **Data Privacy**: Handle collected email addresses responsibly and in compliance with GDPR/privacy laws
- **Search Engines**: The application uses Walla search; search engine APIs may change over time
- **Database Growth**: The `CVMails` table will grow over time; implement archiving if needed
- **Concurrent Access**: The application uses locking for thread-safe database operations

## Best Practices

1. **Thread Safety**: Always use the `lock(typeof(DAL))` or similar patterns when performing write operations to the database to prevent deadlocks and race conditions.
2. **Regex Performance**: Test regex patterns against large HTML samples to ensure they don't cause ReDoS (Regular Expression Denial of Service).
3. **Data Cleaning**: Periodically review the `ClearEmail` method to add new common typos found in the field.
4. **Error Logging**: Check the IIS logs or implement a custom logger in `App_Code` to track scraping failures.
5. **SQL Indexing**: Ensure the `Mail` column in `CVMails` is indexed (it is by default as a UNIQUE constraint) for fast lookups.

## Documentation

- **README.md**: Overview of the project, architecture, and features.
- **CONTRIBUTING.md**: Guidelines for contributing to the project.
- **Inline Comments**: Refer to the C# code in `App_Code/` for detailed implementation logic.

## Extending the Application

1. **Adding New Search Engines**: Create a new method in `BLL.cs` to handle different search result HTML structures.
2. **Custom Validation**: Add new rules to the email validation logic to filter out more noise.
3. **Export Formats**: Modify `PrintMails.ashx` to support CSV or Excel export by changing the `Response.ContentType`.
4. **Scheduled Tasks**: Use Windows Task Scheduler to call `FetchMails.ashx` periodically using `curl` or `powershell`.

## External Resources

- [.NET Framework 3.5 Documentation](https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed)
- [LINQ to SQL Overview](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/index)
- [SQL Server Express Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [IIS Configuration Guide](https://docs.microsoft.com/en-us/iis/get-started/getting-started-with-iis/getting-started-with-the-default-document-on-iis)

## Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Verify SQL Server is running
   - Check connection string in `web.config`
   - Ensure database user has proper permissions

2. **LINQ-to-SQL Errors**
   - Regenerate the `.designer.cs` file
   - Verify table names match the database schema

3. **No Emails Found**
   - Check if search engine structure has changed
   - Verify internet connection
   - Check regex patterns in email extraction code

4. **Duplicate Key Errors**
   - Ensure `Mail` column has UNIQUE constraint
   - Check for race conditions in concurrent operations

## Author

- **Or Assayag** - _Initial work_ - [orassayag](https://github.com/orassayag)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag

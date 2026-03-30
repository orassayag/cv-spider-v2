# Instructions

## Setup Instructions

### Prerequisites

1. **Windows Operating System** (for IIS hosting)
2. **.NET Framework 3.5** or higher
3. **SQL Server** (2008 or higher recommended)
4. **Visual Studio** (2010 or higher) or any ASP.NET compatible IDE
5. **IIS (Internet Information Services)** for hosting

### Installation

1. Clone or download the repository to your local machine
2. Open the project in Visual Studio
3. Set up the SQL Server database (see Database Setup below)
4. Update the connection string in `web.config`
5. Build the LINQ-to-SQL mapping (see LINQ-to-SQL Setup below)

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

* **Or Assayag** - *Initial work* - [orassayag](https://github.com/orassayag)
* Or Assayag <orassayag@gmail.com>
* GitHub: https://github.com/orassayag
* StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
* LinkedIn: https://linkedin.com/in/orassayag
